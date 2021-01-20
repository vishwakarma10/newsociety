using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Society.Models;
using SocietyDataLayer;

namespace Society.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        SocietyDataContext sdx;
        public AdminController(SocietyDataContext _sdx)
        {
            sdx = _sdx;
        }

        public ActionResult AddExpenses()
        {
            SummaryItemMasterModel entities = new SummaryItemMasterModel();
            entities.listItem = new List<ItemMasterModel>();
            return View(entities);
        }

        [HttpPost]
        public JsonResult AddExpenses(SummaryItemMasterModel entities)
        {
            if (entities.listItem.Count > 0)
            {
                SummaryItemMaster data = new SummaryItemMaster()
                {
                    BillNo = entities.BillNo,
                    SupplierName = entities.SupplierName,
                    BuildingId = 1,
                    PurchaseDate = System.DateTime.Now,
                    GST = entities.GST,
                    Amount = entities.Amount,
                    TotalAmount = entities.TotalAmount
                };

                List<ItemMaster> listdata = new List<ItemMaster>();
                foreach (var item in entities.listItem)
                {
                    ItemMaster itemData = new ItemMaster();
                    itemData.ItemName = item.ItemName;
                    itemData.Description = item.Description;
                    itemData.Amount = item.Amount;
                    listdata.Add(itemData);
                }
                data.ItemMasterListChild.AddRange(listdata);
                sdx.SummaryItemMaster.Add(data);
                sdx.SaveChanges();
            }
            return new JsonResult("Recored added");
        }

        public ActionResult AddFlat()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFlat(FlatMaster flatMaster)
        {
            if (ModelState.IsValid)
            {
                var flatno = flatMaster.Flat_No;
                var count = sdx.flat_Master.Where(s => s.Flat_No == flatno).Count();
                if (count > 0)
                {
                    ViewBag.message = "Flat " + flatno + " already exists";
                    return View();
                }
                //flatMaster.Created_at = DateTime.Now;
                //flatMaster.Updated_at = DateTime.Now;
                //flatMaster.BuildingId = 1;//  B building
                Flat_Master flatData = new Flat_Master()
                {
                    BuildingId = 1,
                    Flat_No = flatno,
                    Description = flatMaster.Description
                };
                sdx.flat_Master.Add(flatData);
                sdx.SaveChanges();
                ViewBag.message = "Flat " + flatno + " added successfully..";
                return View();
            }
            ViewBag.message = "Insert failed due to Validation";
            return View();
        }

        public ActionResult AdminHome()
        {
            return View();
        }

        public ActionResult Complaints()
        {
            var data = sdx.ownerComplaints.Select(m => new Complaints
            {
                ComplaintTitle = m.ComplaintTitle,
                Description = m.Description,
                FlatNo = sdx.flat_Master.Where(x => x.Id == m.FlatId).FirstOrDefault().Flat_No,
                Date = m.Date
            }).ToList();
            //ViewBag.Maintenence = data;
            return View(data);
        }

        public ActionResult Maintenance()
        {
            //List<Maintenance> mtlist = new List<Maintenance>();
            var data = sdx.societyMaintanance.Select(m => new Maintenance
            {
                Description = m.Description,
                Amount = m.Amount,
                FlatNo = sdx.flat_Master.Where(x => x.Id == m.FlatId).FirstOrDefault().Flat_No,
                CheckNo = m.CheckNo,
                TransactionNo = m.TransactionNo,
                Date = m.Date
            }).ToList();
            //ViewBag.Maintenence = data;
            return View(data);
        }

        public ActionResult Billing()
        {
            return View();
        }

        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
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

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
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

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
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
