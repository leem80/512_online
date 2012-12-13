using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Trainings;
using PROnline.Models;
using PROnline.Src;
using PROnline.Models.Users;
using PROnline.Models.Letters;

namespace PROnline.Controllers.Trainings
{ 
    public class TrainingController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        public TrainingController()
        {
            this.ValidateRequest = false;
        }
        //
        // GET: /Training/

        public ViewResult Index()
        {
            var result = Utils.PageIt(this, db.Training.Where(r => r.isVerify == true).Where(r => r.State != "待审核")
                .OrderByDescending(r => r.State).ThenByDescending(r => r.StartDate)).ToList();
            return View(result);
        }

        //
        // GET: /Training/Details/5
        //参数:type:detail不提供签到，attend:提供签到
        public ViewResult Details(Guid id, String type)
        {
            Training training = db.Training.Find(id);
            ViewBag.type = type;
            return View(training);
        }

        //我的培训列表
        public ActionResult MyTraining()
        {
            User user = Utils.CurrentUser(this);
            ViewBag.userType = user.UserTypeId;
            if (user.UserTypeId == UserType.SUPERVISOR)
            {
                var list = from ts in db.TrainingSupervisor
                           from t in db.Training
                           where ts.Supervisor.User.Id == user.Id
                           && t.TrainingID == ts.TrainingID
                           && t.State == "已通过"
                           && t.isVerify == true
                           orderby ts.Training.StartDate
                           select t;
                return View(Utils.PageIt(this, list).ToList());
            }
            else if(user.UserTypeId == UserType.VOLUNTEER)
            {
                var list = from tv in db.TrainingVolunteer
                           from t in db.Training
                           where tv.Volunteer.User.Id == user.Id
                           && tv.TrainingID == t.TrainingID
                           && t.State == "已通过"
                           && t.isVerify == true
                           orderby tv.Training.StartDate
                           select t;
                return View(Utils.PageIt(this, list).ToList());
            }
            return View();           
        }

        //
        // GET: /Training/Create

        public ActionResult Create()
        {
            User user = Utils.CurrentUser(this);

            if (user.UserTypeId == UserType.SUPERVISOR)
            {
                ViewBag.isShow = false;
            }
            else if (user.UserTypeId == UserType.ADMIN || user.UserTypeId == UserType.MANAGER)
            {
                ViewBag.isShow = true;
            }
            return View();
        } 

        //
        // POST: /Training/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Training training)
        {
            this.ValidateRequest = false;
            String[] supervisors = Request.Form.GetValues("SupervisorID");
            String[] volunteers = Request.Form.GetValues("VolunteerID");

            String startDate = Request.Params.Get("StartDate");
            DateTime start = Convert.ToDateTime(startDate);
            String nowDate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime now = Convert.ToDateTime(nowDate);

            if (start.CompareTo(Convert.ToDateTime(now)) <= 0)
            {
                ModelState.AddModelError("StartDateError", "所选择日期不恰当，请选择恰当的日期(大于当前日期)");
                return View();
            }

            String time1 = Request.Form.Get("StartTime");
            String time2 = Request.Form.Get("EndTime");
            String[] sTime = time1.Split(':');
            String[] eTime = time2.Split(':');
            String StartTime = sTime[0];
            String EndTime = eTime[0];
            if (Convert.ToInt32(StartTime) >= Convert.ToInt32(EndTime))
            {
                ModelState.AddModelError("Error", "培训开始时间必须小于结束时间");
                return View();
            }
         
            if (ModelState.IsValid)
            {
                training.TrainingID = Guid.NewGuid();
                this.ValidateRequest = false;
                supervisors = Request.Form.GetValues("SupervisorID");
                if (supervisors != null)
                {
                    List<TrainingSupervisor> l = new List<TrainingSupervisor>();
                    foreach (String s in supervisors)
                    {
                        TrainingSupervisor ts = new TrainingSupervisor();
                        ts.TrainingSupervisorID = Guid.NewGuid();
                        ts.SupervisorID = Guid.Parse(s);
                        ts.TrainingID = training.TrainingID;
                        ts.IsAttend = false;
                        l.Add(ts);
                    }
                    training.SupervisorList = l;
                }

                volunteers = Request.Form.GetValues("VolunteerID");
                if (volunteers != null)
                {
                    List<TrainingVolunteer> v = new List<TrainingVolunteer>();
                    foreach (String s in volunteers)
                    {
                        TrainingVolunteer tv = new TrainingVolunteer();
                        tv.TrainingVolunteerID = Guid.NewGuid();
                        tv.VolunteerID = Guid.Parse(s);
                        tv.TrainingID = training.TrainingID;
                        tv.IsAttend = false;
                        tv.isFeedback = false;
                        v.Add(tv);
                    }
                    training.VolunteerList = v;
                }

                User user = Utils.CurrentUser(this);

                training.CreatorID = user.Id;
                training.CreateTime = DateTime.Now;
                if (user.UserTypeId == UserType.SUPERVISOR)
                {
                    training.State = "待审核";
                    training.isVerify = false;
                }
                else if (user.UserTypeId == UserType.ADMIN || user.UserTypeId == UserType.MANAGER)
                {
                    training.State = "已通过";
                    training.isVerify = true;
                }

                db.Training.Add(training);
                db.SaveChanges();

                if (user.UserTypeId == UserType.ADMIN || user.UserTypeId == UserType.MANAGER)
                {
                    if (training.SupervisorList != null)
                    {
                        IList<TrainingSupervisor> list = training.SupervisorList;
                        foreach (TrainingSupervisor ts in list)
                        {
                            Supervisor temp = db.Supervisor.Find(ts.SupervisorID);
                            //发送站内信
                            Letter l = new Letter();
                            l.LetterID = Guid.NewGuid();
                            l.IsRead = "未读";
                            l.Title = "培训通知！";
                            l.Content = "您收到了主题为“" + training.Title + "”的培训申请的通知！请按时参加！详细信息请查看培训列表。";
                            l.ReceiverID = temp.UserID;
                            l.SenderID = Utils.CurrentUser(this).Id;
                            l.SenderDate = DateTime.Now;
                            db.Letter.Add(l);
                            db.SaveChanges();
                        }
                    }
                    if (training.VolunteerList != null)
                    {
                        IList<TrainingVolunteer> list = training.VolunteerList;
                        foreach (TrainingVolunteer tv in list)
                        {
                            Volunteer temp = db.Volunteer.Find(tv.VolunteerID);
                            //发送站内信
                            Letter l = new Letter();
                            l.LetterID = Guid.NewGuid();
                            l.IsRead = "未读";
                            l.Title = "培训通知！";
                            l.Content = "您收到了主题为“" + training.Title + "”的培训申请的通知！请按时参加！详细信息请查看培训列表。";
                            l.ReceiverID = temp.UserID;
                            l.SenderID = Utils.CurrentUser(this).Id;
                            l.SenderDate = DateTime.Now;
                            db.Letter.Add(l);
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index");
                } 
                else if (user.UserTypeId == UserType.SUPERVISOR)
                {
                    String[] date = training.CreateTime.ToShortDateString().Split('/');
                    String year = date[0];
                    String month = date[1];
                    String day = date[2];
                    String time = year + "年" + month + "月" + day + "日";

                    var ulist = db.Users.Where(r => r.RoleId == Role.SERVICE_MANAGER).ToList();
                    foreach (User u in ulist)
                    {
                        //发送站内信
                        Letter l = new Letter();
                        l.LetterID = Guid.NewGuid();
                        l.IsRead = "未读";
                        l.Title = "培训申请审核！";
                        l.Content = "督导老师" + user.RealName + "于" + time + "提出了主题为“" + training.Title + "”的培训申请，请尽快审核！";
                        l.ReceiverID = u.Id;
                        l.SenderID = Utils.CurrentUser(this).Id;
                        l.SenderDate = DateTime.Now;
                        db.Letter.Add(l);
                        db.SaveChanges();
                    }
                    return RedirectToAction("MyVerifyingTrainingApply");
                }                
            }

            return View(training);
        }
        
        //
        // GET: /Training/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Training training = db.Training.Find(id);
            return View(training);
        }

        //
        // POST: /Training/Edit/5

        [HttpPost]
        public ActionResult Edit(Training training)
        {
            if (ModelState.IsValid)
            {
                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(training);
        }

        //
        // GET: /Training/Cancel/5

        public ActionResult Cancel(Guid id)
        {
            Training training = db.Training.Find(id);
            return View(training);
        }

        //
        // POST: /Training/Cancel/5

        [HttpPost, ActionName("Cancel")]
        public ActionResult CancelConfirmed(Guid id)
        {            
            Training training = db.Training.Find(id);
            training.State = "已取消";
            db.Entry(training).State = EntityState.Modified;

            db.SaveChanges();

            String st = training.StartTime.Split(':').First();
            String et = training.EndTime.Split(':').First();
            String[] date = training.StartDate.ToShortDateString().Split('/');
            String year = date[0];
            String month = date[1];
            String day = date[2];
            String time = year + "年" + month + "月" + day + "日" + st + "点至"
                        + et + "点";

            if (training.SupervisorList != null)
            {
                IList<TrainingSupervisor> list = training.SupervisorList;
                foreach (TrainingSupervisor ts in list)
                {
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "培训取消！";
                    l.Content = "原定" + time + "于" + training.Locale + "进行的主题为“" + training.Title + "”的培训已取消，请相互转告。";
                    l.ReceiverID = ts.Supervisor.UserID;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }
            }
            if (training.VolunteerList != null)
            {
                IList<TrainingVolunteer> list = training.VolunteerList;
                foreach (TrainingVolunteer ts in list)
                {
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "培训取消";
                    l.Content = "原定" + time + "于" + training.Locale + "进行的主题为“" + training.Title + "”的培训已取消，请相互转告。";
                    l.ReceiverID = ts.Volunteer.UserID;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        //公共模块中列出已经通过审核的培训
        public ActionResult TrainingList()
        {
            //var result = Utils.PageIt(this, db.Training.Where(r => r.State == "正常").OrderBy(r => r.StartDate)).ToList();
            var result = db.Training.Where(r => r.State == "已通过").Where(r => r.isVerify == true)
                .OrderBy(r => r.StartDate).ToList();
            return View(result);
        }

        //公共培训列表中的详细信息
        public ViewResult TrainingDetails(Guid id)
        {
            Training training = db.Training.Find(id);
            return View(training);
        }

        //督导专家创建完培训之后，需要进行审核
        public ActionResult TrainingVerify()
        {
            var result = db.Training.Where(r => r.State == "待审核").Where(r => r.isVerify == false)
                .OrderBy(r => r.StartDate).ToList();

            var list = from t in db.Training
                       from u in db.Users
                       where t.CreatorID == u.Id
                       select new { t, u };

            Dictionary<Guid, String> dic = new Dictionary<Guid, String>();
            foreach(var item in list.ToList())
            {
                dic.Add(item.t.TrainingID, item.u.RealName);
            }
            ViewBag.dic = dic;

            return View(result);
        }

        //审核培训申请时参看其详细信息界面
        public ActionResult TrainingInfo(Guid id, String type)
        {
            var result = db.Training.Find(id);
            ViewBag.type = type;
            return View(result);
        }

        //审核处理,verify为accept为同意，为refuse为拒绝
        public ActionResult Verifying(Guid id, String verify)
        {
            Training training = db.Training.Find(id);
            if (verify == "accept")
            {
                training.isVerify = true;
                training.State = "已通过";

                String[] date = training.StartDate.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                //发送站内信
                Letter l = new Letter();
                l.LetterID = Guid.NewGuid();
                l.IsRead = "未读";
                l.Title = "培训申请审核通过";
                l.Content = "您于" + time + "提出的主题为“" + training.Title + "”的培训申请已经通过审核！详细信息请查看已审提案。";
                l.ReceiverID = training.CreatorID;
                l.SenderID = Utils.CurrentUser(this).Id;
                l.SenderDate = DateTime.Now;
                db.Letter.Add(l);
                db.SaveChanges();

                if (training.SupervisorList != null)
                {
                    IList<TrainingSupervisor> list = training.SupervisorList;
                    foreach (TrainingSupervisor ts in list)
                    {
                        //发送站内信
                        Letter ll = new Letter();
                        ll.LetterID = Guid.NewGuid();
                        ll.IsRead = "未读";
                        ll.Title = "培训通知！";
                        ll.Content = "您收到了主题为“" + training.Title + "”的培训申请的通知！请按时参加！详细信息请查看培训列表。";
                        ll.ReceiverID = ts.Supervisor.UserID;
                        ll.SenderID = Utils.CurrentUser(this).Id;
                        ll.SenderDate = DateTime.Now;
                        db.Letter.Add(ll);
                        db.SaveChanges();
                    }
                }
                if (training.VolunteerList != null)
                {
                    IList<TrainingVolunteer> list = training.VolunteerList;
                    foreach (TrainingVolunteer ts in list)
                    {
                        //发送站内信
                        Letter ll = new Letter();
                        ll.LetterID = Guid.NewGuid();
                        ll.IsRead = "未读";
                        ll.Title = "培训通知！";
                        ll.Content = "您收到了主题为“" + training.Title + "”的培训申请的通知！请按时参加！详细信息请查看培训列表。";
                        ll.ReceiverID = ts.Volunteer.UserID;
                        ll.SenderID = Utils.CurrentUser(this).Id;
                        ll.SenderDate = DateTime.Now;
                        db.Letter.Add(ll);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                training.isVerify = true;
                training.State = "未通过";

                String[] date = training.StartDate.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                //发送站内信
                Letter l = new Letter();
                l.LetterID = Guid.NewGuid();
                l.IsRead = "未读";
                l.Title = "培训申请审核未通过。";
                l.Content = "很遗憾，您于" + time + "提出的主题为“" + training.Title + "”的培训未通过审核！";
                l.ReceiverID = training.CreatorID;
                l.SenderID = Utils.CurrentUser(this).Id;
                l.SenderDate = DateTime.Now;
                db.Letter.Add(l);
                db.SaveChanges();
            }
            db.SaveChanges();

            return RedirectToAction("TrainingVerify");
        }

        //自己提出的仍然在审核的培训申请
        public ActionResult MyVerifyingTrainingApply()
        {
            User user = Utils.CurrentUser(this);
            var result = db.Training.Where(r => r.isVerify == false).Where(r => r.State == "待审核")
                .Where(r => r.CreatorID == user.Id).ToList();

            return View(result);
        }

        //自己提出的已经过审核的培训申请
        public ActionResult MyVerifiedTrainingApply()
        {
            User user = Utils.CurrentUser(this);
            var result = db.Training.Where(r => r.isVerify == true).Where(r => r.State != "待审核")
                .Where(r => r.CreatorID == user.Id).ToList();

            return View(result);
        }

        //志愿者点击“培训反馈”的页面
        public ActionResult MyTrainingFeedback()
        {
            User user = Utils.CurrentUser(this);

            //找出与当前用户（志愿者）相关的已参加但还未反馈的培训
            var result = from tv in db.TrainingVolunteer
                         from t in db.Training
                         where tv.Volunteer.UserID == user.Id && tv.TrainingID == t.TrainingID
                         && tv.IsAttend == true && tv.isFeedback == false
                         orderby t.StartDate
                         select t;

            var list = Utils.PageIt(this, result).ToList();
            ViewBag.userId = user.Id;

            return View(list);
        }

        //管理员点击“培训反馈”的页面
        public ActionResult TrainingFeedbackList()
        {
            var result = Utils.PageIt(this, 10, db.Training.Where(r => r.State == "已通过").OrderBy(r => r.StartDate)).ToList();
            return View(result);
        }

        //id为TrainingID
        public ActionResult MemberList(Guid id)
        {
//             var result = db.TrainingVolunteer.Where(r => r.TrainingID == id).Where(r => r.IsAttend == true)
//                 .Where(r => r.isFeedback == true).OrderBy(r => r.Volunteer.User.RealName);
            var result = db.TrainingVolunteer.Where(r => r.TrainingID == id)
                .Where(r => r.isFeedback == true).OrderBy(r => r.Volunteer.User.RealName).ToList();

            Training training = db.Training.Find(id);
            String date = training.StartDate.ToShortDateString() + " " + training.StartTime + "--" 
                + training.EndTime;
            String info = "培训主题:" + training.Title + ";培训地点:" + training.Locale + ";培训时间:" + date;
            ViewBag.info = info;

            ViewBag.trainingID = id;

            return View(result);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}