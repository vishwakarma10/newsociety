using System;
using System.Collections.Generic;
using System.Text;

namespace SocietyDataLayer
{
    public class ItemMaster
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal GST { get; set; }
        public decimal TotalAmount { get; set; }
        public int SummayItemId { get; set; }
        public SummaryItemMaster SummaryItemMasterParent { get; set; }
    }
    public class SummaryItemMaster
    {
        public int Id { get; set; }
        public string BillNo { get; set; }
        public string SupplierName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string ItemSummary { get; set; }
        public decimal Amount { get; set; }
        public decimal GST { get; set; }
        public decimal TotalAmount { get; set; }
        public int BuildingId { get; set; }
        public string SystemUser { get; set; }
        public List<ItemMaster> ItemMasterListChild { get; set; } = new List<ItemMaster>();
    }
}
