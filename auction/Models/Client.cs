using System.ComponentModel.DataAnnotations;

namespace assignment1web.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string? ClientType { get; set; }
        [Required(ErrorMessage = "Please enter a valid first name")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a valid last name")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please enter a valid email")]
        public string? Email { get; set; }
        public string Slug => FirstName?.Replace(" ", "-").ToLower() + "-" + LastName?.Replace(" ", "-");
    }
}
