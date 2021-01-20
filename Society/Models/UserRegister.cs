using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Society.Models
{
    public class UserRegister
    {
        public int OwnerId { get; set; }
        [Required]
        public string OwnerName { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public int FlatId { get; set; }
        public List<FlatList> flatList { get; set; }
    }
    public class FlatList
    {
        public int FlatId { get; set; }

        public int FlatNo { get; set; }
    }
}
