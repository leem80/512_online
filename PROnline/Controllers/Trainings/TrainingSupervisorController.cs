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
    public class TrainingSupervisorController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //督导专家出席
        public ActionResult SupervisorAttend(Guid id)
        {
            TrainingSupervisor trainingsupervisor = db.TrainingSupervisor.Find(id);
            trainingsupervisor.IsAttend = true;
            db.Entry(trainingsupervisor).State = EntityState.Modified;
            db.SaveChanges();
            return Content("已签到");
        }

        //
        // GET: /TrainingSupervisor/

        public ViewResult Index()
        {
            var trainingsupervisor = db.TrainingSupervisor;
            return View(trainingsupervisor.ToList());
        }

        //
        // GET: /TrainingSupervisor/Details/5

        public ViewResult Details(Guid id)
        {
            TrainingSupervisor trainingsupervisor = db.TrainingSupervisor.Find(id);
            return View(trainingsupervisor);
        }

        //
        // GET: /TrainingSupervisor/Create

        public ActionResult Create()
        {
            ViewBag.TrainingID = new SelectList(db.Training, "TrainingID", "Title");
            ViewBag.VolunteerID = new SelectList(db.Volunteer, "VolunteerID", "PoliticalExperience");
            return View();
        } 

        //
        // POST: /TrainingSupervisor/Create

        [HttpPost]
        public ActionResult Create(TrainingSupervisor trainingsupervisor)
        {
            if (ModelState.IsValid)
            {
                trainingsupervisor.TrainingSupervisorID = Guid.NewGuid();
                db.TrainingSupervisor.Add(trainingsupervisor);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            return View(trainingsupervisor);
        }
        
        //
        // GET: /TrainingSupervisor/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            TrainingSupervisor trainingsupervisor = db.TrainingSupervisor.Find(id);
            return View(trainingsupervisor);
        }

        //
        // POST: /TrainingSupervisor/Edit/5

        [HttpPost]
        public ActionResult Edit(TrainingSupervisor trainingsupervisor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainingsupervisor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainingsupervisor);
        }

        //
        // GET: /TrainingSupervisor/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            TrainingSupervisor trainingsupervisor = db.TrainingSupervisor.Find(id);
            return View(trainingsupervisor);
        }

        //
        // POST: /TrainingSupervisor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            TrainingSupervisor trainingsupervisor = db.TrainingSupervisor.Find(id);
            db.TrainingSupervisor.Remove(trainingsupervisor);
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