using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core;
using Core.Nodes;
using Core.Nodes.Strategies;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CircuitViewController _controller;
        private Circuit _circuit;

        public MainWindow()
        {
            InitializeComponent();
            _controller = new CircuitViewController();
            DataContext = _controller;
        }

        private void LoadInCircuitFile(object sender, RoutedEventArgs e)
        {
            try
            {
                _circuit = _controller.LoadCircuit();
                RestartSimulationButton.IsEnabled = true;
                _circuit.Simulate();
                DrawCircuit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DrawCircuit()
        {
            // Reset the grid first
            ResetGrid();
            ExecutionTimeLabel.Content = $"Total propagation time: {_circuit.ExecutionTime()} nanoseconds";

            var dictionary = new Dictionary<Node, (int x, int y)>();

            var inputY = 1;
            // The inputs are always to the most left corner
            const int inputX = 1;


            foreach (var circuitNode in _circuit.InputNodes)
            {
                var panel = BuildPanel(circuitNode);

                NodeGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                });

                Grid.SetColumn(panel, inputX);
                Grid.SetRow(panel, inputY);

                dictionary.Add(circuitNode, (inputX, inputY++));

                NodeGrid.Children.Add(panel);
            }

            foreach (var circuitInputNode in _circuit.InputNodes)
            {
                foreach (var output in circuitInputNode.Outputs)
                {
                    BuildOutputs(output, dictionary);
                }
            }

            NodeGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Auto)
            });
            NodeGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Auto)
            });

            var outputX = NodeGrid.ColumnDefinitions.Count + 1;
            var outputY = 1;

            foreach (var output in _circuit.OutputNodes)
            {
                var panel = BuildPanel(output);

                NodeGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                });

                Grid.SetColumn(panel, outputX);
                Grid.SetRow(panel, outputY++);

                NodeGrid.Children.Add(panel);
            }
        }

        private void BuildOutputs(Node node, Dictionary<Node, (int x, int y)> dictionary)
        {
            // If the node has already been drawn we don't need to do it again!
            if (dictionary.ContainsKey(node) || node.Strategy is ProbeStrategy)
                return;

            var inputNode = node.Inputs.FirstOrDefault(dictionary.ContainsKey);

            if (inputNode == null)
                throw new Exception("An error occured while drawing!");

            var coords = dictionary.GetValueOrDefault(inputNode);

            // Go one to left
            coords.x++;


            var isTaken = dictionary.Any(x => x.Value == coords);

            while (isTaken)
            {
                coords.y++;
                isTaken = dictionary.Any(x => x.Value == coords);
            }

            var panel = BuildPanel(node);

            if (NodeGrid.ColumnDefinitions.Count - 1 < coords.x)
                NodeGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Auto)
                });

            if (NodeGrid.RowDefinitions.Count - 1 < coords.y)
                NodeGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                });

            Grid.SetColumn(panel, coords.x);
            Grid.SetRow(panel, coords.y);

            dictionary.Add(node, coords);

            NodeGrid.Children.Add(panel);

            foreach (var nodeOutput in node.Outputs) BuildOutputs(nodeOutput, dictionary);
        }

        private StackPanel BuildPanel(Node node)
        {
            var imageComponent = new Image
            {
                Source = new BitmapImage(new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}Images/{node.Draw()}",
                    UriKind.Absolute)),
                Width = 64,
                Height = 64
            };

            if (node.Strategy is InputStrategy)
            {
                imageComponent.MouseLeftButtonDown += (sender, args) =>
                {
                    CircuitViewController.UpdateInput(_circuit, node);
                    DrawCircuit();
                };
            }

            var infoComponent = new Label
            {
                Foreground = Brushes.White,
                Content = $"Name: {node.Name}\n" +
                          $"Delay: {node.TimesCalculated * 15} nanoseconds\n" +
                          $"State: {node.Output}\n" +
                          (node.Strategy is InputStrategy
                              ? ""
                              : $"Inputs: {string.Join(", ", node.Inputs.Select(x => x.Name))}")
            };

            var panel = new StackPanel
            {
                Background = node.Output == NodeOutput.Off ? Brushes.Red : Brushes.ForestGreen,
                Orientation = Orientation.Horizontal
            };

            panel.Children.Add(imageComponent);
            panel.Children.Add(infoComponent);

            return panel;
        }

        private void ResetGrid()
        {
            NodeGrid.ColumnDefinitions.Clear();
            NodeGrid.RowDefinitions.Clear();
            NodeGrid.Children.Clear();
        }

        private void RestartSimulation(object sender, RoutedEventArgs e)
        {
            _circuit.ResetSimulation();
            _circuit.Simulate();
            DrawCircuit();
        }
    }
}