using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;

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
            // If any of them are off we know the result
            if (inputs.Any(x => x.Output == NodeOutput.Off))
                return NodeOutput.Off;

            // If any of them are not calculated we can't determine the result yet.
            if (inputs.Any(x => x.Output == NodeOutput.NotCalculated))
                return NodeOutput.NotCalculated;

            // Otherwise: return on if all of the inputs are on
            return inputs.All(x => x.Output == NodeOutput.On)
                ? NodeOutput.On
                : NodeOutput.Off;
        }

        public string Draw(NodeOutput state) => "AND.png";
    }
}