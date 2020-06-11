using System.Collections.Generic;
using System.Linq;

namespace Core.Nodes.Strategies
{
    public class NotStrategy : INodeStrategy
    {
        // Not gate truth table:
        // 1 = 0
        // 0 = 1
        
        public int MinimumInputs => 1;
        public int MaximumInputs => 1;
        public NodeOutput Execute(List<Node> inputs, NodeOutput currentOutput)
        {
            return inputs.First().Output == NodeOutput.Off ? NodeOutput.On : NodeOutput.Off;
        }
    }
}