using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionSubcription.Models
{
    public class Subscription
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client? Client { get; set; }  // ✅ Make nullable

        [Required]
        public int ApplicationId { get; set; }
        public Application? Application { get; set; }  // ✅ Make nullable

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        
        public string? LicenseKey { get; set; } = "";  // ✅ Provide default value
    }

}


