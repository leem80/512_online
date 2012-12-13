using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Donation;
using PROnline.Models;
using PROnline.Models.Users;
using PROnline.Src;
using PROnline.Models.Letters;

namespace PROnline.Controllers.Donations
{
    public class DonationItemController : Controller 
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /DonationItem/
        //状态
        //Submit 已提交
        //Success 已通过
        //Failure 未通过
        //index用于捐助项审核
        public ViewResult Index()
        {
            Guid id = Utils.CurrentUser(this).Id;
            //未删除、已经提交的、通过、未通过的
            var result = Utils.PageIt(this, 10, db.DonationItem.Where(d => d.isDeleted == false)
                .Where(d => d.VerifyStateId == "已提交").OrderByDescending(d => d.CreateDate)).ToList();
            Dictionary<Guid, String> dic2 = new Dictionary<Guid, String>();
            foreach (var donationItem in result)
            {
                User user = db.Users.First(u => u.Id == donationItem.CreatorID);
                /*var AdminName = from u in db.Users
                                where u.Id == donationItem.CreatorID
                                select u.UserName;*/
                dic2.Add(donationItem.DonationItemID, user.UserName);
            }

            ViewBag.dic2 = dic2;
            return View(result);
        }
        //Statistic用于捐助项统计
        public ViewResult Statistic()
        {
            Guid id = Utils.CurrentUser(this).Id;
            var result = Utils.PageIt(this, 10, db.DonationItem.Where(n => n.isDeleted == false).Where(n => n.VerifyStateId == "已执行" || n.VerifyStateId == "已通过")
    .OrderBy(n => n.VerifyStateId).ThenByDescending(n => n.CreateDate)).ToList();
            Dictionary<Guid, String> dic = new Dictionary<Guid, String>();
            foreach (var donationItem in result)
            {
                IList<DonationStudent> l = donationItem.DonationStudentList;
                String allName = null;
                foreach (DonationStudent ds in l)
                {
                    String s = ds.Student.User.RealName;
                    allName += s + " ";
                }
                dic.Add(donationItem.DonationItemID, allName);
            }
            ViewBag.dic = dic;
            return View(result);

        }
        //志愿者的捐助申请，按状态分类
        public ViewResult MySubmitDonation()
        {
            Guid id = Utils.CurrentUser(this).Id;
            var result = Utils.PageIt(this, 10, db.DonationItem.Where(d => d.CreatorID == id).Where(d => d.isDeleted == false).Where(d => (d.VerifyStateId == "已提交")).OrderByDescending(d => d.CreateDate)).ToList();
            return View(result);
        }
        public ViewResult MyExecuteDonation()
        {
            Guid id = Utils.CurrentUser(this).Id;

            List<DonationItem> DonationList = new List<DonationItem>();
            var result = Utils.PageIt(this, 10, db.DonationItem.Where(d => d.isDeleted == false).Where(d => (d.VerifyStateId == "已执行")).OrderByDescending(d => d.CreateDate)).ToList();
            foreach (DonationItem di in result)
            {
                List<DonationDonator> DonatorList = di.DonatorList;
                foreach (DonationDonator tempDonator in DonatorList)
                {
                    if (tempDonator.donator.UserID == id)
                    {
                        DonationList.Add(di);
                    }
                }
            }
            return View(DonationList);
        }

        //已审核
        public ViewResult MyCheckedDonation()
        {
            Guid id = Utils.CurrentUser(this).Id;
            var result = Utils.PageIt(this, 10, db.DonationItem.Where(d => d.CreatorID == id).Where(d => d.isDeleted == false).Where(d => (d.VerifyStateId != "已提交")).OrderByDescending(d => d.VerifyStateId)
                .ThenByDescending(d => d.CreateDate)).ToList();
            return View(result);
        }

        public ActionResult Top()
        {
            List<DonationItem> list = db.DonationItem.Where(n => n.VerifyStateId == "已执行")
                .OrderByDescending(n => n.CreateDate).Take(MvcApplication.TopCount).ToList();
            return View(list);
        }

        //
        // GET: /DonationItem/Details/5

        public ViewResult Details(Guid id)
        {
            DonationItem donationitem = db.DonationItem.Find(id);
            IList<DonationStudent> l = donationitem.DonationStudentList;
            List<DonationDonator> donatorList = donationitem.DonatorList;
            String allName = null;
            String donatorName = null;
            foreach (DonationStudent ds in l)
            {
                String s = ds.Student.User.RealName;
                allName += s + " ";
            }
            foreach (DonationDonator don in donatorList)
            {
                String s = don.donator.User.RealName;
                donatorName += s + " ";
            }
            ViewBag.allName = allName;
            ViewBag.donatorName = donatorName;
            return View(donationitem);
        }

        public ViewResult PerDetails(Guid id)
        {
            DonationItem donationitem = db.DonationItem.Find(id);
            IList<DonationStudent> i = donationitem.DonationStudentList;
            List<DonationDonator> donatorList = donationitem.DonatorList;
            int number = i.Count;
/*

            String donatorUserName = null;
            foreach (DonationDonator don in donatorList)
            {
                String s = don.donator.User.RealName;
                donatorUserName += s + " ";
            }
            ViewBag.donatorUserName = donatorUserName;
*/

            String donatorUserName = null;
            foreach (DonationDonator don in donatorList)
            {
                String s = don.donator.User.UserName;
                donatorUserName += s + ",";
            }

            ViewBag.donatorUserName = donatorUserName;

            ViewBag.number = number;
            return View(donationitem);


        }

        //
        // GET: /DonationItem/Create

        public ActionResult Create()
        {
            //ViewBag.DonationTypeID = new SelectList(db.DonationType, "DonationTypeID", "TypeName");
            return View();
        }

        //
        // POST: /DonationItem/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DonationItem donationitem, String[] StudentID)
        {
            /*if (StudentID == null)
            {
                ModelState.AddModelError("StudentError", "请选择捐助的学生");
            }*/
            if (ModelState.IsValid)
            {
                donationitem.DonationItemID = Guid.NewGuid();
                donationitem.CreateDate = DateTime.Now;
                //donationitem.StartDate = DateTime.Now;
                //donationitem.ExcuteDate = DateTime.Now;
                donationitem.CreatorID = Utils.CurrentUser(this).Id;
                donationitem.VerifyStateId = "已提交";
                String[] studentId = StudentID;
                List<DonationStudent> slist = new List<DonationStudent>();
                if (studentId != null)
                {
                    foreach (String s in studentId)
                    {
                        DonationStudent ds = new DonationStudent();
                        ds.DonationStudentID = Guid.NewGuid();
                        ds.DonationItemID = donationitem.DonationItemID;
                        ds.StudentID = Guid.Parse(s);
                        slist.Add(ds);
                    }
                }
                donationitem.DonationStudentList = slist;
                db.DonationItem.Add(donationitem);

                String[] date = DateTime.Now.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                var ulist = db.Users.Where(r => r.RoleId == Role.PROJECT_MANAGER).ToList();
                foreach (User u in ulist)
                {
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "爱心捐助提案审核！";
                    l.Content = "志愿者小组长" + Utils.CurrentUser(this).RealName + "于" + time + "提出了主题为“" + donationitem.Title + "”的爱心捐助提案，请尽快审核！";
                    l.ReceiverID = u.Id;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now; ;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }
                db.SaveChanges();
                return RedirectToAction("MySubmitDonation");
            }
            
            return View(donationitem);
        }

        //捐助浏览
        public ActionResult DonationItemBrowse()
        {
            ViewBag.top = "捐助类型";
            ViewBag.typename = "爱心天地";
            var result = Utils.PageIt(this, 10, db.DonationItem.Where(n => n.isDeleted == false).Where(n => n.VerifyStateId == "已执行" || n.VerifyStateId == "已通过")
                .OrderBy(n => n.VerifyStateId).ThenByDescending(n => n.CreateDate)).ToList();
            /*if (typelist.ToList().Count > 0)
            {
                DonationType donationType = typelist.ToList().First();
                ViewBag.typename = donationType.TypeName;
                var result = Utils.PageIt(this, db.DonationItem.Where(n => n.isDeleted == false).Where(n => n.DonationTypeID == donationType.DonationTypeID).Where(n=>n.VerifyStateId==VerifyState.SUCCESS).OrderByDescending(n => n.CreateDate)).ToList();
                return View(result);
            }*/
            return View(result);

        }

        //浏览，显示指定公告类型的公告
        public ViewResult Browse(Guid id)
        {
            ViewBag.top = "捐助类型";
            /*DonationType dtype = db.DonationType.Find(id);
            ViewBag.typename = dtype.TypeName;*/
            var result = Utils.PageIt(this, 10, db.DonationItem.Where(n => n.isDeleted == false).Where(n => n.VerifyStateId == "已通过").OrderByDescending(n => n.CreateDate)).ToList();

            return View("DonationItemBrowse", result);
        }
        //
        // GET: /DonationItem/Edit/5

        public ActionResult Edit(Guid id)
        {
            DonationItem donationitem = db.DonationItem.Find(id);
            //ViewBag.DonationTypeID = new SelectList(db.DonationType, "DonationTypeID", "TypeName", donationitem.DonationTypeID);
            return View(donationitem);
        }

        //
        // POST: /DonationItem/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(DonationItem donationitem)
        {
            if (ModelState.IsValid)
            {
                DonationItem temp = db.DonationItem.Find(donationitem.DonationItemID);
                //temp.DonationTypeID = donationitem.DonationTypeID;
                temp.Title = donationitem.Title;
                temp.Content = donationitem.Content;
                temp.ModifierID = Utils.CurrentUser(this).Id;
                temp.ModifyDate = DateTime.Now;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MySubmitDonation");
            }
            
            return View(donationitem);
        }

        public ActionResult Submit(Guid id)
        {
            DonationItem donationitem = db.DonationItem.Find(id);
            donationitem.VerifyStateId = "已提交";
            db.Entry(donationitem).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MySubmitDonation");
        }

        //
        // GET: /DonationItem/Edit/5

        public ActionResult Verify(Guid id)
        {
            DonationItem donationitem = db.DonationItem.Find(id);
            return View(donationitem);
        }

        //
        // POST: /DonationItem/Edit/5

        [HttpPost]
        public ActionResult Verify(DonationItem donationitem)
        {
            if (donationitem.DonationItemID != null)
            {
                DonationItem temp = db.DonationItem.Find(donationitem.DonationItemID);
                String[] date = temp.CreateDate.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                if (Request.Params.Get("type") == "pass")
                {
                    temp.VerifyStateId = "已通过";
                    temp.StartDate = DateTime.Now;
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "捐助提案审核已通过。";
                    l.Content = "您于" + time + "提出的主题为“" + temp.Title + "”的捐助提案已通过审核！";
                    l.ReceiverID = temp.CreatorID;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }
                else
                {
                    String Cause = Request.Params.Get("Cause");
                    temp.Cause = Cause;
                    temp.VerifyStateId = "未通过";
                  
                    //发送站内信
                    Letter l = new Letter();
                    l.LetterID = Guid.NewGuid();
                    l.IsRead = "未读";
                    l.Title = "捐助提案审核未通过。";
                    l.Content = "很遗憾，您于" + time + "提出的主题为“" + temp.Title + "”的捐助提案未通过审核！原因为:" + Cause;
                    l.ReceiverID = temp.CreatorID;
                    l.SenderID = Utils.CurrentUser(this).Id;
                    l.SenderDate = DateTime.Now;
                    db.Letter.Add(l);
                    db.SaveChanges();
                }
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donationitem);
        }

        public ActionResult Execute(Guid id)
        {
            DonationItem donationitem = db.DonationItem.Find(id);

            IList<DonationStudent> l = donationitem.DonationStudentList;
            String allName = null;
            foreach (DonationStudent ds in l)
            {
                String s = ds.Student.User.RealName;
                allName += s + " ";
            }
            ViewBag.allName2 = allName;

            return View(donationitem);
        }

        [HttpPost]
        public ActionResult Execute(DonationItem donationitem)
        {
            DonationItem tempItem = db.DonationItem.Find(donationitem.DonationItemID);
            if (tempItem.DonationItemID != null)
            {
                String[] donatorIds = Request.Form.GetValues("DonatorID");
                if (donatorIds != null)
                {
                    List<DonationDonator> donatorList = new List<DonationDonator>();

                    foreach (String d in donatorIds)
                    {
                        DonationDonator dd = new DonationDonator();
                        dd.DonationDonatorID = Guid.NewGuid();
                        dd.DonationItemID = tempItem.DonationItemID;
                        dd.DonatorID = Guid.Parse(d);
                        donatorList.Add(dd);
                    }

                    tempItem.DonatorList = donatorList;


                    if (Request.Params.Get("type") == "success")
                    {
                        tempItem.VerifyStateId = "已执行";
                        tempItem.ExcuteDate = DateTime.Now;
                    }
                }
                else {
                    ModelState.AddModelError("DonatorIsNull", "请选择捐助人");
                    return View(tempItem);
                }

                /*else {
                    tempItem.VerifyStateId = "未执行";
                }*/
                db.Entry(tempItem).State = EntityState.Modified;
                db.SaveChanges();

                String[] date = tempItem.CreateDate.ToShortDateString().Split('/');
                String year = date[0];
                String month = date[1];
                String day = date[2];
                String time = year + "年" + month + "月" + day + "日";

                //发送站内信(提案提出者)
                Letter l = new Letter();
                l.LetterID = Guid.NewGuid();
                l.IsRead = "未读";
                l.Title = "捐助提案审核已执行。";
                l.Content = "您于" + time + "提出的主题为“" + tempItem.Title + "”的捐助提案已执行！";
                l.ReceiverID = tempItem.CreatorID;
                l.SenderID = Utils.CurrentUser(this).Id;
                l.SenderDate = DateTime.Now;
                db.Letter.Add(l);

                String[] date1 = DateTime.Now.ToShortDateString().Split('/');
                String year1 = date1[0];
                String month1 = date1[1];
                String day1 = date1[2];
                String time1 = year1 + "年" + month1 + "月" + day1 + "日";
                ////发送站内信(爱心人士)
                foreach (String s in donatorIds)
                {
                    Guid dId = Guid.Parse(s);
                    Donator d = db.Donator.Find(dId);

                    Letter ll = new Letter();
                    ll.LetterID = Guid.NewGuid();
                    ll.IsRead = "未读";
                    ll.Title = "您已捐助成功！";
                    ll.Content = "您于" + time1 + "已经对捐助项“" + tempItem.Title + "”进行了捐助！详细信息请查看我的捐助！";
                    ll.ReceiverID = d.UserID;
                    ll.SenderID = Utils.CurrentUser(this).Id;
                    ll.SenderDate = DateTime.Now;
                    db.Letter.Add(ll);
                }
                db.SaveChanges();

                return RedirectToAction("Statistic");
            }
            return View(donationitem);
        }

        public ActionResult ExecuteFailure(Guid id)
        {
            DonationItem donationitem = db.DonationItem.Find(id);
            return View(donationitem);
        }
        [HttpPost]
        public ActionResult ExecuteFailure(DonationItem donationitem)
        {
            if (donationitem.DonationItemID != null)
            {
                DonationItem tempItem = db.DonationItem.Find(donationitem.DonationItemID);
                if (Request.Params.Get("type") == "failure")
                {
                    tempItem.VerifyStateId = "未执行";
                }
                db.Entry(tempItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Statistic");
            }
            return View(donationitem);
        }

        //
        // GET: /DonationItem/Delete/5

        public ActionResult Delete(Guid id)
        {
            DonationItem donationitem = db.DonationItem.Find(id);
            return View(donationitem);
        }

        //
        // POST: /DonationItem/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DonationItem donationitem = db.DonationItem.Find(id);
            donationitem.isDeleted = true;
            db.Entry(donationitem).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MySubmitDonation");
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}