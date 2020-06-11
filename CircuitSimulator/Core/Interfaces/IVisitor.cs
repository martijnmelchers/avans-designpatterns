using Core.Nodes;

namespace Core.Interfaces
{
    public interface IVisitor
    {
        void VisitNode(Node node);
    }
}