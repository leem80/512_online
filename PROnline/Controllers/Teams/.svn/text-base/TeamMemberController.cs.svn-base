using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Teams;
using PROnline.Models;
using PROnline.Src;
using PROnline.Models.Users;
using PROnline.Models.Letters;

namespace PROnline.Controllers.Teams
{ 
    public class TeamMemberController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /TeamMember/

        public ViewResult Index()
        {
            return View(db.TeamMember.ToList());
        }

        //
        // GET: /TeamMember/Details/5

        public ViewResult Details(Guid id)
        {
            TeamMember teammember = db.TeamMember.Find(id);
            return View(teammember);
        }

        //id:TeamMemberID
        //state:
        public ActionResult Verify(Guid id)
        {
            String url = Request.UrlReferrer.ToString();
            TeamMember teammember = db.TeamMember.Find(id);
            teammember.State = "正常";
            db.Entry(teammember).State = EntityState.Modified;
            db.SaveChanges();
            Response.Redirect(url);             
            return View();
        } 
        
        [HttpPost]
        public ActionResult Create()
        {
            String tid = Request.Params.Get("TeamID");
            String Apply = Request.Params.Get("Apply");

            if (tid == null)
            {
                return RedirectToAction("MyTeam", "Team");  
            }
            TeamMember teammember = new TeamMember();
            teammember.JoinDeclaration = Apply;
            User usr = Utils.CurrentUser(this);
            if (ModelState.IsValid && usr != null)
            { 
                teammember.TeamMemberID = Guid.NewGuid();
                teammember.State = "申请加入";
                teammember.TeamID = Guid.Parse(tid);
                Guid userId = Utils.CurrentUser(this).Id;
                var data = db.Volunteer.Where(v => v.User.Id == userId).ToList();
                if (data.Count() == 1)
                {
                    Team team = db.Team.Find(Guid.Parse(tid));
                    String wName = team.WorkStation.WorkStationName;
                    String tName = team.TeamName;

                    teammember.VolunteerID = data[0].VolunteerID;

                    teammember.JoinDate = DateTime.Now;
                    db.TeamMember.Add(teammember);
                    db.SaveChanges();

                    String[] date = DateTime.Now.ToShortDateString().Split('/');
                    String year = date[0];
                    String month = date[1];
                    String day = date[2];
                    String time = year + "年" + month + "月" + day + "日";

                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "加入小组审核！";
                    l.Content = "志愿者" + Utils.CurrentUser(this).RealName + "于" + time + "申请加入小组:" + wName + tName + ",请及时处理！";
                    l.ReceiverID = team.TeamLeader.Volunteer.UserID;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                  
                }
                return RedirectToAction("MyTeam", "Team");  
            }
            return View();
        }
        
        //
        // GET: /TeamMember/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            TeamMember teammember = db.TeamMember.Find(id);
            return View(teammember);
        }

        //
        // POST: /TeamMember/Edit/5

        [HttpPost]
        public ActionResult Edit(TeamMember teammember)
        {
            if (ModelState.IsValid)
            {
                String type = Request.Params.Get("type");
                TeamMember temp = db.TeamMember.Find(teammember.TeamMemberID);
                Team team = db.Team.Find(temp.TeamID);
                String wName = team.WorkStation.WorkStationName;
                String tName = team.TeamName;

                String[] date = DateTime.Now.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                if (type == "join")
                {
                    temp.State = "正常";
                    db.Entry(temp).State = EntityState.Modified;

                    //加入小组之后，志愿者的isHaveTeam变为true
                    Volunteer volunteer = db.Volunteer.Find(temp.VolunteerID);
                    volunteer.FirtPassword = "x";
                    volunteer.ConfirmPassword = "x";
                    volunteer.isHaveTeam = true;
                    volunteer.TeamID = team.TeamID;
                    db.Entry(volunteer).State = EntityState.Modified;

                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "通过小组审核！";
                    l.Content = "您于" + time + "申请加入小组:" + wName + tName + "审核通过！";
                    l.ReceiverID = temp.Volunteer.UserID;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }
                //拒绝加入
                else if (type == "refuse")
                {
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "未通过小组审核！";
                    l.Content = "您于" + time + "申请加入小组:" + wName + tName + "审核未通过！";
                    l.ReceiverID = temp.Volunteer.UserID;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();

                    temp.Volunteer = null;
                    db.TeamMember.Remove(temp);
                }
                else if (type == "fire" || type == "esc")
                {
                    if (type == "fire")
                    {
                        //发送站内信
                        Letter l = new Letter();
                        l.LetterID = Guid.NewGuid();
                        l.IsRead = "未读";
                        l.Title = "被小组开除！";
                        l.Content = "您于" + time + "从:" + wName + tName + "中删除！";
                        l.ReceiverID = temp.Volunteer.UserID;
                        l.SenderID = Utils.CurrentUser(this).Id;
                        l.SenderDate = DateTime.Now;
                        db.Letter.Add(l);
                        db.SaveChanges();
                    } 

                    temp.State = "脱离";
                    temp.Volunteer = null;
                    db.TeamMember.Remove(temp);

                    Volunteer volunteer = db.Volunteer.Find(temp.VolunteerID);
                    volunteer.FirtPassword = "x";
                    volunteer.ConfirmPassword = "x";
                    volunteer.isHaveTeam = false;
                    db.Entry(volunteer).State = EntityState.Modified;
                }
                else if (type == "quitApply")
                {
                    temp.State = "申请脱离";
                    db.Entry(temp).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("MyTeam", "Team");
            }
            return View(teammember);
        }

        //
        // GET: /TeamMember/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            TeamMember teammember = db.TeamMember.Find(id);
            return View(teammember);
        }

        //
        // POST: /TeamMember/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            TeamMember teammember = db.TeamMember.Find(id);
            db.TeamMember.Remove(teammember);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //组员申请脱离界面,id为申请脱离的TeamMemberID
        public ActionResult ApplyingExit(Guid id)
        {
            TeamMember teamMember = db.TeamMember.Find(id);
            if (teamMember != null)
            {
                ViewBag.ApplyTime = DateTime.Now;
                return View(teamMember);
            }

            return View();
        }

        [HttpPost]
        public ActionResult ApplyingExit(TeamMember teamMember)
        {
            String temp = Request.Form.Get("Reason");
            String apply = Request.Form.Get("Apply");
            /*if (teamMember != null)
            {
                return RedirectToAction("Edit", "TeamMember", new { id = teamMember.TeamMemberID, type = "esc" });
            }*/
            TeamMember tm = db.TeamMember.Find(Guid.Parse(Request.Params.Get("TeamMemberID")));

            Team team = db.Team.Find(tm.TeamID);
            String wName = team.WorkStation.WorkStationName;
            String tName = team.TeamName;

            if (team.TeamLeaderID != null)
            {
                String[] date = DateTime.Now.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                //发送站内信
                Letter l = new Letter();
                l.LetterID = Guid.NewGuid();
                l.IsRead = "未读";
                l.Title = "成员脱离小组！";
                if (apply != null)
                {
                    l.Content = "志愿者" + tm.Volunteer.User.RealName + "于" + time + "脱离小组:" + wName + tName + ",原因为:" + temp + ",备注:" + apply;
                }
                else
                {
                    l.Content = "志愿者" + tm.Volunteer.User.RealName + "于" + time + "脱离小组:" + wName + tName + ",原因为:" + temp;
                }
                l.ReceiverID = team.TeamLeader.Volunteer.UserID;
                l.SenderID = Utils.CurrentUser(this).Id;
                l.SenderDate = DateTime.Now;
                db.Letter.Add(l);
                db.SaveChanges();
            }

            tm.State = "脱离";
            tm.Volunteer = null;
            db.TeamMember.Remove(tm);

            Volunteer volunteer = db.Volunteer.Find(tm.VolunteerID);
            volunteer.FirtPassword = "x";
            volunteer.ConfirmPassword = "x";
            volunteer.isHaveTeam = false;
            db.Entry(volunteer).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("MyTeam", "Team", null);
        }
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}