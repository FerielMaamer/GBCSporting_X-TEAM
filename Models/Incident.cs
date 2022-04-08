using System.ComponentModel.DataAnnotations;

namespace GBCSporting_X_TEAM.Models
{
    public class Incident
    {
        [Key]
        public int IncidentId { get; set; }


        [Range(1, 1000, ErrorMessage = "Please enter a Customer!")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Range(1, 1000, ErrorMessage = "Please enter a Product!")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }


        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateOpened { get; set; } = DateTime.Now;
        public DateTime? DateClosed { get; set; }

        public int? TechnicianId { get; set; }
        public Technician? Technician { get; set; }

    }

}
