using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;

namespace Core.Nodes.Strategies
{
    public class NandStrategy : INodeStrategy
    {
        // Nand port truth table:
        // 0 - 0 = 1
        // 0 - 1 = 1
        // 1 - 0 = 1
        // 1 - 1 = 0

        public int MinimumInputs => 2;
        public int MaximumInputs => int.MaxValue;

        public NodeOutput Execute(List<Node> inputs, NodeOutput currentOutput)
        {
            if (inputs.Any(x => x.Output == NodeOutput.Off))
                return NodeOutput.On;
            
            // If any of them are not calculated we can't determine the result yet.
            if (inputs.Any(x => x.Output == NodeOutput.NotCalculated))
                return NodeOutput.NotCalculated;
            
            return inputs.All(x => x.Output == NodeOutput.On)
                    ? NodeOutput.Off
                    : NodeOutput.On;
        }
        
        public string Draw(NodeOutput state) => "NAND.png";
    }
}