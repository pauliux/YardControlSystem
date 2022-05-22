using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YardControlSystem.Models
{
    public class RampsViewModel
    {
        public Warehouse Warehouse { get; set; }
        public List<Ramp> Ramps { get; set; }
    }
}
