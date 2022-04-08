using System.ComponentModel.DataAnnotations;

namespace GBCSporting_X_TEAM.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public string Slug => Name?.Replace(' ', '-').ToLower() + '-' + Code?.Replace(' ', '-').ToLower();

        // navigation property to linking entity
        public ICollection<Registration> Registrations { get; set; }
    }
}
