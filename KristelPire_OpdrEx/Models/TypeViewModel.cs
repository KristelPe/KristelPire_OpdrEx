using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristelPire_OpdrEx.Models
{
    // This view makes a list of list detail view models
    public class TypeViewModel
    {
        public List<TypeDetailViewModel> TypeList { get; set; }
        public DateTime GeneratedAt => DateTime.Now;
    }

    // This contains a brand, a type or model and a list of car detail views
    public class TypeDetailViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public List<CarDetailViewModel> Cars { get; set; }
    }
}
