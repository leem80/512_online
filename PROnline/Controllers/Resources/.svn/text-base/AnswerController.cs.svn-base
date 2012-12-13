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
    public class AnswerController : Controller
    {
        //
        // GET: /ResourceDiscussion/

        PROnlineContext db = new PROnlineContext();
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Answer ans)
        {
            ans.AnswerId = Guid.NewGuid();
            ans.CreateDate = DateTime.Now;
            User usr=Utils.CurrentUser(this);
            ans.UserId = usr.Id;
            db.Answer.Add(ans);
            db.SaveChanges();
            return RedirectToAction("Show", "Question", new { id = ans.QuestionId });
        }

    }
}
