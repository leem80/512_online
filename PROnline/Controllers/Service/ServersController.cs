using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Service;
using PROnline.Models;
using MySql.Data.MySqlClient;
using PROnline.DataAccess;

namespace PROnline.Controllers.Service
{ 
    public class ServersController : Controller
    {
        private PROnlineContext db = new PROnlineContext();

        //
        // GET: /Servers/

        public ViewResult Index()
        {
            return View(db.Servers.ToList());
        }

        //
        // GET: /Servers/Details/5

        public ViewResult Details(Guid id)
        {
            Servers servers = db.Servers.Find(id);
            return View(servers);
        }

        //下拉框，可用的视讯服务器列表
        public SelectList ServerDropDownList()
        {
            String sql = "select * from servers";
            SqlHelper help = new SqlHelper();
            DataTable table = new DataTable();
            help.Select(sql, table);
            List<Servers> list = new List<Servers>();
           
            for (int k = 0; k < table.Rows.Count; k++)
            {
                Servers s = new Servers();
                s.ServersID = int.Parse(table.Rows[k]["server_id"].ToString());
                s.ServerName = table.Rows[k]["servername"].ToString();
                s.ServerIP = table.Rows[k]["serverip"].ToString();
                s.ServerPort = int.Parse(table.Rows[k]["serverport"].ToString());
                list.Add(s);
            }
            return new SelectList(list, "ServersID", "ServerName");
        }

        //选择时间段
        public void selecttime() {
            String startdate11 = Request.Params.Get("startdate");
            String send1 = null;
            if (startdate11 != null)
            {
                String dayweek = DateTime.Parse(startdate11).DayOfWeek.ToString();

                if (dayweek == "Monday" || dayweek == "Tuesday" || dayweek == "Wednesday" || dayweek == "Thursday" || dayweek == "Friday")
                {
                    send1 += "19点-21点,19,21点-23点,21";
                }
                else if (dayweek == "Saturday" || dayweek == "Sunday") {
                    send1 += "09点-11点,9,14点-16点,14,16点-18点,16,19点-21点,19,21点-23点,21";
                }

            }
            Response.Write(send1);

        }

        //选择视讯服务器
        public void serverlist()
        {

            String startdate = Request.Params.Get("startdate");
            String hour = Request.Params.Get("hour");
           
            this.Session.Add("startdate", startdate);
            this.Session.Add("hour",hour);

            String sql = "select * from servers";
            SqlHelper help = new SqlHelper();
            DataTable table = new DataTable();
            help.Select(sql, table);
            List<Servers> list = new List<Servers>();



            for (int k = 0; k < table.Rows.Count; k++)
            {
                Servers s = new Servers();
                s.ServersID = int.Parse(table.Rows[k]["server_id"].ToString());
                s.ServerName = table.Rows[k]["servername"].ToString();
                s.ServerIP = table.Rows[k]["serverip"].ToString();
                s.ServerPort = int.Parse(table.Rows[k]["serverport"].ToString());
                list.Add(s);

            }

            DateTime dtstart = DateTime.Parse(startdate);
            String startweekday = dtstart.DayOfWeek.ToString();
            int starthour = int.Parse(hour);
            int endhour = starthour + 2;

          /*
            var pairappointmentlist =from pairappointment in db.PairAppointment
                                    where (pairappointment.startweekday == startweekday) && (((pairappointment.StartDate.Hour <= starthour) && (starthour < pairappointment.EndDate.Hour))
                                    || ((pairappointment.StartDate.Hour < endhour) && (endhour <= pairappointment.EndDate.Hour)))
                                    select pairappointment.ServerName;
           
            var pairappointmentlist = from pairappointment in db.PairAppointment
                                      where (pairappointment.startweekday == startweekday) && (((pairappointment.StartDate.Hour <starthour) && (starthour < pairappointment.EndDate.Hour))
                                      || ((pairappointment.StartDate.Hour < endhour) && (endhour < pairappointment.EndDate.Hour)) || (pairappointment.StartDate.Hour == starthour))
                                      select pairappointment.ServerName;
               */
            var pairappointmentlist = from pairappointment in db.PairAppointment
                                      from paa in db.Pairs
                                      where (pairappointment.startweekday == startweekday) && (pairappointment.StartDate.Hour == starthour) && (pairappointment.PairsID == paa.PairsID && paa.State == "正常")
                                      select pairappointment.ServerName;

            int tesa = pairappointmentlist.Distinct().ToList().Count;
            if (pairappointmentlist.Distinct().ToList().Count > 0)
            {
                foreach (var pairappointment1 in pairappointmentlist)
                {
                    String sername = pairappointment1;

                    for (int m = 0; m < list.Count; m++)
                    {
                        if (list[m].ServerName.Equals(sername))
                        {
                            list.RemoveAt(m);
                        }
                    }

                   
                }
            }

            String send = null;
            for (int j = 0; j < list.Count; j++)
            {
                send += list[j].ServerName + ",";
            }
            Response.Write(send);
        }
/*
        public void serverlist() {

            String test = "jiang,hao,ran";
            String startdate=Request.Params.Get("startdate");
            String hour = Request.Params.Get("hour");

            String startdate1 = startdate + " " + hour + ":00:00";
            DateTime dtstart = DateTime.Parse(startdate1);
            //hour = hour + 2;
            int a = int.Parse(hour);
            a = a + 2;
            String enddate1 = startdate + " " + a + ":00:00";
            DateTime dtend = DateTime.Parse(enddate1);
            String send = null;
            String sql = "select * from servers";
            SqlHelper help = new SqlHelper();
            DataTable table = new DataTable();
            help.Select(sql, table);
            int b = table.Rows.Count;
            var dellist = from servers in db.Servers
                          select servers;
            foreach (var server in dellist)
            {
                db.Servers.Remove(server);
                db.SaveChanges();
            }
            for (int k = 0; k < table.Rows.Count; k++)
            {
                Servers s = new Servers();
                s.ServersID = int.Parse(table.Rows[k]["server_id"].ToString());
                s.ServerName = table.Rows[k]["servername"].ToString();
                s.ServerIP = table.Rows[k]["serverip"].ToString();
                s.ServerPort = int.Parse(table.Rows[k]["serverport"].ToString());
                db.Servers.Add(s);
                db.SaveChanges();
              //  list.Add(s);
            }
            var serverlist = from ss in db.Servers
                             from pairappointment in db.PairAppointment                         
                             where (ss.ServersID != pairappointment.ServersID) || (ss.ServersID == pairappointment.ServersID && (pairappointment.EndDate < dtstart || pairappointment.StartDate > dtend))
                             select ss;
          
       //     List<Servers> allstuList = serverlist.ToList() as List<Servers>;
       //     int tt = allstuList.Count;
            foreach (var server in serverlist)
            {
                send += server.ServersID + "," + server.ServerName+",";
            }

            Response.Write(send);
        }
*/

        //
        // GET: /Servers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Servers/Create

        [HttpPost]
        public ActionResult Create(Servers servers)
        {
            if (ModelState.IsValid)
            {
                servers.IsAvaiable = true;
                db.Servers.Add(servers);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(servers);
        }
        
        //
        // GET: /Servers/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Servers servers = db.Servers.Find(id);
            return View(servers);
        }

        //
        // POST: /Servers/Edit/5

        [HttpPost]
        public ActionResult Edit(Servers servers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servers);
        }

        //
        // GET: /Servers/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Servers servers = db.Servers.Find(id);
            return View(servers);
        }

        //
        // POST: /Servers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Servers servers = db.Servers.Find(id);
            db.Servers.Remove(servers);
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