using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Society.Models;
using SocietyDataLayer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Society.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        SocietyDataContext sdx;

        public HomeController(ILogger<HomeController> logger, SocietyDataContext _sdx)
        {
            _logger = logger;
            sdx = _sdx;
        }

        public IActionResult Index(bool loginError)
        {
            if(loginError)
            ViewBag.loginError = "Login Failed";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(User u)
        {

            if (ModelState.IsValid)
            {
                if (u.IsAdmin)
                {
                    var adminExist = sdx.adminMaster.Where(m => m.UserName.Equals(u.UserName) && m.Password.Equals(u.Password)).FirstOrDefault();
                    if (adminExist != null)
                    {
                        var userClaims = new List<Claim>()
                {
                new Claim(ClaimTypes.Name, u.UserName)
               
                 };

                        var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

                        var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                        HttpContext.SignInAsync(userPrincipal);

                        HttpContext.Session.SetString("AdminName", u.UserName);
                        return RedirectToAction("AdminHome", "Admin");
                    }
                }
                else
                {
                    var userExist = sdx.flatOwner.Where(m => m.UserName.Equals(u.UserName) && m.Password.Equals(u.Password)).FirstOrDefault();
                    if (userExist != null)
                    {



                        var userClaims = new List<Claim>()
                {
                new Claim(ClaimTypes.Name, u.UserName),
                new Claim(ClaimTypes.Email, userExist.FlatId.ToString()),
                 };

                        var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

                        var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                        HttpContext.SignInAsync(userPrincipal);

                        HttpContext.Session.SetString("UserName", u.UserName);
                        HttpContext.Session.SetString("FlatId", userExist.FlatId.ToString());
                        return RedirectToAction("Privacy");
                    }
                }

            }

            return RedirectToAction("Index",new { loginError = true });
        }

        [Authorize]
        public IActionResult Privacy()
        {
            var user = HttpContext.User;
            //First get user claims    
            //var claims = ClaimsPrincipal.Current.Identities.FirstOrDefault().Claims.ToList();

            ////Filter specific claim    
            //var user=claims?.FirstOrDefault(x => x.Type.Equals("Name", StringComparison.OrdinalIgnoreCase))?.Value;
            //ViewBag.UserName = user;
            return View();
        }

        public List<FlatList> GetFlatList()
        {
            var list= sdx.flat_Master.Where(m => m.BuildingId == 1).Select(m => new FlatList
            {
                FlatId = m.Id,
                FlatNo = m.Flat_No
            }).ToList();
            return list;
        }

        public IActionResult Register()
        {
            var reg = new UserRegister()
            {
                flatList = GetFlatList()
            };

            return View(reg);
        }

        [HttpPost]
        public IActionResult Register(UserRegister userRegister)
        {
            var reg = new UserRegister()
            {
                flatList = GetFlatList()
            };

            if (ModelState.IsValid)
            {                

                var flatId = userRegister.FlatId;
                var count = sdx.flatOwner.Where(s => s.FlatId==flatId).Count();
                if (count > 0)
                {
                    ViewBag.message = "This flat is already registered.";
                    return View(reg);
                }
                var count1 = sdx.flatOwner.Where(s => s.UserName == userRegister.UserName).Count();
                if (count1 > 0)
                {
                    ViewBag.message = "This User Name is already registered.";
                    return View(reg);
                }
                //flatMaster.Created_at = DateTime.Now;
                //flatMaster.Updated_at = DateTime.Now;
                //flatMaster.BuildingId = 1;//  B building
                FlatOwner ownerData = new FlatOwner()
                {
                    FlatId = userRegister.FlatId,
                    OwnerName = userRegister.OwnerName.Trim(),
                   MobileNumber=userRegister.MobileNumber.Trim(),
                   EmailId=userRegister.EmailId.Trim(),
                   UserName=userRegister.UserName.Trim(),
                   Password=userRegister.Password.Trim()
                };
                sdx.flatOwner.Add(ownerData);
                sdx.SaveChanges();
                ViewBag.message = "Owner added successfully..";
                return View(reg);
            }
            ViewBag.message = "Insert failed due to Validation.";
            return View(reg);
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("FlatId");
            HttpContext.Session.Remove("AdminName");
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
