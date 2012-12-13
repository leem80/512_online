using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Donation;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.Donations
{ 
    public class DonationTypeController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /DonationType/

        public ViewResult Index()
        {
            return View(db.DonationType.Where(d=>d.isDeleted==false).ToList());
        }

        //下拉框，未删除的捐助类型
        public SelectList DonationTypeDropDownList()
        {
            var list = from c in db.DonationType
                       where c.isDeleted == false
                       orderby c.TypeName
                       select c;
            return new SelectList(list.ToList(), "DonationTypeID", "TypeName");
        }

        //
        // GET: /DonationType/Details/5

        public ViewResult Details(Guid id)
        {
            DonationType donationtype = db.DonationType.Find(id);
            return View(donationtype);
        }

        //类型名称重复验证
        [HttpGet]
        public JsonResult DonationTypeNameAvailable(String TypeName, Guid DonationTypeID)
        {
            DonationType noticetype = db.DonationType.Find(DonationTypeID);
            if (DonationTypeID == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                //create
                var list = from c in db.DonationType
                           where c.TypeName == TypeName && c.isDeleted == false
                           select c;
                return Json(!(list.Count() > 0), JsonRequestBehavior.AllowGet);

            }
            else
            {
                //edit
                var list = from c in db.DonationType
                           where c.TypeName == TypeName && c.DonationTypeID != DonationTypeID && c.isDeleted == false
                           select c;
                return Json(!(list.Count() > 0), JsonRequestBehavior.AllowGet);
            }
        } 

        //
        // GET: /DonationType/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /DonationType/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DonationType donationtype)
        {
            if (ModelState.IsValid)
            {
                donationtype.DonationTypeID = Guid.NewGuid();
                donationtype.CreatorID = Utils.CurrentUser(this).Id;
                donationtype.CreateDate = DateTime.Now;
                donationtype.isDeleted = false;
                db.DonationType.Add(donationtype);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(donationtype);
        }
        
        //
        // GET: /DonationType/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            DonationType donationtype = db.DonationType.Find(id);
            return View(donationtype);
        }

        //
        // POST: /DonationType/Edit/5

        [HttpPost]
        public ActionResult Edit(DonationType donationtype)
        {
            if (ModelState.IsValid)
            {
                DonationType temp = db.DonationType.Find(donationtype.DonationTypeID);
                temp.TypeName = donationtype.TypeName;
                temp.Introduction = donationtype.Introduction;
                temp.ModifierID = Utils.CurrentUser(this).Id;
                temp.ModifyDate = DateTime.Now;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donationtype);
        }

        //
        // GET: /DonationType/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            DonationType donationtype = db.DonationType.Find(id);
            return View(donationtype);
        }

        //
        // POST: /DonationType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            DonationType donationtype = db.DonationType.Find(id);
            donationtype.isDeleted = true;
            db.Entry(donationtype).State = EntityState.Modified;
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