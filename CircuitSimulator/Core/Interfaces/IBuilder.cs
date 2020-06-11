namespace Core.Interfaces
{
    public interface IBuilder<TOut, TIn>
    {
        TOut Build();
        void Add(TIn input);
    }
}