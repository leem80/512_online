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
    public class HeartWishController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /heartwish/
        //心声管理操作：心声审核，心声回复审核，心声以及心声回复删除
        public ViewResult Index(String type)
        {
             ViewBag.type = type;
             if (type == "heartVerify")
             {
                 var heartlist = from c in db.HeartWish
                        where c.isDeleted == false && c.VerifyingState=="待审核"
                        orderby c.CreateDate
                        select c;
                  return View(heartlist.ToList());

             }
             else if (type == "replyVerify")
             {
                 var replylist = from c in db.HeartWish
                                 from r in db.HeartWishReply
                                 where c.isDeleted == false && c.VerifyingState == "通过" && r.VerifyingState == "待审核"
                                 orderby c.CreateDate
                                 select c;
                 return View(replylist.Distinct().ToList());
             }
             else 
             {
                 var passlist = from c in db.HeartWish
                                 where c.isDeleted == false && c.VerifyingState == "通过"
                                 orderby c.CreateDate
                                 select c;
                 return View(passlist.ToList());
             }           
        }

        public ActionResult Top()
        {
            List<PROnline.Models.HeartWishes.HeartWish> list = db.HeartWish.Where(n => n.isDeleted == false).Where(n => n.VerifyingState == "通过")
                .OrderByDescending(n => n.CreateDate).Take(MvcApplication.TopCount).ToList();
            return View(list);
        }

        //
        // GET: /heartwish/MyHeartWish
        //当前用户发表的心声：只显示待审核与通过状态的心声
        public ViewResult MyHeartWish()
        {
            Guid userId= Utils.CurrentUser(this).Id;
            var list = from c in db.HeartWish
                       where c.CreatorID == userId && (c.VerifyingState == "待审核" || c.VerifyingState=="通过")
                       orderby c.VerifyingState, c.CreateDate                     
                       select c;
            return View(list.ToList());
        }

        //心声浏览
        public ViewResult HeartWishBrowse(String id)
        {
            var typelist = from type in db.HeartWishType
                           where type.isDeleted == false 
                           orderby type.TypeName
                           select type;
            ViewBag.typelist = typelist.ToList();
            ViewBag.top = "心声类型";
            ViewBag.typename = "心声";
            if (typelist.ToList().Count > 0)
            {
                if (id != null && id !="")
                {
                    var guid=Guid.Parse(id);
                    var list = Utils.PageIt(this, db.HeartWish
                        .Where(c => c.isDeleted == false && c.HeartWishTypeID == guid && c.VerifyingState == "通过")
                        .OrderBy(r => r.CreateDate)
                        ).ToList();
                    ViewBag.list = list.ToList();
                }else{
                    var list = Utils.PageIt(this, db.HeartWish
                        .Where(c=>c.VerifyingState == "通过")
                        .OrderBy(r => r.CreateDate)
                        ).ToList();
                    ViewBag.list = list.ToList();
                }
            }
            return View();
        }

        //浏览，显示指定心声类型的公告
        public ViewResult Browse(Guid id)
        {
            var typelist = from type in db.HeartWishType
                           where type.isDeleted == false
                           orderby type.TypeName
                           select type;
            ViewBag.typelist = typelist.ToList();

            ViewBag.top = "心声类型";
            HeartWishType htype = db.HeartWishType.Find(id);
            ViewBag.typename = htype.TypeName;

            var list = from c in db.HeartWish
                       where c.isDeleted == false && c.HeartWishTypeID == id && c.VerifyingState == "通过"
                       orderby c.CreateDate
                       select c;
            ViewBag.list = list.ToList();

            return View("HeartWishBrowse");
        }

        //心声审核：通过、不通过
        public void Verify(Guid id, String state)
        {
            Guid userId = Utils.CurrentUser(this).Id;
            PROnline.Models.HeartWishes.HeartWish heartwish = db.HeartWish.Find(id);
            heartwish.VerifyingState = state;
            heartwish.ModifierID = Utils.CurrentUser(this).Id;
            heartwish.ModifyDate = DateTime.Now;
            db.Entry(heartwish).State = EntityState.Modified;
            db.SaveChanges();
            Response.Redirect("/HeartWish?type=heartVerify");
        }

        //
        // GET: /heartwish/HeartWishSearch
        //心声搜索：只显示通过状态的心声
        public ViewResult HeartWishSearch(String search)
        {
            
            var typelist = from type in db.HeartWishType
                           where type.isDeleted == false
                           orderby type.TypeName
                           select type;
            ViewBag.typelist = typelist.ToList();
            if ("" != search)
            {
                var list = from c in db.HeartWish
                           where c.Title.Contains(search) && c.isDeleted == false && c.VerifyingState == "通过"
                           orderby c.HeartWishType.TypeName
                           select c;
                ViewBag.list = list.ToList();
            }
            else
            {
                var list = from c in db.HeartWish
                           where c.isDeleted == false && c.VerifyingState == "通过"
                           orderby c.HeartWishType.TypeName
                           select c;
                ViewBag.list = list.ToList();
            }
            


            return View("HeartWishBrowse");
        }


        //
        // GET: /heartwish/Details/5
        //显示心声详情，包括回复列表。
        //TODO:allowReply:自已发表的心声不允许回复，另外进行用户类型检测
        public ViewResult Show(Guid id)
        {


            var list = db.HeartWish.Where(r => r.isDeleted == false && r.VerifyingState == "通过").OrderBy(r => r.CreateDate)
                        .Take(20).ToList();

            PROnline.Models.HeartWishes.HeartWish heartwish = db.HeartWish.Find(id);
            heartwish.BrowsingNum += 1;
            db.Entry(heartwish).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.replyList = db.HeartWishReply.Where(r => r.HeartWishID == heartwish.HeartWishID).ToList();
            PROnline.Models.Users.User usr=Utils.CurrentUser(this);
            if ( usr!=null&&heartwish.CreatorID == usr.Id)
            {
                ViewBag.allowReply = false;
            }
            else
            {
                ViewBag.allowReply = true;
            }
            ViewBag.id = id;
            ViewBag.top = list;

            return View(heartwish);
        }


        //
        // GET: /heartwish/Details/5
        //显示心声详情，包括回复列表。
        //TODO:allowReply:自已发表的心声不允许回复，另外进行用户类型检测
        public ViewResult Details(Guid id)
        {
            PROnline.Models.HeartWishes.HeartWish heartwish = db.HeartWish.Find(id);
            heartwish.BrowsingNum += 1;
            db.Entry(heartwish).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.replyList = db.HeartWishReply.Where(r => r.HeartWishID == heartwish.HeartWishID).ToList();
            PROnline.Models.Users.User usr = Utils.CurrentUser(this);
            if (usr != null && heartwish.CreatorID == usr.Id)
            {
                ViewBag.allowReply = false;
            }
            else
            {
                ViewBag.allowReply = true;
            }
            ViewBag.id = id;

            return View(heartwish);
        }

        [HttpPost]
        public ViewResult Details(Guid id, String ReplyContent)
        {
            PROnline.Models.HeartWishes.HeartWish heartwish = db.HeartWish.Find(id);

            HeartWishReplyController c = new HeartWishReplyController();
            HeartWishReply reply = new HeartWishReply();
            reply.HeartWishID = id;
            reply.ReplyContent = ReplyContent;
            c.Create(reply);

            ViewBag.replyList = c.AllReply();
            ViewBag.id = id;
            return View(heartwish);
        }

        //
        // GET: /heartwish/Create

        [Auth(MenuId = "MyHeartWish_create")]
        public ActionResult Create()
        {
            var typelist = from type in db.HeartWishType
                           where type.isDeleted == false
                           orderby type.TypeName
                           select type;
            ViewBag.typelist = typelist.ToList();

            HeartWishTypeController controller = new HeartWishTypeController();
            ViewBag.list = controller.HeartWishTypeDropDownList();
            return View();
        }

        //
        // POST: /heartwish/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PROnline.Models.HeartWishes.HeartWish heartwish)
        {
            if (ModelState.IsValid)
            {
                heartwish.HeartWishID = Guid.NewGuid();
                heartwish.VerifyingState = "待审核";
                heartwish.CreatorID = Utils.CurrentUser(this).Id;
                heartwish.CreateDate = DateTime.Now;
                heartwish.isDeleted = false;
                db.HeartWish.Add(heartwish);
                db.SaveChanges();
                return RedirectToAction("HeartWishBrowse", "HeartWish");
            }

            return View(heartwish);
        }

        //
        // GET: /heartwish/Edit/5

        public ActionResult Edit(Guid id)
        {
            PROnline.Models.HeartWishes.HeartWish heartwish = db.HeartWish.Find(id);
            return View(heartwish);
        }

        //
        // POST: /heartwish/Edit/5

        [HttpPost]
        public ActionResult Edit(PROnline.Models.HeartWishes.HeartWish heartwish)
        {
            if (ModelState.IsValid)
            {
                PROnline.Models.HeartWishes.HeartWish temp = db.HeartWish.Find(heartwish.HeartWishID);
                Guid type = heartwish.HeartWishType.HeartWishTypeID;
                temp.Title = heartwish.Title;
                temp.Content = heartwish.Content;
                temp.ModifierID = Utils.CurrentUser(this).Id;
                temp.ModifyDate = DateTime.Now;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

                Response.Redirect("/HeartWishType/Details/" + type.ToString());
                return RedirectToAction("Index");
            }
            return View(heartwish);
        }

        //
        // GET: /heartwish/Delete/5

        public ActionResult Delete(Guid id)
        {
            PROnline.Models.HeartWishes.HeartWish heartwish = db.HeartWish.Find(id);
            return View(heartwish);
        }

        //
        // POST: /heartwish/Delete/5
        //删除心声
        [HttpPost, ActionName("Delete")]
        public void DeleteConfirmed(Guid id)
        {
            PROnline.Models.HeartWishes.HeartWish heartwish = db.HeartWish.Find(id);
            Guid noticeType = heartwish.HeartWishType.HeartWishTypeID;
            heartwish.isDeleted = true;
            heartwish.Content = heartwish.Content;
            heartwish.ModifierID = Utils.CurrentUser(this).Id;
            db.Entry(heartwish).State = EntityState.Modified;
            db.SaveChanges();
            Response.Redirect("/HeartWish?type=pass");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}