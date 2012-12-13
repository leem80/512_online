using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;

namespace PROnline.Models.Donation
{
    //
    public class DonationDonator
    {
        public Guid DonationDonatorID { get; set; }

        //爱心人士
        public Guid DonatorID { get; set; }
        public virtual Donator donator { get; set; }

        //捐助项
        public Guid DonationItemID { get; set; }
        public virtual DonationItem DonationItem { get; set; }
    }
}