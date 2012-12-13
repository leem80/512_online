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
    public class UserRoleController : Controller
    {
        //
        // GET: /UserRole/
        private PROnlineContext db = new PROnlineContext();

        public ActionResult Index()
        {
            var users = Utils.PageIt(this, db.Users.Include("Role").Where(r => r.UserTypeId == UserType.MANAGER&& !r.isDeleted)
                .OrderBy(r => r.CreateDate)).ToList();
            return View(users);
        }

        public ActionResult Change(Guid id)
        {
            Administrator admin = db.Administrator.Include("User")
                .Where(r => r.UserID == id).Single();
            var roles = db.Role.Where(r => r.CanDelete||r.Id==Role.MANAGER)
                .Select(r=>new SelectListItem{Value=r.Id,Text=r.Note}).ToList();
            ViewBag.roles = roles;
            return View(admin);
        }

        [HttpPost]
        public ActionResult Change(Guid UserId, String RoleId)
        {
            User usr = db.Users.Find(UserId);
            usr.RoleId = RoleId;
            db.Entry(usr).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
