using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROnline.Models.Service
{
    //注意：此表与openmeetings中的appointment相似，目的是方便网站访问
    public class PairAppointment
    {
        public Guid PairAppointmentID { get; set; }

        //预约名称
        public String Name { get; set; }

        //对应的配对
        public Guid PairsID { get; set; }

        public virtual Pairs Pairs { get; set; }

        //openmeetings Appointment表
        public int AppointmentID { get; set; }

        //NA 对应的视讯服务器
        // public int ServersID { get; set; }
        //public Servers Servers { get; set; }

        public String ServerName { get; set; }

        public virtual IList<AppointmentMember> AppointmentMember { get; set; }

        //状态：取消、正常
        public String State { get; set; }

        //开始日期
        public DateTime StartDate { get; set; }

        public String startweekday { get; set; }

        public int totalMinitues { get; set; }

        //结束日期
        public DateTime EndDate { get; set; }

      //  public String endweekday { get; set; }

       // public String test { get; set; }
    }
}