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
        public int Number { get; set; }

        public int WarehouseId { get; set; }

        public Ramp()
        {

        }
    }
}
