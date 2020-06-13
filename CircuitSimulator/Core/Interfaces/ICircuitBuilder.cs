using System.Collections.Generic;
using Core.Builders;

namespace Core.Interfaces
{
    public interface ICircuitBuilder
    {
        Circuit BuildCircuit();
        CircuitBuilder BuildNodes(Dictionary<string, string> nodes);
        CircuitBuilder BuildEdges(Dictionary<string, string[]> edges);
    }
}