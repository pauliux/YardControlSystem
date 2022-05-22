using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YardControlSystem.Areas.Identity.Data;


namespace YardControlSystem.Models.ViewModels
{
    public class CreateOrderViewModel
    {
        public Order Order { get; set; }
        public string PickUpWarehouse { get; set; }
        public string DropOffWarehouse { get; set; }
        public IEnumerable<SelectListItem> Warehouses { get; set; }
    }
}
