using System.Collections.Generic;
using Core.Nodes;

namespace Core.Interfaces
{
    public interface INodeStrategy : IDrawableNode
    {
        int MinimumInputs { get; }
        int MaximumInputs { get; } 
        NodeOutput Execute(List<Node> inputs, NodeOutput currentOutput);
    }
}