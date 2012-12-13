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
    public class AdministratorController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Administrator/

        public ViewResult Index()
        {
            //得到未删除的管理员列表
            var administrator = db.Administrator.Where(admin => admin.User.isDeleted == false)
                .Where(admin => admin.User.UserTypeId != UserType.ADMIN)
                .OrderBy(r => r.User.RoleId).ThenBy(r => r.User.RealName);
            return View(administrator.ToList());
        }

        //
        // GET: /Administrator/Details/5
        //type：个人空间或者后台管理或者其它
        public ViewResult Details(Guid id, String type)
        {
            Administrator administrator = db.Administrator.Find(id);

            ViewBag.type = type;
            return View(administrator);
        }
        //
        // GET: /Administrator/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Administrator/Create

        [HttpPost]
        public ActionResult Create(Administrator administrator,String role)
        {
            if (Request.Params.Get("YYYY") == "" || Request.Params.Get("MM") == "" || Request.Params.Get("DD") == "")
            {
                ModelState.AddModelError("Birthday", "请选择生日！");
            }
            if (ModelState.IsValid)
            {
                administrator.User.Id = Guid.NewGuid();
                administrator.User.Password = Utils.GetMd5(administrator.User.Password);
                administrator.User.UserTypeId = UserType.MANAGER;
                administrator.User.RoleId = role;
                administrator.User.CreateDate = DateTime.Now;

                int year = int.Parse(Request.Params.Get("YYYY"));
                int month = int.Parse(Request.Params.Get("MM"));
                int day = int.Parse(Request.Params.Get("DD"));

                DateTime birthday = new DateTime(year, month, day);
                administrator.Birthday = birthday;
                administrator.AdministratorID = Guid.NewGuid();
                db.Administrator.Add(administrator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(administrator);
        }


              
        //
        // GET: /Administrator/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Administrator administrator = db.Administrator.Find(id);
            return View(administrator);
        }

        //
        // POST: /Administrator/Edit/5

        [HttpPost]
        public ActionResult Edit(Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                Administrator temp = db.Administrator.Find(administrator.AdministratorID);
                temp.Telephone = administrator.Telephone;
                temp.MobileTelephone = administrator.MobileTelephone;
                temp.Address = administrator.Address;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","Users");
            }
            return View(administrator);
        }

        //
        // GET: /Administrator/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Administrator administrator = db.Administrator.Find(id);
            return View(administrator);
        }

        //
        // POST: /Administrator/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Administrator administrator = db.Administrator.Find(id);
            administrator.User.isDeleted = true;
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