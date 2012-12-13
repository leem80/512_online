using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;

namespace PROnline.Models.Donation
{
    //需要捐助的学生
    public class DonationStudent
    {
        public Guid DonationStudentID { get; set; }

        //学生
        public Guid StudentID { get; set; }
        public virtual Student Student { get; set; }

        //捐助项
        public Guid DonationItemID { get; set; }
        public virtual DonationItem DonationItem { get; set; }
    }
}