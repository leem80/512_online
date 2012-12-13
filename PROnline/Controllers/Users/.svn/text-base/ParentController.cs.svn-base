using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Users;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.Users
{ 
    public class ParentController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Parent/Details/5

        public ViewResult Details(Guid id )
        {
            Parent parent = db.Parent.Find(id);
            return View(parent);
        }

        [HttpGet]
        public ViewResult SchoolNavigation(String SchoolID)
        {
            SchoolController sc = new SchoolController();
            ViewBag.SchoolDropDownList = sc.SchoolDropDownList();

            School school = null;
            if (SchoolID != null)
            {
                school = db.School.Find(Guid.Parse(SchoolID));
                var list = from students in db.Student
                               where students.SchoolID==school.SchoolID
                               select students;
                ViewBag.list = list.ToList();
            }
            return View(school);
        }

        //
        // GET: /Parent/Create

        public ActionResult Create()
        {
            ViewBag.StudentID = Request.Params.Get("StudentID");
            return View();
        } 

        //
        // POST: /Parent/Create

        [HttpPost]
        public ActionResult Create(Parent parent)
        {
            if (Request.Params.Get("YYYY") == "" || Request.Params.Get("MM") == "" || Request.Params.Get("DD") == "" ||
                Request.Params.Get("YYYY") == null || Request.Params.Get("MM") == null || Request.Params.Get("DD") == null)
            {
                ViewBag.StudentID = Request.Params.Get("StudentID");
                ModelState.AddModelError("Birthday", "请选择生日！");
            }
            if (ModelState.IsValid)
            {
                parent.User.Id = Guid.NewGuid();
                parent.User.Password = Utils.GetMd5("123456");
                parent.User.UserTypeId = UserType.PARENT;
                parent.User.RoleId = UserType.PARENT;
                parent.User.CreateDate = DateTime.Now;
                parent.User.isDeleted = false;
                parent.User.RoleId = "parent";
                parent.User.isVerify = true;

                int year = int.Parse(Request.Params.Get("YYYY"));
                int month = int.Parse(Request.Params.Get("MM"));
                int day = int.Parse(Request.Params.Get("DD"));

                DateTime birthday = new DateTime(year, month, day);
                parent.Birthday = birthday;

                parent.ParentID = Guid.NewGuid();
                db.Users.Add(parent.User);
                db.Parent.Add(parent);
                db.SaveChanges();

               
                return RedirectToAction("MyStudent","Student");  
            }

            return View(parent);
        }
        
        //
        // GET: /Parent/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Parent parent = db.Parent.Find(id);
            return View(parent);
        }

        //
        // POST: /Parent/Edit/5

        [HttpPost]
        public ActionResult Edit(Parent parent)
        {
            Parent temp = db.Parent.Find(parent.ParentID);
            String realName = Request.Params.Get("User.RealName");
            temp.User.RealName = realName;
            temp.Birthday = parent.Birthday;
            temp.WorkPlace = parent.WorkPlace;
            temp.Career = parent.Career;
            temp.Telepone = parent.Telepone;
            temp.MobileTelephone = parent.MobileTelephone;
            db.Entry(temp).State = EntityState.Modified;
            db.SaveChanges();
            
            if (Utils.CurrentUser(this).UserTypeId == UserType.PARENT)
            {
                return RedirectToAction("Details", "Users");
            }
            else
            {
                return RedirectToAction("ParentInfo", new { id = temp.ParentID });
            }
        }

        //
        // GET: /Student/Delete/5

        public ActionResult Delete(Guid id)
        {
            Parent parent = db.Parent.Find(id);
            return View(parent);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Parent parent = db.Parent.Find(id);
            parent.User.isDeleted = true;
            db.Parent.Remove(parent);

            //db.Entry(parent).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MyStudent", "Student");
        }

        //家长个人空间->查看学生个人信息
        public ActionResult MyChild()
        {
            User user = Utils.CurrentUser(this);
            if (user.UserTypeId == UserType.PARENT)
            {
                Parent p = db.Parent.Where(r => r.UserID == user.Id).Single();
                Student s = db.Student.Find(p.StudentID);
                School school = db.School.Find(s.SchoolID);
                ViewBag.schoolName = school.SchoolName;

                if (s.SurfTime != null)
                {
                    String serverStartTime = s.SurfTime;
                    String serverEndTime = Convert.ToString(int.Parse(serverStartTime) + 2);
                    ViewBag.serverTime = serverStartTime + "点至" + serverEndTime + "点";
                }

                return View(s);
            }

            return View();
        }

        public ActionResult ParentInfo(Guid id)
        {
            Parent parent = db.Parent.Find(id);
            return View(parent);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}