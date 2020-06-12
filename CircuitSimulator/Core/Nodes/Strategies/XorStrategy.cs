using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;

namespace Core.Nodes.Strategies
{
    public class XorStrategy : INodeStrategy
    {
        // Xor gate truth table:
        // 0 - 0 = 0
        // 1 - 0 = 1
        // 0 - 1 = 1
        // 1 - 1 = 0
        public int MinimumInputs => 2;
        public int MaximumInputs => int.MaxValue;
        public NodeOutput Execute(List<Node> inputs, NodeOutput currentOutput)
        {
            // If any of them are not calculated we can't determine the result yet.
            if (inputs.Any(x => x.Output == NodeOutput.NotCalculated))
                return NodeOutput.NotCalculated;
            
            return inputs.Count(x => x.Output == NodeOutput.On) == 1 ? NodeOutput.On : NodeOutput.Off;
        }
        
        public string Draw(NodeOutput state) => "XOR.png";
    }
}