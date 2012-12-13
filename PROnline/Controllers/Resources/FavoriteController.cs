using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Resources;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.Resources
{ 
    public class FavoriteController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Favorite/

        public ViewResult Index()
        {
            Guid id = Utils.CurrentUser(this).Id;
            var result = db.Favorite.Where(usr => usr.CreatorId == id).ToList();
            return View(result);
        }

        //
        // GET: /Favorite/Details/5

        public ViewResult Details(Guid id)
        {
            Favorite favorite = db.Favorite.Find(id);
            return View(favorite);
        }

        //
        // GET: /Favorite/Create

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateFrom()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateFrom(Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                String from = Request.Params["from"];
                favorite.FavoriteID = Guid.NewGuid();
                favorite.ModifyDate = favorite.CreateDate = DateTime.Now;
                favorite.isDeleted = false;
                favorite.ModifierId = favorite.CreatorId = Utils.CurrentUser(this).Id;
                db.Favorite.Add(favorite);
                db.SaveChanges();
                if (from != null && from != "")
                {
                    return Redirect(from);
                }
                return RedirectToAction("Index");
            }

            return View(favorite);
        }


        //
        // POST: /Favorite/Create

        [HttpPost]
        public ActionResult Create(Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                String from=Request.Params["from"];
                favorite.FavoriteID = Guid.NewGuid();
                favorite.ModifyDate = favorite.CreateDate = DateTime.Now;
                favorite.isDeleted = false;
                favorite.ModifierId=favorite.CreatorId = Utils.CurrentUser(this).Id;
                db.Favorite.Add(favorite);
                db.SaveChanges();
                if (from != null && from != "")
                {
                    return Redirect(from);
                }
                return RedirectToAction("Index");  
            }

            return View(favorite);
        }
        
        //
        // GET: /Favorite/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Favorite favorite = db.Favorite.Find(id);
            return View(favorite);
        }

        //
        // POST: /Favorite/Edit/5

        [HttpPost]
        public ActionResult Edit(Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                Favorite favoriteX = db.Favorite.Find(favorite.FavoriteID);
                favoriteX.FavoriteName = favorite.FavoriteName;
                favoriteX.ModifyDate = DateTime.Now;
                favoriteX.Commnet = favorite.Commnet;
                db.Entry(favoriteX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(favorite);
        }

        //
        // GET: /Favorite/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Favorite favorite = db.Favorite.Find(id);
            return View(favorite);
        }

        //
        // POST: /Favorite/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Favorite favorite = db.Favorite.Find(id);
            db.Favorite.Remove(favorite);
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