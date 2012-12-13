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
    public class RoleController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Role/

        public ViewResult Index()
        {
            //return View(db.Role.Where(r=>r.Id!="admin").ToList());
            return View(db.Role.ToList());
        }

        //
        // GET: /Role/Details/5

        public ViewResult Details(String id)
        {
            Role role = db.Role.Find(id);
            return View(role);
        }

        //
        // GET: /Role/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Role/Create

        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                role.CreateDate = DateTime.Now;
                role.isDelete = false;
                role.CanDelete = true;
                db.Role.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(role);
        }
        

        //
        // GET: /Role/Delete/5

        public ActionResult Delete(String id)
        {
            Role role = db.Role.Find(id);
            return View(role);
        }

        //
        // POST: /Role/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(String id)
        {            
            Role role = db.Role.Find(id);
            db.Role.Remove(role);
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