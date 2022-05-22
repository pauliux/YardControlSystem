﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YardControlSystem.Models.ViewModels
{
    public class CreateOrderViewModel
    {
        public Order Order { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        public List<User> Drivers { get; set; }
    }
}