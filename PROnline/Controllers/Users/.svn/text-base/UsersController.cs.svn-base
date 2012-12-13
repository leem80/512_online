using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models;
using PROnline.Models.Trainings;
using PROnline.Models.Users;
using PROnline.Models.Letters;
using System.Data;
using NPOI.HSSF;
using PROnline.Src;
using NPOI.HSSF.UserModel;

namespace PROnline.Controllers.Users
{
    public class UsersController : Controller
    {
        PROnlineContext db = new PROnlineContext();
        //
        // GET: /Users/

        public ActionResult Index()
        {
            //return View(db.Users.OrderBy(t=>t.UserName).Skip(1).Take(1).ToList());
            return View(db.Users.OrderBy(t => t.UserName).ToList());
        }


        public ActionResult Import()
        {
            return View();
        }

        //导入心理数据
        public ActionResult ImportTest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(String type,HttpPostedFileBase upload,String SchoolID,String TeacherID)
        {
            ViewBag.type = type;
            User user = Utils.CurrentUser(this);
            if (user!=null)
            {
                List<SchoolAdministrator> sa = db.SchoolAdministrator.Where(r => r.UserID == user.Id).ToList();
                if (sa.Count > 0)
                {
                    ViewBag.SchoolID = sa[0].SchoolID.ToString();
                }

            }
            else
            {
                return RedirectToAction("Default", "Workspace");
            }
            if (upload != null)
            {
                HSSFWorkbook xls;
                try
                {
                    xls = new HSSFWorkbook(upload.InputStream);
                }
                catch
                {
                    return RedirectToAction("Error", "Error", new { title = "错误！", message = "导入错误.请选择正确的文件!" });
                }
                if (xls != null)
                {
                    IUserImport import = ImportFactory.GetImport(type);
                    if (import != null)
                    {
                        List<ImportResult> result = import.import(xls);
                        ViewBag.result = result;
                        if ("class" == type)
                            return View("ImportClass", ImportClass(result));
                        else if ("test" == type)
                            return View("ImportTestDetail", ImportTest(result));
                    }

                }

            }

            return View();
        }

        [NonAction]
        public List<ClassImportModel> ImportClass(List<ImportResult> input)
        {
            List<ClassImportModel> clmList= new List<ClassImportModel>();

            foreach(ImportResult temp in input){
                if(temp.Status==ImportResult.SUCCESS){
                    clmList.Add((ClassImportModel)temp.Data);
                }
            }

            return clmList;
        }


        [NonAction]
        public List<UserPTest> ImportTest(List<ImportResult> input)
        {
            List<UserPTest> clmList = new List<UserPTest>();

            foreach (ImportResult temp in input)
            {
                if (temp.Status == ImportResult.SUCCESS)
                {
                    clmList.Add((UserPTest)temp.Data);
                }
            }

            return clmList;
        }

        [HttpPost]
        public ActionResult ImportTest(UserPTest[] PTest)
        {

            foreach (var p in PTest)
            {
                p.ID = Guid.NewGuid();
                p.TestDate = DateTime.Now;
                db.UserPTest.Add(p);
            }
            db.SaveChanges();
            //return Content("导入成功！<p><a href=\"/Student/MyStudent\">返回</a></p>");
            return RedirectToAction("MyStudent", "Student");
        }

        [HttpPost]
        public ActionResult ImportClass(String type,Guid SchoolID)
        {
            if (type == "class")
            {
                String[] name = Request.Params.GetValues("item.Name");
                String[] sex = Request.Params.GetValues("item.Sex");
                String[] birthDay = Request.Params.GetValues("item.BirthDay");
                String[] userName = Request.Params.GetValues("item.UserName");
                String[] People = Request.Params.GetValues("item.People");
                String[] Career = Request.Params.GetValues("item.Career");
                String[] HomeNum = Request.Params.GetValues("item.HomeNum");
                String[] Hobby = Request.Params.GetValues("item.Hobby");
                String[] Telephone = Request.Params.GetValues("item.Telephone");
                String[] CanSurf = Request.Params.GetValues("item.CanSurf");
                String[] IsAlone = Request.Params.GetValues("item.IsAlone");
                String[] SurfTime = Request.Params.GetValues("item.SurfTime");
                String[] Hurt = Request.Params.GetValues("item.Hurt");
                String[] Die = Request.Params.GetValues("item.Die");
                String[] DayOfWeek = Request.Params.GetValues("item.DayOfWeek");
                String[] Lose = Request.Params.GetValues("item.Lose");
                String[] Math = Request.Params.GetValues("item.Math");
                String[] Chinese = Request.Params.GetValues("item.Chinese");
                String[] English = Request.Params.GetValues("item.English");
                String[] Physics = Request.Params.GetValues("item.Physics");
                String[] Chemistry = Request.Params.GetValues("item.Chemistry");
                String[] Sw = Request.Params.GetValues("item.Sw");
                String[] Dl = Request.Params.GetValues("item.Dl");
                String[] Ls = Request.Params.GetValues("item.Ls");
                for (int i = 0; i < name.Length; i++)
                {
                    var Uid = Guid.NewGuid();
                    User usr = new User
                    {
                        Id=Uid,
                        RealName = name[i],
                        Password = Utils.GetMd5("123456"),
                        UserName = userName[i],
                        CreateDate = DateTime.Now,
                        RoleId = Role.STUDENT,
                        isDeleted = false,
                        isVerify = false,
                        UserTypeId = UserType.STUDENT

                    };
                    db.Users.Add(usr);

                    Student stu = new Student();
                    stu.UserID = usr.Id;
                    stu.StudentID = Guid.NewGuid();
                    stu.SchoolID = SchoolID;
                    stu.Birthday = DateTime.Parse(birthDay[i]);
                    stu.Sex = sex[i];
                    stu.People = People[i];
                    stu.Career =Career [i];
                    stu.HomeNum =Convert.ToInt32(HomeNum[i]) ;
                    stu.Hobby =Hobby[i] ;
                    stu.Telephone =Telephone [i];
                    stu.IsAlone = IsAlone[i] == "True";
                    stu.CanSurf =CanSurf[i]=="True" ;
                    stu.SurfTime = SurfTime[i];
                    stu.Hurt =Hurt[i];
                    stu.Die =Die[i];
                    stu.Lose =Lose[i];
                    stu.Math =Math[i];
                    stu.Sex = sex[i];
                    stu.SurfDayOfWeek = DayOfWeek[i];
                    stu.Chinese =Chinese[i];
                    stu.English =English[i];
                    stu.Physics =Physics[i];
                    stu.Chemistry = Chemistry[i];
                    stu.Sw =Sw[i];
                    stu.Dl =Dl[i];
                    stu.Ls =Ls[i];
                    db.SaveChanges();
                    db.Student.Add(stu);
                    db.SaveChanges();

                }

                //return Content("导入成功！<p><a href=\"/Student/MyStudent\">返回</a></p>");
                return RedirectToAction("MyStudent", "Student");
                

            }
            return Content("导入失败！<p><a href=\"/Student/MyStudent\">返回</a></p>");
        }



        public ActionResult Select(String selectType,String filter,String[] userType)
        {
            
            string selectTypex = selectType == null ? "radio" : selectType;
            ViewBag.selectType = selectTypex;
            ViewBag.userType = userType;
            ViewBag.filter = filter;
           
            int start = filter.IndexOf("=") + 1;
            int end = filter.IndexOf("&");
            end = end == -1 ? filter.Length : end;

            String showType = filter.Substring(start, end - start);
            ViewBag.showType = showType;
            return View();
        }

        /*
         * 新增了一个形参,因为不同的界面，需要进行的筛选条件不同
         * 活动：找出当前并未参加任何活动的用户
         * 培训：找出当前并未参加任何培训的用户
         * 小组：找出当前尚未加入任何小组的小组长
        */
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
                else if(type == "selPairMember")
                {
                    if (Qname == "")
                    {
                     /*   var list = from s in db.Supervisor
                                   where !(from ts in db.Pairs
                                           select ts.SupervisorID).Contains(s.SupervisorID) && s.User.isVerify == true
                                   select s;
                      */
                        var list = from s in db.Supervisor
                                   where s.User.isVerify == true
                                   select s;
                        ViewBag.list = list.ToList();
                    }
                    else
                    {
                        /*
                        var list = from s in db.Supervisor
                                   where !(from ts in db.Pairs
                                           select ts.SupervisorID).Contains(s.SupervisorID) && s.User.isVerify == true
                                           && s.User.RealName == Qname
                                   select s;
                         * */
                        var list = from s in db.Supervisor
                                   where s.User.isVerify == true && s.User.RealName == Qname
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
                //选择参加培训的志愿者
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
                if (type == "selTrainingMember")
                {
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
                                               && v.User.UserTypeId == "team_leader"
                                               && v.isHaveTeam == true
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
                                               && v.User.UserTypeId == "team_leader"
                                               && v.isHaveTeam == true
                                               && v.User.RealName == Qname
                                       select v;
                            ViewBag.list = list.ToList();
                        }
                    }
                }
            }
            return View();
        }

        public JsonResult UserNameAvailable()
        {
            String username = Request.Params["User.UserName"];
            int count = db.Users.Where(usr => usr.UserName == username).Count();

            return Json(count == 0, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OldPasswordAvailable()
        {
            User user = Utils.CurrentUser(this);
            String OldPassword = Utils.GetMd5(Request.Params["oldPassword"]);

            return Json(user.Password == OldPassword, JsonRequestBehavior.AllowGet);
        }

        //修改密码
        public ActionResult ChangePassword(Guid id)
        {
            Password p = new Password();
            return View(p);
        }

        [HttpPost]
        public ActionResult ChangePassword(Guid id, Password password)
        {
            if (ModelState.IsValid)
            {
                User u = db.Users.Find(id);
                u.Password = Utils.GetMd5(password.Pass);
                u.IsPasswordChanged = true;
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(password);
        }
  
        
        //
        // GET: /Users/Details/5
        public ActionResult Details()
        {
            User user = Utils.CurrentUser(this);
            String userType = user.UserType.Id;
            Guid userId = user.Id;
            ViewBag.userType = userType;

             
            if (userType == UserType.VOLUNTEER)
            {
                Volunteer u = db.Volunteer.Where(v => v.User.Id == userId).Single();
                ViewBag.id = u.VolunteerID;
            }
            else if (userType == UserType.TEAM_LEADER)
            {
                Volunteer v = db.Volunteer.Where(t => t.User.Id == userId).Single();
                ViewBag.id = v.VolunteerID;
            }
            else if (userType == UserType.SUPERVISOR)
            {
                Supervisor u = db.Supervisor.Where(s => s.User.Id == userId).Single();
                ViewBag.id = u.SupervisorID;
            }
            else if (userType == UserType.STUDENT)
            {
                Student u = db.Student.Where(s => s.User.Id == userId).Single();
                ViewBag.id = u.StudentID;
            }
            else if (userType == UserType.PARENT)
            {
                Parent u = db.Parent.Where(s => s.User.Id == userId).Single();
                ViewBag.id = u.ParentID;
            }
            else if (userType == UserType.ADMIN)
            {
                User u = db.Users.Find(userId);
                ViewBag.id = u.Id;
            }
            else if (userType == UserType.MANAGER)
            {
                Administrator u = db.Administrator.Where(s => s.User.Id == userId).Single();
                ViewBag.id = u.AdministratorID;
            }
            else if (userType == UserType.DONATOR)
            {
                Donator u = db.Donator.Where(s => s.User.Id == userId).Single();
                ViewBag.id = u.DonatorID;
            }
            else if (userType == UserType.SCHOOL_MANAGER)
            {
                SchoolAdministrator u = db.SchoolAdministrator.Where(s => s.User.Id == userId).Single();
                ViewBag.id = u.ID;
            }
            
            return View();
        }
 
        //
        // GET: /Users/Delete/5

        public ActionResult Delete(Guid id)
        {
            return View();
        }

        //
        // POST: /Users/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
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

        //用户审核
        public ActionResult UserVerifying()
        {
            var result = from c in db.Users
                         where c.isDeleted == false && (c.RoleId == "volunteer" || c.RoleId == "supervisor" || c.RoleId == "donator") && c.isVerify == false
                         orderby c.CreateDate descending
                         select c;
            
            return View(result.ToList());
        }

        //审核用户资料时点击用户名后出现的页面
        public ActionResult UserInfo(Guid id)
        {
            var result = db.Users.Find(id);
            String userType = result.UserType.Id;
            if (userType == UserType.VOLUNTEER)
            {
                Volunteer v = db.Volunteer.Where(temp => temp.User.Id == id).Single();
                ViewBag.userType = UserType.VOLUNTEER;
                ViewBag.id = v.VolunteerID;
                ViewBag.title = "志愿者信息";
            } 
            else if (userType == UserType.SUPERVISOR)
            {
                Supervisor s = db.Supervisor.Where(temp => temp.User.Id == id).Single();
                ViewBag.userType = UserType.SUPERVISOR;
                ViewBag.id = s.SupervisorID;
                ViewBag.title = "督导信息";
            }
            else if (userType == UserType.DONATOR)
            {
                Donator d = db.Donator.Where(temp => temp.User.Id == id).Single();
                ViewBag.userType = UserType.DONATOR;
                ViewBag.id = d.DonatorID;
                ViewBag.title = "爱心人士信息";
            }
            else if (userType == UserType.TEAM_LEADER)
            {
                Volunteer v = db.Volunteer.Where(temp => temp.User.Id == id).Single();
                ViewBag.userType = UserType.TEAM_LEADER;
                ViewBag.id = v.VolunteerID;
                ViewBag.title = "志愿者信息";
            }
            else if (userType == UserType.STUDENT)
            {
                Student s = db.Student.Where(temp => temp.User.Id == id).Single();
                ViewBag.userType = UserType.STUDENT;
                ViewBag.id = s.StudentID;
                ViewBag.title = "学生信息";
            }
            else if(userType == UserType.SCHOOL_MANAGER)
            {
                SchoolAdministrator sa = db.SchoolAdministrator.Where(temp => temp.User.Id == id).Single();
                ViewBag.userType = UserType.SCHOOL_MANAGER;
                ViewBag.id = sa.ID;
                ViewBag.title = "校方管理员信息";
            }
            else if (userType == UserType.PARENT)
            {
                Parent p = db.Parent.Where(temp => temp.User.Id == id).Single();
                ViewBag.userType = UserType.PARENT;
                ViewBag.id = p.ParentID;
                ViewBag.title = "家长信息";
            }
            return View();
        }

        //审核处理,verify为accept为同意，为refuse为拒绝
        public ActionResult Verifying(Guid id, String verify)
        {
            var user = db.Users.Find(id);
            if (verify == "accept")
            {
                user.isVerify = true;
                String[] date = DateTime.Now.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                //发送站内信
                Letter l = new Letter();
                l.LetterID = Guid.NewGuid();
                l.IsRead = "未读";
                l.Title = "注册审核通过！";
                l.Content = "您的注册审核已经于" + time + "通过。";
                l.ReceiverID = user.Id;
                l.SenderID = Utils.CurrentUser(this).Id;
                l.SenderDate = DateTime.Now;
                db.Letter.Add(l);
                db.SaveChanges();
            } 
            else
            {
                user.isDeleted = true;
            }
            db.SaveChanges();
            return RedirectToAction("UserVerifying");
        }

        public ViewResult SADetails(Guid id, String type)
        {
            User user = db.Users.Find(id);

            ViewBag.type = type;
            ViewBag.id = id;
            return View(user);
        }
    }
}
