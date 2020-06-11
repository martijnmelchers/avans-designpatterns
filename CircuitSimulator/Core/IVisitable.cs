namespace Core.Nodes.Strategies
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}