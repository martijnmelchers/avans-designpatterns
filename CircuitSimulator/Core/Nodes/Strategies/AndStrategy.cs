using System.Collections.Generic;
using System.Linq;

namespace Core.Nodes.Strategies
{
    public class AndStrategy : INodeStrategy
    {
        // And port truth table:
        // 1 - 1 = 0
        // 0 - 1 = 0
        // 1 - 0 = 0
        // 1 - 1 = 1
        
        public int MinimumInputs => 2;
        public int MaximumInputs => int.MaxValue;

        public NodeOutput Execute(List<Node> inputs, NodeOutput currentOutput)
        {
            return inputs.Any(x => x.Output == NodeOutput.Off)
                ? NodeOutput.Off
                : inputs.All(x => x.Output == NodeOutput.On)
                    ? NodeOutput.On
                    : NodeOutput.Off;
        }
    }
}