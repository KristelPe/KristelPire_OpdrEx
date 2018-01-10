using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristelPire_OpdrEx.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public List<Car> Cars { get; set; }
    }
}
