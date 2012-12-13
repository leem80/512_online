using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models;
using PROnline.Models.Users;

namespace PROnline.Controllers.Users
{
    public class RoleMenuController : Controller
    {
        //
        // GET: /RoleMenu/
        PROnlineContext db = new PROnlineContext();

        public ActionResult Index()
        {
            String rid = Request.Params["rid"];
            Role role = db.Role.Find(rid);
            List<String> limit = db.RoleMenu.Where(r => r.RoleId == rid).Select(r => r.menuId).ToList();
            ViewBag.Role = role;
            ViewBag.limit = limit;
            return View();
        }


        public ActionResult Save(List<String> MenuId,String RoleId)
        {
            List<RoleMenu> delResult = db.RoleMenu.Where(r => r.RoleId == RoleId).ToList();
            foreach(RoleMenu rm in delResult){
                db.RoleMenu.Remove(rm);
            }
            foreach (String menu in MenuId)
            {
                RoleMenu rm=new RoleMenu{Id=Guid.NewGuid(),RoleId=RoleId,menuId=menu};
                db.RoleMenu.Add(rm);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Role");
        }

    }
}
