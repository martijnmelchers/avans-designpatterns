using System;
using System.Collections.Generic;
using System.Linq;
using Core.Nodes;
using Core.Nodes.Strategies;

namespace Core
{
    public class Circuit
    {
        public readonly List<Node> Nodes;
        public IEnumerable<Node> InputNodes => Nodes.Where(x => x.StrategyType == typeof(InputStrategy)).ToList();
        public IEnumerable<Node> OutputNodes => Nodes.Where(x => x.StrategyType == typeof(ProbeStrategy)).ToList();

        public Circuit()
        {
            Nodes = new List<Node>();
        }
        
        public void Simulate()
        {
            foreach (var inputNode in InputNodes) inputNode.Process();
        }

        public int ExecutionTime()
        {
            return Nodes.Sum(x => x.TimesTraversed) * 15;
        }
    }
}