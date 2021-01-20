using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Society.Models
{
    public class Complaints
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ComplaintTitle { get; set; }
        public DateTime Date { get; set; }
        public int FlatId { get; set; }
        public int FlatNo { get; set; }
        public List<FlatList> flatList { get; set; }
    }
}
