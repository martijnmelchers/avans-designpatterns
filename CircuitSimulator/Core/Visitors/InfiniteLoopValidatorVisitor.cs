using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Nodes;

namespace Core.Visitors
{
    public class InfiniteLoopValidatorVisitor : IVisitor
    {
        public void VisitNode(Node node)
        {
            var hashSet = new HashSet<Node>();
            while (node != null)
            {
                if (hashSet.Contains(node))
                    throw new Exception($"Infinite loop found at {node.Name}! Path: {string.Join(" -> ", hashSet.Select(x => x.Name))} -> {node.Name}");
            
                hashSet.Add(node);
                node = node.Outputs.FirstOrDefault();
            }
        }
        
    }
}