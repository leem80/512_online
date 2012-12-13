using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Users;
using PROnline.Models;
using PROnline.Models.Service;
using PROnline.Src;
using PROnline.Models.WorkStations;

namespace PROnline.Controllers.Xmdt
{
    public class XmdtController : Controller
    {
        //
        // GET: /Xmdt/

        public PROnlineContext db = new PROnlineContext();

        public ActionResult Index()
        {
            return View();
        }

        //今日辅导信息
        public ActionResult TodayService()
        {
            List<School> schools = db.School.Where(r => !r.isDeleted).ToList();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string startDate = date + " 00:00:00";
            string endDate = date + " 23:59:59";
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);

            List<PairAppointment> PAList = db.PairAppointment
                .Where(r => r.StartDate.CompareTo(start) > 0 && r.StartDate.CompareTo(end) < 0)
                .ToList();
            Dictionary<Guid, School> pcon = new Dictionary<Guid, School>();

            foreach (School sc in schools)
            {
                pcon[sc.SchoolID] = sc;
                sc.Pairs = new List<Pairs>();
                sc.StudentCount = (int)db.Student.Where(r => r.SchoolID == sc.SchoolID).Count();
                sc.TotalServiceTime = (int)db.PairAppointment
                    .Where(r => r.Pairs.Student.SchoolID == sc.SchoolID)
                    .Where(r => r.StartDate.CompareTo(end) < 0)
                    .Sum(r=>r.totalMinitues);
            }
            foreach (PairAppointment sc in PAList)
            {
                if (pcon.ContainsKey(sc.Pairs.Student.SchoolID))
                {
                    pcon[sc.Pairs.Student.SchoolID].Pairs.Add(sc.Pairs);
                }

            }
            return View(schools);
        }

        //
        //学校今日辅导

        public ActionResult SchoolToday(Guid id)
        {
            DateTime datetime = DateTime.Now;
            School school = db.School.Find(id);
            string date = datetime.ToString("yyyy-MM-dd");
            if (school != null)
            {
                string startDate = date + " 00:00:00";
                string endDate = date + " 23:59:59";
                var start = DateTime.Parse(startDate);
                var end = DateTime.Parse(endDate);

                List<PairAppointment> PAList = db.PairAppointment
                    .Where(r => r.StartDate.CompareTo(start) > 0 && r.StartDate.CompareTo(end) < 0)
                    .Where(r => r.Pairs.Student.SchoolID == school.SchoolID)
                    .ToList();

                school.Pairs = new List<Pairs>();
                foreach (PairAppointment sc in PAList)
                {
                    sc.Pairs.TodayAppointment = sc;
                    school.Pairs.Add(sc.Pairs);


                }

            }
            return View(school);
        }

        //学生辅导历史
        public ActionResult StuAHistroy(Guid id)
        {
            Student stu = db.Student.Find(id);
            ViewBag.Student = stu;
            if (stu != null)
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                string endDate = date + " 23:59:59";
                var limitDate = DateTime.Parse(endDate);
                List<PairAppointment> paList = Utils.PageIt(this,db.PairAppointment.Where(r => r.Pairs.StudentID == stu.StudentID)
                    .Where(r => r.StartDate < limitDate).OrderBy(r=>r.StartDate)).ToList();
                return View(paList);
            }
            else
                return Content("学生不存在！");
        }


        //学生辅导历史
        public ActionResult StuCal(Guid id,String datex)
        {
            DateTime datetime=DateTime.Now;
            if (datex != null)
            {
                datetime = DateTime.Parse(datex);
            }
            ViewBag.date = datetime;
            Student stu = db.Student.Find(id);
            ViewBag.Student = stu;
            if (stu != null)
            {
                string date = datetime.ToString("yyyy-MM-dd");
                string endDate = date + " 23:59:59";
                string startDate = date + " 00:00:00";
                var start = DateTime.Parse(startDate);
                var end = DateTime.Parse(endDate);
                List<PairAppointment> PAList = db.PairAppointment
                    .Where(r => r.StartDate.CompareTo(start) > 0 && r.StartDate.CompareTo(end) < 0)
                    .Where(r => r.Pairs.StudentID==stu.StudentID)
                    .ToList();
                if (PAList.Count > 0)
                {
                    return View(PAList[0]);
                }
                else
                {
                    return Content("无辅导记录！");
                }
            }
            else
                return Content("学生不存在！");
        }

        public ActionResult StationDt()
        {
            var stations = db.WorkStation.Include("Teams").Where(r => r.isDeleted == false).ToList();

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string startDate = date + " 00:00:00";
            string endDate = date + " 23:59:59";
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);


            foreach(var station in stations){
                station.Vcount = db.TeamMember.Where(r => r.Team.WorkStationID == station.WorkStationID).Count();
                station.PcountToday = db.PairAppointment
                     .Where(r => r.Pairs.Volunteer.Team.WorkStationID.Value == station.WorkStationID)
                     .Where(r => r.StartDate.CompareTo(start) > 0 && r.StartDate.CompareTo(end) < 0)
                    .Count();
                station.TotalServiceTime = db.PairAppointment
                    .Where(r => r.Pairs.Volunteer.Team.WorkStationID.Value == station.WorkStationID)
                    .Where(r => r.StartDate.CompareTo(end) < 0)
                    .Sum(r=>r.totalMinitues);
            }
            return View(stations);
        }


        //
        //学校今日辅导

        public ActionResult StationToday(Guid id)
        {
            DateTime datetime = DateTime.Now;
            WorkStation station = db.WorkStation.Find(id);
            string date = datetime.ToString("yyyy-MM-dd");
            if (station != null)
            {
                string startDate = date + " 00:00:00";
                string endDate = date + " 23:59:59";
                var start = DateTime.Parse(startDate);
                var end = DateTime.Parse(endDate);

                List<PairAppointment> PAList = db.PairAppointment
                    .Where(r => r.StartDate.CompareTo(start) > 0 && r.StartDate.CompareTo(end) < 0)
                    .Where(r => r.Pairs.Volunteer.Team.WorkStationID==station.WorkStationID)
                    .ToList();

                station.Pairs = new List<Pairs>();
                foreach (PairAppointment sc in PAList)
                {
                    sc.Pairs.TodayAppointment = sc;
                    station.Pairs.Add(sc.Pairs);


                }

            }
            return View(station);
        }



        public ActionResult VolHistroy(Guid id)
        {
            Volunteer vol = db.Volunteer.Find(id);
            ViewBag.Volunteer = vol;
            if (vol != null)
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                string endDate = date + " 23:59:59";
                var limitDate = DateTime.Parse(endDate);
                List<PairAppointment> paList = Utils.PageIt(this, db.PairAppointment.Where(r => r.Pairs.VolunteerID == vol.VolunteerID)
                    .Where(r => r.StartDate < limitDate).OrderBy(r => r.StartDate)).ToList();
                return View(paList);
            }
            else
                return Content("志愿者不存在！");
        }


        public ActionResult Activity()
        {
            var list = db.Activity.Where(r => r.VerifyState == "审核通过");
            return View(list);
        }

        public ActionResult Donation()
        {
            var list = db.DonationItem
                .Include("DonationStudentList").Include("DonatorList")
                .Where(r => r.VerifyStateId == VerifyState.SUCCESS).ToList();
            return View(list);
        }


        //学生辅导历史
        public ActionResult VolCal(Guid id, String datex)
        {
            DateTime datetime = DateTime.Now;
            if (datex != null)
            {
                datetime = DateTime.Parse(datex);
            }
            ViewBag.date = datetime;
            Volunteer vol = db.Volunteer.Find(id);
            ViewBag.Volunteer = vol;
            if (vol != null)
            {
                string date = datetime.ToString("yyyy-MM-dd");
                string endDate = date + " 23:59:59";
                string startDate = date + " 00:00:00";
                var start = DateTime.Parse(startDate);
                var end = DateTime.Parse(endDate);
                List<PairAppointment> PAList = db.PairAppointment
                    .Where(r => r.StartDate.CompareTo(start) > 0 && r.StartDate.CompareTo(end) < 0)
                    .Where(r => r.Pairs.VolunteerID == vol.VolunteerID)
                    .ToList();
                if (PAList.Count > 0)
                {
                    return View(PAList[0]);
                }
                else
                {
                    return Content("无辅导记录！");
                }
            }
            else
                return Content("志愿者不存在！");
        }
        
        public ActionResult SchoolTotal()
        {
            List<School> schools = db.School.Where(r => !r.isDeleted).ToList();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string startDate = date + " 00:00:00";
            string endDate = date + " 23:59:59";
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);

            List<PairAppointment> PAList = db.PairAppointment
                .Where(r => r.StartDate.CompareTo(end) < 0)
                .ToList();
            Dictionary<Guid, School> pcon = new Dictionary<Guid, School>();

            foreach (School sc in schools)
            {
                pcon[sc.SchoolID] = sc;
                sc.Pairs = new List<Pairs>();
                sc.StudentCount = (int)db.Student.Where(r => r.SchoolID == sc.SchoolID).Count();
                sc.TotalServiceTime = (int)db.PairAppointment
                    .Where(r => r.Pairs.Student.SchoolID == sc.SchoolID)
                    .Where(r => r.StartDate.CompareTo(end) < 0)
                    .Sum(r => r.totalMinitues);
            }
            foreach (PairAppointment sc in PAList)
            {
                if (pcon.ContainsKey(sc.Pairs.Student.SchoolID))
                {
                    pcon[sc.Pairs.Student.SchoolID].Pairs.Add(sc.Pairs);
                }

            }

            ViewBag.total = db.PairAppointment.Sum(r => r.totalMinitues) / 60;
            return View(schools);
        }

        public ActionResult StationTotal()
        {
            var stations = db.WorkStation.Include("Teams").Where(r => r.isDeleted == false).ToList();

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string startDate = date + " 00:00:00";
            string endDate = date + " 23:59:59";
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);
            foreach (var station in stations)
            {
                station.Vcount = db.TeamMember.Where(r => r.Team.WorkStationID == station.WorkStationID).Count();
                station.TotalServiceTime = db.PairAppointment
                    .Where(r => r.Pairs.Volunteer.Team.WorkStationID.Value == station.WorkStationID)
                    .Where(r => r.StartDate.CompareTo(end) < 0)
                    .Sum(r=>r.totalMinitues);
            }

            ViewBag.total = db.PairAppointment.Sum(r => r.totalMinitues)/60;
            return View(stations);
        }


    }
}
