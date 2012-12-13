using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Guides;
using PROnline.Models;
using PROnline.Src;
using System.Web.UI;


namespace PROnline.Controllers.Guides
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class GuideTypeController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /GuideType/

        public ViewResult Index()
        {
            var data = Utils.PageIt(this, db.GuideType.Where(n => n.isDeleted == false).OrderBy(r => r.TypeName)).ToList();
            return View(data);
        }

        //
        // GET: /GuideType/Details/5

        public ViewResult Details(Guid id)
        {
            GuideType guidetype = db.GuideType.Find(id);
            return View(guidetype);
        }

        //公告浏览
        public ActionResult GuideTypeBrowse()
        {
            var typelist = from type in db.GuideType
                           where type.isDeleted == false
                           orderby type.TypeName
                           select type;
            ViewBag.typelist = typelist.ToList();
            ViewBag.top = "新手导航";
            ViewBag.typename = "新手导航";
            if (typelist.ToList().Count > 0)
            {
                GuideType noticeType = typelist.ToList().First();
                ViewBag.typename = noticeType.TypeName;
                return View(noticeType);
            }
            return View();
        }

        //浏览，显示指定新手导航
        public ViewResult Browse(Guid id)
        {
            var typelist = from type in db.GuideType
                           where type.isDeleted == false
                           orderby type.TypeName
                           select type;
            ViewBag.typelist = typelist.ToList();

            ViewBag.top = "新手导航";
            GuideType noticetype = db.GuideType.Find(id);
            ViewBag.typename = noticetype.TypeName;

            return View("GuideTypeBrowse", noticetype);
        }


        //类型名称重复验证
        [HttpGet]
        public JsonResult GuideTypeNameAvailable(String TypeName, Guid GuideTypeID)
        {
            if (GuideTypeID == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                //create
                var list = from c in db.GuideType
                           where c.TypeName == TypeName && c.isDeleted == false
                           select c;
                return Json(!(list.Count() > 0), JsonRequestBehavior.AllowGet);

            }
            else
            {
                //edit
                var list = from c in db.GuideType
                           where c.TypeName == TypeName && c.GuideTypeID != GuideTypeID && c.isDeleted == false
                           select c;
                return Json(!(list.Count() > 0), JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /GuideType/Create
        
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GuideType/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(GuideType guidetype)
        {
            if (ModelState.IsValid)
            {
                guidetype.GuideTypeID = Guid.NewGuid();
                guidetype.CreatorID = Utils.CurrentUser(this).Id;
                guidetype.CreateDate = DateTime.Now;
                guidetype.isDeleted = false;
                db.GuideType.Add(guidetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guidetype);
        }

        //
        // GET: /GuideType/Edit/5

        public ActionResult Edit(Guid id)
        {
            GuideType guidetype = db.GuideType.Find(id);
            return View(guidetype);
        }

        //
        // POST: /GuideType/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(GuideType guidetype)
        {
            if (ModelState.IsValid)
            {
                GuideType temp = db.GuideType.Find(guidetype.GuideTypeID);
                temp.TypeName = guidetype.TypeName;
                temp.Introduction = guidetype.Introduction;
                temp.ModifyDate = DateTime.Now;
                temp.ModifierID = Utils.CurrentUser(this).Id;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guidetype);
        }

        //
        // GET: /GuideType/Delete/5

        public ActionResult Delete(Guid id)
        {
            GuideType guidetype = db.GuideType.Find(id);
            return View(guidetype);
        }

        //
        // POST: /GuideType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            GuideType guidetype = db.GuideType.Find(id);
            guidetype.isDeleted = true;
            guidetype.ModifyDate = DateTime.Now;
            guidetype.ModifierID = Utils.CurrentUser(this).Id;
            db.Entry(guidetype).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}