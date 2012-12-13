using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Resources;
using PROnline.Models;
using PROnline.Models.Users;
using PROnline.Models.Letters;
using System.Web.Script.Serialization;
using PROnline.Controllers.Manage;
using PROnline.Src;
using PROnline.Models.Files;

namespace PROnline.Controllers.Resources
{ 
    public class ResourcesController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Resources/

        public ActionResult Index()
        {
            User user=Utils.CurrentUser(this);
            if (user != null)
            {
                @ViewBag.UserType = user.UserTypeId;
                var result = Utils.PageIt(this, db.Resource.Where(r => r.CreatorId == user.Id).OrderBy(r => r.CreateDate)).ToList();
                return View(result);
            }
            else
                return View(new List<Resource>());
            
        }

        public ActionResult Manage()
        {

            List<Resource> result = Utils.PageIt(this, db.Resource.Where(r => r.VerifyStateId==VerifyState.SUCCESS).OrderBy(r => r.CreateDate)).ToList();
            return View(result);


        }


        public ActionResult Verify()
        {
            var result = Utils.PageIt(this, db.Resource
                .Where(r=>r.VerifyStateId==VerifyState.SUBMIT).OrderBy(r => r.CreateDate)).ToList();
            return View(result);
        }


        [HttpPost]
        public ActionResult EditorSave(HttpPostedFileBase upload)
        {
            Guid guid = Utils.SaveFile("\\resources\\", upload);
            String path = "/Files/GetFile?uuid=" + guid.ToString();
            String callback = Request.Params["CKEditorFuncNum"];
            ViewBag.FilePath = path;
            ViewBag.CallBack = callback;
            return View();

        }

        public ActionResult DeleteFile(Guid fileId)
        {
            ResourceFile rf=db.ResourceFile.Find(fileId);
            if (rf != null)
            {
                try
                {
                    if (rf.FileId != null && rf.FileId != Guid.Empty)
                    {
                        String fid = rf.FileId.ToString();
                        Utils.DeleteFile(Guid.Parse(fid));
                    }
                    db.ResourceFile.Remove(rf);
                    db.SaveChanges();
                    return Content("true");
                }
                catch
                {
                    return Content("true");
                }
            }
            else return Content("false");
            
        }

        public ActionResult Upload(HttpPostedFileBase qqfile)
        {
            Guid id = Utils.SaveFile("\\resources\\", qqfile);
            if (id != Guid.Empty)
            {
                FileTemp.Add(id.ToString());
                String path = "/Files/GetFile?uuid=" + id.ToString();
                return Content("{id:'" +id +"',url:'" + path + "'}");
            }
            return Content("{}");
        }



        public ActionResult Submit(Guid id)
        {
            var Resource = db.Resource.Find(id);
            Resource.VerifyStateId = VerifyState.SUBMIT;
            db.Entry(Resource).State = EntityState.Modified;

            if (Utils.CurrentUser(this).UserTypeId == UserType.TEAM_LEADER)
            {
                String[] date = DateTime.Now.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                var uList = db.Users.Where(r => r.RoleId == Role.PROJECT_MANAGER).ToList();
                foreach (User u in uList)
                {
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "资源审核！";
                    l.Content = "志愿者小组长" + Utils.CurrentUser(this).RealName + "于" + time + "上传“" + Resource.Title + "”的新资源，请尽快审核！";
                    l.ReceiverID = u.Id;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                }
            }

            db.SaveChanges();
            return  RedirectToAction("Index", "Resources");
        }

        [HttpPost]
        public ActionResult VerifyIt(Guid id,String status,String reason)
        {
            if (Session["user"] != null)
            {
                var Resource = db.Resource.Find(id);

                Resource.VerifyStateId = "Y" == status?VerifyState.SUCCESS:VerifyState.FAILURE;
                Resource.VerifierId = ((User)Session["user"]).Id;
                db.Entry(Resource).State = EntityState.Modified;

                String[] date = Resource.CreateDate.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                if (status == "Y")
                {
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "资源审核通过！";
                    l.Content = "您于" + time + "上传的“" + Resource.Title + "”新资源审核通过！";
                    l.ReceiverID = Resource.CreatorId;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                } 
                else
                {
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "资源审核未通过！";
                    l.Content = "您于" + time + "上传的“" + Resource.Title + "”新资源审核未通过！<br/>原因：" + reason;
                    l.ReceiverID = Resource.CreatorId;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }

                db.SaveChanges();
                return RedirectToAction("Verify", "Resources");
            }
            else
            {
                return RedirectToAction("LoginMini", "Account");
            }
        }



        public ActionResult Show(Guid id)
        {
            var Resource = db.Resource.Find(id);
            ViewBag.Discussion=Utils.PageIt(this,db.ResourceDiscussion.Include(r=>r.Reply)
                .Where(r=>r.ResourceId==id).OrderBy(r=>r.CreateDate)).ToList();
            return View(Resource);
        }


        public ActionResult Top()
        {
            int count=MvcApplication.TopCount*3;
            var result=db.Resource.OrderBy(r => r.CreateDate).Take(count).ToList();
            return View(result);
        }


        public ActionResult List(String tid)
        {


            if (tid == null)
            {
                var result = Utils.PageIt(this, db.Resource.Where(r => r.VerifyStateId == VerifyState.SUCCESS).OrderBy(r => r.CreateDate)).ToList();
                return View(result);
            }
            else
            {
                ResourceType rt = db.ResourceType.Find(Guid.Parse(tid));
                List<Guid> rtlist = rt.GetChildren();
                ViewBag.tid = tid;
                var result = Utils.PageIt(this, db.Resource.Where(r=>rtlist.Contains(r.ResourceTypeId))
                    .Where(r => r.VerifyStateId == VerifyState.SUCCESS).OrderBy(r => r.CreateDate)).ToList();
                return View(result);
            }
        }

        //
        // GET: /Resources/Details/5

        public ViewResult Details(Guid id)
        {
            Resource resource = db.Resource.Find(id);
            return View(resource);
        }

        //
        // GET: /Resources/Create

        public ActionResult Create()
        {
            ViewBag.TypeTree = ResourceTypeController.GetAll("");
            return View();
        } 

        //
        // POST: /Resources/Create
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Resource resource,String[] Files,String[] FileNames,String[] FileID,String[] cusName,String[] cusUrl)
        {

            if (Session["user"]!=null)
            {
                var user=((User)Session["user"]);

                resource.ResourceID = Guid.NewGuid();
                resource.CreateDate = DateTime.Now;
                resource.CreationDate = DateTime.Now;
                resource.VerifyingDate = DateTime.Now;
                resource.ModifyDate = DateTime.Now;
                resource.CreatorId = user.Id;
                resource.isDeleted = false;
                resource.VerifyStateId = VerifyState.NO;
                if (user.UserTypeId == UserType.ADMIN || user.UserTypeId == UserType.MANAGER)
                {
                    resource.VerifyStateId = VerifyState.SUCCESS;
                    resource.VerifyingDate = DateTime.Now;
                }
                db.Resource.Add(resource);
                db.SaveChanges();
                for (int i = 0; cusName!=null&&i < cusName.Length; i++)
                {
                    ResourceFile rf = new ResourceFile();
                    rf.Id = Guid.NewGuid();
                    if (cusUrl != null && cusUrl.Length > i)
                    { rf.Url = cusUrl[i]; }
                    rf.IsUpload = false;
                    rf.Name = cusName[i];
                    rf.ResourceId = resource.ResourceID;
                    db.ResourceFile.Add(rf);
                }
                for (int i = 0; Files!=null&&i < Files.Length; i++)
                {
                    ResourceFile rf = new ResourceFile();
                    rf.Id = Guid.NewGuid();
                    rf.Url = Files[i];
                    rf.IsUpload = true;
                    rf.Name = FileNames[i];
                    rf.FileId = Guid.Parse(FileID[i]);
                    rf.ResourceId = resource.ResourceID;
                    db.ResourceFile.Add(rf);
                    FileTemp.Remove(FileID[i]);
                }
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(resource);
        }
        
        //
        // GET: /Resources/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Resource resource = db.Resource.Find(id);
            ViewBag.TypeTree = ResourceTypeController.GetAll(resource.ResourceTypeId.ToString());
            return View(resource);
        }

        //
        // POST: /Resources/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Resource resource, String[] Files, String[] FileNames, String[] FileID, String[] cusName, String[] cusUrl)
        {
            if (ModelState.IsValid)
            {
                Resource temp = db.Resource.Find(resource.ResourceID);
                temp.Title = resource.Title;
                temp.ResourceTypeId = resource.ResourceTypeId;
                temp.Content = resource.Content;
                temp.ModifyDate = DateTime.Now;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

             for (int i = 0; cusName!=null&&i < cusName.Length; i++)
                {
                    ResourceFile rf = new ResourceFile();
                    rf.Id = Guid.NewGuid();
                    if (cusUrl != null && cusUrl.Length > i)
                    { rf.Url = cusUrl[i]; }
                    rf.IsUpload = false;
                    rf.Name = cusName[i];
                    rf.ResourceId = resource.ResourceID;
                    db.ResourceFile.Add(rf);
                }
                for (int i = 0; Files!=null&&i < Files.Length; i++)
                {
                    ResourceFile rf = new ResourceFile();
                    rf.Id = Guid.NewGuid();
                    rf.Url = Files[i];
                    rf.IsUpload = true;
                    rf.Name = FileNames[i];
                    rf.ResourceId = resource.ResourceID;
                    rf.FileId = Guid.Parse(FileID[i]);
                    db.ResourceFile.Add(rf);
                    FileTemp.Remove(FileID[i]);
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(resource);
        }

        //
        // GET: /Resources/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Resource resource = db.Resource.Find(id);
            return View(resource);
        }

        //
        // POST: /Resources/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Resource resource = db.Resource.Find(id);
            var rfs = db.ResourceFile.Where(r => r.ResourceId == resource.ResourceID).ToList();
            foreach (ResourceFile rf in rfs)
            {
                db.ResourceFile.Remove(rf);
                Utils.DeleteFile(rf.FileId.Value);
            }
            db.Resource.Remove(resource);
            db.SaveChanges();
            String t=Request.Params["t"];
            if (t == "i")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Manage");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}