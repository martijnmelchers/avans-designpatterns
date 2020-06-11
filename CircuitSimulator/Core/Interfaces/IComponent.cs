namespace Core.Interfaces
{
    public interface IComponent<TIn>
    {
        void AddOutput(TIn value);
        void AddInput(TIn value);
        void Process();
    }
}