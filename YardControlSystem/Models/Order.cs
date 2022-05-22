﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YardControlSystem.Models
{
    public class Order
    {
        [Key]
        public int OrderNr { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string TrailerLicensePlate { get; set; }
        public int PickUpWarehouseId { get; set; }
        public Warehouse PickUpWarehouse { get; set; }
        public int DropOffWarehouseId { get; set; }
        public Warehouse DropOffWarehouse { get; set; }
        public int DriverId { get; set; }
        public User Driver { get; set; }
    }
}
