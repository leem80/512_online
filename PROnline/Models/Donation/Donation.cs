﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROnline.Models.Donation
{
    //成功的捐助
    public class Donation
    {
        //捐助ID
        public Guid DonationID { get; set; }

        //对应的捐助条目
        public virtual DonationItem DonationItem { get; set; }

        //视讯安排
        public virtual IList<DonationVideo> VideoList { get; set; }

        //捐助金额
        public int Money { get; set; }

        //捐助方式
        public String Form { get; set; }


    }
}