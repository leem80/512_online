using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Trainings;
using PROnline.Models;

namespace PROnline.Controllers.Trainings
{ 
    public class TrainingVolunteerController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //志愿者出席
        public ActionResult VolunteerAttend(Guid id)
        {
            TrainingVolunteer trainingvolunteer = db.TrainingVolunteer.Find(id);
            trainingvolunteer.IsAttend = true;
            db.Entry(trainingvolunteer).State = EntityState.Modified;
            db.SaveChanges();
            return Content("已签到");
        }

        //返回志愿者参与的所有培训
        // GET: /TrainingVolunteer/
        //参数:Volunteer=>VolunteerID
        public List<TrainingVolunteer> Index(Guid volunteerId)
        {
            return db.TrainingVolunteer.Where(t=>t.VolunteerID==volunteerId).Where(t=>t.Training.State=="正常").Include(t => t.Training).ToList();
        }

        //
        // GET: /TrainingVolunteer/Details/5

        public ViewResult Details(Guid id)
        {
            TrainingVolunteer trainingvolunteer = db.TrainingVolunteer.Find(id);
            return View(trainingvolunteer);
        }

        //
        // GET: /TrainingVolunteer/Create

        public ActionResult Create()
        {
            ViewBag.TrainingID = new SelectList(db.Training, "TrainingID", "Title");
            return View();
        } 

        //
        // POST: /TrainingVolunteer/Create

        [HttpPost]
        public ActionResult Create(TrainingVolunteer trainingvolunteer)
        {
            if (ModelState.IsValid)
            {
                trainingvolunteer.TrainingVolunteerID = Guid.NewGuid();
                db.TrainingVolunteer.Add(trainingvolunteer);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.TrainingID = new SelectList(db.Training, "TrainingID", "Title", trainingvolunteer.TrainingID);
            return View(trainingvolunteer);
        }
        
        //
        // GET: /TrainingVolunteer/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            TrainingVolunteer trainingvolunteer = db.TrainingVolunteer.Find(id);
            ViewBag.TrainingID = new SelectList(db.Training, "TrainingID", "Title", trainingvolunteer.TrainingID);
            return View(trainingvolunteer);
        }

        //
        // POST: /TrainingVolunteer/Edit/5

        [HttpPost]
        public ActionResult Edit(TrainingVolunteer trainingvolunteer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainingvolunteer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrainingID = new SelectList(db.Training, "TrainingID", "Title", trainingvolunteer.TrainingID);
            return View(trainingvolunteer);
        }

        //
        // GET: /TrainingVolunteer/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            TrainingVolunteer trainingvolunteer = db.TrainingVolunteer.Find(id);
            return View(trainingvolunteer);
        }

        //
        // POST: /TrainingVolunteer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            TrainingVolunteer trainingvolunteer = db.TrainingVolunteer.Find(id);
            db.TrainingVolunteer.Remove(trainingvolunteer);
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