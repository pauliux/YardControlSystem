﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YardControlSystem.Models.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public bool HasOperations { get; set; }
    }
}