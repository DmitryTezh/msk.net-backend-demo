namespace CuttingEdge.Patterns.Abstractions
{
    /// <summary>
    /// Interface for domain entity in repository.
    /// </summary>
    public interface IDomain
    {
        /// <summary>
        /// Entity identifier.
        /// </summary>
        long Id
        {
            get;
            set;
        }

        /// <summary>
        /// Is entity actual at the moment.
        /// </summary>
        bool IsActual
        {
            get;
            set;
        }
    }
}
