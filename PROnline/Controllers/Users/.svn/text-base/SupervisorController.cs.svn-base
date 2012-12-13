using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Users;
using PROnline.Models.Letters;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.Users
{ 
    public class SupervisorController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Supervisor/

        public ViewResult Index()
        {
            var result = Utils.PageIt(this, 10, db.Supervisor.OrderBy(r => r.User.RealName)).ToList();
            return View(result);
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
                return View(db.Supervisor.ToList());
            }
            else
            {
                return View(db.Supervisor.Where(s => s.User.RealName.Contains(Search)).ToList());
            }
        }

        //
        // GET: /Supervisor/Details/5

        public ViewResult Details(Guid id)
        {
            Supervisor supervisor = db.Supervisor.Find(id);
            return View(supervisor);
        }

        //
        // GET: /Supervisor/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Supervisor/Create

        [HttpPost]
        public ActionResult Create(Supervisor supervisor)
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
                supervisor.User.Id = Guid.NewGuid();
                supervisor.User.Password = Utils.GetMd5(supervisor.FirtPassword);
                supervisor.User.UserTypeId = UserType.SUPERVISOR;
                supervisor.User.RoleId = Role.SUPERVISOR;
                supervisor.User.CreateDate = DateTime.Now;

                int year = int.Parse(Request.Params.Get("YYYY"));
                int month = int.Parse(Request.Params.Get("MM"));
                int day = int.Parse(Request.Params.Get("DD"));

                DateTime birthday = new DateTime(year, month, day);
                supervisor.SupervisorID = Guid.NewGuid();
                supervisor.Birthday = birthday;
                db.Supervisor.Add(supervisor);
                db.SaveChanges();

                var u = db.Users.Where(r => r.RoleId == Role.MANAGER).ToList();
                foreach(User user in u)
                {
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "督导注册审核";
                    l.Content = "您收到了新的注册信息审核申请，请尽快审核。";
                    l.ReceiverID = user.Id;
                    l.SenderID = supervisor.UserID;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }

                //注册成功
                return RedirectToAction("Index", "Home");
            }

            return View(supervisor);
        }
        
        //
        // GET: /Supervisor/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Supervisor supervisor = db.Supervisor.Find(id);
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName", supervisor.UserID);
            return View(supervisor);
        }

        //
        // POST: /Supervisor/Edit/5

        [HttpPost]
        public ActionResult Edit(Supervisor supervisor)
        {
            String id = Request.Form.Get("User.Id");
            Guid userId = Guid.Parse(id);
            User user = db.Users.Find(userId);
            user.RealName = Request.Form.Get("User.RealName");
            user.ModifyDate = DateTime.Now;
            db.Entry(user).State = EntityState.Modified;
            Guid supervisorId = Guid.Parse(Request.Form.Get("SupervisorID"));
            Supervisor temp = db.Supervisor.Find(supervisorId);
            if (temp != null)
            {
                if (supervisor.Birthday != null)
                {
                    temp.Birthday = supervisor.Birthday;
                }
                if (supervisor.Sex != null)
                {
                    temp.Sex = supervisor.Sex;
                }
                if (supervisor.Nationality != null)
                {
                    temp.Nationality = supervisor.Nationality;
                }
                if (supervisor.PoliticalExperience != null)
                {
                    temp.PoliticalExperience = supervisor.PoliticalExperience;
                }
                if (supervisor.Telepone != null)
                {
                    temp.Telepone = supervisor.Telepone;
                }
                if (supervisor.MobileTelephone != null)
                {
                    temp.MobileTelephone = supervisor.MobileTelephone;
                }
                if (supervisor.EMail != null)
                {
                    temp.EMail = supervisor.EMail;
                }
                if (supervisor.QQ != null)
                {
                    temp.QQ = supervisor.QQ;
                }
                if (supervisor.University != null)
                {
                    temp.University = supervisor.University;
                }
                if (supervisor.Degree != null)
                {
                    temp.Degree = supervisor.Degree;
                }
                if (supervisor.Major != null)
                {
                    temp.Major = supervisor.Major;
                }
                if (supervisor.Career != null)
                {
                    temp.Career = supervisor.Career;
                }
                if (supervisor.HomeTown != null)
                {
                    temp.HomeTown = supervisor.HomeTown;
                }
                if (supervisor.PDegree != null)
                {
                    temp.PDegree = supervisor.PDegree;
                }
                if (supervisor.Degree != null)
                {
                    temp.Degree = supervisor.Degree;
                }

                temp.FirtPassword = "x";
                temp.ConfirmPassword = "x";

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

                if (Utils.CurrentUser(this).UserTypeId == UserType.ADMIN ||
                    Utils.CurrentUser(this).UserTypeId == UserType.MANAGER)
                {
                    return RedirectToAction("Index");
                }
                else if (Utils.CurrentUser(this).UserTypeId == UserType.SUPERVISOR)
                {
                    return RedirectToAction("Details", "Users");
                }
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        //
        // GET: /Supervisor/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Supervisor supervisor = db.Supervisor.Find(id);
            return View(supervisor);
        }

        //
        // POST: /Supervisor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Supervisor supervisor = db.Supervisor.Find(id);
            supervisor.User.isDeleted = true;
            db.Entry(supervisor).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //从用户审核链接过来的显示督导专家详细信息的界面
        public ActionResult SupervisorInfo(Guid id)
        {
            Supervisor supervisor = db.Supervisor.Find(id);
            return View(supervisor);
        }
    }
}