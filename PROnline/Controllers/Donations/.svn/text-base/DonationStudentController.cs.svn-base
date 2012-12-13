using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Donation;
using PROnline.Models;

namespace PROnline.Controllers.Donations
{ 
    public class DonationStudentController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /DonationStudent/

        public ViewResult Index()
        {
            var donationstudent = db.DonationStudent.Include(d => d.Student).Include(d => d.DonationItem);
            return View(donationstudent.ToList());
        }

        //
        // GET: /DonationStudent/Details/5

        public ViewResult Details(Guid id)
        {
            DonationStudent donationstudent = db.DonationStudent.Find(id);
            return View(donationstudent);
        }

        //
        // GET: /DonationStudent/Create

        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Student, "StudentID", "State");
            ViewBag.DonationItemID = new SelectList(db.DonationItem, "DonationItemID", "Title");
            return View();
        } 

        //
        // POST: /DonationStudent/Create

        [HttpPost]
        public ActionResult Create(DonationStudent donationstudent)
        {
            if (ModelState.IsValid)
            {
                donationstudent.DonationStudentID = Guid.NewGuid();
                db.DonationStudent.Add(donationstudent);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.StudentID = new SelectList(db.Student, "StudentID", "State", donationstudent.StudentID);
            ViewBag.DonationItemID = new SelectList(db.DonationItem, "DonationItemID", "Title", donationstudent.DonationItemID);
            return View(donationstudent);
        }
        
        //
        // GET: /DonationStudent/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            DonationStudent donationstudent = db.DonationStudent.Find(id);
            ViewBag.StudentID = new SelectList(db.Student, "StudentID", "State", donationstudent.StudentID);
            ViewBag.DonationItemID = new SelectList(db.DonationItem, "DonationItemID", "Title", donationstudent.DonationItemID);
            return View(donationstudent);
        }

        //
        // POST: /DonationStudent/Edit/5

        [HttpPost]
        public ActionResult Edit(DonationStudent donationstudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donationstudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Student, "StudentID", "State", donationstudent.StudentID);
            ViewBag.DonationItemID = new SelectList(db.DonationItem, "DonationItemID", "Title", donationstudent.DonationItemID);
            return View(donationstudent);
        }

        //
        // GET: /DonationStudent/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            DonationStudent donationstudent = db.DonationStudent.Find(id);
            return View(donationstudent);
        }

        //
        // POST: /DonationStudent/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            DonationStudent donationstudent = db.DonationStudent.Find(id);
            db.DonationStudent.Remove(donationstudent);
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