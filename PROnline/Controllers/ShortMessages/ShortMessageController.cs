using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.ShortMessages;
using PROnline.Models;
using PROnline.Models.Users;
using PROnline.Models.Service;
using PROnline.Src;
using System.Runtime.InteropServices; 

namespace PROnline.Controllers.ShortMessages
{
    public class ShortMessageController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /ShortMessage/Details/5

        public ViewResult Details(Guid id)
        {
            ShortMessage shortmessage = db.ShortMessage.Find(id);
            return View(shortmessage);
        }

        //保存短信
         [HttpPost]
        public ActionResult SaveShortMessage(String studentId, Guid ShortMessageTemplateID)
        {
             Guid sid = Guid.Parse(studentId);
             Student student = db.Student.Find(sid);

             //家长
             List<Parent> parents = db.Parent.Where(p => p.StudentID==sid).ToList();
             if(parents.Count>0 && parents[0].MobileTelephone!=null){
              ShortMessage sm = new ShortMessage();
              sm.ShortMessageID = Guid.NewGuid();
              sm.ToNum = parents[0].MobileTelephone;
              sm.ToUserID = parents[0].User.Id;
              sm.FromUserID = Utils.CurrentUser(this).Id;
             sm.SendDate=DateTime.Now;
             db.ShortMessage.Add(sm);
              db.SaveChanges();
             }
             
             return JavaScript(" $('#shortmessage').dialog('close');alert('发送成功!');");

            //return Content("发送成功");  
        }

        //发送短信
        public ActionResult SendShortMessage()
        {
            ViewBag.ShortMessageTemplateID = new SelectList(db.ShortMessageTemplate, "ShortMessageTemplateID", "Title");
            return View();
        }

        //
        // GET: /ShortMessage/MyShortMessage

        public ViewResult MyShortMessage()
        {
            //当前用户的站内信
            Guid id = Utils.CurrentUser(this).Id;
            var list = Utils.PageIt(this, db.ShortMessage.Where(l => l.ToUser.Id == id).OrderByDescending(l=>l.SendDate)).ToList();
            return View(list);
        }     
    }
}