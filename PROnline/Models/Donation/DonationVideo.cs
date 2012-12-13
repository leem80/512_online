using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Donation
{
    //捐助视讯服务
    public class DonationVideo
    {
        public Guid DonationVideoID { get; set; }

        //对应的学生
        public Guid DonationStudentID { get; set; }
        public virtual DonationStudent DonationStudent { get; set; }

        //审核状态
        [Display(Name = "审核状态")]
        public string VerifyStateId { get; set; }
        public virtual VerifyState VerifyState { get; set; }

        //学生
        public Guid StudentID { get; set; }
        
        //学生视讯hash
        public Guid StudentHash { get; set; }

        //由于管理员是后面插入的，只保存其UserID
        public Guid AdministratorID { get; set; }
        
        //管理员视讯hash
        public Guid AdministratorHash { get; set; }

        //爱心人士
        public Guid DonatorID { get; set; }

        //爱心人士视讯hash
        public Guid DonatorHash { get; set; }

        //开始时间
        public DateTime StartDate{get; set;}

        //结束时间
        public DateTime EndDate { get; set; }
        
    }
}