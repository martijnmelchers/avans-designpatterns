using System;
using System.Linq;
using Core.Interfaces;
using Core.Nodes;
using Core.Visitors;

namespace Core
{
    public class CircuitBuilder : IBuilder<Circuit, Node>
    {
        private readonly Circuit _circuit;

        public CircuitBuilder()
        {
            _circuit = new Circuit();
        }

        public Circuit Build()
        {
             
            var connectionValidator = new ConnectionValidatorVisitor();
            var infiniteLoopValidator = new InfiniteLoopValidatorVisitor();
            
            foreach (var node in _circuit.Nodes) (node as IVisitable).Accept(connectionValidator);
            foreach (var inputNode in _circuit.InputNodes) inputNode.Accept(infiniteLoopValidator);

            return _circuit;
        }

        public void Add(Node input)
        {
            _circuit.Nodes.Add(input);
        }

        // Connects two nodes, grabs nodes by name.
        public void Connect(string from, string to)
        {
            var fromNode = _circuit.Nodes.FirstOrDefault(x => x.Name == from);
            var toNode = _circuit.Nodes.FirstOrDefault(x => x.Name == to);
            
            if(fromNode == null || toNode == null)
                throw new Exception($"Invalid connection found! Cannot connect {from} to {to} because one of the nodes was not created.");

            fromNode.AddOutput(toNode);
        }
    }
}