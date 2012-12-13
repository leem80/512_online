using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using PROnline.Models;
using PROnline.Models.Users;
using PROnline.Models.Letters;
using PROnline.Models.Activities;
using PROnline.Src;

namespace PROnline.Controllers.Activities
{
    public class ActivityContributeController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /ActivityContribute/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Top()
        {
            var result = db.ActivityContribute.Where(r => r.State == "已通过").
                OrderByDescending(r => r.CreateDate).ThenBy(r => r.Title).Take(MvcApplication.ListSize).ToList();

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

        //公告模块显示的活动风采
        public ActionResult List()
        {
            var result = Utils.PageIt(this, 10, db.ActivityContribute.Where(r => r.State == "已通过")
                .OrderByDescending(r => r.CreateDate).ThenBy(r => r.Title)).ToList();

            return View(result);
        }

        //公共模块活动风采详细内容
        public ActionResult Show(Guid id)
        {
            ActivityContribute ac = db.ActivityContribute.Find(id);

            User user = db.Users.Find(ac.CreatorID);
            ViewBag.RealName = user.RealName;

            return View(ac);
        }

        //风采审核界面
        public ActionResult VerifyList()
        {
            var result = Utils.PageIt(this, db.ActivityContribute.Where(r => r.State == "待审核")
                .OrderBy(r => r.CreateDate)).ToList();

            Dictionary<Guid, String> dic = new Dictionary<Guid, String>();
            foreach (var item in result)
            {
                User user = db.Users.Find(item.CreatorID);
                dic.Add(item.ActivityContributeID, user.RealName);
            }
            ViewBag.dic = dic;

            return View(result);
        }

        //id为投稿id，type为审核类型，pass为通过，否则为不通过
        public ActionResult Verify(Guid id, String type)
        {
            ActivityContribute ac = db.ActivityContribute.Find(id);
            ViewBag.type = type;

            User user = db.Users.Find(ac.CreatorID);
            ViewBag.name = user.RealName;

            return View(ac);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Verify(Guid id)
        {
            String type = Request.Params.Get("type");
            ActivityContribute ac = db.ActivityContribute.Find(id);

            if (type == "pass")
            {
                ac.State = "已通过";

                String[] date = ac.CreateDate.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                //发送站内信
                Letter l = new Letter();
                l.LetterID = Guid.NewGuid();
                l.IsRead = "未读";
                l.Title = "风采投稿审核通过！";
                l.Content = "您于" + time + "提出的主题为“" + ac.Title + "”的风采投稿已经通过审核！";
                l.ReceiverID = ac.CreatorID;
                l.SenderID = Utils.CurrentUser(this).Id;
                l.SenderDate = DateTime.Now;
                db.Letter.Add(l);
                db.SaveChanges();
            }
            else
            {
                String cause = Request.Form.Get("Cause");
                ac.State = "未通过";
                ac.Cause = cause;

                String[] date = ac.CreateDate.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                //发送站内信
                Letter l = new Letter();
                l.LetterID = Guid.NewGuid();
                l.IsRead = "未读";
                l.Title = "风采投稿审核未通过";
                l.Content = "很遗憾，您于" + time + "提出的主题为“" + ac.Title + "”的风采投稿审核未通过！原因为:" + cause;
                l.ReceiverID = ac.CreatorID;
                l.SenderID = Utils.CurrentUser(this).Id;
                l.SenderDate = DateTime.Now;
                db.Letter.Add(l);
                db.SaveChanges();
            }

            db.Entry(ac).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("VerifyList");
        }
        //
        // GET: /ActivityContribute/Details/5

        public ActionResult Details(Guid id)
        {
            ActivityContribute ac = db.ActivityContribute.Find(id);
            User user = db.Users.Find(ac.CreatorID);
            ViewBag.name = user.RealName;

            return View(ac);
        }

        //
        // GET: /ActivityContribute/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ActivityContribute/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ActivityContribute ac, String Content)
        {
            if (Content == null || Content == "")
            {
                ModelState.AddModelError("ContentError", "风采投稿内容不能为空!");
                return View();
            }

//             if (Content == null || Content == "") 
//             {
//                 ModelState.AddModelError("ContentError", "风采投稿内容不能为空!");
//                 return View();
//             }

            ac.ActivityContributeID = Guid.NewGuid();
            ac.CreateDate = DateTime.Now;
            ac.CreatorID = Utils.CurrentUser(this).Id;
            ac.State = "草稿";

            db.ActivityContribute.Add(ac);
            db.SaveChanges();

            return RedirectToAction("MyDraftList");
        }

        //我的投稿=>查看已提交的投稿
        public ActionResult MyList()
        {
            User user = Utils.CurrentUser(this);
            var result = Utils.PageIt(this, db.ActivityContribute.Where(r => r.CreatorID == user.Id)
                .Where(r => r.State != "草稿").OrderBy(r => r.CreateDate)).ToList();

            return View(result);
        }

        //创建投稿时出现的界面=>查看草稿状态的投稿
        public ActionResult MyDraftList()
        {
            User user = Utils.CurrentUser(this);
            var result = Utils.PageIt(this, db.ActivityContribute.Where(r => r.CreatorID == user.Id)
                .Where(r => r.State == "草稿").OrderBy(r => r.CreateDate)).ToList();

            return View(result);
        }

        public ActionResult Submit(Guid id)
        {
            ActivityContribute ac = db.ActivityContribute.Find(id);
            ac.State = "待审核";
            ac.CreateDate = DateTime.Now;
            db.Entry(ac).State = EntityState.Modified;
            db.SaveChanges();

            String[] date = ac.CreateDate.ToShortDateString().Split('/');
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
                l.Title = "风采投稿审核！";
                l.Content = "志愿者小组长" + Utils.CurrentUser(this).RealName + "于" + time + "提出了主题为“" + ac.Title + "”的风采投稿，请尽快审核！";
                l.ReceiverID = u.Id;
                l.SenderID = Utils.CurrentUser(this).Id;
                l.SenderDate = DateTime.Now; ;
                db.Letter.Add(l);
                db.SaveChanges();
            }
            return RedirectToAction("MyList");
        }

        //
        // GET: /ActivityContribute/Edit/5

        public ActionResult Edit(Guid id)
        {
            ActivityContribute ac = db.ActivityContribute.Find(id);

            return View(ac);
        }

        //
        // POST: /ActivityContribute/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Guid id, String Content)
        {
//             if (Content == null || Content == "")
//             {
//                 ModelState.AddModelError("ContentError", "风采投稿内容不能为空!");
//                 return View();
//             }
            ActivityContribute ac = db.ActivityContribute.Find(id);
            String Title = Request.Form.Get("Title");
            //String Content = Request.Form.Get("Content");
            ac.Title = Title;
            ac.Content = Content;
            db.Entry(ac).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("MyDraftList");
        }

        //
        // GET: /ActivityContribute/Delete/5

        public ActionResult Delete(Guid id)
        {
            ActivityContribute ac = db.ActivityContribute.Find(id);

            return View(ac);
        }

        //
        // POST: /ActivityContribute/Delete/5

        [HttpPost]
        public ActionResult Delete()
        {
            Guid id = Guid.Parse(Request.Params.Get("ActivityContributeID"));
            ActivityContribute ac = db.ActivityContribute.Find(id);
            db.ActivityContribute.Remove(ac);
            db.SaveChanges();

            return RedirectToAction("MyDraftList");
        }
    }
}
