using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using PROnline.Src;
using PROnline.Models.Users;
using PROnline.Models.Trainings;
using PROnline.Models.Letters;
using PROnline.Models;

namespace PROnline.Controllers.Trainings
{
    public class TrainingFeedbackController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /TrainingFeedback/

        public ActionResult Index()
        {         
            return View();
        }

        //
        // GET: /TrainingFeedback/Details/5

        public ActionResult Details(Guid id, Guid vid)
        {
            TrainingFeedback tf = db.TrainingFeedback.Where(r => r.TrainingID == id)
                .Where(r => r.VolunteerID == vid).First();

            Training t = db.Training.Find(id);
            String time = t.StartDate.ToShortDateString() + " " + t.StartTime + "--"+ t.EndTime;
            ViewBag.time = time;
            
            return View(tf);
        }

        //
        // GET: /TrainingFeedback/Create
        //传来的是培训id
        public ActionResult Create(Guid id)
        {
            String userID = Request.Params.Get("userID");
            ViewBag.userID = userID;

            User user = db.Users.Find(Guid.Parse(userID));
            ViewBag.name = user.RealName;

            ViewBag.trainingID = Convert.ToString(id);

            Training training = db.Training.Find(id);

            String trainingTime = training.StartDate.ToShortDateString() + " " 
                + training.StartTime + "--" + training.EndTime;
            ViewBag.trainingTime = trainingTime;

            return View();
        } 

        //
        // POST: /TrainingFeedback/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            String uid = Request.Params.Get("userID");
            String tid = Request.Params.Get("trainingID");
            String selfGains = Request.Params.Get("selfGains");
            String trainingAdvice = Request.Params.Get("trainingAdvice");

            Guid userID = Guid.Parse(uid);
            Guid trainingID = Guid.Parse(tid);

            Volunteer v = db.Volunteer.Where(r => r.UserID == userID).First();
            TrainingVolunteer tv = db.TrainingVolunteer.Where(r => r.TrainingID == trainingID)
                .Where(r => r.VolunteerID == v.VolunteerID).First();

            tv.isFeedback = true;
            db.Entry(tv).State = EntityState.Modified;

            TrainingFeedback trainingFeedback = new TrainingFeedback();
            trainingFeedback.TrainingFeedbackID = Guid.NewGuid();
            trainingFeedback.TrainingID = trainingID;
            trainingFeedback.VolunteerID = v.VolunteerID;
            trainingFeedback.SelfGains = selfGains;
            trainingFeedback.TrainingAdvice = trainingAdvice;

            db.TrainingFeedback.Add(trainingFeedback);
            db.SaveChanges();

            User user = Utils.CurrentUser(this);
            var ulist = db.Users.Where(r => r.RoleId == Role.SERVICE_MANAGER).ToList();
            foreach(User u in ulist)
            {
                //发送站内信
                Letter l = new Letter();
                l.LetterID = Guid.NewGuid();
                l.IsRead = "未读";
                l.Title = "培训反馈";
                l.Content = "您收到了来自志愿者" + user.RealName + "的培训反馈信息，详细信息请查看培训反馈。";
                l.ReceiverID = u.Id;
                l.SenderID = user.Id;
                l.SenderDate = DateTime.Now;
                db.Letter.Add(l);
                db.SaveChanges();
            }

            return RedirectToAction("MyTrainingFeedback", "Training", null);
        }
        
        //
        // GET: /TrainingFeedback/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TrainingFeedback/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TrainingFeedback/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TrainingFeedback/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
