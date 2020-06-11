using System.Collections.Generic;

namespace Core.Nodes.Strategies
{
    public class InputStrategy : INodeStrategy
    {
        public int MinimumInputs => 0;
        public int MaximumInputs => 0;
        public NodeOutput Execute(List<Node> inputs, NodeOutput currentOutput)
        {
            return currentOutput;
        }
    }
}