using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Resources;
using Core.Interfaces;
using Core.Nodes.Strategies;

namespace Core.Nodes
{
    public class Node : IVisitable, IComponent<Node>, IDrawable
    {
        public int TimesCalculated;

        public readonly INodeStrategy Strategy;
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

        private void TriggerOutputs()
        {
            
            foreach (var node in Outputs) 
                node.Process();
        }

        public void Process()
        {
            if (Output != NodeOutput.NotCalculated)
            {
                TriggerOutputs();
                return;
            }
            
            TimesCalculated++;

            Output = Strategy.Execute(Inputs, Output);

            if (Output == NodeOutput.NotCalculated)
                return;
            
            TriggerOutputs();

        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitNode(this);
        }

        public string Draw() => Strategy.Draw(Output);

        public void Reset(bool resetOutput)
        {
            TimesCalculated = 0;
            
            if(resetOutput)
                Output = NodeOutput.NotCalculated;

            foreach (var output in Outputs)
                output.Reset(true);
        }
    }
}