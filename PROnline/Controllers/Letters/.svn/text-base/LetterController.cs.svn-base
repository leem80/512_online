using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Letters;
using PROnline.Models.Users;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.Letters
{ 
    public class LetterController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Letter/
        //当前用户站内信列表
        public ViewResult Index()
        {
            //当前用户的站内信
            Guid id = Utils.CurrentUser(this).Id;
            var letter = Utils.PageIt(this, 10, db.Letter.Where(l => l.ReceiverID == id).OrderBy(l => l.IsRead)
                .ThenByDescending(l => l.SenderDate));
            return View(letter.ToList());
        }

        //
        // GET: /Letter/Details/5

        public ViewResult Details(Guid id)
        {
            Letter letter = db.Letter.Find(id);
            letter.IsRead = "已读";
            db.Entry(letter).State = EntityState.Modified;
            db.SaveChanges();

            return View(letter);
        }

        public ActionResult Create()
        {
//             ViewBag.SenderID = new SelectList(db.Users, "Id", "UserName");
//             ViewBag.ReceiverID = new SelectList(db.Users, "Id", "UserName");
//             String tem1 = Request.Params.Get("temp1");
//             String tem2 = Request.Params.Get("temp2");
            String sID = Request.Params.Get("sID");
            String rID = Request.Params.Get("rID");
            ViewBag.sID = sID;
            ViewBag.rID = rID;
            return View();
        }


        public ActionResult Top()
        {
            User user = Utils.CurrentUser(this);
            var result = db.Letter.Where(r => r.ReceiverID == user.Id).Where(r=>r.IsRead=="未读")
                .OrderByDescending(r => r.SenderDate).Take(5).ToList();
            return View(result);
        }

        //
        // POST: /Letter/Create

        [HttpPost]
        public ActionResult Create(Letter letter)
        {
            if (ModelState.IsValid)
            {
                String sID = Request.Params.Get("sID");
                String rID = Request.Params.Get("rID");

                letter.IsRead = "未读";
                letter.LetterID = Guid.NewGuid();
                letter.SenderID = Guid.Parse(sID);
                letter.ReceiverID = Guid.Parse(rID);
                letter.SenderDate = DateTime.Now;
                db.Letter.Add(letter);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

//             ViewBag.SenderID = new SelectList(db.Users, "Id", "UserName", letter.SenderID);
//             ViewBag.ReceiverID = new SelectList(db.Users, "Id", "UserName", letter.ReceiverID);
            return View(letter);
        }
        
        //
        // GET: /Letter/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Letter letter = db.Letter.Find(id);
            ViewBag.SenderID = new SelectList(db.Users, "Id", "UserName", letter.SenderID);
            ViewBag.ReceiverID = new SelectList(db.Users, "Id", "UserName", letter.ReceiverID);
            return View(letter);
        }

        //
        // POST: /Letter/Edit/5

        [HttpPost]
        public ActionResult Edit(Letter letter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(letter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SenderID = new SelectList(db.Users, "Id", "UserName", letter.SenderID);
            ViewBag.ReceiverID = new SelectList(db.Users, "Id", "UserName", letter.ReceiverID);
            return View(letter);
        }

        //
        // GET: /Letter/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Letter letter = db.Letter.Find(id);
            return View(letter);
        }

        //
        // POST: /Letter/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Letter letter = db.Letter.Find(id);
            db.Letter.Remove(letter);
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