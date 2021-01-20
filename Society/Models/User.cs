using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Society.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        //public IEnumerable<User> GetUsers()
        //{
        //    return new List<Users>() { new Users { Id = 101, UserName = "anet", Name = "Anet", EmailId = "anet@test.com", Password = "anet123" } };
        //}
    }
}
