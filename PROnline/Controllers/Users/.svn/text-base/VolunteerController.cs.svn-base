using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Users;
using PROnline.Models.Teams;
using PROnline.Models.Letters;
using PROnline.Models;
using PROnline.Models.WorkStations;
using PROnline.Src;

namespace PROnline.Controllers.Users
{ 
    public class VolunteerController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Volunteer/

        public ViewResult Index()
        {
            var list = Utils.PageIt(this, 10, db.Volunteer.OrderBy(r => r.User.UserTypeId).ThenBy(r => r.User.RealName)).ToList();
            return View(list);
        }

        //
        // GET: /Volunteer/Details/5

        public ViewResult Details(Guid id)
        {
            Volunteer volunteer = db.Volunteer.Find(id);
            String serverStartTime = volunteer.Time;
            String serverEndTime = Convert.ToString(int.Parse(serverStartTime) + 2);
            ViewBag.serverTime = serverStartTime + "点至" + serverEndTime + "点";

            return View(volunteer);
        }

        public ActionResult Select(String selectType)
        {
            string selectTypex = selectType == null ? "radio" : selectType;
            ViewBag.selectType = selectTypex;
            return View();
        }

        public ActionResult Query(String selectType, String Search)
        {
            string selectTypex = selectType == null ? "radio" : selectType;
            ViewBag.selectType = selectTypex;
            if (Search == "")
            {
                return View(db.Volunteer.ToList());
            }
            else
            {
                return View(db.Volunteer.Where(s => s.User.RealName.Contains(Search)).ToList());
            }
        }

        //
        // GET: /Volunteer/Create

        //指定辅导类型,生成不同的View
        public ActionResult Create(String TrainingType)
        {
            return View();
        } 

        //
        // POST: /Volunteer/Create

        [HttpPost]
        public ActionResult Create(Volunteer volunteer)
        {
            String uName = Request.Params.Get("User.UserName");
            if (uName != null)
            {
                var c = db.Users.Where(r => r.RealName == uName).ToList();
                if (c.Count != 0)
                {
                    ModelState.AddModelError("UserNameError", "用户名已经存在");
                    return View();
                }
            }
            if (Request.Params.Get("FirtPassword").Length < 6)
            {
                ModelState.AddModelError("PWTooShort", "密码不足6位，长度过短！");
                return View();
            }
            if (Request.Params.Get("FirtPassword").Length > 10)
            {
                ModelState.AddModelError("PWTooLong", "密码超过10位，长度过长！");
                return View();
            }
            if (Request.Params.Get("YYYY") == null || Request.Params.Get("MM") == null || Request.Params.Get("DD") == null || Request.Params.Get("YYYY").Length == 0 || Request.Params.Get("MM").Length == 0 || Request.Params.Get("DD").Length == 0)
            {
                ModelState.AddModelError("BirthdayError", "请选择生日日期！");
                return View();
            }
            if (ModelState.IsValid)
            {
                volunteer.User.Id = Guid.NewGuid();
                volunteer.User.Password = Utils.GetMd5(volunteer.FirtPassword);
                volunteer.User.UserTypeId = UserType.VOLUNTEER;
                volunteer.User.RoleId = Role.VOLUNTEER;
                volunteer.User.CreateDate = DateTime.Now;
                volunteer.User.isDeleted = false;


                int year = int.Parse(Request.Params.Get("YYYY"));
                int month = int.Parse(Request.Params.Get("MM"));
                int day = int.Parse(Request.Params.Get("DD"));

                DateTime birthday = new DateTime(year, month, day);
                volunteer.Birthday = birthday;

                String motive = Request.Params.Get("Motive");
                volunteer.Motive = motive;

                String motto = Request.Params.Get("Motto");
                volunteer.Motto = motto;

                volunteer.VolunteerID = Guid.NewGuid();
                db.Volunteer.Add(volunteer);
                db.SaveChanges();

                String[] date = DateTime.Now.ToShortDateString().Split('/');
                String yearl = date[0];
                String monthl = date[1];
                String dayl = date[2];
                String time = year + "年" + month + "月" + day + "日";

                var ulist = db.Users.Where(r => r.RoleId == Role.MANAGER).ToList();
                foreach (User u in ulist)
                {
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "志愿者注册审核！";
                    l.Content = "您收到了新的注册信息审核申请，请尽快审核。";
                    l.ReceiverID = u.Id;
                    l.SenderID = volunteer.User.Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }
                //注册成功
                return RedirectToAction("Index", "Home");
            }

            return View(volunteer);
        }


        
        //
        // GET: /Volunteer/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Volunteer volunteer = db.Volunteer.Find(id);
            ViewBag.motive = volunteer.Motive;
            return View(volunteer);
        }

        //
        // POST: /Volunteer/Edit/5

        [HttpPost]
        public ActionResult Edit(Volunteer volunteer)
        {
            String id = Request.Form.Get("User.Id");
            Guid userId = Guid.Parse(id);
            User user = db.Users.Find(userId);
            user.RealName = Request.Form.Get("User.RealName");
            user.ModifyDate = DateTime.Now;
            db.Entry(user).State = EntityState.Modified;

            Guid volunteerId = Guid.Parse(Request.Form.Get("VolunteerID"));
            Volunteer temp = db.Volunteer.Find(volunteerId);
            if (temp != null)
            {
                if (volunteer.Birthday != null)
                {
                    temp.Birthday = volunteer.Birthday;
                }
                if (volunteer.Sex != null)
                {
                    temp.Sex = volunteer.Sex;
                }
                if (volunteer.Nationality != null)
                {
                    temp.Nationality = volunteer.Nationality;
                }
                if (volunteer.PoliticalExperience != null)
                {
                    temp.PoliticalExperience = volunteer.PoliticalExperience;
                }
                if (volunteer.DayOfWeek != null)
                {
                    temp.DayOfWeek = volunteer.DayOfWeek;
                }
                if (volunteer.Time != null)
                {
                    temp.Time = volunteer.Time;
                }
                if (volunteer.Telepone != null)
                {
                    temp.Telepone = volunteer.Telepone;
                }
                if (volunteer.MobileTelephone != null)
                {
                    temp.MobileTelephone = volunteer.MobileTelephone;
                }
                if (volunteer.EMail != null)
                {
                    temp.EMail = volunteer.EMail;
                }
                if (volunteer.QQ != null)
                {
                    temp.QQ = volunteer.QQ;
                }
                if (volunteer.TrainingType != null)
                {
                    temp.TrainingType = volunteer.TrainingType;
                }
                if ((bool?)volunteer.IsStudent != null)
                {
                    temp.IsStudent = volunteer.IsStudent;
                }
                if (volunteer.University != null)
                {
                    temp.University = volunteer.University;
                }
                if (volunteer.Degree != null)
                {
                    temp.Degree = volunteer.Degree;
                }
                if (volunteer.Major != null)
                {
                    temp.Major = volunteer.Major;
                }
                if (volunteer.Grade != null)
                {
                    temp.Grade = volunteer.Grade;
                }
                if (volunteer.Career != null)
                {
                    temp.Career = volunteer.Career;
                }
                if (volunteer.HomeTown != null)
                {
                    temp.HomeTown = volunteer.HomeTown;
                }
                if ((bool?)volunteer.IsExperenced != null)
                {
                    temp.IsExperenced = volunteer.IsExperenced;
                }
                if (volunteer.Experence != null)
                {
                    temp.Experence = volunteer.Experence;
                }
                if (volunteer.Motto != null)
                {
                    temp.Motto = volunteer.Motto;
                }
                if (volunteer.Motive != null)
                {
                    temp.Motive = volunteer.Motive;
                }
                if (volunteer.GoodAtField != null)
                {
                    temp.GoodAtField = volunteer.GoodAtField;
                }
                temp.FirtPassword = "x";
                temp.ConfirmPassword = "x";

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

                if (Utils.CurrentUser(this).UserTypeId == UserType.ADMIN ||
                    Utils.CurrentUser(this).UserTypeId == UserType.MANAGER)
                {
                    return RedirectToAction("TeamLeaderList");
                }
                else if (Utils.CurrentUser(this).UserTypeId == UserType.VOLUNTEER || Utils.CurrentUser(this).UserTypeId == UserType.TEAM_LEADER)
                {
                    return RedirectToAction("Details", "Users");
                }
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return HttpNotFound();
            }
        }

        //
        // GET: /Volunteer/Delete/5

        public ActionResult QueryX(String selectType)
        {
            String type = Request.Params.Get("type");
            String mytype = Request.Params.Get("myvol");
            String Qname = "";
            String time = Request.Params.Get("StartDate");


            List<WorkStation> stations = db.WorkStation.Where(r => r.isDeleted == false).ToList();
            List<Team> teams = db.Team.Where(r => r.isDeleted == false).ToList();
            List<TeamMember> members = db.TeamMember.Include(r => r.Volunteer).ToList();
            List<TeamMember> memberslist = new List<TeamMember>();

            if (type == "selPairMember")
            {
                String ss = (String)Session["startdate"];
                String hh = (String)Session["hour"];
                String chdayweek = null;
                String dayweek;
                String hour;
                if (ss != null)
                {
                    dayweek = DateTime.Parse(ss).DayOfWeek.ToString();
                    switch (dayweek)
                    {
                        case "Monday": chdayweek = "星期一"; break;
                        case "Tuesday": chdayweek = "星期二"; break;
                        case "Wednesday": chdayweek = "星期三"; break;
                        case "Thursday": chdayweek = "星期四"; break;
                        case "Friday": chdayweek = "星期五"; break;
                        case "Saturday": chdayweek = "星期六"; break;
                        case "Sunday": chdayweek = "星期日"; break;
                    }

                }
                else
                {
                    chdayweek = "星期一";
                }

                if (hh != null)
                {
                    hour = hh;
                }
                else
                {
                    hour = "9";
                }

                //选出在该段时间内有空闲的志愿者且没有安排预约的
                /*
                var volunteerlist = from volunteers in db.Volunteer
                                    from pairs in db.Pairs
                                    where volunteers.DayOfWeek == chdayweek && volunteers.Time == hour && volunteers.VolunteerID != pairs.VolunteerID
                                    select volunteers;
                */

                var volunteerlist = from volunteers in db.Volunteer
                                    where volunteers.DayOfWeek == chdayweek && volunteers.Time == hour && !(from pairs in db.Pairs where pairs.State=="正常" select pairs.VolunteerID).Contains(volunteers.VolunteerID)
                                    select volunteers;

                if (volunteerlist.Distinct().ToList().Count > 0)
                {
                    foreach (var volunteer in volunteerlist.Distinct())
                    {
                        Guid id = volunteer.VolunteerID;
                        for (int m = 0; m < members.Count; m++)
                        {
                            if (members[m].VolunteerID.Equals(id))
                            {
                                memberslist.Add(members[m]);
                            }
                        }
                    }
                }
            }
            else if (type == "selTrainingMember")
            {
                DateTime startDate;
                if (time != "")
                {
                    startDate = Convert.ToDateTime(time);
                }
                else
                {
                    startDate = DateTime.Now;
                }

                var volunteerlist = from v in db.Volunteer
                           where !(from tv in db.TrainingVolunteer
                                   from t in db.Training
                                   where tv.TrainingID == t.TrainingID && t.State == "已通过"
                                   && tv.Training.StartDate.CompareTo(startDate) == 0
                                   select tv.VolunteerID).Contains(v.VolunteerID)
                                   && v.User.isVerify == true
                           select v;
                if (volunteerlist.Distinct().ToList().Count > 0)
                {
                    foreach (var volunteer in volunteerlist.Distinct())
                    {
                        Guid id = volunteer.VolunteerID;
                        for (int m = 0; m < members.Count; m++)
                        {
                            if (members[m].VolunteerID.Equals(id))
                            {
                                memberslist.Add(members[m]);
                            }
                        }
                    }
                }
            }
            else if (type == "selActivityMember")
            {
                var volunteerlist = db.Volunteer.Where(s => s.User.isDeleted == false).Where(s => s.User.UserTypeId == "volunteer")
                              .Where(v => v.User.isVerify == true);
                if (volunteerlist.Distinct().ToList().Count > 0)
                {
                    foreach (var volunteer in volunteerlist.Distinct())
                    {
                        Guid id = volunteer.VolunteerID;
                        for (int m = 0; m < members.Count; m++)
                        {
                            if (members[m].VolunteerID.Equals(id))
                            {
                                memberslist.Add(members[m]);
                            }
                        }
                    }
                }
            }

            stations = MemberTools.CombineMember(stations, teams, memberslist);
            ViewBag.selectType = selectType;
            return View(stations);

        }
 
        public ActionResult Delete(Guid id)
        {
            Volunteer volunteer = db.Volunteer.Find(id);
            return View(volunteer);
        }

        //
        // POST: /Volunteer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User currentUser = Utils.CurrentUser(this);

            Volunteer volunteer = db.Volunteer.Find(id);

            //有小组的志愿者小组长
            if (volunteer.User.UserTypeId == UserType.TEAM_LEADER && volunteer.isHaveTeam)
            {
                var teamleader = db.TeamLeader.Where(t => t.VolunteerID == volunteer.VolunteerID).First();
                var team = db.Team.Where(t => t.TeamLeaderID == teamleader.TeamLeaderID).First();

                team.TeamLeaderID = null;
                team.ModifierID = currentUser.Id;
                team.ModifyDate = DateTime.Now;
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();

                db.TeamLeader.Remove(teamleader);
                db.SaveChanges();
            }
//             User user = db.Users.Find(volunteer.UserID);
// 
//             volunteer.UserID = Guid.Empty;
//             volunteer.User = null;
//             db.Volunteer.Remove(volunteer);
//             db.SaveChanges();
// 
//             db.Users.Remove(user);
            volunteer.User.isDeleted = true;
            db.SaveChanges();

            return RedirectToAction("TeamLeaderList");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //用户审核链接过来的显示志愿者详细信息的页面
        public ActionResult VolunteerInfo(Guid id)
        {
            Volunteer volunteer = db.Volunteer.Find(id);
            String serverStartTime = volunteer.Time;
            String serverEndTime = Convert.ToString(int.Parse(serverStartTime) + 2);
            ViewBag.serverTime = serverStartTime + "点至" + serverEndTime + "点";

            return View(volunteer);
        }

        //添加志愿者小组长，用的和志愿者注册一样的页面，但是角色不同
        public ActionResult CreateTeamLeader(String TrainingType)
        {
            GoodAtFieldController good = new GoodAtFieldController();
            ViewBag.field = good.All();
            ViewBag.TrainingType = TrainingType;
            return View();
        }

        [HttpPost]
        public ActionResult CreateTeamLeader(Volunteer volunteer)
        {
            String uName = Request.Params.Get("User.UserName");
            if (uName != null)
            {
                var c = db.Users.Where(r => r.RealName == uName).ToList();
                if (c.Count != 0)
                {
                    ModelState.AddModelError("UserNameError", "用户名已经存在");
                    return View();
                }
            }
            if (Request.Params.Get("FirtPassword").Length < 6)
            {
                ModelState.AddModelError("PWTooShort", "密码不足6位，长度过短！");
                return View();
            }
            if (Request.Params.Get("FirtPassword").Length > 10)
            {
                ModelState.AddModelError("PWTooLong", "密码超过10位，长度过长！");
                return View();
            }
            if (Request.Params.Get("YYYY") == null || Request.Params.Get("MM") == null || Request.Params.Get("DD") == null || Request.Params.Get("YYYY").Length == 0 || Request.Params.Get("MM").Length == 0 || Request.Params.Get("DD").Length == 0)
            {
                ModelState.AddModelError("BirthdayError", "请选择生日日期！");
                return View();
            }
            if (ModelState.IsValid)
            {
                volunteer.User.Id = Guid.NewGuid();
                volunteer.User.Password = Utils.GetMd5(volunteer.FirtPassword);
                volunteer.User.UserTypeId = UserType.TEAM_LEADER;
                volunteer.User.RoleId = Role.TEAM_LEADER;
                volunteer.User.CreateDate = DateTime.Now;
                volunteer.User.isVerify = true;
                volunteer.User.isDeleted = false;


                int year = int.Parse(Request.Params.Get("YYYY"));
                int month = int.Parse(Request.Params.Get("MM"));
                int day = int.Parse(Request.Params.Get("DD"));

                DateTime birthday = new DateTime(year, month, day);
                volunteer.Birthday = birthday;

                volunteer.VolunteerID = Guid.NewGuid();
                db.Volunteer.Add(volunteer);
                db.SaveChanges();
                //注册成功
                return RedirectToAction("TeamLeaderList", "Volunteer");
            }

            return View(volunteer);
        }

        //列出全部志愿者小组长
        public ActionResult TeamLeaderList()
        {
            var result = Utils.PageIt(this,db.Volunteer.Where(r => r.User.isDeleted == false).Where(r => r.User.UserTypeId == UserType.TEAM_LEADER).OrderBy(r => r.User.CreateDate)).ToList();
            var list = from v in db.Volunteer
                       from tl in db.TeamLeader
                       from t in db.Team
                       where v.User.isDeleted == false && v.User.UserTypeId == UserType.TEAM_LEADER && v.VolunteerID == tl.VolunteerID && tl.TeamID == t.TeamID
                       orderby v.User.CreateDate
                       select new {v,t};

            Dictionary<Guid, String> dic = new Dictionary<Guid, String>();
            foreach(var item in list.ToList())
            {
                dic.Add(item.v.VolunteerID, item.t.TeamName);
            }

            ViewBag.dic = dic;
            return View(result);
        }
    }
}