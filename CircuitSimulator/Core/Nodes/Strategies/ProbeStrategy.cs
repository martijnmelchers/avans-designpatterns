using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;

namespace Core.Nodes.Strategies
{
    public class ProbeStrategy : INodeStrategy
    {
        public int MinimumInputs => 1;
        public int MaximumInputs => 1;
        
        public NodeOutput Execute(List<Node> inputs, NodeOutput currentOutput) => inputs.First().Output;

        public string Draw(NodeOutput state) => state switch
        {
            NodeOutput.Off => "OUTPUT_OFF.png",
            NodeOutput.On => "OUTPUT_ON.png",
            _ => "OUTPUT_UNKOWN.png"
        };
    }
}