using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Users;
using PROnline.Models;

namespace PROnline.Controllers.Users
{ 
    public class xxxController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /xxx/

        public ViewResult Index()
        {
            var volunteer = db.Volunteer.Include(v => v.User);
            return View(volunteer.ToList());
        }

        //
        // GET: /xxx/Details/5

        public ViewResult Details(Guid id)
        {
            Volunteer volunteer = db.Volunteer.Find(id);
            return View(volunteer);
        }

        //
        // GET: /xxx/Create

        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName");
            return View();
        } 

        //
        // POST: /xxx/Create

        [HttpPost]
        public ActionResult Create(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                volunteer.VolunteerID = Guid.NewGuid();
                db.Volunteer.Add(volunteer);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", volunteer.UserID);
            return View(volunteer);
        }
        
        //
        // GET: /xxx/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Volunteer volunteer = db.Volunteer.Find(id);
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", volunteer.UserID);
            return View(volunteer);
        }

        //
        // POST: /xxx/Edit/5

        [HttpPost]
        public ActionResult Edit(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", volunteer.UserID);
            return View(volunteer);
        }

        //
        // GET: /xxx/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Volunteer volunteer = db.Volunteer.Find(id);
            return View(volunteer);
        }

        //
        // POST: /xxx/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Volunteer volunteer = db.Volunteer.Find(id);
            db.Volunteer.Remove(volunteer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}