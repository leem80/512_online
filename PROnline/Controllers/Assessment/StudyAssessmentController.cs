using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Assessments;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.Assessment
{ 
    public class StudyAssessmentController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /StudyAssessment/

        public ViewResult Index()
        {
            return View(db.StudyAssessment.ToList());
        }

        //
        // GET: /StudyAssessment/Details/5

        public ViewResult Details(Guid StudentID)
        {
            List<StudyAssessment> result = db.StudyAssessment.Where(a => a.StudentID == StudentID).OrderByDescending(a => a.CreateDate).ToList();
            ViewBag.schoolID = Request.Params.Get("SchoolID");
            return View(result);
        }

        //
        // GET: /StudyAssessment/Create

        public ActionResult Create()
        {
            //便于返回学校
            ViewBag.schoolID = Request.Params.Get("SchoolID");
            return View();
        } 

        //
        // POST: /StudyAssessment/Create

        [HttpPost]
        public ActionResult Create(StudyAssessment studyassessment)
        {
            if (ModelState.IsValid)
            {
                studyassessment.StudyAssessmentID = Guid.NewGuid();
                Guid studentId = Guid.Parse(Request.Params.Get("studentId"));
                studyassessment.StudentID = studentId;
                studyassessment.CreateDate = DateTime.Now;
                studyassessment.CreatorID = Utils.CurrentUser(this).Id;
                db.StudyAssessment.Add(studyassessment);
                db.SaveChanges();

                String schoolID = Request.Params.Get("SchoolID");
                return RedirectToAction("SchoolNavigation", "AssessmentResult", new { SchoolID = schoolID, type = "study" });  
            }

            return View(studyassessment);
        }
        
        //
        // GET: /StudyAssessment/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            StudyAssessment studyassessment = db.StudyAssessment.Find(id);
            return View(studyassessment);
        }

        //
        // POST: /StudyAssessment/Edit/5

        [HttpPost]
        public ActionResult Edit(StudyAssessment studyassessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studyassessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studyassessment);
        }

        //
        // GET: /StudyAssessment/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            StudyAssessment studyassessment = db.StudyAssessment.Find(id);
            return View(studyassessment);
        }

        //
        // POST: /StudyAssessment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            StudyAssessment studyassessment = db.StudyAssessment.Find(id);
            db.StudyAssessment.Remove(studyassessment);
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