using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Users;
using PROnline.Models;
using PROnline.Src;
using System.Data;

namespace PROnline.Controllers.Users
{
    public class SchoolAdministratorController : Controller
    {

        PROnlineContext db = new PROnlineContext();
        //
        // GET: /SchoolAdministrator/

        public ActionResult Index()
        {
            var administrator = db.SchoolAdministrator.Where(admin => admin.User.isDeleted == false);
            return View(administrator.ToList());
        }


        public ViewResult Details(Guid id, String type)
        {
            SchoolAdministrator administrator = db.SchoolAdministrator.Find(id);

            ViewBag.type = type;
            return View(administrator);
        }


        public ActionResult Create()
        {
            ViewBag.SchoolID = new SelectList(db.School.Where(s => s.isDeleted == false).OrderBy(s => s.SchoolName), "SchoolID", "SchoolName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(SchoolAdministrator administrator)
        {
            if (Request.Params.Get("YYYY") == "" || Request.Params.Get("MM") == "" || Request.Params.Get("DD") == ""
                || Request.Params.Get("YYYY") == null || Request.Params.Get("MM") == null || Request.Params.Get("DD") == null)
            {
                ModelState.AddModelError("Birthday", "请选择生日！");
            }
            if (Request.Params.Get("User.Password") == "" || Request.Params.Get("User.Password") == null)
            {
                ModelState.AddModelError("PasswordError", "请输入初始密码！");
            }
            if (ModelState.IsValid)
            {
                administrator.User.Id = Guid.NewGuid();
                administrator.User.Password = Utils.GetMd5(administrator.User.Password);
                administrator.User.UserTypeId = UserType.SCHOOL_MANAGER;
                administrator.User.RoleId = Role.SCHOOL_MANAGER;
                administrator.User.CreateDate = DateTime.Now;
                administrator.User.RoleId = "school_manager";
                administrator.User.isDeleted = false;
                administrator.User.isHaveTraining = false;
                administrator.User.isVerify = true;
                db.Users.Add(administrator.User);

                int year = int.Parse(Request.Params.Get("YYYY"));
                int month = int.Parse(Request.Params.Get("MM"));
                int day = int.Parse(Request.Params.Get("DD"));

                DateTime birthday = new DateTime(year, month, day);

                administrator.ID = Guid.NewGuid();
                administrator.Birthday = birthday;
                db.SchoolAdministrator.Add(administrator);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolID = new SelectList(db.School.Where(s => s.isDeleted == false).OrderBy(s => s.SchoolName), "SchoolID", "SchoolName");
            return View(administrator);
        }



        //
        // GET: /Administrator/Edit/5

        public ActionResult Edit(Guid id)
        {
            SchoolAdministrator administrator = db.SchoolAdministrator.Find(id);
            return View(administrator);
        }

        //
        // POST: /Administrator/Edit/5

        [HttpPost]
        public ActionResult Edit(SchoolAdministrator administrator)
        {
            SchoolAdministrator temp = db.SchoolAdministrator.Find(administrator.ID);
            temp.User.RealName = administrator.User.RealName;
            temp.Gender = administrator.Gender;
            temp.Birthday = administrator.Birthday;
            temp.Telephone = administrator.Telephone;
            temp.MobileTelephone = administrator.MobileTelephone;
            temp.Address = administrator.Address;
            db.Entry(temp).State = EntityState.Modified;
            db.SaveChanges();
            if (Utils.CurrentUser(this).UserTypeId == UserType.ADMIN ||
                Utils.CurrentUser(this).UserTypeId == UserType.MANAGER)
            {
                return RedirectToAction("Index");
            } 
            else
            {
                return RedirectToAction("Details", "Users");
            }
        }

        //
        // GET: /Administrator/Delete/5

        public ActionResult Delete(Guid id)
        {
            SchoolAdministrator administrator = db.SchoolAdministrator.Find(id);
            return View(administrator);
        }

        //
        // POST: /Administrator/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SchoolAdministrator administrator = db.SchoolAdministrator.Find(id);
            administrator.User.isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SchoolAdministratorInfo(Guid id)
        {
            SchoolAdministrator administrator = db.SchoolAdministrator.Find(id);

            return View(administrator);
        }
    }
}
