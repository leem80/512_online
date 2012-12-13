using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Users;
using PROnline.Models;
using PROnline.Models.Letters;
using PROnline.Src;

namespace PROnline.Controllers.Users
{
    public class DonatorController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Donator/

        public ViewResult Index()
        {
            var result = Utils.PageIt(this, db.Donator.Where(u => u.User.isDeleted == false).OrderBy(u => u.User.UserName)).ToList();
            return View(result);
        }

        //
        // GET: /Donator/Details/5

        public ViewResult Details(Guid id, String type)
        {
            Donator donator = db.Donator.Find(id);

            ViewBag.type = type;
            return View(donator);
        }

        //
        // GET: /Donator/Create

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Query(String selectType, String UserType)
        {
            string type = this.Request.Params.Get("type");
            string Qname = "";
            string time = this.Request.Params.Get("StartDate");
            string selectTypex = selectType == null ? "radio" : selectType;
            ViewBag.selectType = selectTypex;

            if (UserType == "student")
            {
                //选择参加活动的学生
                if (type == "selActivityMember")
                {
                    if (Qname == "")
                    {
                        ViewBag.list = db.Student.Where(s => s.User.isDeleted == false).ToList();
                    }
                    else
                    {
                        ViewBag.list = db.Student.Where(s => s.User.isDeleted == false)
                            .Where(s => s.User.RealName == Qname).ToList();
                    }
                }
                else
                {
                    ViewBag.list = db.Student.Where(s => s.User.isDeleted == false).ToList();
                }
                //选择成为爱心捐助对象的学生
                if (type == "selDonationMember")
                {
                    if (Qname == "")
                    {

                    }
                }
            }

            if (UserType == "supervisor")
            {
                //选择参加培训的督导老师
                if (type == "selTrainingMember")
                {
                    if (Qname == "")
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

                        var list = from s in db.Supervisor
                                   where !(from ts in db.TrainingSupervisor
                                           from t in db.Training
                                           where ts.TrainingID == t.TrainingID && t.State == "已通过"
                                           && ts.Training.StartDate.CompareTo(startDate) == 0
                                           select ts.SupervisorID).Contains(s.SupervisorID)
                                           && s.User.isVerify == true
                                   select s;
                        ViewBag.list = list.ToList();
                    }
                    else
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

                        var list = from s in db.Supervisor
                                   where !(from ts in db.TrainingSupervisor
                                           from t in db.Training
                                           where ts.TrainingID == t.TrainingID && t.State == "已通过"
                                           && ts.Training.StartDate.CompareTo(startDate) == 0
                                           select ts.SupervisorID).Contains(s.SupervisorID)
                                           && s.User.isVerify == true
                                           && s.User.RealName == Qname
                                   select s;
                        ViewBag.list = list.ToList();
                    }
                }
                else if (type == "selPairMember")
                {
                    if (Qname == "")
                    {
                        var list = from s in db.Supervisor
                                   where !(from ts in db.Pairs
                                           select ts.SupervisorID).Contains(s.SupervisorID) && s.User.isVerify == true
                                   select s;
                        ViewBag.list = list.ToList();
                    }
                    else
                    {
                        var list = from s in db.Supervisor
                                   where !(from ts in db.Pairs
                                           select ts.SupervisorID).Contains(s.SupervisorID) && s.User.isVerify == true
                                           && s.User.RealName == Qname
                                   select s;
                        ViewBag.list = list.ToList();
                    }

                }
            }

            if (UserType == "volunteer")
            {
                //选择参加活动的志愿者
                if (type == "selActivityMember")
                {
                    if (Qname == "")
                    {
                        ViewBag.list = db.Volunteer.Where(s => s.User.isDeleted == false).Where(s => s.User.UserTypeId == "volunteer")
                           .Where(v => v.User.isVerify == true).ToList();
                    }
                    else
                    {
                        ViewBag.list = db.Volunteer.Where(s => s.User.isDeleted == false).Where(s => s.User.UserTypeId == "volunteer")
                           .Where(v => v.User.isVerify == true).Where(v => v.User.RealName == Qname).ToList();
                    }
                }
                //选择参加培训的志愿者（包括小组长）
                if (type == "selTrainingMember")
                {
                    if (Qname == "")
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

                        var list = from v in db.Volunteer
                                   where !(from tv in db.TrainingVolunteer
                                           from t in db.Training
                                           where tv.TrainingID == t.TrainingID && t.State == "已通过"
                                           && tv.Training.StartDate.CompareTo(startDate) == 0
                                           select tv.VolunteerID).Contains(v.VolunteerID)
                                           && v.User.isVerify == true
                                   select v;
                        ViewBag.list = list.ToList();
                    }
                    else
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

                        var list = from v in db.Volunteer
                                   where !(from tv in db.TrainingVolunteer
                                           from t in db.Training
                                           where tv.TrainingID == t.TrainingID && t.State == "已通过"
                                           && tv.Training.StartDate.CompareTo(startDate) == 0
                                           select tv.VolunteerID).Contains(v.VolunteerID)
                                           && v.User.isVerify == true
                                           && v.User.RealName == Qname
                                   select v;
                        ViewBag.list = list.ToList();
                    }
                }
                else if (type == "selPairMember")
                {
                    if (Qname == "")
                    {
                        var list = from s in db.Volunteer
                                   where !(from ts in db.Pairs
                                           select ts.SupervisorID).Contains(s.VolunteerID)
                                   && s.User.isVerify == true
                                   select s;
                        ViewBag.list = list.ToList();
                    }
                    else
                    {
                        var list = from s in db.Volunteer
                                   where !(from ts in db.Pairs
                                           select ts.SupervisorID).Contains(s.VolunteerID)
                                            && s.User.RealName == Qname && s.User.isVerify == true
                                   select s;
                        ViewBag.list = list.ToList();
                    }

                }
            }

            if (UserType == "team_leader")
            {
                //选择小组长（新建小组）
                if (type == "selTeamLeader")
                {
                    if (Qname == "")
                    {
                        ViewBag.list = db.Volunteer.Where(s => s.User.isDeleted == false).Where(v => v.User.UserTypeId == "team_leader")
                        .Where(v => v.isHaveTeam == false).Where(s => s.User.isVerify == true).ToList();
                    }
                    else
                    {
                        ViewBag.list = db.Volunteer.Where(s => s.User.isDeleted == false).Where(v => v.User.UserTypeId == "team_leader")
                           .Where(v => v.isHaveTeam == false).Where(v => v.User.RealName == Qname)
                           .ToList();
                    }
                }
                //选择参加活动的小组长
                if (type == "selActivityMember")
                {
                    if (Qname == "")
                    {
                        ViewBag.list = db.Volunteer.Where(s => s.User.isDeleted == false)
                        .Where(s => s.User.UserTypeId == "team_leader").ToList();
                    }
                    else
                    {
                        ViewBag.list = db.Volunteer.Where(s => s.User.isDeleted == false)
                        .Where(s => s.User.UserTypeId == "team_leader").Where(s => s.User.RealName == Qname)
                        .ToList();
                    }
                }
            }
            if (UserType == "donator")
            {
                //选择爱心人士
                if (type == "selDonator")
                {
                    if (Qname == "")
                    {
                        ViewBag.list = db.Donator.Where(s => s.User.isDeleted == false && s.User.isVerify == true).ToList();
                    }
                    else
                    {
                        ViewBag.list = db.Donator.Where(s => s.User.isDeleted == false && s.User.isVerify == true).Where(s => s.User.RealName == Qname).ToList();
                    }
                }
                else
                {
                    ViewBag.list = db.Donator.Where(s => s.User.isDeleted == false && s.User.isVerify == true).ToList();
                }
            }
            return View();
        }
        //
        // POST: /Donator/Create

        [HttpPost]
        public ActionResult Create(Donator donator)
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
            String temp = Request.Form["Connection"];
            if (String.IsNullOrEmpty(temp))
            {
                ModelState.AddModelError("ConnectionError", "请选择联系方式");
                return View();
            }
            if (ModelState.IsValid)
            {
                donator.User.Id = Guid.NewGuid();
                donator.User.Password = Utils.GetMd5(donator.FirtPassword);
                donator.User.UserTypeId = UserType.DONATOR;
                donator.User.RoleId = Role.DONATOR;
                donator.User.CreateDate = DateTime.Now;
                donator.User.isDeleted = false;

                donator.User.LoginNum = 0;
                donator.User.IsPasswordChanged = false;
                donator.User.LastLoginDate = DateTime.Now;

                int year = int.Parse(Request.Params.Get("YYYY"));
                int month = int.Parse(Request.Params.Get("MM"));
                int day = int.Parse(Request.Params.Get("DD"));

                DateTime birthday = new DateTime(year, month, day);
                donator.Birthday = birthday;

                if (temp.IndexOf("其他") != -1)
                {
                    donator.Connection = temp + ":" + Request.Params.Get("ConnectionWay");
                }
                else
                {
                    donator.Connection = temp;
                }
                donator.DonatorID = Guid.NewGuid();
                db.Donator.Add(donator);
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
                    l.Title = "爱心人士注册审核！";
                    l.Content = "您收到了新的注册信息审核申请，请尽快审核。";
                    l.ReceiverID = u.Id;
                    l.SenderID = donator.User.Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }
                //注册成功
                return RedirectToAction("Index", "Home");
            }
            return View(donator);
        }

        //
        // GET: /Donator/Edit/5

        public ActionResult Edit(Guid id)
        {
            Donator donator = db.Donator.Find(id);
            return View(donator);
        }

        //
        // POST: /Donator/Edit/5

        [HttpPost]
        public ActionResult Edit(Donator donator)
        {

            String tempConnection = Request.Form["Connection"];
            if (String.IsNullOrEmpty(tempConnection))
            {
                ModelState.AddModelError("ConnectionError", "请选择联系方式");
                return View();
            }
            Donator temp = db.Donator.Find(donator.DonatorID);
            if (temp!=null)
            {

                temp.FirtPassword = donator.FirtPassword;
                temp.ConfirmPassword = donator.ConfirmPassword;
                if (donator.User.RealName != null)
                {
                    temp.User.RealName = donator.User.RealName;
                }
                if (donator.Sex != null)
                {
                    temp.Sex = donator.Sex;
                }
                if (donator.EMail != null)
                {
                    temp.EMail = donator.EMail;
                }
                if (donator.Telephone != null)
                {
                    temp.Telephone = donator.Telephone;
                }
                if (donator.MobileTelephone != null)
                {
                    temp.MobileTelephone = donator.MobileTelephone;
                }
                if (donator.QQ != null)
                {
                    temp.QQ = donator.QQ;
                }
                if (donator.Address != null)
                {
                    temp.Address = donator.Address;
                }
                if (donator.Birthday != null)
                {

                    temp.Birthday = donator.Birthday;
                }
                if ((bool?)donator.isHaveExperience != null)
                {
                    temp.isHaveExperience = donator.isHaveExperience;
                }
                if (donator.Occupation != null)
                {
                    temp.Occupation = donator.Occupation;
                }

                if (tempConnection.IndexOf("其他") != -1)
                {
                    temp.Connection = tempConnection + ":" + Request.Params.Get("ConnectionWay");
                }
                else
                {
                    temp.Connection = tempConnection;
                }
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Users");
            }
            return View(donator);
        }

        //
        // GET: /Donator/Delete/5

        public ActionResult Delete(Guid id)
        {
            Donator donator = db.Donator.Find(id);
            return View(donator);
        }

        //
        // POST: /Donator/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Donator donator = db.Donator.Find(id);
            User user = db.Users.Find(donator.UserID);
            user.isDeleted = true;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //从用户审核处链接过来的显示爱心人士详细信息的页面
        public ActionResult DonatorInfo(Guid id)
        {
            Donator donator = db.Donator.Find(id);
            return View(donator);
        }
    }
}