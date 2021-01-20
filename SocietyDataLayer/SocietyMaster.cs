using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SocietyDataLayer
{
    public class SocietyMaster
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class Building_Master
    {
        [Key]
        public int Id { get; set; }
        public string BuildingName { get; set; }
        public string Description { get; set; }
    }

    public class Flat_Master
    {
        [Key]
        public int Id { get; set; }
        public int Flat_No { get; set; }
        public string Description { get; set; }
        public int BuildingId { get; set; }
    }

}
