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
    public class AssessmentQuestionController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /AssessmentQuestion/

        public ViewResult Index()
        {
            var list = from a in db.AssessmentQuestion
                        where a.isDeleted==false
                        select  a;        
            return View(list.ToList());
        }

        //所有未删除的问题
        public List<AssessmentQuestion> AllQuestion()
        {
            var list = from a in db.AssessmentQuestion
                       where a.isDeleted == false
                       select a;
            return list.ToList();
        }

        public ViewResult Add(Guid id)
        {
            AssessmentQuestion assessmentquestion = db.AssessmentQuestion.Find(id);
            return View(assessmentquestion);
        }

        //
        // GET: /AssessmentQuestion/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AssessmentQuestion/Create

        [HttpPost]
        public ActionResult Create(AssessmentQuestion question)
        {
            question.AssessmentQuestionID = Guid.NewGuid();
            question.CreatorID = Utils.CurrentUser(this).Id;
            question.CreateDate = DateTime.Now;
            question.isDeleted = false;
            foreach (AssessmentOption option in question.AssessmentOptionList)
            {
                option.AssessmentOptionID = Guid.NewGuid();
                option.CreatorID = Utils.CurrentUser(this).Id;
                option.CreateDate = DateTime.Now;
            }
            db.AssessmentQuestion.Add(question);         
            db.SaveChanges();
            return RedirectToAction("Index");  
        }
        
        //
        // GET: /AssessmentQuestion/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            AssessmentQuestion assessmentquestion = db.AssessmentQuestion.Find(id);
            return View(assessmentquestion);
        }

        //
        // POST: /AssessmentQuestion/Edit/5

        [HttpPost]
        public ActionResult Edit(AssessmentQuestion assessmentquestion)
        {
            if (ModelState.IsValid)
            {
                AssessmentQuestion temp = db.AssessmentQuestion.Find(assessmentquestion.AssessmentQuestionID);
                temp.Title = assessmentquestion.Title;
                temp.ModifierID = Utils.CurrentUser(this).Id;
                temp.ModifyDate = DateTime.Now;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assessmentquestion);
        }

        //
        // GET: /AssessmentQuestion/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            AssessmentQuestion assessmentquestion = db.AssessmentQuestion.Find(id);
            return View(assessmentquestion);
        }

        //
        // POST: /AssessmentQuestion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            AssessmentQuestion assessmentquestion = db.AssessmentQuestion.Find(id);
            assessmentquestion.isDeleted = true;
            assessmentquestion.ModifierID = Utils.CurrentUser(this).Id;
            assessmentquestion.ModifyDate = DateTime.Now;
            db.Entry(assessmentquestion).State = EntityState.Modified;
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