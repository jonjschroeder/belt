using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using belt.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace belt.Controllers
{
    public class ActionController : Controller
    {
    private YourContext _context;
 
    public ActionController(YourContext context)
    {
        _context = context;
    }
        
        // GET: /Home/
        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard(){
                if (HttpContext.Session.GetInt32("CurrentUserId") == null ){
                return RedirectToAction("Index", "Home");
                        }
            List <Activity> act = _context.Activities.ToList();  
            List <Invitation> invite = _context.Invitations.ToList(); // SELECT * FROM Invitations
            List <Activity> allinvites = _context.Activities.Include(y => y.Invitations).ToList();  //Select * From Weddings and include the invitations list
            ViewBag.allinvites = allinvites;
            ViewBag.invite = invite;
            ViewBag.AllActivities = act;
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Id = HttpContext.Session.GetInt32("CurrentUserId");

           

        return View("Dashboard");
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(ActivitiesViewModel model){

            List<string> allErrors = new List <string>();
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrentUserId"));
            System.Console.WriteLine("In Register***********************************************");
            System.Console.WriteLine(model);
            if (ModelState.IsValid){
                Activity newActivity = new Activity{
                    Title = model.Title,
                    Time = model.Time,
                    Date = model.Date,
                    Duration = model.Duration,
                    Hours = model.Hours,
                    Description = model.Hours,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UserId = CurrentUser.UserId,
                };
                if(model.Date < DateTime.Now){
                    return RedirectToAction("planpage", "Home");
                }
                System.Console.WriteLine(newActivity);
                _context.Add(newActivity);
                _context.SaveChanges();
                // HttpContext.Session.SetInt32("CurrentUserId", newWedding.UserId); This is how to set Http Session currid to newwedding.userid...dont need this here
                return RedirectToAction("showpage", new { id = newActivity.ActivitiesId });
            }
                System.Console.WriteLine("Not Good***********************************************");
                ViewBag.Errors = ModelState.Values;
            {
                    foreach(var error in ViewBag.Errors)
            {
           
                    if(error.Errors.Count > 0)
            {  
                System.Console.WriteLine(error.Errors[0].ErrorMessage);  
            }
        }
    }
                System.Console.WriteLine("Hello____________________*********");
            
            return View("planpage");
        } 
                [HttpGet]
        [Route("showpage/{id}")]
        
        public IActionResult showpage(int id){
            if (HttpContext.Session.GetInt32("CurrentUserId") == null ){
                return RedirectToAction("Index", "Home");
                        }
            Activity CurrentWedding = _context.Activities.Where(ActivitiesId => ActivitiesId.ActivitiesId == id).Include(guests => guests.Invitations).ThenInclude(guest => guest.User).SingleOrDefault();
            ViewBag.CurrentWedding = CurrentWedding;

            ViewBag.wedid = id;
            int? sessionid = HttpContext.Session.GetInt32("CurrentUserId");
            ViewBag.sessionid = sessionid;
            List <Invitation> invite = _context.Invitations.ToList();
            ViewBag.invite = invite;
            List <Invitation> query = _context.Invitations.Where(x => x.UserId == sessionid).Include(x => x.User).ToList();
            ViewBag.query = query;
            ViewBag.userquery = _context.Invitations
                .Where(x => x.UserId == sessionid);
            ViewBag.newquery = _context.Invitations
                .Where(x => x.ActivitiesId == id);
            ViewBag.querywed = _context.Activities
                    .Where(x => x.ActivitiesId == id)
                    .Include(x => x.User)
                    .SingleOrDefault();
            List <Activity> allinvites = _context.Activities.Include(y => y.Invitations).ToList();  //Select * From Weddings and include the invitations list
            ViewBag.allinvites = allinvites;
            ViewBag.Id = HttpContext.Session.GetInt32("CurrentUserId");
            List <User> act = _context.Users.ToList(); 
            ViewBag.act = act;

           
            
            return View("showpage");
        }
        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id){
            if (HttpContext.Session.GetInt32("CurrentUserId") == null ){
                return RedirectToAction("Index", "Home");
                        }
            Activity DeleteWedding = _context.Activities
                .Where(wedding => wedding.ActivitiesId == id)
                .Include(a => a.Invitations)
                .SingleOrDefault();
            //Remove 
            
            _context.Remove(DeleteWedding);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        } 
        [HttpPost]
        [Route("RSVP/{id}")]
        public IActionResult RSVP(int id){

            int CurrentUser = (int)HttpContext.Session.GetInt32("CurrentUserId");
            
            Invitation NewInvite = new Invitation{
                UserId = CurrentUser,
                ActivitiesId = id
            };
            _context.Add(NewInvite);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        [Route("UnRSVP/{id}")]
        public IActionResult UnRSVP(int id){
            if (HttpContext.Session.GetInt32("CurrentUserId") == null ){
                return RedirectToAction("Index", "Home");
                        }
            int CurrentUser = (int)HttpContext.Session.GetInt32("CurrentUserId");
            Invitation UnRSVP = _context.Invitations
            .Where(user => user.UserId == CurrentUser)
            .Where(wedding => wedding.ActivitiesId == id).SingleOrDefault();
            _context.Remove(UnRSVP);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [Route("Showpage/RSVP/{id}")]
        public IActionResult RSVPshow(int id){
            if (HttpContext.Session.GetInt32("CurrentUserId") == null ){
                return RedirectToAction("Index", "Home");
                        }
            int CurrentUser = (int)HttpContext.Session.GetInt32("CurrentUserId");
            
            Invitation NewInvite = new Invitation{
                UserId = CurrentUser,
                ActivitiesId = id
            };
            _context.Add(NewInvite);
            _context.SaveChanges();
            return RedirectToAction("Showpage", new { id = id });

    }
            [HttpGet]
        [Route("Showpage/UnRSVP/{id}")]
        public IActionResult UnRSVPshow(int id){
            if (HttpContext.Session.GetInt32("CurrentUserId") == null ){
                return RedirectToAction("Index", "Home");
                        }
            int CurrentUser = (int)HttpContext.Session.GetInt32("CurrentUserId");
            Invitation UnRSVP = _context.Invitations
            .Where(user => user.UserId == CurrentUser)
            .Where(wedding => wedding.ActivitiesId == id).SingleOrDefault();
            _context.Remove(UnRSVP);
            _context.SaveChanges();
            return RedirectToAction("Showpage", new { id = id });
        }
        [HttpGet]
        [Route("Showpage/Delete/{id}")]
        public IActionResult Deleteshow(int id){
            if (HttpContext.Session.GetInt32("CurrentUserId") == null ){
                return RedirectToAction("Index", "Home");
                        }
            Activity DeleteWedding = _context.Activities
                .Where(wedding => wedding.ActivitiesId == id)
                .Include(a => a.Invitations)
                .SingleOrDefault();
            //Remove 
            
            _context.Remove(DeleteWedding);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }  
    }
}