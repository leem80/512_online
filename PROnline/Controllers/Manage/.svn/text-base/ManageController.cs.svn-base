using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Users;
using PROnline.Src;

namespace PROnline.Controllers.Manage
{
    public class ManageController : Controller
    {
        //
        // GET: /Manage/

        public ActionResult Index()
        {
            User user = Utils.CurrentUser(this);
            return View();
        }

        public ActionResult Codes()
        {
            return View();
        }

    }
}
