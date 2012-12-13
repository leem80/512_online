using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Assessments;
using PROnline.Models;
using PROnline.Models.Users;
using PROnline.Controllers.Users;

namespace PROnline.Controllers.Assessment
{ 
    public class AssessmentResultController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /AssessmentResult/
        //参数：Student=>StudentID
        [ValidateInput(false)]
        public ViewResult Index(Guid StudentID)
        {
            List<AssessmentResult> resultList = db.AssessmentResult.Where(a => a.StudentID == StudentID).ToList();
            return View(resultList);
        }

        public ViewResult SchoolNavigation()
        {
            SchoolController sc = new SchoolController();
            ViewBag.SchoolDropDownList = sc.SchoolDropDownList();
            string dd = Request.Params.Get("type");
            ViewBag.type = Request.Params.Get("type");
            String id = Request.Params.Get("SchoolID");
            School school = null;
            if (id != null)
            {
                school = db.School.Find(Guid.Parse(id));
            }
            return View(school);
        }

        //
        // GET: /AssessmentResult/Details/5

        public ViewResult Details(Guid StudentID)
        {
            List<AssessmentResult> result = db.AssessmentResult.Where(a => a.StudentID == StudentID).OrderByDescending(a=>a.CreateDate).ToList();
            ViewBag.schoolID = Request.Params.Get("SchoolID");
            return View(result);
        }

        // GET: /AssessmentQuestion/Assess

        public ActionResult Assess(AssessmentQuestion q)
        {
            String dd = Request.Params.Get("optioncount");          
            return RedirectToAction("Index");
        }

        //
        // GET: /AssessmentResult/Create

        public ActionResult Create()
        {
            AssessmentQuestionController controller = new AssessmentQuestionController();
            ViewBag.list = controller.AllQuestion();
            //便于返回学校
            ViewBag.schoolID = Request.Params.Get("SchoolID");
            return View();
        }

        //
        // POST: /AssessmentResult/Create

        [HttpPost]
        public ActionResult Create(AssessmentResult assessmentresult)
        {
            if (ModelState.IsValid)
            {
                assessmentresult.AssessmentResultID = Guid.NewGuid();
                Guid studentId = Guid.Parse(Request.Params.Get("studentId"));
                assessmentresult.StudentID = studentId;
                if (assessmentresult.ResultOptionList != null)
                {
                    foreach (AssessmentResultOption resultOption in assessmentresult.ResultOptionList)
                    {
                        resultOption.AssessmentResultOptionID = Guid.NewGuid();
                    }
                }
                assessmentresult.CreateDate = DateTime.Now;
                db.AssessmentResult.Add(assessmentresult);
                db.SaveChanges();

                String schoolID = Request.Params.Get("SchoolID");
                return RedirectToAction("SchoolNavigation", new { SchoolID = schoolID, type = "psychological" });  
            }

            return View(assessmentresult);
        }
        
        //
        // GET: /AssessmentResult/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            AssessmentResult assessmentresult = db.AssessmentResult.Find(id);            
            return View(assessmentresult);
        }

        //
        // POST: /AssessmentResult/Edit/5

        [HttpPost]
        public ActionResult Edit(AssessmentResult assessmentresult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assessmentresult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assessmentresult);
        }

        //
        // GET: /AssessmentResult/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            AssessmentResult assessmentresult = db.AssessmentResult.Find(id);
            return View(assessmentresult);
        }

        //
        // POST: /AssessmentResult/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            AssessmentResult assessmentresult = db.AssessmentResult.Find(id);
            db.AssessmentResult.Remove(assessmentresult);
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