using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;

namespace PROnline.Models.Activities
{
    //活动人员
    public class ActivityMember
    {
        //活动人员ID
        public Guid ActivityMemberID { get; set; }

        //所属活动
        public Guid ActivityID { get; set; }
        public Activity Activity { get; set; }

        //用户
        public Guid UserID { get; set; }
        public virtual User User { get; set; }

        //NA：是否参与
        public Boolean IsJoined { get; set; }
    }
}