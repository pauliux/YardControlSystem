using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YardControlSystem.Models.ViewModels
{
    public class RampManagementViewModel
    {
        public Warehouse Warehouses { get; set; }
        public List<Ramp> Ramps { get; set; }
        public List<Operation> Operations { get; set; }

    }
}
