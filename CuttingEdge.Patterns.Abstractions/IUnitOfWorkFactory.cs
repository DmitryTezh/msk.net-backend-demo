namespace CuttingEdge.Patterns.Abstractions
{
    public interface IUnitOfWorkFactory<TDomain> where TDomain : class
    {
        IUnitOfWork<TDomain> Create();
    }
}
