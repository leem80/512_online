using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Service;
using PROnline.Models;
using PROnline.Models.Users;
using PROnline.Src;
using System.Data;

namespace PROnline.Controllers.Service
{
    public class VolunteerFeedbackController : Controller
    {
        private PROnlineContext db = new PROnlineContext();
        //
        // GET: /VolunteerFeedback/

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Nofeedback()
        {
            return View();
        }
        public ActionResult Create(Guid id)
        {
            PairAppointment pa = db.PairAppointment.Find(id);
            Pairs pair1 = db.Pairs.Find(pa.PairsID);
            Student stu = db.Student.Find(pair1.StudentID);
            Guid userId = Utils.CurrentUser(this).Id;
            Volunteer vol = db.Volunteer.Where(s=>s.UserID==userId).Single();

            ViewBag.student = stu;
            ViewBag.volunter = vol;
            ViewBag.PairAppointment = pa;
            return View();

        }

        [HttpPost]
        public ActionResult Create(VolunteerFeedback volunteerfeedback)
        {
            if (ModelState.IsValid)
            {
                volunteerfeedback.VolunteerFeedbackID = Guid.NewGuid();
                volunteerfeedback.volunteerID=Guid.Parse(Request.Params.Get("VolunteerID"));
                Guid appointmentid = Guid.Parse(Request.Params.Get("PairAppointmentID"));
                volunteerfeedback.PairAppointmentID = appointmentid;
                db.VolunteerFeedback.Add(volunteerfeedback);

                PairAppointment pra = db.PairAppointment.Find(appointmentid);
                pra.State = "完成";
                //db.Entry(pra).modify
                db.Entry(pra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyPair", "Pair");
            }
            return View(volunteerfeedback);
        }

        public ActionResult Edit(Guid id)
        {
            var volunteerfeedbacklist = from vfbl in db.VolunteerFeedback
                                        where vfbl.PairAppointmentID == id
                                        select vfbl;

            VolunteerFeedback vf = db.VolunteerFeedback.Find(volunteerfeedbacklist.ToList()[0].VolunteerFeedbackID);
            PairAppointment pa = db.PairAppointment.Find(id);
            Pairs pair1 = db.Pairs.Find(pa.PairsID);
            Student stu = db.Student.Find(pair1.StudentID);
            Guid userId = Utils.CurrentUser(this).Id;
            Volunteer vol = db.Volunteer.Where(s => s.UserID == userId).Single();

            ViewBag.student = stu;
            ViewBag.volunter = vol;
            ViewBag.PairAppointment = pa;
            return View(vf);
        }

        [HttpPost]
        public ActionResult Edit(VolunteerFeedback volunteerfeedback)
        {
            if (ModelState.IsValid)
            {
                VolunteerFeedback ss = db.VolunteerFeedback.Find(volunteerfeedback.VolunteerFeedbackID);
                if (ss != null)
                {
                    ss.Results = volunteerfeedback.Results;
                    ss.Feelings = volunteerfeedback.Feelings;
                }
                db.Entry(ss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyPair", "Pair");
            }
            return View(volunteerfeedback);
        }

    }
}
