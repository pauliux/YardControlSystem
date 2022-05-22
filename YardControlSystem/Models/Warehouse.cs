using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YardControlSystem.Models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public List<Ramp> Ramps { get; set; }
    }
}
