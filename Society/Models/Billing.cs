using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Society.Models
{
    public class ItemViewModel
    {
        public string BilNo { get; set; }
        public string SupplierName { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal GST { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PurchaseDate { get; set; }

    }

    public class MasterBilling
    {
        public List<ItemViewModel> itemViewModelList { get; set; }
        public int CurrentPageIndex { get; set; }

        ///<summary>
        /// Gets or sets PageCount.
        ///</summary>
        public int PageCount { get; set; }
    }

    public class ItemMasterModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal GST { get; set; }
        public decimal TotalAmount { get; set; }
        public int SummayItemId { get; set; }
    }
    public class SummaryItemMasterModel
    {
        public List<ItemMasterModel> listItem { get; set; }
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
    }
}
