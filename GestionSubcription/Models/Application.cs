using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GestionSubcription.Models
{
    public class Application
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Application Name")]
        public string ApplicationName { get; set; }

        public string Description { get; set; }
    }

}
