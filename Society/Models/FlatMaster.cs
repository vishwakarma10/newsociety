using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Society.Models
{
    public class FlatMaster
    {
        public int Id { get; set; }
        [Required]
        public int Flat_No { get; set; }
        public string Description { get; set; }
        public int BuildingId { get; set; }
    }
}
