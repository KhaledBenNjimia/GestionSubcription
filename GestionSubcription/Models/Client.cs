using System.ComponentModel.DataAnnotations;

namespace GestionSubcription.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Tel { get; set; }
        public string Description { get; set; }
    }

}
