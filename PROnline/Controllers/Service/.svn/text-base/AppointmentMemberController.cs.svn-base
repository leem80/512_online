using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Service;
using PROnline.Models;

namespace PROnline.Controllers.Service
{ 
    public class AppointmentMemberController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /AppointmentMember/

        public ViewResult Index()
        {
            var appointmentmember = db.AppointmentMember.Include(a => a.PairAppointment).Include(a => a.User);
            return View(appointmentmember.ToList());
        }

        //
        // GET: /AppointmentMember/Details/5

        public ViewResult Details(Guid id)
        {
            AppointmentMember appointmentmember = db.AppointmentMember.Find(id);
            return View(appointmentmember);
        }

        //
        // GET: /AppointmentMember/Create

        public ActionResult Create()
        {
            ViewBag.PairAppointmentID = new SelectList(db.PairAppointment, "PairAppointmentID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName");
            return View();
        } 

        //
        // POST: /AppointmentMember/Create

        [HttpPost]
        public ActionResult Create(AppointmentMember appointmentmember)
        {
            if (ModelState.IsValid)
            {
                appointmentmember.AppointmentMemberID = Guid.NewGuid();
                db.AppointmentMember.Add(appointmentmember);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PairAppointmentID = new SelectList(db.PairAppointment, "PairAppointmentID", "Name", appointmentmember.PairAppointmentID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", appointmentmember.UserID);
            return View(appointmentmember);
        }
        
        //
        // GET: /AppointmentMember/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            AppointmentMember appointmentmember = db.AppointmentMember.Find(id);
            ViewBag.PairAppointmentID = new SelectList(db.PairAppointment, "PairAppointmentID", "Name", appointmentmember.PairAppointmentID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", appointmentmember.UserID);
            return View(appointmentmember);
        }

        //
        // POST: /AppointmentMember/Edit/5

        [HttpPost]
        public ActionResult Edit(AppointmentMember appointmentmember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointmentmember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PairAppointmentID = new SelectList(db.PairAppointment, "PairAppointmentID", "Name", appointmentmember.PairAppointmentID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", appointmentmember.UserID);
            return View(appointmentmember);
        }

        //
        // GET: /AppointmentMember/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            AppointmentMember appointmentmember = db.AppointmentMember.Find(id);
            return View(appointmentmember);
        }

        //
        // POST: /AppointmentMember/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            AppointmentMember appointmentmember = db.AppointmentMember.Find(id);
            db.AppointmentMember.Remove(appointmentmember);
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