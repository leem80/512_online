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
    public class AssessmentResultOptionController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        public ViewResult Index(Guid AssessmentResultID)
        {
            List<AssessmentResultOption> resultList = db.AssessmentResultOption.Where(a => a.AssessmentResult.AssessmentResultID == AssessmentResultID).ToList();
            return View(resultList);
        }

        //创建结果选项
        public void Create(List<AssessmentResultOption> resultOptionList)
        {
            if (ModelState.IsValid)
            {
                foreach (AssessmentResultOption option in resultOptionList)
                {
                    option.AssessmentResultOptionID = Guid.NewGuid();
                    db.AssessmentResultOption.Add(option);
                }
                
                db.SaveChanges();
                
            }

            //ViewBag.AssessmentQuestionID = new SelectList(db.AssessmentQuestion, "AssessmentQuestionID", "Title", assessmentresultoption.AssessmentQuestionID);
            //return View(assessmentresultoption);
        }
        
        //
        // GET: /AssessmentResultOption/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            AssessmentResultOption assessmentresultoption = db.AssessmentResultOption.Find(id);
            //ViewBag.AssessmentQuestionID = new SelectList(db.AssessmentQuestion, "AssessmentQuestionID", "Title", assessmentresultoption.AssessmentQuestionID);
            return View(assessmentresultoption);
        }

        //
        // POST: /AssessmentResultOption/Edit/5

        [HttpPost]
        public ActionResult Edit(AssessmentResultOption assessmentresultoption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assessmentresultoption).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.AssessmentQuestionID = new SelectList(db.AssessmentQuestion, "AssessmentQuestionID", "Title", assessmentresultoption.AssessmentQuestionID);
            return View(assessmentresultoption);
        }

        //
        // GET: /AssessmentResultOption/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            AssessmentResultOption assessmentresultoption = db.AssessmentResultOption.Find(id);
            return View(assessmentresultoption);
        }

        //
        // POST: /AssessmentResultOption/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            AssessmentResultOption assessmentresultoption = db.AssessmentResultOption.Find(id);
            db.AssessmentResultOption.Remove(assessmentresultoption);
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