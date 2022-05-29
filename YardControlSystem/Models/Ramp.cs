using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YardControlSystem.Models
{
    public class Ramp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool Working { get; set; }
        [Required]
		[Range(1, int.MaxValue, ErrorMessage = "Rampos numeris turi būti teigiamas")]
        public int Number { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public Ramp()
        {

        }
    }
}
