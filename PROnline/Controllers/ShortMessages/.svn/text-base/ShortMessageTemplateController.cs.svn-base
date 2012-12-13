using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.ShortMessages;
using PROnline.Models;
using PROnline.Src;
using PROnline.Models.Users;

namespace PROnline.Controllers.ShortMessages
{ 
    public class ShortMessageTemplateController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /ShortMessageTemplate/

        public ViewResult Index()
        {
            User user = Utils.CurrentUser(this);
            if (user.UserTypeId == UserType.ADMIN || user.UserTypeId == UserType.MANAGER)
            {
                ViewBag.userType = "admin";
            }
            else
            {
                ViewBag.userType = "supervisor";
            }

            var list = Utils.PageIt(this, db.ShortMessageTemplate.Where(l => l.isDelete == false)
                .OrderByDescending(l => l.Title)).ToList();
            return View(list);
        }

        //
        // GET: /ShortMessageTemplate/Details/5

        public ViewResult Details(Guid id)
        {
            ShortMessageTemplate shortmessagetemplate = db.ShortMessageTemplate.Find(id);
            return View(shortmessagetemplate);
        }

        //
        // GET: /ShortMessageTemplate/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ShortMessageTemplate/Create

        [HttpPost]
        public ActionResult Create(ShortMessageTemplate shortmessagetemplate)
        {
            if (ModelState.IsValid)
            {
                shortmessagetemplate.ShortMessageTemplateID = Guid.NewGuid();
                shortmessagetemplate.CreateDate = DateTime.Now;
                shortmessagetemplate.Creator = Utils.CurrentUser(this).Id;
                db.ShortMessageTemplate.Add(shortmessagetemplate);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(shortmessagetemplate);
        }
        
        //
        // GET: /ShortMessageTemplate/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            ShortMessageTemplate shortmessagetemplate = db.ShortMessageTemplate.Find(id);
            return View(shortmessagetemplate);
        }

        //
        // POST: /ShortMessageTemplate/Edit/5

        [HttpPost]
        public ActionResult Edit(ShortMessageTemplate shortmessagetemplate)
        {
            if (ModelState.IsValid)
            {
                ShortMessageTemplate temp = db.ShortMessageTemplate.Find(shortmessagetemplate.ShortMessageTemplateID);
                temp.Title = shortmessagetemplate.Title;
                temp.Content = shortmessagetemplate.Content;
                temp.ModifyDate = DateTime.Now;
                temp.Modifier = Utils.CurrentUser(this).Id;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shortmessagetemplate);
        }

        //
        // GET: /ShortMessageTemplate/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            ShortMessageTemplate shortmessagetemplate = db.ShortMessageTemplate.Find(id);
            return View(shortmessagetemplate);
        }

        //
        // POST: /ShortMessageTemplate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            ShortMessageTemplate shortmessagetemplate = db.ShortMessageTemplate.Find(id);
            shortmessagetemplate.isDelete = true;
            shortmessagetemplate.ModifyDate = DateTime.Now;
            shortmessagetemplate.Modifier = Utils.CurrentUser(this).Id;
            db.Entry(shortmessagetemplate).State = EntityState.Modified;

            db.ShortMessageTemplate.Remove(shortmessagetemplate);
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