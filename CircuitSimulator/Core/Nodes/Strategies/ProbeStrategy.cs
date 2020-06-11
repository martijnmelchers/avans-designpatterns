using System.Collections.Generic;
using System.Linq;

namespace Core.Nodes.Strategies
{
    public class ProbeStrategy : INodeStrategy
    {
        public int MinimumInputs => 1;
        public int MaximumInputs => 1;
        public NodeOutput Execute(List<Node> inputs, NodeOutput currentOutput)
        {
            return inputs.First().Output;
        }
    }
}