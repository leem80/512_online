using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;

namespace PROnline.Models.Service
{
    //与meeting_member等同。
    public class AppointmentMember
    {
        public Guid AppointmentMemberID { get; set; }

        //对应的预约
        public Guid PairAppointmentID { get; set; }

        public virtual PairAppointment PairAppointment { get; set; }

        //openmeetings表中的meeting_member中的hash
        public String AppointmentHash { get; set; }

        //对应的用户ID
        public Guid UserID { get; set; }

        public virtual User User { get; set; }

        public String UserRealName { get; set; }

        //实际进入时间
        public DateTime StartRealDate { get; set; }

        //实际退出时间
        public DateTime EndRealDate { get; set; }

      //  public String test { get; set; }
    }
}