using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KristelPire_OpdrEx.Entities;

namespace KristelPire_OpdrEx.Models
{
    // Owner view model, contains a list 
    public class OwnerViewModel
    {
        public List<OwnerDetailViewModel> OwnerList { get; set; }
        public DateTime GeneratedAt => DateTime.Now;
    }

    // This view contains a name, firstname and a list of car detail views
    public class OwnerDetailViewModel
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string FirstName { get; set; }
        public List<CarDetailViewModel> Cars { get; set; }
    }
}
