using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KristelPire_OpdrEx.Models
{
    public class CarViewModel
    {
        public List<CarDetailViewModel> CarList { get; set; }
        public DateTime GeneratedAt => DateTime.Now;
    }

    public class CarDetailViewModel
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public DateTime Date { get; set; }
        public string LicensePlate { get; set; }
        public string Owner { get; set; }
        public string CarType { get; set; }
    }

    public class CarEditDetailViewModel : CarDetailViewModel
    {
        public int? OwnerId { get; set; }
        public int TypeId { get; set; }
        public List<SelectListItem> Owners { get; set; }
        public List<SelectListItem> Types { get; set; }
    }
}
