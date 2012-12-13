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
using PROnline.Models.Users;

namespace PROnline.Controllers.Service
{ 
    public class PairAppointmentController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /PairAppointment/

        public ViewResult Index()
        {
            return View(db.PairAppointment.ToList());
        }

        //
        // GET: /PairAppointment/Details/5

        public ViewResult Details(Guid id)
        {
            PairAppointment pairappointment = db.PairAppointment.Find(id);
            return View(pairappointment);
        }

        //
        // GET: /PairAppointment/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /PairAppointment/Create

        [HttpPost]
        public ActionResult Create(PairAppointment pairappointment)
        {
            if (ModelState.IsValid)
            {
                pairappointment.PairAppointmentID = Guid.NewGuid();
                db.PairAppointment.Add(pairappointment);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(pairappointment);
        }
        
        //
        // GET: /PairAppointment/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            PairAppointment pairappointment = db.PairAppointment.Find(id);
            return View(pairappointment);
        }

        //
        // POST: /PairAppointment/Edit/5

        [HttpPost]
        public ActionResult Edit(PairAppointment pairappointment)
        {
            if (ModelState.IsValid)
            {
                PairAppointment temp = db.PairAppointment.Find(pairappointment.PairAppointmentID);
                temp.StartDate = pairappointment.StartDate;
                temp.EndDate = pairappointment.EndDate;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                //修改mysql
                SqlHelper help = new SqlHelper();
                String sql = "update appointments set appointment_starttime ='" + pairappointment.StartDate + "', appointment_endtime='" + pairappointment.StartDate + "' where appointment_id= "+pairappointment.AppointmentID;
                help.Update(sql);
                return RedirectToAction("Details", "Pair", new { id = temp.PairsID, SchoolID = temp.Pairs.Student.SchoolID });
            }
            return View(pairappointment);
        }

        public ActionResult Delete1(Guid id)
        {
            PairAppointment pairappointment = db.PairAppointment.Find(id);
            Guid pairid = pairappointment.PairsID;
            Guid schoolid = pairappointment.Pairs.Student.SchoolID;

            var appointmembers = from appointmember in db.AppointmentMember
                                 where appointmember.PairAppointmentID == pairappointment.PairAppointmentID
                                 select appointmember;
            foreach (var member in appointmembers)
            {

                AppointmentMember tempappointmentmember = db.AppointmentMember.Find(member.AppointmentMemberID);
                db.AppointmentMember.Remove(tempappointmentmember);
            }

            db.PairAppointment.Remove(pairappointment);
            db.SaveChanges();
            return RedirectToAction("SupPairlast", "Pair");
        }


        //
        // GET: /PairAppointment/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            PairAppointment pairappointment = db.PairAppointment.Find(id);
            Guid pairid = pairappointment.PairsID;
            Guid schoolid = pairappointment.Pairs.Student.SchoolID;

            var appointmembers = from appointmember in db.AppointmentMember
                                 where appointmember.PairAppointmentID == pairappointment.PairAppointmentID
                                 select appointmember;
            foreach (var member in appointmembers)
            {

                AppointmentMember tempappointmentmember = db.AppointmentMember.Find(member.AppointmentMemberID);
                db.AppointmentMember.Remove(tempappointmentmember);
            }

            db.PairAppointment.Remove(pairappointment);
            db.SaveChanges();
            return RedirectToAction("Details", "Pair", new { id = pairid, SchoolID = schoolid });
        }


        public ActionResult Top()
        {
            List<School> schools = db.School.Where(r => !r.isDeleted).ToList();
            string date=DateTime.Now.ToString("yyyy-MM-dd");
            string startDate=date+" 00:00:00";
            string endDate=date+" 23:59:59";
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);

            List<PairAppointment> PAList = db.PairAppointment
                .Where(r => r.StartDate.CompareTo(start)>0&&r.StartDate.CompareTo(end)<0)
                .ToList();
            Dictionary<Guid, School> pcon = new Dictionary<Guid, School>();
            
            foreach (School sc in schools)
            {
                pcon[sc.SchoolID] = sc;
                sc.Pairs = new List<Pairs>();
            }
            foreach (PairAppointment sc in PAList)
            {
                if(pcon.ContainsKey(sc.Pairs.Student.SchoolID)){
                    pcon[sc.Pairs.Student.SchoolID].Pairs.Add(sc.Pairs);
                }
                
            }
            return View(schools);
        }

        //
        // POST: /PairAppointment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            PairAppointment pairappointment = db.PairAppointment.Find(id);
            db.PairAppointment.Remove(pairappointment);
            db.SaveChanges();
            return RedirectToAction("Details", "Pair", new { id = pairappointment.PairsID, SchoolID = pairappointment.Pairs.Student.SchoolID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}