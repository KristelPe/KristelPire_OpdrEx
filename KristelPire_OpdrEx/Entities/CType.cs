using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristelPire_OpdrEx.Entities
{
    public class CarType
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public List<Car> Cars { get; set; }
    }
}
