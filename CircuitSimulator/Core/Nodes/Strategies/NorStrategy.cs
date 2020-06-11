using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;

namespace Core.Nodes.Strategies
{
    public class NorStrategy : INodeStrategy
    {
        // Nor gate truth table:
        // 0 - 0 = 1
        // 1 - 0 = 0
        // 0 - 1 = 0
        // 1 - 1 = 0
        public int MinimumInputs => 2;
        public int MaximumInputs => int.MaxValue;
        public NodeOutput Execute(IEnumerable<Node> inputs, NodeOutput currentOutput)
        {
            return inputs.Any(x => x.Output == NodeOutput.On) ? NodeOutput.Off : NodeOutput.On;
        }
    }
}