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
    public class SchoolController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /School/
        //type:school学校数据字典管理、teachers列出添加老师按钮
        public ViewResult Index(String type)
        {
            ViewBag.type = type;
            if (type == "school")
                ViewBag.menu = "codes";
            else
                ViewBag.menu = "users";
            return View(db.School.Where(s=>s.isDeleted==false).ToList());
        }

        //下拉框，未删除的学校
        public SelectList SchoolDropDownList()
        {
            var list = from c in db.School
                       where c.isDeleted == false
                       orderby c.SchoolName
                       select c;
            return new SelectList(list.ToList(), "SchoolID", "SchoolName");
        }

        //
        // GET: /School/Details/5

        public ViewResult Details(Guid id)
        {
            School school = db.School.Find(id);
            return View(school);
        }

        public ViewResult SchoolNavigation()
        {
            String id = Request.Params.Get("SchoolID");
            School school = null;
            if (id != null)
            {
                school = db.School.Find(Guid.Parse(id));
            }
            ViewBag.SchoolDropDownList = SchoolDropDownList();
            return View(school);
        }

        //类型名称重复验证
        [HttpGet]
        public JsonResult SchoolNameAvailable(String SchoolName, Guid SchoolID)
        {
            if (SchoolID == Guid.Empty)
            {
                //create
                var list = from c in db.School
                           where c.SchoolName == SchoolName && c.isDeleted == false
                           select c;
                return Json(!(list.Count() > 0), JsonRequestBehavior.AllowGet);
            }
            else
            {
                //edit
                var list = from c in db.School
                           where c.SchoolName == SchoolName && c.SchoolID != SchoolID && c.isDeleted == false
                           select c;
                return Json(!(list.Count() > 0), JsonRequestBehavior.AllowGet);
            }
        } 

        //
        // GET: /School/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /School/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(School school)
        {
            if (ModelState.IsValid)
            {
                school.SchoolID = Guid.NewGuid();
                school.CreatorID = Utils.CurrentUser(this).Id;
                school.CreateDate = DateTime.Now;
                school.isDeleted = false;
                db.School.Add(school);
                db.SaveChanges();
                return RedirectToAction("Index", new { type="school"});  
            }

            return View(school);
        }
        
        //
        // GET: /School/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            School school = db.School.Find(id);
            return View(school);
        }

        //
        // POST: /School/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(School school)
        {
            if (ModelState.IsValid)
            {
                School temp = db.School.Find(school.SchoolID);
                temp.SchoolNo = school.SchoolNo;
                temp.SchoolName = school.SchoolName;
                temp.SchoolIntro = school.SchoolIntro;
                temp.SchoolCommnet = school.SchoolCommnet;
                temp.ModifierID = Utils.CurrentUser(this).Id;
                temp.ModifyDate = DateTime.Now;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { type = "school" });  
            }
            return View(school);
        }

        //
        // GET: /School/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            School school = db.School.Find(id);
            return View(school);
        }

        //
        // POST: /School/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            School school = db.School.Find(id);
            school.isDeleted = true;
            school.ModifierID = Utils.CurrentUser(this).Id;
            school.ModifyDate = DateTime.Now;
            db.Entry(school).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { type = "school" });  
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}