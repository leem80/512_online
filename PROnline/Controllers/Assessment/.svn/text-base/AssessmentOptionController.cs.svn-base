using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Assessments;
using PROnline.Models;

namespace PROnline.Controllers.Assessment
{ 
    public class AssessmentOptionController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /AssessmentOption/

        public ViewResult Index()
        {
            return View(db.AssessmentOption.ToList());
        }

        //
        // GET: /AssessmentOption/Details/5

        public ViewResult Details(Guid id)
        {
            AssessmentOption assessmentoption = db.AssessmentOption.Find(id);
            return View(assessmentoption);
        }

        //
        // GET: /AssessmentOption/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AssessmentOption/Create

        [HttpPost]
        public ActionResult Create(AssessmentOption assessmentoption)
        {
            if (ModelState.IsValid)
            {
                assessmentoption.AssessmentOptionID = Guid.NewGuid();
                db.AssessmentOption.Add(assessmentoption);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(assessmentoption);
        }
        
        //
        // GET: /AssessmentOption/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            AssessmentOption assessmentoption = db.AssessmentOption.Find(id);
            return View(assessmentoption);
        }

        //
        // POST: /AssessmentOption/Edit/5

        [HttpPost]
        public ActionResult Edit(AssessmentOption assessmentoption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assessmentoption).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assessmentoption);
        }

        //
        // GET: /AssessmentOption/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            AssessmentOption assessmentoption = db.AssessmentOption.Find(id);
            return View(assessmentoption);
        }

        //
        // POST: /AssessmentOption/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            AssessmentOption assessmentoption = db.AssessmentOption.Find(id);
            db.AssessmentOption.Remove(assessmentoption);
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