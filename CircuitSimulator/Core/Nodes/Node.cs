using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Core.Interfaces;
using Core.Nodes.Strategies;

namespace Core.Nodes
{
    public class Node : IVisitable, IComponent<Node>
    {
        public int TimesTraversed;

        public readonly INodeStrategy Strategy;

        public Type StrategyType => Strategy.GetType();

        public Node(string name, INodeStrategy strategy)
        {
            Name = name;
            Strategy = strategy;
            Inputs = new List<Node>();
            Outputs = new List<Node>();
        }
        public string Name { get; }
        public List<Node> Inputs { get; }
        public List<Node> Outputs { get; }

        public NodeOutput Output = NodeOutput.NotCalculated;

        public void SetOutput(NodeOutput output) => Output = output;

        public void AddInput(Node node)
        {
            Inputs.Add(node);
        }

        public void AddOutput(Node node)
        {
            Outputs.Add(node);
            node.AddInput(this);
        }

        private bool InvalidInputs()
        {
            return Inputs.Any(x => x.Output == NodeOutput.NotCalculated);
        }

        private void BeforeProcess()
        {
            TimesTraversed++;
            // Console.Out.WriteLine($"Processing log of node: {Name}");
            // Console.Out.WriteLine("-> Start processing of node...");
            // Console.Out.WriteLine($"-> Inputs found: {Inputs.Count}");
            // Console.Out.WriteLine($"-> Outputs found: {Outputs.Count}");
            // Console.Out.WriteLine($"-> Current state: {Output}");
        }

        private void AfterProcess()
        {
            
            foreach (var node in Outputs) 
                node.Process();
        }

        public void Process()
        {
            BeforeProcess();
            //
            if (InvalidInputs())
                 return;
            Output = Strategy.Execute(Inputs, Output);
            
            
            
            AfterProcess();
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitNode(this);
        }
    }
}