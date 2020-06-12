using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Core;
using Core.Builders;
using Core.Nodes;
using Core.Nodes.Strategies;

namespace View
{
    public class CircuitViewController
    {
        private static string _circuitPath = "Circuits";

        public List<string> CircuitFiles { get; set; }
        public string SelectedCircuit { get; set; }

        public CircuitViewController()
        {
            NodeFactory.GetInstance
                .RegisterStrategy<InputStrategy>("input_high", "input_low")
                .RegisterStrategy<ProbeStrategy>("probe")
                .RegisterStrategy<NotStrategy>("not")
                .RegisterStrategy<AndStrategy>("and")
                .RegisterStrategy<OrStrategy>("or")
                .RegisterStrategy<NorStrategy>("nor")
                .RegisterStrategy<NandStrategy>("nand")
                .RegisterStrategy<XorStrategy>("xor");

            CircuitFiles = FileReader.GetFiles(_circuitPath);
            SelectedCircuit = CircuitFiles.FirstOrDefault();
        }

        public Circuit LoadCircuit()
        {
            if(string.IsNullOrEmpty(SelectedCircuit))
                throw new Exception("Please select a circuit.");
            
            var (nodes, edges) =  FileParser.ParseFile(FileReader.ReadFile($"{_circuitPath}/{SelectedCircuit}"));
            
            var circuitBuilder = new CircuitBuilder();
            
            circuitBuilder.BuildNodes(nodes);
            circuitBuilder.BuildEdges(edges);

            var circuit =  circuitBuilder.BuildCircuit();
           // circuit.Simulate();

            return circuit;
        }

        public void UpdateInput(Circuit circuit, Node node)
        {
            circuit.ResetSimulation();

            if(node.Output == NodeOutput.NotCalculated)
                throw new Exception("Can't update node with value NotCalculated!");
            
            node.SetOutput(node.Output == NodeOutput.Off ? NodeOutput.On : NodeOutput.Off);
            
            circuit.Simulate();
        }
    }
}
