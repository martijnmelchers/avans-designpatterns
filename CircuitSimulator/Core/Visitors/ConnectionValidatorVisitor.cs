using System;
using Core.Interfaces;
using Core.Nodes;

namespace Core.Visitors
{
    public class ConnectionValidatorVisitor : IVisitor
    {
        public void VisitNode(Node node)
        {
            if(node.Inputs.Count < node.Strategy.MinimumInputs)
                throw new Exception($"Invalid amount of inputs at node {node.Name}! Expected at least {node.Strategy.MinimumInputs} but had {node.Inputs.Count}");
            
            if(node.Inputs.Count > node.Strategy.MaximumInputs)
                throw new Exception($"Invalid amount of inputs at node {node.Name}! Exceeded maximum of {node.Strategy.MaximumInputs} inputs, got {node.Inputs.Count}");
        }
       
        
    }
}