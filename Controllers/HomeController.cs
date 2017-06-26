using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using belt.Models;
using System.Linq;

namespace belt.Controllers
{
    public class HomeController : Controller
    {
    private YourContext _context;
 
    public HomeController(YourContext context)
    {
        _context = context;
    }
        
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            ViewBag.Errors = new List<string>();
            return View();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model){
            System.Console.WriteLine("In Register***********************************************");
            if (ModelState.IsValid){
                User NewUser = new User{
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Add(NewUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("CurrentUserId", NewUser.UserId);
                HttpContext.Session.SetString("UserName", NewUser.FirstName);
                
                return RedirectToAction("Dashboard", "Action");
            }else{
                System.Console.WriteLine("Not Good***********************************************");
                ViewBag.Errors = ModelState.Values;
            }
            return View("index");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            User CurrUser = _context.Users.Where(x => x.Email == email).SingleOrDefault();
             if (CurrUser != null){
                if (password == CurrUser.Password){
                    HttpContext.Session.SetInt32("CurrentUserId", CurrUser.UserId);
                    HttpContext.Session.SetString("UserName", CurrUser.FirstName);
                    return RedirectToAction("Dashboard", "Action");  
               }
             }
             ViewBag.LoginError = "Invalid combination. Please provide a valid username/password.";
             return View("Index");              
       }
        [HttpGet]
        [Route("planpage")]
        public IActionResult Planpage(){
            ViewBag.Errors = "";
            return View("planpage");
        }
        [HttpGet]
        [RouteAttribute("Logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }  
    }
}
