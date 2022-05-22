using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YardControlSystem.Models
{
    public class Operation
    {
        [Key]
        public int Id { get; set; }
        public int Duration { get; set; }
        public DateTime ReservedDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int RampId { get; set; }
    }
}
