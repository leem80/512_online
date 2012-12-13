using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.WorkStations;
using PROnline.Models.Teams;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.WorkStations
{
    public class WorkStationController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /WorkStation/

        public ActionResult Index()
        {
            ViewBag.list = Utils.PageIt(this, db.WorkStation.Where(t => t.isDeleted == false)
                   .OrderBy(t => t.CreateDate)).ToList();
            return View();
        }


        public JsonResult NameCheck()
        {
            String name = Request.Params["WorkStationName"];

            int count = db.WorkStation.Where(r => r.WorkStationName == name).Count();

            return Json(count == 0, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /WorkStation/Details/5

        public ActionResult Details(Guid id)
        {
            WorkStation result = db.WorkStation.Find(id);
            return View(result);
        }

        //
        // GET: /WorkStation/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /WorkStation/Create

        [HttpPost]
        public ActionResult Create(WorkStation workstation)
        {
            if (ModelState.IsValid)
            {
                WorkStation temp = new WorkStation();
                temp.WorkStationID = Guid.NewGuid();
                temp.WorkStationName = workstation.WorkStationName;
                temp.WorkStationerName = workstation.WorkStationerName;
                temp.Telephone = workstation.Telephone;
                temp.CreateDate = DateTime.Now;
                temp.CreatorID = Utils.CurrentUser(this).Id;
                temp.isDeleted = false;

                db.WorkStation.Add(temp);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
        
        //
        // GET: /WorkStation/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            WorkStation result = db.WorkStation.Find(id);
            return View(result);
        }

        //
        // POST: /WorkStation/Edit/5

        [HttpPost]
        public ActionResult Edit(WorkStation workstation)
        {
            String wName = Request.Form.Get("WorkStationName");
            Guid wid = Guid.Parse(Request.Form.Get("WorkStationID"));
            int count = db.WorkStation.Where(r => r.WorkStationName == wName).ToList().Count();
            if (count != 0)
            {
                if (count == 1)
                {
                    Guid temp = db.WorkStation.Where(r => r.WorkStationName == wName).Single().WorkStationID;
                    if (temp.CompareTo(wid) != 0)
                    {
                        ModelState.AddModelError("Error", "工作站名不能重复");
                        return View(workstation);
                    }
                }
                else if (count > 1)
                {
                    ModelState.AddModelError("Error", "工作站名不能重复");
                    return View(workstation);
                }
            }

            if (ModelState.IsValid)
            {
                Guid id = Guid.Parse(Request.Form.Get("WorkStationID"));

                WorkStation temp = db.WorkStation.Find(id);
                temp.WorkStationName = workstation.WorkStationName;
                temp.WorkStationerName = workstation.WorkStationerName;
                temp.Telephone = workstation.Telephone;
                temp.ModifierID = Utils.CurrentUser(this).Id;
                temp.ModifyDate = DateTime.Now;

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        //
        // GET: /WorkStation/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            WorkStation result = db.WorkStation.Find(id);
            return View(result);
        }

        //
        // POST: /WorkStation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            WorkStation temp = db.WorkStation.Find(id);
            temp.isDeleted = true;

            var list = db.Team.Where(r => r.WorkStationID == id).ToList();
            if (list.Count != 0)
            {
                foreach(Team item in list)
                {
                    item.WorkStationID = null;
                    item.WorkStation = null;
                    db.Entry(item).State = EntityState.Modified;
                }
            }
            db.WorkStation.Remove(temp);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Tips()
        {
            Guid id = Guid.Parse(Request.Params.Get("wID"));
            WorkStation w = db.WorkStation.Find(id);
            String wName = w.WorkStationName;
            String werName = w.WorkStationerName;
            String wTel = w.Telephone;
            String result = wName + werName + wTel;
            return Json(w, JsonRequestBehavior.AllowGet);
        }
    }
}
