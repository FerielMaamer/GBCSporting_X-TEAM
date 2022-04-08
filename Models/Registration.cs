namespace GBCSporting_X_TEAM.Models
{
    public class Registration
    {
        // composite primary key
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        // navigation properties
        public Customer? Customer { get; set; }
        public Product? Product { get; set; }


    }
}
