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
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
    }
}
