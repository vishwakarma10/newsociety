using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Society.Models;
using SocietyDataLayer;

namespace Society.Controllers
{
    [Authorize]
    public class OwnerController : Controller
    {
        SocietyDataContext sdx;
        public OwnerController(SocietyDataContext _sdx)
        {
            sdx = _sdx;
        }

        public ActionResult Events()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShowBilling(int currentPageIndex)
        {
            return View("ShowExpenses",GetExpansesWithPagination(currentPageIndex));
        }

        public ActionResult ShowExpenses()
        { 
            return View(GetExpansesWithPagination(1));
        }

        private MasterBilling GetExpansesWithPagination(int currentPage)
        {
            var dataSummary = sdx.SummaryItemMaster.Select(m => new SummaryItemMasterModel
            {
                SupplierName = m.SupplierName,
                BillNo = m.BillNo,
                GST = m.GST,
                TotalAmount = m.TotalAmount,
                Id = m.Id,
                PurchaseDate=m.PurchaseDate
            }).ToList();

            var items = sdx.ItemMaster.Select(x => new ItemMasterModel
            {
                ItemName = x.ItemName,
                Description = x.Description,
                Amount = x.Amount,
                SummayItemId = x.SummayItemId
            }).ToList();

            int maxRows = 20;
            MasterBilling masterBilling = new MasterBilling();
            int itemscount = (from d in dataSummary
                         join item in items on d.Id equals item.SummayItemId
                         select new ItemViewModel
                         {
                             BilNo = d.BillNo,
                             SupplierName = d.SupplierName,
                             ItemName = item.ItemName,
                             Description = item.Description,
                             Amount = item.Amount,
                             GST = d.GST,
                             TotalAmount = d.TotalAmount,
                             PurchaseDate = d.PurchaseDate
                         }).Count();

            masterBilling.itemViewModelList = (from d in dataSummary
                              join item in items on d.Id equals item.SummayItemId
                              select new ItemViewModel
                              {
                                  BilNo = d.BillNo,
                                  SupplierName = d.SupplierName,
                                  ItemName = item.ItemName,
                                  Description = item.Description,
                                  Amount = item.Amount,
                                  GST = d.GST,
                                  TotalAmount = d.TotalAmount,
                                  PurchaseDate=d.PurchaseDate
                              }).OrderByDescending(kp => kp.PurchaseDate)
                            .Skip((currentPage - 1) * maxRows)
                            .Take(maxRows).ToList();

            double pageCount = (double)((decimal)itemscount / Convert.ToDecimal(maxRows));
            masterBilling.PageCount = (int)Math.Ceiling(pageCount);

            masterBilling.CurrentPageIndex = currentPage;

            return masterBilling;          
        }

        public ActionResult Maintenance()
        {
            var mt = new Maintenance()
            {
                flatList = GetFlatList()
            };

            var flatId = HttpContext.Session.GetString("FlatId");
            mt.FlatId = int.Parse(flatId);
            return View(mt);
        }

        [HttpPost]
        public ActionResult Maintenance(Maintenance maintenance)
        {
            var mt = new Maintenance()
            {
                flatList = GetFlatList()
            };

            if (ModelState.IsValid)
            {

                SocietyMaintanance maintananceData = new SocietyMaintanance()
                {

                    FlatId = maintenance.FlatId,
                    Description = maintenance.Description,
                    Amount = maintenance.Amount,
                    Date = System.DateTime.Now,
                    CheckNo = maintenance.CheckNo,
                    TransactionNo = maintenance.TransactionNo

                };
                sdx.societyMaintanance.Add(maintananceData);
                sdx.SaveChanges();
                ViewBag.message = "SocietyMaintanance added successfully..";
                return View(mt);
            }
            ViewBag.message = "Insert failed due to Validation";
            return View(mt);
        }

        public ActionResult Complaints()
        {
            var mt = new Complaints();

            var flatId = HttpContext.Session.GetString("FlatId");
            mt.FlatId = int.Parse(flatId);
            return View(mt);
        }

        [HttpPost]
        public ActionResult Complaints(Complaints complaints)
        {
            var mt = new Complaints();

            if (ModelState.IsValid)
            {

                OwnerComplaints complaintsData = new OwnerComplaints()
                {

                    FlatId = complaints.FlatId,
                    Description = complaints.Description,
                    ComplaintTitle = complaints.ComplaintTitle,
                    Date = System.DateTime.Now,


                };
                sdx.ownerComplaints.Add(complaintsData);
                sdx.SaveChanges();
                ViewBag.message = "Your Complaints added successfully..";
                return View(mt);
            }
            ViewBag.message = "Insert failed due to Validation";
            return View(mt);
        }

        public List<FlatList> GetFlatList()
        {
            var list = sdx.flat_Master.Where(m => m.BuildingId == 1).Select(m => new FlatList
            {
                FlatId = m.Id,
                FlatNo = m.Flat_No
            }).ToList();
            return list;
        }

        // GET: OwnerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OwnerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OwnerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OwnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OwnerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OwnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OwnerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OwnerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
