using System;
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
        public Operation Operation { get; set; }
        [Required]
        public User Driver { get; set; }
    }
}
