using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Service;
using PROnline.Models;
using PROnline.DataAccess;
using MySql.Data.MySqlClient;
using PROnline.Models.Users;
using PROnline.Controllers.Users;
using PROnline.Src;

namespace PROnline.Controllers.Service
{
    public class PairController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Pair/

        public ViewResult Index()
        {
            return View(db.Pairs.ToList());
        }

        public ViewResult Openpublicroom() {

        String sql = "select * from meeting_members where sername is not null";
        SqlHelper help = new SqlHelper();
        DataTable table1 = new DataTable();
        DataTable table2 = new DataTable();
        List<String> publicroomlist = new List<String>();
        help.Select(sql, table1);
       //table.Rows[0]["server_id"].ToString()
        int a=table1.Rows.Count;
        for (int i = 0; i < a; i++) {
            String sername1 = table1.Rows[i]["sername"].ToString();
            String publichash = table1.Rows[i]["hash"].ToString();
            String sql1 = "select * from servers where servername='" + sername1 + "'";
            help.Select(sql1, table2);
            String sip = table2.Rows[0]["serverip"].ToString();

            String address = null;
            address = "http://" + sip + ":5080/openmeetings/?appointmentHash="+publichash;
            publicroomlist.Add(address);
        }

        ViewBag.publicroomlist = publicroomlist;
            return View();
        }
        //
        // GET: /Pair/Details/5

        public ViewResult Details(Guid id)
        {
            Pairs pairs = db.Pairs.Find(id);
            ViewBag.SchoolID = Request.Params.Get("SchoolID");
            return View(pairs);
        }

        public ActionResult LookVolunteer(Guid id)
        {
            PairAppointment pa1 = db.PairAppointment.Find(id);
            String sername = pa1.ServerName;
            String serip = GetAppointmentIP(sername);
            Guid userId = Utils.CurrentUser(this).Id;

            var suplist = from joinsuper in db.AppointmentMember
                          where joinsuper.UserID == userId && joinsuper.PairAppointmentID == pa1.PairAppointmentID
                          select joinsuper;
            //User user = db.Users.Find(userId);
            String hash = null;
            if (suplist.ToList().Count > 0) {
                 hash = suplist.ToList()[0].AppointmentHash;
            }

            String address = "http://" + serip + ":5080/openmeetings/streams/hibernate/" + hash + ".flv";
            ViewBag.flvaddress = address;
            return View();
        }
        //返回服务器IP地址
        public static String GetAppointmentIP(String servername)
        {
            SqlHelper help = new SqlHelper();
          //  String sql = "select * from meeting_members as mm, appointments as ap, servers  where mm.appointment_id=ap.appointment_id and servers.server_id=ap.server_id and mm.hash='" + AppointmentHash.ToString() + "'";
            String sql = "select * from servers where servername='" + servername+"'";
            DataTable table = new DataTable();
            help.Select(sql, table);
            String server = "";
            if (table.Rows.Count > 0)
            {
                server = table.Rows[0]["serverip"].ToString();
            }
            return server;
        }

        [HttpGet]
        public ViewResult SchoolNavigation(String SchoolID)
        {
            SchoolController sc = new SchoolController();
            ViewBag.SchoolDropDownList = sc.SchoolDropDownList();

            School school = null;
            if (SchoolID != null)
            {
                school = db.School.Find(Guid.Parse(SchoolID));
                //找出已经过期的配对，设设置为解除
                var plist = from pairs1 in db.Pairs
                            where pairs1.EndDate.CompareTo(DateTime.Now) < 0 && pairs1.State == "正常"
                            select pairs1;
                foreach (var pp in plist) {
                   var temp=db.Pairs.Find(pp.PairsID);
                   temp.State = "解除";
                   db.Entry(temp).State = EntityState.Modified;
                   db.SaveChanges();
                }



                //已经配对列表
                var pairlist = from user in db.Student
                               from pair in db.Pairs
                               where user.StudentID == pair.StudentID && pair.State == "正常"&&pair.EndDate>=DateTime.Now
                               select user;
                //该校的所有学生
                var allstudentlist = from user in db.Student
                                     where user.SchoolID==school.SchoolID
                                     select user;
                ViewBag.pairList = pairlist.ToList();
                ViewBag.allstudentlist = allstudentlist.ToList();
            }
            return View(school);
        }

        //督导的对子
        public ActionResult SupPairfuture()
        {
            Guid userId = Utils.CurrentUser(this).Id;
            User user = db.Users.Find(userId);
            List<Pairs> pairs = new List<Pairs>();
           
            if (user.UserTypeId == UserType.SUPERVISOR)
            {
                List<Pairs> pairList = db.Pairs.Where(p => p.Supervisor.User.Id == userId).ToList();
                foreach (Pairs p in pairList)
                {
                    if (p.State == "正常")
                        pairs.Add(p);
                }
            }

            ViewBag.userType = user.UserType;
            ViewBag.temp = user.UserTypeId;

            return View(pairs);

        }

        //督导查看过去的督导情况
        public ActionResult SupPairlast()
        {
            Guid userId = Utils.CurrentUser(this).Id;
            User user = db.Users.Find(userId);
            List<Pairs> pairs = new List<Pairs>();

            if (user.UserTypeId == UserType.SUPERVISOR)
            {
                List<Pairs> pairList = db.Pairs.Where(p => p.Supervisor.User.Id == userId).ToList();
                foreach (Pairs p in pairList)
                {
                 //   if (p.State == "正常")
                        pairs.Add(p);
                }
            }

            ViewBag.userType = user.UserType;
            ViewBag.temp = user.UserTypeId;
            return View(pairs);

        }


        //我的对子(志愿者和学生)
        public ActionResult MyPair()
        {
            ViewBag.ShortMessageTemplateID = new SelectList(db.ShortMessageTemplate, "ShortMessageTemplateID", "Title");

            Guid userId = Utils.CurrentUser(this).Id;
            User user = db.Users.Find(userId);
            List<Pairs> pairs = new List<Pairs>();
            List<PairAppointment> havefeedback = new List<PairAppointment>();

            if (user.UserTypeId == UserType.STUDENT)
            {
                List<Pairs> pairList = db.Pairs.Where(p => p.Student.User.Id == userId).ToList();
                foreach (Pairs p in pairList)
                {
                    /*
                    List<PairAppointment> pappoint = (List<PairAppointment>) p.AppointmentList;
                    foreach (var ap in pappoint) {
                        var studentfeedbacklist = from sfb in db.StudentFeedback
                                                  where sfb.PairAppointmentID == ap.PairAppointmentID
                                                  select sfb;
                        if (studentfeedbacklist.ToList().Count > 0) {
                            havefeedback.Add(ap);

                        }
                                             
                    }
                    ViewBag.havefeedback = havefeedback;
                    ViewBag.mypairtype = "student";
                     */
                    String a = p.StudentID.ToString();
                    if (p.State == "正常")
                    {
                        List<PairAppointment> pappoint = (List<PairAppointment>)p.AppointmentList;
                        foreach (var ap in pappoint)
                        {
                            var studentfeedbacklist = from sfb in db.StudentFeedback
                                                      where sfb.PairAppointmentID == ap.PairAppointmentID
                                                      select sfb;
                            if (studentfeedbacklist.ToList().Count > 0)
                            {
                                havefeedback.Add(ap);

                            }

                        }
                        ViewBag.havefeedback = havefeedback;
                        ViewBag.mypairtype = "student";
                        pairs.Add(p);
                    }
                }
            }
            else if (user.UserTypeId == UserType.VOLUNTEER)
            {
                List<Pairs> pairList = db.Pairs.Where(p => p.Volunteer.User.Id == userId).ToList();
                foreach (Pairs p in pairList)
                {
                    /*
                    List<PairAppointment> pappoint = (List<PairAppointment>)p.AppointmentList;
                    foreach (var ap in pappoint)
                    {
                        var volunteerfeedbacklist = from sfb in db.VolunteerFeedback
                                                  where sfb.PairAppointmentID == ap.PairAppointmentID
                                                  select sfb;
                        if (volunteerfeedbacklist.ToList().Count > 0)
                        {
                            havefeedback.Add(ap);

                        }

                    }
                    ViewBag.havefeedback = havefeedback;
                    ViewBag.mypairtype = "volunteer";
                     * */
                    if (p.State == "正常")
                    {
                        List<PairAppointment> pappoint = (List<PairAppointment>)p.AppointmentList;
                        foreach (var ap in pappoint)
                        {
                            var volunteerfeedbacklist = from sfb in db.VolunteerFeedback
                                                        where sfb.PairAppointmentID == ap.PairAppointmentID
                                                        select sfb;
                            if (volunteerfeedbacklist.ToList().Count > 0)
                            {
                                havefeedback.Add(ap);

                            }

                        }
                        ViewBag.havefeedback = havefeedback;
                        ViewBag.mypairtype = "volunteer";
                        pairs.Add(p);
                    }
                }
            }
            else if (user.UserTypeId == UserType.SUPERVISOR)
            {
                List<Pairs> pairList = db.Pairs.Where(p => p.Supervisor.User.Id == userId).ToList();
                foreach (Pairs p in pairList)
                {
                    if (p.State == "正常")
                        pairs.Add(p);
                }
            }
             /*
            var studentfeedback = from feedback in db.StudentFeedback
                                  where feedback.PairAppointmentID == pa.PairAppointmentID
                                  select feedback;
            if (studentfeedback.ToList().Count > 0)
            {
                StudentFeedback sfeedback = studentfeedback.ToList()[0];
                
            }
            */
            ViewBag.userType = user.UserType;
            ViewBag.temp = user.UserTypeId;

            return View(pairs);
        }

        public ActionResult Tips(Guid id)
        {
            Pairs pair = db.Pairs.Find(id);
            return View(pair);
        }

        public ActionResult NavSchool(Guid id,string datex)
        {
            School school = db.School.Find(id);
            DateTime datetime=DateTime.Now;
            if(datex!=null){
                datetime=DateTime.Parse(datex);
            }
            int d = (Int32)datetime.DayOfWeek;
            DateTime weekFirst = datetime.AddDays(-1 * (d + 6) % 7);
            ViewBag.WeekFirst = weekFirst;
            ViewBag.id = id;
            return View(school);
        }

        public ActionResult  GetPair(Guid id, string datex)
        {
            DateTime datetime = DateTime.Now;
            if (datex != null)
            {
                datetime = DateTime.Parse(datex);
            }
            School  school = db.School.Find(id);
            string date = datetime.ToString("yyyy-MM-dd");
            if(school!=null){
                string startDate=date+" 00:00:00";
                string endDate=date+" 23:59:59";
                var start = DateTime.Parse(startDate);
                var end = DateTime.Parse(endDate);

                List<PairAppointment> PAList = db.PairAppointment
                    .Where(r => r.StartDate.CompareTo(start) > 0 && r.StartDate.CompareTo(end) < 0)
                    .Where(r=>r.Pairs.Student.SchoolID==school.SchoolID)
                    .ToList();

                school.Pairs = new List<Pairs>();
                foreach (PairAppointment sc in PAList)
                {
                    school.Pairs.Add(sc.Pairs);


                }
                
            }
            return View(school);
            
        }

        //申请解除对子
        public ActionResult CeasePairCause(Guid id,Guid schoolid)
        {
            Pairs pairs = db.Pairs.Find(id);
            String schoolid2 =schoolid+"";
            ViewBag.schoolid1 = schoolid;

            return View(pairs);
        }

        [HttpPost]
        public ActionResult CeasePairCause(Pairs pairs)
        {
            //Guid SchoolID = pairs.Student.SchoolID;
            Guid SchoolID=Guid.Parse(Request.Params.Get("sc1"));
            if (ModelState.IsValid)
            {
                Pairs temp = db.Pairs.Find(pairs.PairsID);
               
                temp.State = "解除";
                temp.CeaseDate = DateTime.Now;

                db.Entry(temp).State = EntityState.Modified;
                //删除所有开始时间大约结束时间的预约
                var pairappointments = from appointments in db.PairAppointment
                                       where appointments.PairsID == pairs.PairsID&&appointments.StartDate.CompareTo(temp.CeaseDate)>0
                                       select appointments;
                foreach (var pairappoint in pairappointments)
                {
                    var appointmembers = from appointmember in db.AppointmentMember
                                         where appointmember.PairAppointmentID == pairappoint.PairAppointmentID
                                         select appointmember;
                    foreach (var member in appointmembers)
                    {

                        AppointmentMember tempappointmentmember = db.AppointmentMember.Find(member.AppointmentMemberID);
                        db.AppointmentMember.Remove(tempappointmentmember);
                    }

                    PairAppointment temppairappointment = db.PairAppointment.Find(pairappoint.PairAppointmentID);
                    db.PairAppointment.Remove(temppairappointment);
                }
                db.SaveChanges();
               /*
                var pairappointments = from appointments in db.PairAppointment
                                       where appointments.PairsID == pairs.PairsID
                                       select appointments;

                foreach(var pairappoint in pairappointments){
                    var appointmembers = from appointmember in db.AppointmentMember
                                         where appointmember.PairAppointmentID == pairappoint.PairAppointmentID
                                         select appointmember;
                    foreach (var member in appointmembers) { 
                    
                        AppointmentMember tempappointmentmember = db.AppointmentMember.Find(member.AppointmentMemberID);
                        db.AppointmentMember.Remove(tempappointmentmember);
                    }

                    PairAppointment temppairappointment = db.PairAppointment.Find(pairappoint.PairAppointmentID);
                    db.PairAppointment.Remove(temppairappointment);
                }
                
               // List<PairAppointment> pairappointments = db.PairAppointment.Find(pairs.PairsID);
              //  temp.CeaseCause = pairs.CeaseCause;
             //   temp.State = "解除";
                Guid SchoolID = temp.Student.SchoolID;
                db.Pairs.Remove(temp);
              //  db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SchoolNavigation", new { SchoolID = SchoolID });*/

            }
            //return View(pairs);
            return RedirectToAction("SchoolNavigation", new { SchoolID = SchoolID });
        }

        //
        // GET: /Pair/Create

        public ActionResult Create(Guid studentId)
        {
            ServersController sc = new ServersController();
           // ViewBag.ServerDropDownList = sc.ServerDropDownList();

            Student stu = db.Student.Find(studentId);
            ViewBag.SurfDayOfWeek = stu.SurfDayOfWeek;
            ViewBag.SurfTime = stu.SurfTime;
            ViewBag.SchoolID = stu.SchoolID;

            return View();
        }

        //创建配对
        // POST: /Pair/Create

        [HttpPost]
        public ActionResult Create(Pairs pairs)
        {
            if (pairs.SupervisorID == Guid.Parse("00000000-0000-0000-0000-000000000000") || pairs.VolunteerID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                ModelState.AddModelError("Volunteer", "请选择志愿者与督导专家！");
                ServersController sc = new ServersController();
                ViewBag.ServerDropDownList = sc.ServerDropDownList();
            }
            if (ModelState.IsValid)
            {
                //服务器ID
              // String ServerID = Request.Params.Get("ServerID");
                String ServerName = Request.Params.Get("serverlist");  
                Guid SupervisorID = Guid.Parse(Request.Params.Get("SupervisorID"));
                Guid VolunteerID = Guid.Parse(Request.Params.Get("VolunteerID"));

                Supervisor sup = db.Supervisor.Where(s => s.SupervisorID == SupervisorID).Single();
                Volunteer vol = db.Volunteer.Where(s => s.VolunteerID == VolunteerID).Single();
                Student stu = db.Student.Where(s => s.StudentID == pairs.StudentID).Single();

                String sName = sup.User.RealName;
                Guid sUserId = sup.User.Id;

                String stuName = stu.User.RealName;
                Guid stuUserId = stu.User.Id;
                //返回学校列表
                Guid SchoolID = stu.SchoolID;

                String vName = vol.User.RealName;
                Guid vUserId = vol.User.Id;

                String date = pairs.StartDate.ToString("yyyy-MM-dd");
               // pairs.StartDate.DayOfWeek
                String dt = date + " " + Request.Params.Get("Hour") + ":00:00";

                pairs.PairsID = Guid.NewGuid();
                pairs.SupervisorID = SupervisorID;
                pairs.VolunteerID = VolunteerID;
                pairs.StartDate = DateTime.Parse(dt);
                pairs.State = "正常";
                pairs.CreateDate = DateTime.Now;
                pairs.CeaseDate = DateTime.Parse("2000-1-1 00:00:00");
                db.Pairs.Add(pairs);
                db.SaveChanges();

                DateTime start = pairs.StartDate;
                DateTime end = pairs.EndDate;
                DateTime temp = start;
                
                MySqlConnection conn = new SqlHelper().conn;
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                
                //选择服务器ID
                MySqlCommand serveridCommand = new MySqlCommand();
                String sql = "select * from servers where servername='" + ServerName + "'";
                SqlHelper help = new SqlHelper();
                DataTable table = new DataTable();
                help.Select(sql, table);
                int ServersID = int.Parse(table.Rows[0]["server_id"].ToString());


                //新建房间
                MySqlCommand roomCommand = new MySqlCommand();
                roomCommand.CommandText = "insert into rooms (name, roomtypes_id, starttime,deleted, ispublic, numberOfPartizipants, appointMent, isdemoroom, ismoderatedroom,allow_user_questions) values(" +
                "@name,@roomtypes_id,@starttime,@deleted,@ispublic, @numberOfPartizipants, @appointMent,@isdemoroom,@ismoderatedroom,@allow_user_questions)";
                roomCommand.Connection = conn;
                MySqlParameter sqsj = new MySqlParameter("@name", MySqlDbType.String);

                MySqlParameter shid = new MySqlParameter("@roomtypes_id", MySqlDbType.Int32);
                MySqlParameter spid = new MySqlParameter("@starttime", MySqlDbType.DateTime);

                MySqlParameter jlr = new MySqlParameter("@deleted", MySqlDbType.Bit);
                MySqlParameter shcnpar = new MySqlParameter("@ispublic", MySqlDbType.Bit);

                MySqlParameter spcnpar = new MySqlParameter("@numberOfPartizipants", MySqlDbType.Bit);
                MySqlParameter sgddpar = new MySqlParameter("@appointMent", MySqlDbType.Int32);
                MySqlParameter sqrpar = new MySqlParameter("@isdemoroom", MySqlDbType.Bit);
                MySqlParameter ismoder = new MySqlParameter("@ismoderatedroom", MySqlDbType.Bit);
                MySqlParameter yjzxkssjpar = new MySqlParameter("@allow_user_questions", MySqlDbType.Bit);

                sqsj.Value = "room";

                shid.Value = 1;
                spid.Value = DateTime.Now;

                jlr.Value =false;
                shcnpar.Value = 1;

                spcnpar.Value = 28;
                sgddpar.Value = 0;
                sqrpar.Value = 0;
                ismoder.Value = 0;
                yjzxkssjpar.Value = 1;


                roomCommand.Parameters.Add(sqsj);
                roomCommand.Parameters.Add(jlr);
                roomCommand.Parameters.Add(shid);
                roomCommand.Parameters.Add(spid);
                roomCommand.Parameters.Add(shcnpar);
                roomCommand.Parameters.Add(spcnpar);
                roomCommand.Parameters.Add(sgddpar);
                roomCommand.Parameters.Add(sqrpar);
                roomCommand.Parameters.Add(ismoder);
                roomCommand.Parameters.Add(yjzxkssjpar);
                roomCommand.ExecuteNonQuery();

                int roomId = Convert.ToInt32(roomCommand.LastInsertedId);

                //插入预约
                while (start.DayOfYear.CompareTo(end.DayOfYear)<=0)
                {

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
                    server_id.Value = ServersID;
                    appointmentname.Value = "预约";
                    appointment_starttime.Value = start;
                    appointment_endtime.Value = start.AddHours(2);
                    deleted.Value = 0;
                    room_id.Value = roomId;


                    chCommand.Parameters.Add(user_id);
                    chCommand.Parameters.Add(server_id);
                    chCommand.Parameters.Add(appointmentname);
                    chCommand.Parameters.Add(appointment_starttime);
                    chCommand.Parameters.Add(appointment_endtime);
                    chCommand.Parameters.Add(deleted);
                    chCommand.Parameters.Add(room_id);
                    chCommand.ExecuteNonQuery();

                    int id = Convert.ToInt32(chCommand.LastInsertedId);

                    PairAppointment pa = new PairAppointment();
                    pa.PairAppointmentID = Guid.NewGuid();
                    //分配视讯服务器
                    //pa.ServersID = ServerID;
                    pa.ServerName = ServerName;
                    pa.AppointmentID = id;
                    pa.Name = "test";
                    pa.Pairs = pairs;
                    pa.StartDate = start;
                    pa.startweekday = start.DayOfWeek.ToString();
                    pa.EndDate = start.AddHours(2);
                    db.PairAppointment.Add(pa);
                    db.SaveChanges();

                    for (int i = 0; i < 3; i++)
                    {
                        MySqlCommand mxCommand = new MySqlCommand();
                        DateTime userstarttime = DateTime.Now;
                        mxCommand.CommandText = "insert into meeting_members (user_id,hash,appointment_id,deleted,userrealname,issupervisor,ismoderator,starttime) values " +
                            "(@user_id, @hash,@appointment_id,@deleted,@userrealname, @issupervisor, @ismoderator,@starttime)";
                        mxCommand.Parameters.Add("@user_id", 1);
                        mxCommand.Parameters.Add("@starttime",userstarttime.ToString());
                        String appointmentHash = Guid.NewGuid().ToString();

                        AppointmentMember member = new AppointmentMember();
                        member.AppointmentMemberID = Guid.NewGuid();
                        member.AppointmentHash = appointmentHash;
                        member.PairAppointmentID = pa.PairAppointmentID;
                        member.StartRealDate = userstarttime;
                        member.EndRealDate =DateTime.Now;
                        mxCommand.Parameters.Add("@hash", appointmentHash);
                        if (i == 0)
                        {
                            // mxCommand.Parameters.Add("@firstname", sName);
                            member.UserID = sUserId;
                            mxCommand.Parameters.Add("@issupervisor", true);
                            mxCommand.Parameters.Add("@ismoderator", false);

                        }
                        else if (i == 1)
                        {
                            // mxCommand.Parameters.Add("@firstname", stuName);
                            member.UserID = stuUserId;
                            mxCommand.Parameters.Add("@issupervisor", false);
                            mxCommand.Parameters.Add("@ismoderator", false);
                        }
                        else
                        {
                            // mxCommand.Parameters.Add("@firstname", vName);
                            member.UserID = vUserId;
                            mxCommand.Parameters.Add("@issupervisor", false);
                            mxCommand.Parameters.Add("@ismoderator", true);
                        }
                        db.AppointmentMember.Add(member);
                        db.SaveChanges();


                        mxCommand.Parameters.Add("@userrealname", member.User.RealName);
                        mxCommand.Parameters.Add("@appointment_id", id);
                        mxCommand.Parameters.Add("@deleted", false);

                        mxCommand.Connection = conn;
                        mxCommand.ExecuteNonQuery();
                    }

                 
                    start = start.AddDays(7);
                }
                trans.Commit();
                conn.Close();
                return RedirectToAction("SchoolNavigation", new { SchoolID = SchoolID });
            }

            return View(pairs);
        }

        //
        // GET: /Pair/Edit/5

        public ActionResult Edit(Guid id)
        {
            Pairs pairs = db.Pairs.Find(id);
            return View(pairs);
        }

        //
        // POST: /Pair/Edit/5

        [HttpPost]
        public ActionResult Edit(Pairs pairs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pairs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pairs);
        }

        //
        // GET: /Pair/Delete/5

        public ActionResult Delete(Guid id)
        {
            Pairs pairs = db.Pairs.Find(id);
            return View(pairs);
        }

        //
        // POST: /Pair/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Pairs pairs = db.Pairs.Find(id);
            db.Pairs.Remove(pairs);
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