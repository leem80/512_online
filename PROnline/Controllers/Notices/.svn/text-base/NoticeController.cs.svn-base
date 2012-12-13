using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Notices;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.Notices
{ 
    public class NoticeController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Notice/

        public ViewResult Index()
        {
            ViewBag.User = Utils.CurrentUser(this);
            var list = from c in db.Notice
                       where c.isDeleted==false
                       orderby c.CreateDate
                       select c;
            var result = Utils.PageIt(this, list.OrderByDescending(r => r.CreateDate));
            return View(list.ToList());
        }


        public ActionResult ZM()
        {
            List<Notice> list = db.Notice.Where(n => n.isDeleted == false&&n.NoticeType.TypeName=="志愿者招募")
                .OrderByDescending(n => n.CreateDate).Take(MvcApplication.TopCount).ToList();
            return View(list);
        }
        public ActionResult Top()
        {
            List<Notice> list = db.Notice.Where(n => n.isDeleted == false).OrderByDescending(n => n.CreateDate).Take(MvcApplication.TopCount).ToList();
            if (list.Count != 0)
            {
                ViewBag.isShow = true;
                return View(list);
            }
            ViewBag.isShow = false;
            return View();
        }

        //公告浏览
        public ActionResult NoticeBrowse()
        {
            var typelist = from type in db.NoticeType
                           where type.isDeleted == false
                           orderby type.TypeName
                           select type;           
            ViewBag.typelist = typelist.ToList();
            ViewBag.top = "公告类型";
            ViewBag.typename = "公告栏";
            if (typelist.ToList().Count > 0)
            {
                NoticeType noticeType = typelist.ToList().First();
                ViewBag.typename = noticeType.TypeName;
                var result = Utils.PageIt(this, db.Notice.Where(n => n.isDeleted == false).Where(n => n.NoticeTypeID == noticeType.NoticeTypeID).OrderByDescending(n => n.CreateDate)).ToList();
                return View(result);
            }
            return View(new List<Notice>());
            
        }

        //浏览，显示指定公告类型的公告
        public ViewResult Browse(Guid id)
        {
            var typelist = from type in db.NoticeType
                           where type.isDeleted == false
                           orderby type.TypeName
                           select type;
            ViewBag.typelist = typelist.ToList();

            ViewBag.top = "公告类型";
            NoticeType noticetype = db.NoticeType.Find(id);
            ViewBag.typename = noticetype.TypeName;
            
            var result = Utils.PageIt(this, db.Notice.Where(n => n.isDeleted == false).Where(n => n.NoticeTypeID == id).OrderByDescending(n => n.CreateDate)).ToList();

            return View("NoticeBrowse",result);
        }

        //
        // GET: /Notice/Details/5

        public ViewResult Details(Guid id)
        {
            Notice notice = db.Notice.Find(id);
            notice.BrowsingNum += 1;
            db.Entry(notice).State = EntityState.Modified;
            db.SaveChanges();
            return View(notice);
        }

        //
        // GET: /Notice/Create

        public ActionResult Create()
        {
            NoticeTypeController controller = new NoticeTypeController();
            ViewBag.list = controller.NoticeTypeDropDownList();
            return View();
        } 
      
        //
        // POST: /Notice/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Notice notice)
        {
            if (ModelState.IsValid)
            {
                notice.NoticeID = Guid.NewGuid();
                notice.CreatorID = Utils.CurrentUser(this).Id;
                notice.CreateDate = DateTime.Now;
                notice.isDeleted = false;
                db.Notice.Add(notice);
                db.SaveChanges();
                return RedirectToAction("Index","NoticeType");  
            }

            return View(notice);
        }
        
        //
        // GET: /Notice/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Notice notice = db.Notice.Find(id);
            return View(notice);
        }

        //
        // POST: /Notice/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Notice notice)
        {
            if (ModelState.IsValid)
            {
                Notice temp = db.Notice.Find(notice.NoticeID);
                Guid noticeType = temp.NoticeTypeID;
                temp.Title = notice.Title;
                temp.Content = notice.Content;
                temp.ModifierID = Utils.CurrentUser(this).Id;
                temp.ModifyDate = DateTime.Now;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

                Response.Redirect("/NoticeType/Details/" + noticeType.ToString());   
                return RedirectToAction("Index");
            }
            return View(notice);
        }

        //
        // GET: /Notice/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Notice notice = db.Notice.Find(id);
            return View(notice);
        }

        //
        // POST: /Notice/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {        
            Notice notice = db.Notice.Find(id);
            Guid noticeType = notice.NoticeTypeID;
            notice.isDeleted = true;
            notice.Content = notice.Content;
            notice.ModifierID = Utils.CurrentUser(this).Id;
            db.Entry(notice).State = EntityState.Modified;
            db.SaveChanges();
            Response.Redirect("/NoticeType/Details/"+noticeType.ToString());    
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}