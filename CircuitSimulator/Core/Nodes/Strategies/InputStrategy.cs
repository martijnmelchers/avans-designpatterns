using System.Collections.Generic;
using Core.Interfaces;

namespace Core.Nodes.Strategies
{
    public class InputStrategy : INodeStrategy
    {
        public int MinimumInputs => 0;
        public int MaximumInputs => 0;
        public NodeOutput Execute(IEnumerable<Node> inputs, NodeOutput currentOutput)
        {
            return currentOutput;
        }
    }
}