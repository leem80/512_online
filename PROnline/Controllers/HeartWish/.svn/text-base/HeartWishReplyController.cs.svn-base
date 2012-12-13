using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.HeartWishes;
using PROnline.Models;
using PROnline.Src;

namespace PROnline.Controllers.HeartWish
{ 
    public class HeartWishReplyController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //所有通过的心声回复列表
        public List<HeartWishReply> PassedReply()
        {
            return db.HeartWishReply.Where(r=>r.VerifyingState=="通过").ToList();
        }

        //所有心声回复列表，待审核居首
        public List<HeartWishReply> AllReply()
        {
            var list = from reply in db.HeartWishReply
                       where reply.VerifyingState == "通过" || reply.VerifyingState == "待审核"
                       orderby reply.VerifyingState
                       select reply;
            return list.ToList();
        }
        
        //心声审核：通过、未通过、删除
        public void Verify(Guid id, String state, Guid wishid)
        {
            HeartWishReply reply  =db.HeartWishReply.Find(id);
            if (state == "pass")
            {
                reply.VerifyingState = "通过";               
            }
            else if (state == "refuse")
            {
                reply.VerifyingState = "未通过";
            }
            else
            {
                reply.VerifyingState = "删除";
            }
            reply.VerifierID = Utils.CurrentUser(this).Id;
            reply.VerifyingDate = DateTime.Now;
            db.Entry(reply).State = EntityState.Modified;
            db.SaveChanges();
            Response.Redirect("/HeartWish/Details/" + wishid);  
        }

        public void Create(HeartWishReply heartwishreply)
        {
            if (ModelState.IsValid)
            {
                heartwishreply.HeartWishReplyID = Guid.NewGuid();
                heartwishreply.VerifyingState = "通过";
                heartwishreply.ReplierID = Utils.CurrentUser(this).Id;
                heartwishreply.ReplyDate = DateTime.Now;
                db.HeartWishReply.Add(heartwishreply);
                db.SaveChanges();
                Response.Redirect("/HeartWish/Show/" + heartwishreply.HeartWishID);  
            }
        }
      
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}