namespace CuttingEdge.Patterns.Abstractions
{
    public interface IViewManager<TView> where TView : class, IView
    {
        TView GetFor<TEntity>(string aria, string route, string mode) where TEntity : class;

        void Save(TView view);
    }
}
