using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Users;
using PROnline.Models;

namespace PROnline.Controllers.Users
{ 
    public class GoodAtFieldController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /GoodAtField/

        public ViewResult Index()
        {
            return View(db.GoodAtField.ToList());
        }

        public List<GoodAtField> All()
        {
            return  db.GoodAtField.ToList();
        }

        //
        // GET: /GoodAtField/Details/5

        public ViewResult Details(Guid id)
        {
            GoodAtField goodatfield = db.GoodAtField.Find(id);
            return View(goodatfield);
        }

        //
        // GET: /GoodAtField/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /GoodAtField/Create

        [HttpPost]
        public ActionResult Create(GoodAtField goodatfield)
        {
            if (ModelState.IsValid)
            {
                goodatfield.GoodAtFieldID = Guid.NewGuid();
                db.GoodAtField.Add(goodatfield);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(goodatfield);
        }
        
        //
        // GET: /GoodAtField/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            GoodAtField goodatfield = db.GoodAtField.Find(id);
            return View(goodatfield);
        }

        //
        // POST: /GoodAtField/Edit/5

        [HttpPost]
        public ActionResult Edit(GoodAtField goodatfield)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goodatfield).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goodatfield);
        }

        //
        // GET: /GoodAtField/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            GoodAtField goodatfield = db.GoodAtField.Find(id);

            return View(goodatfield);
        }

        //
        // POST: /GoodAtField/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            GoodAtField goodatfield = db.GoodAtField.Find(id);
            db.GoodAtField.Remove(goodatfield);
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