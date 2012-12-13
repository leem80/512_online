using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Resources;
using System.Web.Mvc;
using PROnline.Src;
using PROnline.Models.Users;
using PROnline.Models;

namespace PROnline.Controllers.Resources
{
    public class ResourceDiscussionController : Controller
    {
        //
        // GET: /ResourceDiscussion/

        PROnlineContext db = new PROnlineContext();
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(ResourceDiscussion rd)
        {
            rd.Id = Guid.NewGuid();
            rd.CreateDate = DateTime.Now;
            User usr=Utils.CurrentUser(this);
            rd.UserId = usr.Id;
            rd.UserName = usr.RealName;
            db.ResourceDiscussion.Add(rd);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}
