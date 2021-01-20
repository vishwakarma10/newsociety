using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SocietyDataLayer
{
    public class FlatOwner
    {
        [Key]
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int FlatId { get; set; }
    }
    public class AdminMaster
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SocietyMaintanance
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int FlatId { get; set; }
        public DateTime Date { get; set; }
        public string CheckNo { get; set; }
        public string TransactionNo { get; set; }
    }
    public class OwnerComplaints
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string ComplaintTitle { get; set; }
        public DateTime Date { get; set; }
        public int FlatId { get; set; }
    }
}
