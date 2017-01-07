using CuttingEdge.Patterns.Abstractions;

namespace CuttingEdge.ProgressWeb.Entity
{
    /// <summary>
    /// Abstract domain entity in repository.
    /// </summary>
    public abstract class Domain : IDomain
    {
        /// <summary>
        /// Entity identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Is entity actual at the moment.
        /// </summary>
        public bool IsActual { get; set; }
    }
}
