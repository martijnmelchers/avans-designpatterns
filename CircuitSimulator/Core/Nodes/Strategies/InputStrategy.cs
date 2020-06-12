using System.Collections.Generic;
using Core.Interfaces;

namespace Core.Nodes.Strategies
{
    public class InputStrategy : INodeStrategy
    {
        public int MinimumInputs => 0;
        public int MaximumInputs => 0;

        
        public NodeOutput Execute(List<Node> inputs, NodeOutput currentOutput) => currentOutput;

        public string Draw(NodeOutput state) => state switch
        {
            NodeOutput.Off => "OFF.png",
            NodeOutput.On => "ON.png",
            _ => "NOT_CALCULATED.png"
        };
    }
}