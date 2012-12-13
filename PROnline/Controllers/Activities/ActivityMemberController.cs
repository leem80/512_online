using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Activities;
using PROnline.Models;

namespace PROnline.Controllers.Activities
{ 
    public class ActivityMemberController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /ActivityMember/

        public ViewResult Index()
        {
            var activitymember = db.ActivityMember.Include(a => a.Activity).Include(a => a.User);
            return View(activitymember.ToList());
        }

        //
        // GET: /ActivityMember/Details/5

        public ViewResult Details(Guid id)
        {
            ActivityMember activitymember = db.ActivityMember.Find(id);
            return View(activitymember);
        }

        //
        // GET: /ActivityMember/Create

        public ActionResult Create()
        {
            ViewBag.ActivityID = new SelectList(db.Activity, "ActivityID", "ActivityName");
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName");
            return View();
        } 

        //
        // POST: /ActivityMember/Create

        [HttpPost]
        public ActionResult Create(ActivityMember activitymember)
        {
            if (ModelState.IsValid)
            {
                activitymember.ActivityMemberID = Guid.NewGuid();
                db.ActivityMember.Add(activitymember);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ActivityID = new SelectList(db.Activity, "ActivityID", "ActivityName", activitymember.ActivityID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", activitymember.UserID);
            return View(activitymember);
        }
        
        //
        // GET: /ActivityMember/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            ActivityMember activitymember = db.ActivityMember.Find(id);
            ViewBag.ActivityID = new SelectList(db.Activity, "ActivityID", "ActivityName", activitymember.ActivityID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", activitymember.UserID);
            return View(activitymember);
        }

        //
        // POST: /ActivityMember/Edit/5

        [HttpPost]
        public ActionResult Edit(ActivityMember activitymember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activitymember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityID = new SelectList(db.Activity, "ActivityID", "ActivityName", activitymember.ActivityID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", activitymember.UserID);
            return View(activitymember);
        }

        //
        // GET: /ActivityMember/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            ActivityMember activitymember = db.ActivityMember.Find(id);
            return View(activitymember);
        }

        //
        // POST: /ActivityMember/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            ActivityMember activitymember = db.ActivityMember.Find(id);
            db.ActivityMember.Remove(activitymember);
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