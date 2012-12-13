using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models;
using PROnline.Models.Resources;
using PROnline.Src;
using PROnline.Models.Users;

namespace PROnline.Controllers.Resources
{
    public class RDReplyController : Controller
    {

        PROnlineContext db = new PROnlineContext();

        [HttpPost]
        public ActionResult Create(RDReply reply)
        {
            User user = Utils.CurrentUser(this);
            if (user != null)
            {
                reply.Id = Guid.NewGuid();
                reply.ReplyerId = user.Id;
                reply.ReplyTime = DateTime.Now;
                db.RDReply.Add(reply);
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        
    }
}
