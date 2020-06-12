using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface ICircuitBuilder
    {
        Circuit BuildCircuit();
        void BuildNodes(Dictionary<string, string> nodes);
        void BuildEdges(Dictionary<string, string[]> edges);
    }
}