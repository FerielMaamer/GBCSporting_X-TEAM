using System.ComponentModel.DataAnnotations;

namespace GBCSporting_X_TEAM.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }

        [Required]
        public int CountryId { get; set; }
        public Country? Country { get; set; }

        public string Phone { get; set; }
        public string Email{ get; set; }
        public string Slug => FirstName?.Replace(' ', '-').ToLower() + '-' + LastName?.Replace(' ', '-').ToLower();


    }
}
