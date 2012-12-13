using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Service;
using PROnline.Models;
using PROnline.Models.Users;
using PROnline.Src;

namespace PROnline.Controllers.Service
{ 
    public class StudentFeedbackController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /StudentFeedback/

        public ViewResult Index()
        {
            var studentfeedback = db.StudentFeedback.Include(s => s.Student).Include(s => s.PairAppointment);
            return View(studentfeedback.ToList());
        }

        public ViewResult Nofeedback() {
           
           
            return View();
        }
        //
        // GET: /StudentFeedback/Details/5

        public ViewResult Details(Guid id)
        {
            StudentFeedback studentfeedback = db.StudentFeedback.Find(id);
            return View(studentfeedback);
        }

        //
        // GET: /StudentFeedback/Create
        //id: 视讯预约ID
        public ActionResult Create(Guid id)
        {
            PairAppointment pa = db.PairAppointment.Find(id);
           // List<AppointmentMember> members = (List<AppointmentMember>) pa.AppointmentMember;
            Pairs pair1=db.Pairs.Find(pa.PairsID);
            Volunteer vol = db.Volunteer.Find(pair1.VolunteerID);
            Guid  userId = Utils.CurrentUser(this).Id;
            Student stu = db.Student.Where(s => s.UserID == userId).Single();

            ViewBag.student = stu;
            ViewBag.volunter = vol;
            ViewBag.PairAppointment = pa;
            return View();
           
        }

        public ActionResult Edit(Guid id)
        {
            var studentfeedbacklist = from sfbl in db.StudentFeedback
                                      where sfbl.PairAppointmentID == id
                                      select sfbl;

            StudentFeedback sf = db.StudentFeedback.Find(studentfeedbacklist.ToList()[0].StudentFeedbackID);
            PairAppointment pa = db.PairAppointment.Find(id);
            // List<AppointmentMember> members = (List<AppointmentMember>) pa.AppointmentMember;
            Pairs pair1 = db.Pairs.Find(pa.PairsID);
            Volunteer vol = db.Volunteer.Find(pair1.VolunteerID);
            Guid userId = Utils.CurrentUser(this).Id;
            Student stu = db.Student.Where(s => s.UserID == userId).Single();

            ViewBag.student = stu;
            ViewBag.volunter = vol;
            ViewBag.PairAppointment = pa;
            return View(sf);
        }

        [HttpPost]
        public ActionResult Edit(StudentFeedback studentfeedback)
        {
            if (ModelState.IsValid)
            {
                StudentFeedback ss = db.StudentFeedback.Find(studentfeedback.StudentFeedbackID);
                if (ss != null) {
                    ss.Results = studentfeedback.Results;
                    ss.Feelings = studentfeedback.Feelings;
                }
                db.Entry(ss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyPair", "Pair");
            }
            return View(studentfeedback);
        }
        //
        // POST: /StudentFeedback/Create

        [HttpPost]
        public ActionResult Create(StudentFeedback studentfeedback)
        {
            if (ModelState.IsValid)
            {
                studentfeedback.StudentFeedbackID = Guid.NewGuid();
                studentfeedback.StudentID = Guid.Parse(Request.Params.Get("StudentID"));
                Guid pairappointmentid = Guid.Parse(Request.Params.Get("PairAppointmentID"));
                studentfeedback.PairAppointmentID = pairappointmentid;
               // studentfeedback.VolunteerID = Guid.Parse(Request.Params.Get("VolunteerID"));
                db.StudentFeedback.Add(studentfeedback);
                PairAppointment pra = db.PairAppointment.Find(pairappointmentid);
                pra.State = "完成";
                //db.Entry(pra).modify
                db.Entry(pra).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("MyPair","Pair");  
            }
            return View(studentfeedback);
        }
        
        //
        // GET: /StudentFeedback/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            StudentFeedback studentfeedback = db.StudentFeedback.Find(id);
            return View(studentfeedback);
        }

        //
        // POST: /StudentFeedback/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            StudentFeedback studentfeedback = db.StudentFeedback.Find(id);
            db.StudentFeedback.Remove(studentfeedback);
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