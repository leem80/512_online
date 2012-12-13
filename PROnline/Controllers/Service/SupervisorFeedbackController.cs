using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models;
using PROnline.Models.Service;
using PROnline.Src;
using PROnline.Models.Users;

namespace PROnline.Controllers.Service
{
    public class SupervisorFeedbackController : Controller
    {
        private PROnlineContext db = new PROnlineContext();
        //
        // GET: /SupervisorFeedback/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckStudentFeedback(Guid id)
        {
            var studentfeedbacklist = from sfbl in db.StudentFeedback
                                      where sfbl.PairAppointmentID == id
                                      select sfbl;

            if (studentfeedbacklist.ToList().Count == 0)
            {
                return RedirectToAction("Nofeedback", "StudentFeedback");
            }
            else
            {
                StudentFeedback sf = db.StudentFeedback.Find(studentfeedbacklist.ToList()[0].StudentFeedbackID);
                PairAppointment pa = db.PairAppointment.Find(id);
                Pairs pair1 = db.Pairs.Find(pa.PairsID);
                Guid userId = Utils.CurrentUser(this).Id;
                Supervisor sup = db.Supervisor.Where(s => s.UserID == userId).Single();
                Student stu = db.Student.Find(pair1.StudentID);
                Volunteer vol = db.Volunteer.Find(pair1.VolunteerID);
                ViewBag.student = stu;
                ViewBag.volunter = vol;
                ViewBag.PairAppointment = pa;
                return View(sf);
            }
           
          
        }

        public ActionResult CheckVolunteerFeedback(Guid id)
        {
            var volunteerfeedbacklist = from vfbl in db.VolunteerFeedback
                                        where vfbl.PairAppointmentID == id
                                        select vfbl;

            if (volunteerfeedbacklist.ToList().Count == 0)
            {
                return RedirectToAction("Nofeedback", "VolunteerFeedback");
            }
            else
            {
                VolunteerFeedback vf = db.VolunteerFeedback.Find(volunteerfeedbacklist.ToList()[0].VolunteerFeedbackID);
                PairAppointment pa = db.PairAppointment.Find(id);
                Pairs pair1 = db.Pairs.Find(pa.PairsID);
                Guid userId = Utils.CurrentUser(this).Id;
                Supervisor sup = db.Supervisor.Where(s => s.UserID == userId).Single();
                Student stu = db.Student.Find(pair1.StudentID);
                Volunteer vol = db.Volunteer.Find(pair1.VolunteerID);
                ViewBag.student = stu;
                ViewBag.volunter = vol;
                ViewBag.PairAppointment = pa;
                return View(vf);
            }

        }

    }
}
