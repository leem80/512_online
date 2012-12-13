using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Donation;
using PROnline.Models;
using PROnline.Src;
using PROnline.Models.Users;
using PROnline.Controllers.Service;
using MySql.Data.MySqlClient;
using PROnline.DataAccess;

namespace PROnline.Controllers.Donations
{ 
    public class DonationVideoController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /DonationVideo/

        public ViewResult Index()
        {
            var result = Utils.PageIt(this, db.DonationVideo.Where(r => r.AdministratorID != null).OrderByDescending(r => r.StartDate)).ToList();
            return View(result);
        }

        public ViewResult VideoSubmit()
        {
            var result = Utils.PageIt(this, db.DonationVideo.Where(r => r.AdministratorID == null).OrderByDescending(r => r.StartDate)).ToList();
            return View(result);
        }

        public ViewResult MyDonationVideo()
        {
            Guid id = Utils.CurrentUser(this).Id;
            String type = Utils.CurrentUser(this).UserTypeId;
            ViewBag.userType = type;
            if (type == UserType.DONATOR)
            {
                var result = Utils.PageIt(this, db.DonationVideo.Where(r => r.DonatorID == id).OrderByDescending(r => r.StartDate)).ToList();
                return View(result);
            }
            else if (type == UserType.ADMIN)
            {
                var result = Utils.PageIt(this, db.DonationVideo.Where(r => r.AdministratorID == id).OrderByDescending(r => r.StartDate)).ToList();
                return View(result);
            }
            else if (type == UserType.STUDENT)
            {
                var result = Utils.PageIt(this, db.DonationVideo.Where(r => r.StudentID == id).OrderByDescending(r => r.StartDate)).ToList();
                return View(result);
            }
            return View(new List<DonationVideo>());
        }

        public ViewResult MyDonationVideoReq()
        {
            Guid id = Utils.CurrentUser(this).Id;
            String type = Utils.CurrentUser(this).UserTypeId;
            ViewBag.userType = type;
            if (type == UserType.DONATOR)
            {
                //还未分配管理员。
                var result = Utils.PageIt(this, db.DonationVideo.Where(r => r.DonatorID == id).Where(r=>r.AdministratorID==null).OrderByDescending(r => r.StartDate)).ToList();
                return View(result);
            }
            return View(new List<DonationVideo>());
        }

        //
        // GET: /DonationVideo/Details/5

        public ViewResult Details(Guid id)
        {
            DonationVideo donationvideo = db.DonationVideo.Find(id);
            return View(donationvideo);
        }

        //id: DonationStudentID
        public ActionResult Create(Guid id)
        {
            DonationStudent donationstudent = db.DonationStudent.Find(id);

            DonationVideo video = new DonationVideo();
            video.DonationVideoID = Guid.NewGuid();
            video.DonationStudentID = id;
            video.StudentID = donationstudent.Student.User.Id;
            video.DonatorID = Utils.CurrentUser(this).Id;
            video.VerifyStateId = VerifyState.SUBMIT;
            video.StartDate = DateTime.Now;
            video.EndDate = DateTime.Now;
            db.DonationVideo.Add(video);
            db.SaveChanges();
            return Content("已预约，管理员将尽快与你联系");
        }
        
        //
        // GET: /DonationVideo/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            ViewBag.AdministratorID = new SelectList(db.Users.Where(u => u.isDeleted == false).Where(u => u.UserTypeId == UserType.MANAGER), "Id", "RealName");

            ServersController sc = new ServersController();
            ViewBag.ServerDropDownList = sc.ServerDropDownList();
            DonationVideo donationvideo = db.DonationVideo.Find(id);
            return View(donationvideo);
        }

        //
        // POST: /DonationVideo/Edit/5

        [HttpPost]
        public ActionResult Edit(DonationVideo donationvideo)
        {
            if (ModelState.IsValid)
            {
                DonationVideo temp = db.DonationVideo.Find(donationvideo.DonationVideoID);
                temp.AdministratorID = donationvideo.AdministratorID;
                String date = donationvideo.StartDate.ToString("yyyy-MM-dd");
                String dt = date + " " + Request.Params.Get("Hour") + ":00:00";
                temp.StartDate = DateTime.Parse(dt);
                temp.VerifyStateId = VerifyState.SUCCESS;

                //生成视讯预约
                //服务器ID
                String ServerID = Request.Params.Get("ServerID");
                MySqlConnection conn = new SqlHelper().conn;
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                MySqlCommand chCommand = new MySqlCommand();
                chCommand.CommandText =
                    "insert into appointments (user_id, server_id, appointmentname,  appointment_starttime,appointment_endtime, deleted, room_id) values(" +
                "@user_id,@server_id, @appointmentname,@appointment_starttime,@appointment_endtime,@deleted, @room_id)";
                chCommand.Connection = conn;
                MySqlParameter user_id = new MySqlParameter("@user_id", MySqlDbType.Int32);
                MySqlParameter server_id = new MySqlParameter("@server_id", MySqlDbType.Int32);
                MySqlParameter appointmentname = new MySqlParameter("@appointmentname", MySqlDbType.String);
                MySqlParameter appointment_starttime = new MySqlParameter("@appointment_starttime", MySqlDbType.DateTime);
                MySqlParameter appointment_endtime = new MySqlParameter("@appointment_endtime", MySqlDbType.DateTime);
                MySqlParameter deleted = new MySqlParameter("@deleted", MySqlDbType.Bit);
                MySqlParameter room_id = new MySqlParameter("@room_id", MySqlDbType.Int32);

                user_id.Value = 1;
                server_id.Value = ServerID;
                appointmentname.Value = "预约";
                appointment_starttime.Value = temp.StartDate;
                appointment_endtime.Value = temp.StartDate.AddHours(2);
                deleted.Value = 1;
                room_id.Value = 1;

                chCommand.Parameters.Add(user_id);
                chCommand.Parameters.Add(server_id);
                chCommand.Parameters.Add(appointmentname);
                chCommand.Parameters.Add(appointment_starttime);
                chCommand.Parameters.Add(appointment_endtime);
                chCommand.Parameters.Add(deleted);
                chCommand.Parameters.Add(room_id);
                chCommand.ExecuteNonQuery();

                int id = Convert.ToInt32(chCommand.LastInsertedId);

                for (int i = 0; i < 3; i++)
                {
                    MySqlCommand mxCommand = new MySqlCommand();
                    mxCommand.CommandText = "insert into meeting_members (user_id, hash,  appointment_id,deleted) values " +
                        "(@user_id, @hash,@appointment_id,@deleted)";
                    mxCommand.Parameters.Add("@user_id", 1);

                    Guid appointmentHash = Guid.NewGuid();

                    mxCommand.Parameters.Add("@hash", appointmentHash);
                    if (i == 0)
                    {
                        // mxCommand.Parameters.Add("@firstname", sName);
                        temp.StudentHash = appointmentHash;
                    }
                    else if (i == 1)
                    {
                        // mxCommand.Parameters.Add("@firstname", stuName);
                        temp.AdministratorHash = appointmentHash;
                    }
                    else
                    {
                        // mxCommand.Parameters.Add("@firstname", vName);
                        temp.DonatorHash = appointmentHash;
                    }

                    mxCommand.Parameters.Add("@appointment_id", id);
                    mxCommand.Parameters.Add("@deleted", 1);

                    mxCommand.Connection = conn;
                    mxCommand.ExecuteNonQuery();
                }
                trans.Commit();
                conn.Close();

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(donationvideo);
        }

        //
        // GET: /DonationVideo/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            DonationVideo donationvideo = db.DonationVideo.Find(id);
            return View(donationvideo);
        }

        //
        // POST: /DonationVideo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            DonationVideo donationvideo = db.DonationVideo.Find(id);
            db.DonationVideo.Remove(donationvideo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}