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
    public class StudentController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Student/

        public ActionResult MyStudent()
        {
            User  user= Utils.CurrentUser(this);
            if (user != null && user.UserType.Id == UserType.SCHOOL_MANAGER)
            {
                SchoolAdministrator man = db.SchoolAdministrator.Where(r => r.UserID == user.Id).Single();
                Guid SchoolID = man.SchoolID;
                var result =
                    Utils.PageIt(this, 20, db.Student.Where(r => r.SchoolID == SchoolID)
                    .OrderBy(r => r.User.UserName)).ToList();
                @ViewBag.UserType = user.UserTypeId;
                @ViewBag.UserID = user.Id;
                @ViewBag.schoolID = man.SchoolID;
                return View(result);

            }
            else
            {
                return RedirectToAction("Default", "Workspace");
            }
        }


        public ActionResult AddTest(Guid StudentID)
        {
            ViewBag.StudentID = StudentID;
            return View();
        }

        [HttpPost]
        public ActionResult AddTest(UserPTest test)
        {
            String testDate = Request.Params.Get("TestDate");
            DateTime t = Convert.ToDateTime(testDate);
            String nowDate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime now = Convert.ToDateTime(nowDate);
            if (t.CompareTo(now) > 0)
            {
                ViewBag.StudentID = test.StudentID;
                ModelState.AddModelError("TestDateError", "请选择正确的日期！");
                return View(test);
            }
            test.ID = Guid.NewGuid();
            test.total = test.Ta + test.Tb + test.Tc + test.Td + test.Te + test.Tf + test.Tg + test.Th + test.Ti + test.Tj;
            
            if (test.total < 40)
            {
                test.result = "心理素质很好";
            } 
            else if (test.total < 60)
            {
                test.result = "心理素质较好";
            }
            else if (test.total < 80)
            {
                test.result = "心理素质一般";
            }
            else
            {
                test.result = "心理素质很差";
            }
            
            db.UserPTest.Add(test);
            db.SaveChanges();
            return RedirectToAction("MyStudent");
        }

        //学校列表
        [HttpGet]
        public ViewResult SchoolNavigation(String SchoolID)
        {
            SchoolController sc = new SchoolController();
            ViewBag.SchoolDropDownList = sc.SchoolDropDownList();
            ViewBag.jaing = "haoran";
            String id = Request.Params.Get("SchoolID");
            School school = null;
            if (id != null)
            {
                school = db.School.Find(Guid.Parse(id));
            }
            return View(school);
        }

        public ActionResult DeletePTest(Guid id)
        {
            UserPTest upt = db.UserPTest.Find(id);
            db.UserPTest.Remove(upt);
            db.SaveChanges();
            return  Redirect(Request.UrlReferrer.ToString());
        }


        //学校列表
        [HttpGet]
        public ActionResult Import()
        {
            User user = Utils.CurrentUser(this);
            if (user != null && user.UserType.Id == UserType.SCHOOL_MANAGER)
            {
                SchoolAdministrator man = db.SchoolAdministrator.Where(r => r.UserID == user.Id).Single();
                var SchoolID = man.SchoolID;
                School school = null;
                if (SchoolID != null)
                {
                    school = db.School.Find(SchoolID);
                    return View(school);
                }
            }
            return RedirectToAction("Default", "Workspace");
                
        }


        //导入心理数据
        [HttpGet]
        public ActionResult ImportTest()
        {
            User user = Utils.CurrentUser(this);
            if (user != null && user.UserType.Id == UserType.SCHOOL_MANAGER)
            {
                SchoolAdministrator man = db.SchoolAdministrator.Where(r => r.UserID == user.Id).Single();
                var SchoolID = man.SchoolID;
                School school = null;
                if (SchoolID != null)
                {
                    school = db.School.Find(SchoolID);
                    return View(school);
                }
            }
            return RedirectToAction("Default", "Workspace");

        }

        //
        // GET: /Student/Details/5

        public ViewResult Details(Guid id)
        {
            Student student = db.Student.Find(id);
            if (student.SurfTime != null)
            {
                String serverStartTime = student.SurfTime;
                String serverEndTime = Convert.ToString(int.Parse(serverStartTime) + 2);
                ViewBag.serverTime = serverStartTime + "点至" + serverEndTime + "点";
            }

            return View(student);
        }

        public ViewResult forfudaoDetails(Guid id)
        {
            Student student = db.Student.Find(id);
       
            if (student.SurfTime != null)
            {
                String serverStartTime = student.SurfTime;
                String serverEndTime = Convert.ToString(int.Parse(serverStartTime) + 2);
                ViewBag.serverTime = serverStartTime + "点至" + serverEndTime + "点";
            }
            ViewBag.schoolid=student.SchoolID;
            return View(student);
        }
        public ViewResult Tips(Guid id)
        {
            Student student = db.Student.Find(id);
            return View(student);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            User  user= Utils.CurrentUser(this);
            if (user != null && user.UserType.Id == UserType.SCHOOL_MANAGER)
            {
                SchoolAdministrator man = db.SchoolAdministrator.Where(r => r.UserID == user.Id).Single();
                var SchoolID = man.SchoolID;
                ViewBag.SchoolID = SchoolID;
                return View();
            }
            
            else
            {

                return RedirectToAction("Default", "Workspace");
            }
        } 

        //
        // POST: /Student/Create
        //学生统一由老师创建
        [HttpPost]
        public ActionResult Create(Student student)
        {
            User user = Utils.CurrentUser(this);
            if (Request.Params.Get("YYYY") == "" || Request.Params.Get("MM") == "" || Request.Params.Get("DD") == "" ||
                Request.Params.Get("YYYY") == null || Request.Params.Get("MM") == null || Request.Params.Get("DD") == null)
            {
                ModelState.AddModelError("Birthday", "请选择生日！");
                SchoolAdministrator man = db.SchoolAdministrator.Where(r => r.UserID == user.Id).Single();
                var SchoolID = man.SchoolID;
                ViewBag.SchoolID = SchoolID;
                return View(student);
            }
            if (ModelState.IsValid)
            {
                student.User.Id = Guid.NewGuid();
                student.User.Password = Utils.GetMd5("123456");
                student.User.UserTypeId = UserType.STUDENT;
                student.User.RoleId = Role.STUDENT;
                student.User.CreateDate = DateTime.Now;
                student.User.isVerify = true;
                student.User.isDeleted = false;

                int year = int.Parse(Request.Params.Get("YYYY"));
                int month = int.Parse(Request.Params.Get("MM"));
                int day = int.Parse(Request.Params.Get("DD"));

                DateTime birthday = new DateTime(year, month, day);
                student.Birthday = birthday;
                student.StudentID = Guid.NewGuid();
                db.Student.Add(student);
                db.SaveChanges();

                if (user != null && user.UserType.Id == UserType.SCHOOL_MANAGER)
                {
                    return RedirectToAction("MyStudent");
                }
                
                
            }
            return View(student);
        }
        
        //
        // GET: /Student/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            if (Utils.CurrentUser(this).UserTypeId == UserType.SCHOOL_MANAGER)
            {
                ViewBag.userType = "SCHOOL_MANAGER";
            } 
            else
            {
                ViewBag.userType = "STUDENT";
            }
            Student student = db.Student.Find(id);
            return View(student);
        }


        public ActionResult QueryX(String selectType)
        {
            var schools=db.School.Where(r=>r.isDeleted==false).ToList();
            var students=db.Student.ToList();
            List<School> result = MemberTools.CombineStudent(schools, students);
            ViewBag.selectType = selectType;
            return View(result);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            Guid uID = Guid.Parse(Request.Params.Get("UserID"));
            String RealName = Request.Params.Get("User.RealName");
            if (RealName != null || RealName != "")
            {
                User user = db.Users.Find(uID);
                user.RealName = RealName;
                db.SaveChanges();
            }
             
            Student temp = db.Student.Find(student.StudentID);

            temp.Sex = student.Sex;
            temp.People = student.People;
            temp.Career = student.Career;
            temp.HomeNum = student.HomeNum;
            temp.Hobby = student.Hobby;
            temp.Telephone = student.Telephone;
            temp.CanSurf = student.CanSurf;
            temp.SurfDayOfWeek = student.SurfDayOfWeek;
            temp.SurfTime = student.SurfTime;
            temp.Hurt = student.Hurt;
            temp.Die = student.Die;
            temp.Lose = student.Lose;
            temp.IsAlone = student.IsAlone;
            if (Utils.CurrentUser(this).UserTypeId == UserType.SCHOOL_MANAGER)
            {
                temp.Math = student.Math;
                temp.Chinese = student.Chinese;
                temp.English = student.English;
                temp.Physics = student.Physics;
                temp.Chemistry = student.Chemistry;
                temp.Sw = student.Sw;
                temp.Dl = student.Dl;
                temp.Ls = student.Ls;
            }

            db.Entry(temp).State = EntityState.Modified;
            db.SaveChanges();
            if (Utils.CurrentUser(this).UserTypeId == UserType.SCHOOL_MANAGER)
            {
                return RedirectToAction("MyStudent");
            } 
            else
            {
                return RedirectToAction("Details", "Users", new { id = temp.UserID }); 
            }

        }

        //
        // GET: /Student/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Student student = db.Student.Find(id);
            return View(student);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {                
            Student student = db.Student.Find(id);

            Guid SchoolID = student.SchoolID;

            student.User.isDeleted = true;
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MyStudent"); 
        }

        //删除全部,id为学校id
        public ActionResult DeleteAll(Guid id)
        {
            ViewBag.schoolID = id;
            return View();
        }

        [HttpPost, ActionName("DeleteAll")]
        public ActionResult DeleteAllConfirmed(Guid id)
        {
            var StudentList = db.Student.Where(r => r.SchoolID == id).Where(r => r.User.isDeleted == false).ToList();
            foreach(Student s in StudentList)
            {
                s.User.isDeleted = true;
                db.Entry(s).State = EntityState.Modified;
            }

            db.SaveChanges();
            return RedirectToAction("MyStudent");
        }

        public ActionResult StudentInfo(Guid id)
        {
            Student student = db.Student.Find(id);
            if (student.SurfTime != null)
            {
                String serverStartTime = student.SurfTime;
                String serverEndTime = Convert.ToString(int.Parse(serverStartTime) + 2);
                ViewBag.serverTime = serverStartTime + "点至" + serverEndTime + "点";
            }

            return View(student);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}