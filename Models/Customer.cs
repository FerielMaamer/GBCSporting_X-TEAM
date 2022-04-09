using System.ComponentModel.DataAnnotations;

namespace GBCSporting_X_TEAM.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(51, MinimumLength = 1, ErrorMessage = "Firstname requires between 1 and 51 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(51, MinimumLength = 1, ErrorMessage = "Lastname requires between 1 and 51 characters")]
        public string LastName { get; set; }
        [Required]
        [StringLength(51, MinimumLength = 1, ErrorMessage = "Address requires between 1 and 51 characters")]
        public string Address { get; set; }
        [Required]
        [StringLength(51, MinimumLength = 1, ErrorMessage = "City requires between 1 and 51 characters")]
        public string City { get; set; }
        [Required]
        [StringLength(51, MinimumLength = 1, ErrorMessage = "State requires between 1 and 51 characters")]
        public string State { get; set; }
        [Required]
        [StringLength(21, MinimumLength = 1, ErrorMessage = "Post code requires between 1 and 21 characters")]
        public string PostalCode { get; set; }

        [Required]
        public int CountryId { get; set; }
        public Country? Country { get; set; }
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$", ErrorMessage = "Enter phone in format (999) 999-9999 ")]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Slug => FirstName?.Replace(' ', '-').ToLower() + '-' + LastName?.Replace(' ', '-').ToLower();

        // navigation property to linking entity
        public ICollection<Registration>? Registrations { get; set; }

    }
}
