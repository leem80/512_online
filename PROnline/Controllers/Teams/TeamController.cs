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
using PROnline.Models.WorkStations;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.Teams
{ 
    public class TeamController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Team/

        public JsonResult NameCheck()
        {
            Guid wID = Guid.Parse(Request.Params["WorkStationID"]);
            String tName = Request.Params["TeamName"];
            String name = Request.Params["TeamID"];
            int count = db.Team.Where(r => r.WorkStation.WorkStationID == wID)
                .Where(r => r.TeamName == tName).ToList().Count();

            //小组创建
            if (Request.Params["TeamID"] == null || Request.Params["TeamID"] == "undefined")
            {
                return Json(count == 0, JsonRequestBehavior.AllowGet);
            } 
            //编辑小组
            else
            {
                Guid tID = Guid.Parse(Request.Params["TeamID"]);
                if (count != 0)
                {
                    if (count == 1)
                    {
                        Guid temp = db.Team.Where(r => r.TeamName == tName).Single().TeamID;
                        if (temp.CompareTo(tID) != 0)
                        {
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (count > 1)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public ViewResult Index()
        {
            ViewBag.list = Utils.PageIt(this, db.Team.Where(t => t.isDeleted == false)
                .OrderBy(t => t.Type)).ToList();
             return View();
        }

        public ViewResult MyTeam()
        {
            User user = Utils.CurrentUser(this);
            Guid  id  = user.Id;
            ViewBag.sID = id;
            //当前志愿者所属小组
            List<TeamLeader> tl = db.TeamLeader.Where(t => t.Volunteer.User.Id == id)
                .Where(t => t.State == "正常").ToList();
            List<TeamMember> tm = db.TeamMember.Where(t => t.Volunteer.User.Id == id)
                .Where(t => t.State == "正常").ToList();

            //组长(一个小组长只管一个小组)
            if (tl != null && tl.Count > 0)
            {
                Volunteer v = db.Volunteer.Find(tl[0].VolunteerID);
                if (v.isHaveTeam == false)
                {
                    ViewBag.type = "teamleader";
                    ViewBag.isHaveTeam = false;
                } 
                else
                {
                    Guid tlId = tl[0].TeamLeaderID;
                    ViewBag.list = db.Team.Where(t => t.TeamLeaderID == tlId).ToList();
                    Team team = db.Team.Where(t => t.TeamLeaderID == tlId).First();

                    ViewBag.wID = team.WorkStationID;
                    ViewBag.teamName = team.TeamName;
                    ViewBag.wName = team.WorkStation.WorkStationName;
                    ViewBag.Id = team.TeamID;
                    ViewBag.teamLeaderId = tlId;
                    ViewBag.type = "teamleader";
                    ViewBag.isHaveteam = true;
                }           
            }
            //组员,已经加入了小组
            else if (tm != null && tm.Count > 0)
            {
                ViewBag.list = tm[0].Team;

                ViewBag.wID = tm[0].Team.WorkStationID;
                ViewBag.teamName = tm[0].Team.TeamName;
                ViewBag.wName = tm[0].Team.WorkStation.WorkStationName;
                ViewBag.teamMemberID = tm[0].TeamMemberID;
                ViewBag.Id = tm[0].TeamID;
                ViewBag.member = tm[0];
                ViewBag.type = "teammember";

                if (tm[0].Team.TeamLeaderID != null)
                {
                    ViewBag.isHaveTL = true;
                } 
                else
                {
                    ViewBag.isHaveTL = false;
                }
            }
            //未加入小组，则显示所有小组
            //若已申请假如小组但仍然在审核，则申请加入区域不可见
            else
            {
               if (user.UserTypeId == UserType.TEAM_LEADER)
               {
                   ViewBag.userType = "tl";
                   ViewBag.type = "showTeam";
               } 
               else
               {
                   ViewBag.userType = "tm";
                   ViewBag.list = Utils.PageIt(this, db.Team.Where(t => t.isDeleted == false).Where(t => t.TeamLeaderID != null)
                       .OrderBy(t => t.Type)).ToList();
                   ViewBag.type = "showTeam";
                   List<TeamMember> temp = db.TeamMember.Where(t => t.Volunteer.User.Id == id)
                       .Where(t => t.State == "申请加入").ToList();
                   if (temp != null && temp.Count != 0)
                   {
                       ViewBag.isShow = false;
                   }
                   else
                   {
                       ViewBag.isShow = true;
                   }
               }
            }
            
            return View();
        }

        //
        // GET: /Team/Details/5

        public ViewResult Details(Guid id)
        {         
            Team team = db.Team.Find(id);
            if (team.TeamLeaderID != null)
            {
                ViewBag.tl = true;
            } 
            else
            {
                ViewBag.tl = false;
            }
            var list = from c in db.TeamMember
                       where c.State == "申请加入" && c.TeamID == id
                       select c;
            ViewBag.apply =  list.ToList();

            var divorce = from c in db.TeamMember
                       where c.State == "申请脱离" && c.TeamID == id
                       select c;
            ViewBag.divorce = divorce.ToList();

            var fire = from c in db.TeamMember
                         where c.State == "申请开除" && c.TeamID == id
                         select c;
            ViewBag.fire = fire.ToList();

            var normal = from c in db.TeamMember
                       where c.State == "正常" && c.TeamID == id
                       select c;
            ViewBag.normal = normal.ToList();

            return View(team);
        }

        //
        // GET: /Team/Create

        public ActionResult Create()
        {
            var result = db.WorkStation.OrderBy(r => r.WorkStationName).ToList();
            ViewBag.list = result;

            //小组必须与工作站相关联，没有工作站则不允许创建小组
            if (result.Count == 0)
            {
                ViewBag.isShow = false;
            } 
            else
            {
                ViewBag.isShow = true;
            }

            return View();
        } 

        //
        // POST: /Team/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                String volunteerID = Request.Form.Get("VolunteerID");
                team.TeamID = Guid.NewGuid();

                if (volunteerID != null)
                {
                    TeamLeader tm = new TeamLeader();
                    tm.TeamLeaderID = Guid.NewGuid();

                    tm.VolunteerID = Guid.Parse(volunteerID);
                    tm.State = "正常";
                    tm.JoinDate = DateTime.Now;
                    tm.TeamID = team.TeamID;
                    db.TeamLeader.Add(tm);

                    team.TeamLeaderID = tm.TeamLeaderID;

                    Guid tempId = new Guid(volunteerID);
                    Volunteer v = db.Volunteer.Find(tempId);
                    v.isHaveTeam = true;
                    v.FirtPassword = "x";
                    v.ConfirmPassword = "x";
                    v.User.ModifyDate = DateTime.Now;
                    db.Entry(v).State = EntityState.Modified;
                }
                else
                {
                    team.TeamLeaderID = null;
                }

                WorkStation w = db.WorkStation.Find(team.WorkStationID);
                team.WorkStation = w;
                team.CreateDate = DateTime.Now;
                team.CreatorID = Utils.CurrentUser(this).Id;

                db.Team.Add(team);
                db.SaveChanges();

                if (volunteerID != null)
                {
                    Volunteer v = db.Volunteer.Find(Guid.Parse(volunteerID));
                    String[] date = DateTime.Now.ToShortDateString().Split('/');
                    String year = date[0];
                    String month = date[1];
                    String day = date[2];
                    String time = year + "年" + month + "月" + day + "日";

                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "指派为小组长！";
                    l.Content = "您于" + time + "被指派为服务小组:" + team.WorkStation.WorkStationName + team.TeamName + "的小组长！";
                    l.ReceiverID = v.UserID;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(team);
        }
        
        //
        // GET: /Team/Edit/5
 
        public ActionResult Edit(Guid id, String type)
        {
            Team team = db.Team.Find(id);
            ViewBag.type = Request.Params.Get("type");
            
            if (team.TeamLeaderID != null)
            {
                TeamLeader teamleader = db.TeamLeader.Find(team.TeamLeaderID);
                Volunteer volunteer = db.Volunteer.Find(teamleader.VolunteerID);
                ViewBag.tempGuid = volunteer.VolunteerID.ToString();
            }
            else
            {
                ViewBag.tempGuid = null;
            } 
            
            var result = db.WorkStation.OrderBy(r => r.WorkStationName).ToList();
            ViewBag.list = result;
            
            return View(team);
        }

        //
        // POST: /Team/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Team team)
        {
            if (Request.Form.Get("edit_type") == "change")
            {
                String volunteerID = Request.Form.Get("VolunteerID");
                if (volunteerID == null)
                {
                    ModelState.AddModelError("Error", "请选择组长!");
                    ViewBag.type = "change";
                }
            }
            if (ModelState.IsValid)
            {
                Team temp = db.Team.Find(team.TeamID);
                if (Request.Form.Get("edit_type") == "info")
                {
                    temp.Type = team.Type;
                    temp.TeamName = team.TeamName;
                    temp.Comment = team.Comment;
                    temp.Introduction = team.Introduction;
                    String tempID = Request.Params.Get("WorkStationID");
                    temp.WorkStationID = Guid.Parse(tempID);
                    temp.ModifyDate = DateTime.Now;
                    temp.ModifierID = Utils.CurrentUser(this).Id;
                }
                else if (Request.Form.Get("edit_type") == "change")
                {
                    String volunteerID = Request.Form.Get("VolunteerID");
                    //新的小组长
                    Volunteer new_v = db.Volunteer.Find(Guid.Parse(volunteerID));
                    new_v.isHaveTeam = true;
                    new_v.FirtPassword = "x";
                    new_v.ConfirmPassword = "x";
                    new_v.User.ModifyDate = DateTime.Now;
                    db.Entry(new_v).State = EntityState.Modified;

                    if (temp.TeamLeaderID != null)
                    {
                        TeamLeader old_tl = db.TeamLeader.Find(temp.TeamLeaderID);
                        temp.TeamLeaderID = null;
                        db.TeamLeader.Remove(old_tl);
                        db.SaveChanges();
                    }
                    
                    TeamLeader new_tl = new TeamLeader();
                    new_tl.TeamLeaderID = Guid.NewGuid();
                    new_tl.TeamID = temp.TeamID;
                    new_tl.VolunteerID = new_v.VolunteerID;
                    new_tl.JoinDate = DateTime.Now;
                    new_tl.State = "正常";
                    db.TeamLeader.Add(new_tl);
                    db.SaveChanges();

                    temp.TeamLeaderID = new_tl.TeamLeaderID;
                    temp.ModifierID = Utils.CurrentUser(this).Id;
                    temp.ModifyDate = DateTime.Now;                   
                    temp.TeamLeader = new_tl;

                    db.SaveChanges();

                    //给新的小组长发去站内信
                    String[] date = DateTime.Now.ToShortDateString().Split('/');
                    String year = date[0];
                    String month = date[1];
                    String day = date[2];
                    String time = year + "年" + month + "月" + day + "日";

                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "指派为小组长！";
                    l.Content = "您于" + time + "被指派为服务小组:" + temp.WorkStation.WorkStationName + team.TeamName + "的小组长！";
                    l.ReceiverID = new_v.UserID;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();

                     String old_volunteerID = Request.Form.Get("old_teamleader_volunteerID");
                    //将之前的小组长的isHaveTeam标志设置为false
                    if (old_volunteerID != "")
                    {
                        Volunteer old_v = db.Volunteer.Find(Guid.Parse(old_volunteerID));
                        old_v.isHaveTeam = false;
                        old_v.FirtPassword = "x";
                        old_v.ConfirmPassword = "x";
                        old_v.User.ModifyDate = DateTime.Now;
                        db.Entry(old_v).State = EntityState.Modified;

                        //给旧的小组长发去站内信
                        //发送站内信
                        Letter oldl = new Letter();
                        oldl.LetterID = Guid.NewGuid();
                        oldl.IsRead = "未读";
                        oldl.Title = "被撤销小组长！";
                        oldl.Content = "您于" + time + "被撤销服务小组:" + temp.WorkStation.WorkStationName + team.TeamName + "的小组长一职！";
                        oldl.ReceiverID = old_v.UserID;
                        oldl.SenderID = Utils.CurrentUser(this).Id;
                        oldl.SenderDate = DateTime.Now;
                        db.Letter.Add(oldl);
                        db.SaveChanges();
                    }
          
                    //给所有组员发送更换小组长站内信
                    if (temp.TeamMembers != null)
                    {
                        String new_tlName = new_v.User.RealName;
                        foreach(TeamMember tm in temp.TeamMembers)
                        {
                            //发送站内信
                            Letter vl = new Letter();
                            vl.LetterID = Guid.NewGuid();
                            vl.IsRead = "未读";
                            vl.Title = "更换小组长！";
                            vl.Content = "您所在小组的小组长于" + time + "更换为:" + new_tlName;
                            vl.ReceiverID = tm.Volunteer.UserID;
                            vl.SenderID = Utils.CurrentUser(this).Id;
                            vl.SenderDate = DateTime.Now;
                            db.Letter.Add(vl);
                            db.SaveChanges();
                        }
                    }
                }
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(team);
        }

        //
        // GET: /Team/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Team team = db.Team.Find(id);
            return View(team);
        }

        //
        // POST: /Team/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Team team = db.Team.Find(id);

            String[] date = DateTime.Now.ToShortDateString().Split('/');
            String year = date[0];
            String month = date[1];
            String day = date[2];
            String time = year + "年" + month + "月" + day + "日";

            if (team.TeamLeaderID != null)
            {
                TeamLeader teamleader = db.TeamLeader.Find(team.TeamLeaderID);
                Volunteer volunteer = db.Volunteer.Find(teamleader.VolunteerID);
                volunteer.FirtPassword = "x";
                volunteer.ConfirmPassword = "x";
                volunteer.isHaveTeam = false;
                volunteer.User.ModifyDate = DateTime.Now;
                db.Entry(volunteer).State = EntityState.Modified;

                //发送站内信
                Letter l = new Letter();
                l.LetterID = Guid.NewGuid();
                l.IsRead = "未读";
                l.Title = "小组被删除！";
                l.Content = "您所在的小组于" + time + "被删除，请及时找管理员分配小组!";
                l.ReceiverID = volunteer.UserID;
                l.SenderID = Utils.CurrentUser(this).Id;
                l.SenderDate = DateTime.Now;
                db.Letter.Add(l);
                //db.SaveChanges();

                db.TeamLeader.Remove(teamleader);
            }

            List<TeamMember> teamMemberList = db.TeamMember.Where(t => t.TeamID == team.TeamID)
                .Where(t => t.State == "正常").ToList();
            if (teamMemberList.Count != 0)
            {
                foreach (TeamMember item in teamMemberList)
                {
                    Volunteer volunteer = db.Volunteer.Find(item.VolunteerID);
                    volunteer.FirtPassword = "x";
                    volunteer.ConfirmPassword = "x";
                    volunteer.isHaveTeam = false;
                    volunteer.User.ModifyDate = DateTime.Now;

                    db.Entry(volunteer).State = EntityState.Modified;

                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "小组被删除！";
                    l.Content = "您所在的小组于" + time + "被删除，请及时找管理员分配小组!";
                    l.ReceiverID = volunteer.UserID;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    //db.SaveChanges();

                    item.Volunteer = null;
                    db.TeamMember.Remove(item);
                }
            }

            team.TeamLeaderID = null;
            team.WorkStation = null;
            db.Team.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //组长：组员加入审核
        public ActionResult TeamMemberVerifying(Guid id)
        {
            Team team = db.Team.Find(id);

            ViewBag.wName = team.WorkStation.WorkStationName;
            ViewBag.teamName = team.TeamName;
            ViewBag.teamLeaderId = team.TeamLeaderID;
            var result = db.TeamMember.Where(t => t.TeamID == team.TeamID).Where(t => t.State == "申请加入")
                .OrderBy(t => t.JoinDate).ToList();

            return View(result);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}