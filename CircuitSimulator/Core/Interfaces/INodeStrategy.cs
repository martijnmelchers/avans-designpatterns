using System.Collections.Generic;
using Core.Nodes;

namespace Core.Interfaces
{
    public interface INodeStrategy
    {
        int MinimumInputs { get; }
        int MaximumInputs { get; } 
        NodeOutput Execute(IEnumerable<Node> inputs, NodeOutput currentOutput);
    }
}