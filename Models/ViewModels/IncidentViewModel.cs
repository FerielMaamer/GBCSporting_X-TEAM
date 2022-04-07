using Microsoft.AspNetCore.Mvc.Rendering;

namespace GBCSporting_X_TEAM.Models.ViewModels
{
    public class IncidentViewModel
    {
        public int IncidentId { get; set; }
        public string Title { get; set; }
        public int TechnicianId { get; set; }
        public string? TechnicianName { get; set; }
        public string? firstName { get; set; }
        public string? LastName { get; set; }
        public int ProductId { get; set; }

        public string? ProductName { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime DateClosed { get; set; }

        public int CustomerId { get; set; }
        public string Description { get; set; }


        //drop downs
        public List<SelectListItem>? Customers { get; set; }
        public List<SelectListItem>? Products { get; set; }
        public List<SelectListItem>? Technicians { get; set; }
        public List<SelectListItem>? Incidents { get; set; }



    }
}
