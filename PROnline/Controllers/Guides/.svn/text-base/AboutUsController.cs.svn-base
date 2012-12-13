using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models;
using PROnline.Models.Guides;
using PROnline.Src;
using System.Data;

namespace PROnline.Controllers.Guides
{
    public class AboutUsController : Controller
    {
        //
        // GET: /AboutUs/

        PROnlineContext db = new PROnlineContext();

        public ActionResult Index()
        {
            var list = db.AboutUs.ToList();
            return View(list);
        }

        //
        // GET: /AboutUs/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Top()
        {
            var result = db.AboutUs.OrderBy(r => r.CreateDate).ToList();
            return View(result);
        }

        //
        // GET: /AboutUs/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AboutUs/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(AboutUs au)
        {
            try
            {
                au.ID = Guid.NewGuid();
                au.CreateDate = DateTime.Now;
                db.AboutUs.Add(au);
         
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /AboutUs/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            var result = db.AboutUs.Find(id);
            return View(result);
        }

        //
        // POST: /AboutUs/Edit/5

        [HttpPost]
        public ActionResult Edit(AboutUs au)
        {
            db.Entry(au).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /AboutUs/Delete/5
 
        public ActionResult Delete(int id)
        {
            var result = db.AboutUs.Find(id);
            return View(result);
        }

        //
        // POST: /AboutUs/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var result = db.AboutUs.Find(id);
            db.AboutUs.Remove(result);
            return RedirectToAction("Index");
        }

        public ActionResult Show(Guid id)
        {
            ViewBag.list = db.AboutUs.ToList();
            var result = db.AboutUs.Find(id);
            return View(result);
        }

    }
}
