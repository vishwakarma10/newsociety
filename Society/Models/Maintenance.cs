using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Society.Models
{
    public class Maintenance
    {
       
        public int MaintenanceId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Amount { get; set; }

        public int FlatId { get; set; }
        public DateTime Date { get; set; }
       
        public string CheckNo { get; set; }
        public string TransactionNo { get; set; }
        public int FlatNo { get; set; }
        public List<FlatList> flatList { get; set; }

    }
}
