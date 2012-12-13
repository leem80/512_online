using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Activities;
using PROnline.Models.Users;
using PROnline.Models;
using PROnline.Src;
using PROnline.Models.Letters;

namespace PROnline.Controllers.Activities
{ 
    public class ActivityController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Activity/

        public ViewResult Index()
        {
            return View(db.Activity.ToList());
        }


        public ActionResult List()
        {
            var result = Utils.PageIt(this, 10, db.Activity.Where(r => r.VerifyState == "SUCCESS").OrderByDescending(r => r.CreateDate)
                .ThenBy(r => r.ActivityName)).ToList();

            return View(result);
        }

        public ActionResult Top()
        {

            var result = db.Activity.Where(r => r.VerifyState == "SUCCESS").
                OrderBy(r => r.CreateDate).Take(MvcApplication.ListSize).ToList();

            if (result.Count != 0)
            {
                ViewBag.isShow = true;
                return View(result);
            }
            else
            {
                ViewBag.isShow = false;
                return View();
            }          

        }

        //用户参与的已审核的活动列表
        public ViewResult MyJoinActivity()
        {
            Guid id = Utils.CurrentUser(this).Id;
            var result = from a in db.Activity
                         from am in db.ActivityMember
                         where a.ActivityID == am.ActivityID && a.VerifyState == "SUCCESS" && am.UserID == id
                         orderby a.CreateDate
                         select a;
            var list = Utils.PageIt(this, result).ToList();

            return View(list);
        }

        //
        // GET: /Activity/Details/5

        public ViewResult Details(Guid id)
        {
            Activity activity = db.Activity.Find(id);
            ViewBag.list = activity.memberList;
            return View(activity);
        }

        public ViewResult View(Guid id)
        {
            Activity activity = db.Activity.Find(id);
            return View(activity);
        }

        //
        // GET: /Activity/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Activity/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Activity activity, String[] VolunteerID, String[] StudentID)
        {
            this.ValidateRequest = false;
            String startDate = Request.Params.Get("ActivityStartDate");
            DateTime start = Convert.ToDateTime(startDate);
            String nowDate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime now = Convert.ToDateTime(nowDate);
            if (start.CompareTo(Convert.ToDateTime(now)) <= 0)
            {
                ModelState.AddModelError("ActivityStartDateError", "所选择日期不恰当，请选择恰当的日期(大于当前日期)");
                return View();
            }
            if (ModelState.IsValid)
            {
                activity.ActivityID = Guid.NewGuid();
                activity.CreateDate = DateTime.Now;
                activity.CreatorID = Utils.CurrentUser(this).Id;
                activity.VerifyState = "NO";

                int sCount = 0;
                int vCount = 0;

                //添加活动用户
                String[] studentId = StudentID;
                String[] volunteerId = VolunteerID;

                List<ActivityMember> slist = new List<ActivityMember>();
                if (studentId != null)
                {
                    foreach (String s in studentId)
                    {
                        sCount++;

                        Student stu = db.Student.Find(Guid.Parse(s));

                        ActivityMember ds = new ActivityMember();
                        ds.ActivityMemberID = Guid.NewGuid();
                        ds.ActivityID = activity.ActivityID;
                        ds.UserID = stu.User.Id;
                        slist.Add(ds);
                        db.ActivityMember.Add(ds);
                    }
                }
                if (volunteerId != null)
                {
                    foreach(String v in volunteerId)
                    {
                        vCount++;

                        Volunteer vol = db.Volunteer.Find(Guid.Parse(v));

                        ActivityMember dv = new ActivityMember();
                        dv.ActivityMemberID = Guid.NewGuid();
                        dv.ActivityID = activity.ActivityID;
                        dv.UserID = vol.User.Id;
                        slist.Add(dv);
                        db.ActivityMember.Add(dv);
                    }
                }
                activity.memberList = slist;
                activity.StudentCount = sCount;
                activity.VolunteerCount = vCount;

                db.Activity.Add(activity);
                db.SaveChanges();

                return RedirectToAction("MyActivityList");  
            }

            return View(activity);
        }

        public ActionResult Submit(Guid id)
        {
            Activity activity = db.Activity.Find(id);
            activity.VerifyState = "SUBMIT";
            db.Entry(activity).State = EntityState.Modified;
            db.SaveChanges();

            String[] date = activity.ActivityStartDate.ToShortDateString().Split('/');
            String year = date[0];
            String month = date[1];
            String day = date[2];
            String time = year + "年" + month + "月" + day + "日";

            var ulist = db.Users.Where(r => r.RoleId == Role.PROJECT_MANAGER).ToList();
            foreach (User u in ulist)
            {
                //发送站内信
                Letter l = new Letter();
                l.LetterID = Guid.NewGuid();
                l.IsRead = "未读";
                l.Title = "活动提案审核！";
                l.Content = "志愿者小组长" + Utils.CurrentUser(this).RealName + "提出了" + time + "主题为“" + activity.ActivityName + "”的活动提案，请尽快审核！";
                l.ReceiverID = u.Id;
                l.SenderID = Utils.CurrentUser(this).Id;
                l.SenderDate = DateTime.Now; ;
                db.Letter.Add(l);
                db.SaveChanges();
            }
            return RedirectToAction("VerifyingList");
        }

        //
        // GET: /DonationItem/Edit/5

        public ActionResult Verify(Guid id)
        {
            Activity activity = db.Activity.Find(id);
            return View(activity);
        }

        //
        // POST: /DonationItem/Edit/5

        [HttpPost]
        public ActionResult Verify(Activity activity)
        {
            if (activity.ActivityID != null)
            {
                Activity temp = db.Activity.Find(activity.ActivityID);
                if (Request.Params.Get("type") == "pass")
                {
                    temp.VerifyState = "SUCCESS";

                    String[] date = temp.CreateDate.ToShortDateString().Split('/');
                    String year = date[0];
                    String month = date[1];
                    String day = date[2];
                    String time = year + "年" + month + "月" + day + "日";
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "活动提案审核通过！";
                    l.Content = "您于" + time + "提出的主题为“" + temp.ActivityName + "”的活动提案已经通过审核！详细信息请查看已审方案。";
                    l.ReceiverID = temp.CreatorID;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();

                    if (temp.memberList != null)
                    {
                        foreach(ActivityMember am in temp.memberList)
                        {
                            //发送站内信
                            Letter ll = new Letter();
                            ll.LetterID = Guid.NewGuid();
                            ll.IsRead = "未读";
                            ll.Title = "新的活动日程安排！";
                            ll.Content = "您收到了主题为“" + temp.ActivityName + "”的联谊活动的通知！请按时参加！详细信息请查看活动日程。";
                            ll.ReceiverID = am.UserID;
                            ll.SenderID = Utils.CurrentUser(this).Id;
                            ll.SenderDate = DateTime.Now;
                            db.Letter.Add(ll);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    String Cause = Request.Params.Get("Cause");
                    temp.Cause = Cause;
                    temp.VerifyState = "FAILURE";

                    String[] date = temp.CreateDate.ToShortDateString().Split('/');
                    String year = date[0];
                    String month = date[1];
                    String day = date[2];
                    String time = year + "年" + month + "月" + day + "日";

                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "活动提案审核未通过";
                    l.Content = "很遗憾，您于" + time + "提出的主题为“" + temp.ActivityName + "”的活动提案审核未通过！原因为:" + Cause;
                    l.ReceiverID = temp.CreatorID;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("VerifyList");
            }
            return View(activity);
        }
        
        //
        // GET: /Activity/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Activity activity = db.Activity.Find(id);
            return View(activity);
        }

        //
        // POST: /Activity/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Activity activity)
        {
            String startDate = Request.Params.Get("ActivityStartDate");
            DateTime start = Convert.ToDateTime(startDate);
            String nowDate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime now = Convert.ToDateTime(nowDate);
            if (start.CompareTo(Convert.ToDateTime(now)) <= 0)
            {
                ModelState.AddModelError("ActivityStartDateError", "所选择日期不恰当，请选择恰当的日期(大于当前日期)");
                return View();
            }
            if (ModelState.IsValid)
            {
                Activity temp = db.Activity.Find(activity.ActivityID);
                temp.ActivityName = activity.ActivityName;
                temp.ActivityStartDate = activity.ActivityStartDate;
                temp.ActivityAddr = activity.ActivityAddr;
                temp.PersonInCharge = activity.PersonInCharge;
                temp.Telephone = activity.Telephone;
                temp.ActivityArrangement = activity.ActivityArrangement;
                temp.ModifierID = Utils.CurrentUser(this).Id;
                temp.ModifyDate = DateTime.Now;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyActivityList");
            }
            return View(activity);
        }

        //
        // GET: /Activity/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Activity activity = db.Activity.Find(id);
            return View(activity);
        }

        //
        // POST: /Activity/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Activity activity = db.Activity.Find(id);
            db.Activity.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("MyActivityList");
        }

        //管理员个人空间点击“联谊活动提案审核”
        public ActionResult VerifyList()
        {
            var result = Utils.PageIt(this, db.Activity.Where(t => t.isDeleted == false).Where(t => t.VerifyState == "SUBMIT")
                .OrderBy(t => t.CreateDate)).ToList();

            return View(result);
        }

        //个人空间-“待审方案”
        public ActionResult VerifyingList()
        {
            User user = Utils.CurrentUser(this);
            var result = Utils.PageIt(this, db.Activity.Where(r => r.CreatorID == user.Id).Where(r => r.VerifyState == "SUBMIT")
                .OrderBy(r => r.CreateDate)).ToList();

            return View(result);
        }

        //个人空间-“已审方案”
        public ActionResult VerifiedList()
        {
            User user = Utils.CurrentUser(this);
            var result = from r in db.Activity
                         where r.CreatorID == user.Id && (r.VerifyState == "SUCCESS" || r.VerifyState == "FAILURE")
                         orderby r.CreateDate
                         select r;

            return View(Utils.PageIt(this,result).ToList());
        }

        //创建完后，列表显示，修改，删除，提交
        public ActionResult MyActivityList()
        {
            User user = Utils.CurrentUser(this);
            var result = Utils.PageIt(this, db.Activity.Where(r => r.CreatorID == user.Id).Where(r => r.VerifyState == "NO")
                .OrderBy(r => r.CreateDate)).ToList();

            return View(result);
        }

        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}