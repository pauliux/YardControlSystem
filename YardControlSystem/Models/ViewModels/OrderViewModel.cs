using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YardControlSystem.Models.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public Warehouse PickUpWarehouse { get; set; }
        public Warehouse DropOffWarehouse { get; set; }
        public Operation PickUpOperation { get; set; }
        public Operation DropOffOperation { get; set; }
        public bool HasOperations { get; set; }
    }
}
