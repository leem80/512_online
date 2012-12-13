using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Notices;
using PROnline.Models.Users;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.Notices
{ 
    public class NoticeTypeController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /NoticeType/

        public ViewResult Index()
        {
            var data = Utils.PageIt(this,db.NoticeType.Where(n => n.isDeleted == false).OrderBy(r => r.TypeName)).ToList();
            return View(data);
        }

        //下拉框，未删除的公告类型
        public SelectList NoticeTypeDropDownList()
        {
            var list = from c in db.NoticeType
                       where c.isDeleted == false
                       orderby c.TypeName
                       select c;        
            return new SelectList(list.ToList(), "NoticeTypeID", "TypeName");
        }


        //
        // GET: /NoticeType/Details/5

        public ViewResult Details(Guid id)
        {
            NoticeType noticetype = db.NoticeType.Find(id);
            ViewBag.noticeList = Utils.PageIt(this, db.Notice.Where(n => n.NoticeTypeID == id).Where(n => n.isDeleted == false).OrderBy(r => r.CreateDate)).ToList();
            return View(noticetype);
        }

        //
        // GET: /NoticeType/Create

        public ActionResult Create()
        {
            return View();
        }

        //类型名称重复验证
        [HttpGet]
        public JsonResult NoticeTypeNameAvailable(String TypeName, Guid NoticeTypeID)
        {                   
            if (NoticeTypeID == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                //create
                var list = from c in db.NoticeType
                           where c.TypeName == TypeName && c.isDeleted==false
                           select c;
                return Json(!(list.Count() > 0), JsonRequestBehavior.AllowGet);
                
            }
            else
            {
                //edit
                var list = from c in db.NoticeType
                           where c.TypeName == TypeName && c.NoticeTypeID != NoticeTypeID && c.isDeleted == false
                           select c;
                return Json(!(list.Count() > 0), JsonRequestBehavior.AllowGet);             
            }        
        } 

        //
        // POST: /NoticeType/Create

        [HttpPost]
        public ActionResult Create(NoticeType noticetype)
        {
            if (ModelState.IsValid)
            {
                noticetype.NoticeTypeID = Guid.NewGuid();
                noticetype.CreatorID = Utils.CurrentUser(this).Id;
                noticetype.CreateDate = DateTime.Now;
                noticetype.isDeleted = false;
                db.NoticeType.Add(noticetype);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            return View(noticetype);
        }
        
        //
        // GET: /NoticeType/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            NoticeType noticetype = db.NoticeType.Find(id);
            return View(noticetype);
        }

        //
        // POST: /NoticeType/Edit/5

        [HttpPost]
        public ActionResult Edit(NoticeType noticetype)
        {
            if (ModelState.IsValid)
            {
                NoticeType temp = db.NoticeType.Find(noticetype.NoticeTypeID);
                temp.TypeName = noticetype.TypeName;
                temp.Introduction = noticetype.Introduction;
                temp.ModifyDate = DateTime.Now;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noticetype);
        }

        //
        // GET: /NoticeType/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            NoticeType noticetype = db.NoticeType.Find(id);
            return View(noticetype);
        }

        //
        // POST: /NoticeType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            NoticeType noticetype = db.NoticeType.Find(id);
            noticetype.isDeleted = true;
            noticetype.ModifyDate = DateTime.Now;
            db.Entry(noticetype).State = EntityState.Modified;
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