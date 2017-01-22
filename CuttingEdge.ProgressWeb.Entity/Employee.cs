using System.ComponentModel.DataAnnotations;

namespace CuttingEdge.ProgressWeb.Entity
{
    public class Employee : Domain
    {
        [Required]
        [Display(Name = "Email", Prompt = "Employee email")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string Manager { get; set; }
        public string Company { get; set; }
    }
}
