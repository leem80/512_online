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
    public class FavoriteResourceController : Controller
    {
        PROnlineContext db = new PROnlineContext();
        //
        // GET: /FavoriteResource/

        public ActionResult Index(Guid FavoriteId)
        {
            var result = Utils.PageIt(this,db.FavoriteResource.Where(r => r.FavoriteId == FavoriteId)
                .OrderBy(r => r.CreateDate)).ToList();
            return View(result);
        }

        //
        // GET: /FavoriteResource/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Top()
        {
            var user=Utils.CurrentUser(this);
            if (user != null)
            {
                var result = db.FavoriteResource.Include("Favorite")
                    .Where(r => r.UserId == user.Id).Take(MvcApplication.TopCount).ToList();
                return View(result);
            }
            else
            {
                return View(new List<FavoriteResource>());
            }
        }

        //
        // GET: /FavoriteResource/Add

        public ActionResult Add()
        {
            PROnline.Models.Users.User user = Utils.CurrentUser(this);
            if (user == null)
            {
                return Redirect("/Account/Login");
            }
            String url = Request.Params["resource"];
            string name = Request.Params["typeName"];

            List<Favorite> list = db.Favorite.Where(r => r.CreatorId == user.Id).ToList();
            List<SelectListItem> flist = new List<SelectListItem>();
            foreach (Favorite item in list)
            {
                flist.Add(new SelectListItem { Text = item.FavoriteName, Value = item.FavoriteID.ToString() });
            }
            ViewBag.resource = url;
            ViewBag.name = Request.Params["title"];
            ViewBag.typeName = name;
            ViewBag.FavoriteList = flist;
            return View();
        }

        //
        // POST: /FavoriteResource/Add

        [HttpPost]
        public ActionResult Add(FavoriteResource fr)
        {
            PROnline.Models.Users.User  user= Utils.CurrentUser(this);
            if (user == null)
            {
                return Redirect("/Account/Login");
            }

            fr.CreateDate = DateTime.Now;
            fr.FavoriteResourceID = Guid.NewGuid();
            fr.UserId = Utils.CurrentUser(this).Id;
            db.FavoriteResource.Add(fr);
            db.SaveChanges();
            return Redirect(fr.Url);
        }




        public ActionResult Create()
        {

            String url = Request.Params["resource"];
            var user=Utils.CurrentUser(this);

            List<Favorite> list = db.Favorite.Where(r => r.CreatorId == user.Id).ToList();
            List<SelectListItem> flist = new List<SelectListItem>();
            foreach (Favorite item in list)
            {
                flist.Add(new SelectListItem { Text = item.FavoriteName, Value = item.FavoriteID.ToString() });
            }
            ViewBag.resource = url;
            ViewBag.name = Request.Params["title"];
            ViewBag.FavoriteList = flist;
            return View();
        } 

        //
        // POST: /FavoriteResource/Create

        [HttpPost]
        public ActionResult Create(FavoriteResource fr)
        {
            fr.CreateDate = DateTime.Now;
            fr.FavoriteResourceID = Guid.NewGuid();
            fr.UserId = Utils.CurrentUser(this).Id;
            db.FavoriteResource.Add(fr);
            db.SaveChanges();
            return RedirectToAction("Index", "Favorite");
        }
        
        //
        // GET: /FavoriteResource/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            var user = Utils.CurrentUser(this);
            List<Favorite> list = db.Favorite.Where(r => r.CreatorId == user.Id).ToList();
            List<SelectListItem> flist = new List<SelectListItem>();
            foreach (Favorite item in list)
            {
                flist.Add(new SelectListItem { Text = item.FavoriteName, Value = item.FavoriteID.ToString() });
            }

            ViewBag.FavoriteList = flist;
            return View(db.FavoriteResource.Find(id));
        }

        //
        // POST: /FavoriteResource/Edit/5

        [HttpPost]
        public ActionResult Edit(FavoriteResource fr)
        {
            var frdb = db.FavoriteResource.Find(fr.FavoriteResourceID);
            frdb.Title = fr.Title;
            frdb.Url = fr.Url;
            frdb.Commnet = fr.Commnet;
            try
            {
                db.Entry(frdb).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "FavoriteResource", new { FavoriteId=frdb.FavoriteId});
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FavoriteResource/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            return View(db.FavoriteResource.Find(id));
        }

        //
        // POST: /FavoriteResource/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            { 
                var rf = db.FavoriteResource.Find(id);
                db.Entry(rf).State = EntityState.Deleted;
                db.SaveChanges();

                return RedirectToAction("Index", "FavoriteResource", new {FavoriteId=rf.FavoriteId });
            }
            catch
            {
                return RedirectToAction("Index", "Favorite");
            }
        }
    }
}
