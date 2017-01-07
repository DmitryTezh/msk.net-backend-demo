namespace CuttingEdge.Patterns.Abstractions
{
    public interface IUnitOfWorkFactory<TDomain> where TDomain : class, IDomain
    {
        IUnitOfWork<TDomain> Create();
    }
}
