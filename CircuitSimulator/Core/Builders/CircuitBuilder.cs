using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Nodes;
using Core.Nodes.Strategies;
using Core.Visitors;

namespace Core.Builders
{
    public class CircuitBuilder : ICircuitBuilder
    {
        private readonly Circuit _circuit;
        private readonly NodeFactory _nodeFactory;

        public CircuitBuilder()
        {
            _circuit = new Circuit();
            _nodeFactory = NodeFactory.GetInstance;
        }

        public Circuit BuildCircuit()
        {
            var connectionValidator = new ConnectionValidatorVisitor();
            var infiniteLoopValidator = new InfiniteLoopValidatorVisitor();

            foreach (var node in _circuit.Nodes) node.Accept(connectionValidator);
            foreach (var inputNode in _circuit.InputNodes) inputNode.Accept(infiniteLoopValidator);

            return _circuit;
        }

        public CircuitBuilder BuildNodes(Dictionary<string, string> nodes)
        {
            foreach (var (name, type) in nodes)
            {
                var node = _nodeFactory.CreateNode(name, type);

                if (node.Strategy is InputStrategy)
                    node.SetOutput(type == "input_low" ? NodeOutput.Off : NodeOutput.On);

                Add(node);
            }

            return this;
        }

        public CircuitBuilder BuildEdges(Dictionary<string, string[]> edges)
        {
            foreach (var (name, foundEdges) in edges)
            {
                foreach (var edge in foundEdges)
                {
                    Connect(name, edge);
                }
            }
            
            return this;
        }

        private void Add(Node input)
        {
            _circuit.Nodes.Add(input);
        }

        // Connects two nodes, grabs nodes by name.
        private void Connect(string from, string to)
        {
            var fromNode = _circuit.Nodes.FirstOrDefault(x => x.Name == from);
            var toNode = _circuit.Nodes.FirstOrDefault(x => x.Name == to);

            if (fromNode == null || toNode == null)
                throw new Exception(
                    $"Invalid connection found! Cannot connect {from} to {to} because one of the nodes was not created.");

            fromNode.AddOutput(toNode);
        }
    }
}