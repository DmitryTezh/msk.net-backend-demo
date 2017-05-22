using System.ComponentModel.DataAnnotations;

namespace CuttingEdge.DemoWeb.Entity
{
    /// <summary>
    /// Abstract domain entity in repository.
    /// </summary>
    public abstract class Domain
    {
        /// <summary>
        /// Entity identifier.
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Is entity actual at the moment.
        /// </summary>
        public bool IsActual { get; set; }
    }
}
