using System.Collections.Generic;
using System.Linq;

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
            return inputs.All(x => x.Output == NodeOutput.On) ? NodeOutput.Off : NodeOutput.On;
        }
    }
}