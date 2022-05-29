using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YardControlSystem.Areas.Identity.Data;

namespace YardControlSystem.Models
{
    public class Order
    {
        [Key]
        public int OrderNr { get; set; }
        public DateTime DateOfCreation { get; set; }
        [Required]
        public string TrailerLicensePlate { get; set; }
        public int PickUpWarehouseId { get; set; }
        public int DropOffWarehouseId { get; set; }
        public string DriverId { get; set; }
        public int PickUpOperationId { get; set; }
        public int DropOffOperationId { get; set; }
    }
}
