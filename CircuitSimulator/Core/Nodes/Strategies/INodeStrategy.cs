using System.Collections.Generic;

namespace Core.Nodes.Strategies
{
    public interface INodeStrategy
    {
        int MinimumInputs { get; }
        int MaximumInputs { get; } 
        NodeOutput Execute(List<Node> inputs, NodeOutput currentOutput);
    }
}