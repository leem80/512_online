using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.HeartWishes;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.HeartWish
{ 
    public class HeartWishTypeController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /HeartWishType/

        public ViewResult Index()
        {
            ViewBag.User = Utils.CurrentUser(this);
            return View(db.HeartWishType.Where(n => n.isDeleted == false).ToList());
        }

        //下拉框，未删除的心声类型
        public SelectList HeartWishTypeDropDownList()
        {
            var list = from c in db.HeartWishType
                       where c.isDeleted == false
                       orderby c.TypeName
                       select c;
            return new SelectList(list.ToList(), "HeartWishTypeID", "TypeName");
        }


        //
        // GET: /HeartWishType/Details/5
        //得到一条心声类型条目，以及其属于的所有心声条目
        public ViewResult Details(Guid id)
        {
            HeartWishType type = db.HeartWishType.Find(id);
            var list = from c in db.HeartWish
                       where c.isDeleted == false && c.HeartWishTypeID == id && c.VerifyingState=="通过"
                       select c;
            ViewBag.list = list.ToList();
            return View(type);
        }

        //
        // GET: /HeartWishType/Create

        public ActionResult Create()
        {
            return View();
        }

        //类型名称重复验证
        [HttpGet]
        public JsonResult HeartWishTypeNameAvailable(String TypeName, Guid HeartWishTypeID)
        {
            if (HeartWishTypeID == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                //create
                var list = from c in db.HeartWishType
                           where c.TypeName == TypeName && c.isDeleted == false
                           select c;
                return Json(!(list.Count() > 0), JsonRequestBehavior.AllowGet);

            }
            else
            {
                //edit
                var list = from c in db.HeartWishType
                           where c.TypeName == TypeName && c.HeartWishTypeID != HeartWishTypeID && c.isDeleted == false
                           select c;
                return Json(!(list.Count() > 0), JsonRequestBehavior.AllowGet);
            }
        }

        //
        // POST: /HeartWishType/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(HeartWishType type)
        {
            if (ModelState.IsValid)
            {
                type.HeartWishTypeID = Guid.NewGuid();
                type.CreatorID = Utils.CurrentUser(this).Id;
                type.CreateDate = DateTime.Now;
                type.isDeleted = false;
                db.HeartWishType.Add(type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);
        }

        //
        // GET: /HeartWishType/Edit/5

        public ActionResult Edit(Guid id)
        {
            HeartWishType type = db.HeartWishType.Find(id);
            return View(type);
        }

        //
        // POST: /HeartWishType/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(HeartWishType type)
        {
            if (ModelState.IsValid)
            {
                HeartWishType temp = db.HeartWishType.Find(type.HeartWishTypeID);
                temp.TypeName = type.TypeName;
                temp.Introduction = type.Introduction;
                temp.ReplyUserType = type.ReplyUserType;
                temp.Comment = type.Comment;
                temp.ModifierID = Utils.CurrentUser(this).Id;
                temp.ModifyDate = DateTime.Now;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);
        }

        //
        // GET: /HeartWishType/Delete/5

        public ActionResult Delete(Guid id)
        {
            HeartWishType type = db.HeartWishType.Find(id);
            return View(type);
        }

        //
        // POST: /HeartWishType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            HeartWishType type = db.HeartWishType.Find(id);
            type.isDeleted = true;
            type.ModifierID = Utils.CurrentUser(this).Id;
            type.ModifyDate = DateTime.Now;
            db.Entry(type).State = EntityState.Modified;
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