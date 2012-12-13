using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Teams;
using PROnline.Models.Users;
using PROnline.Models.Letters;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.Teams
{ 
    public class TeamLeaderController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /TeamLeader/

        public ViewResult Index()
        {
            var teamleader = db.TeamLeader.Include(t => t.Volunteer);
            return View(teamleader.ToList());
        }

        //
        // GET: /TeamLeader/Details/5

        public ViewResult Details(Guid id)
        {
            TeamLeader teamleader = db.TeamLeader.Find(id);
            return View(teamleader);
        }

        //
        // GET: /TeamLeader/Create

        public ActionResult Create()
        {
            ViewBag.VolunteerID = new SelectList(db.Volunteer, "VolunteerID", "PoliticalExperience");
            return View();
        } 

        //
        // POST: /TeamLeader/Create

        [HttpPost]
        public ActionResult Create(TeamLeader teamleader)
        {
            if (ModelState.IsValid)
            {
                teamleader.TeamLeaderID = Guid.NewGuid();
                teamleader.Volunteer.isHaveTeam = false;
                db.TeamLeader.Add(teamleader);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.VolunteerID = new SelectList(db.Volunteer, "VolunteerID", "PoliticalExperience", teamleader.VolunteerID);
            return View(teamleader);
        }

        //
        // GET: /TeamLeader/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            TeamLeader teamleader = db.TeamLeader.Find(id);
            ViewBag.VolunteerID = new SelectList(db.Volunteer, "VolunteerID", "PoliticalExperience", teamleader.VolunteerID);
            return View(teamleader);
        }

        //
        // POST: /TeamLeader/Edit/5

        [HttpPost]
        public ActionResult Edit(TeamLeader teamleader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamleader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VolunteerID = new SelectList(db.Volunteer, "VolunteerID", "PoliticalExperience", teamleader.VolunteerID);
            return View(teamleader);
        }

        //
        // GET: /TeamLeader/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            TeamLeader teamleader = db.TeamLeader.Find(id);
            return View(teamleader);
        }

        //
        // POST: /TeamLeader/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            TeamLeader teamleader = db.TeamLeader.Find(id);
            //teamleader.VolunteerID = Guid.Empty;
            db.TeamLeader.Remove(teamleader);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //组长申请脱离界面,id为申请脱离的TeamLeaderID
        public ActionResult ApplyingExit(Guid id)
        {
            TeamLeader teamLeader = db.TeamLeader.Find(id);
            if (teamLeader != null)
            {
                Team team = db.Team.Find(teamLeader.TeamID);
                ViewBag.ApplyTime = DateTime.Now;
                ViewBag.TeamName = team.TeamName;
                return View(teamLeader);
            }

            return View();
        }

        [HttpPost]
        public ActionResult ApplyingExit(TeamLeader t)
        {
            String temp = Request.Form.Get("Reason");
            String apply = Request.Form.Get("Apply");
            String tlID = Request.Form.Get("TeamLeaderID");
            Guid id = Guid.Parse(tlID);
            TeamLeader teamLeader = db.TeamLeader.Find(id);
            if (teamLeader != null)
            {
                if (teamLeader.TeamID != null)
                {
                    Team team = db.Team.Find(teamLeader.TeamID);
                    team.TeamLeaderID = null;
                    db.Entry(team).State = EntityState.Modified;
                    db.SaveChanges();
                }

                Volunteer volunteer = db.Volunteer.Find(teamLeader.VolunteerID);
                volunteer.isHaveTeam = false;
                volunteer.FirtPassword = "x";
                volunteer.ConfirmPassword = "x";
                db.Entry(volunteer).State = EntityState.Modified;

                Team teaml = db.Team.Find(teamLeader.TeamID);
                String wName = teaml.WorkStation.WorkStationName;
                String tName = teaml.TeamName;
                String tlName = volunteer.User.RealName;

                String[] date = DateTime.Now.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                var ulist = db.Users.Where(r => r.RoleId == Role.SERVICE_MANAGER).ToList();
                foreach(User u in ulist)
                {
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "小组长脱离小组！";
                    if (apply != null)
                    {
                        l.Content = "志愿者小组长" + tlName + "于" + time + "脱离小组:" + wName + tName + ",请及时为该小组安排小组长！脱离原因为：" + temp + ",备注为:" + apply;
                    } 
                    else
                    {
                        l.Content = "志愿者小组长" + tlName + "于" + time + "脱离小组:" + wName + tName + ",请及时为该小组安排小组长！脱离原因为：" + temp;
                    }
                    l.ReceiverID = u.Id;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }

                db.TeamLeader.Remove(teamLeader);
                db.SaveChanges();

                return RedirectToAction("MyTeam", "Team");
            }
            return View();
        }
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}