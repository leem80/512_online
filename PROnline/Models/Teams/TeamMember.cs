using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Teams
{
    //小组成员
    public class TeamMember
    {
        public Guid TeamMemberID { get; set; }

        //所属小组
        public Guid TeamID { get; set; }

        public virtual Team Team { get; set; }

        //志愿者ID
        public Guid VolunteerID { get; set; }

        public virtual Volunteer Volunteer { get; set; }

        //状态:申请加入=>正常、申请脱离、申请开除=>脱离(申请加入拒绝、申请脱离通过、申请开除通过)
        [Display(Name = "状态")]
        public String State { get; set; }

        //加入理由
        [MaxLength(200)]
        [Display(Name = "申请加入理由")]
        public String JoinDeclaration { get; set; }

        //加入日期
        [Display(Name = "申请加入日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime JoinDate { get; set; }

        //开除理由
        [MaxLength(200)]
        [Display(Name = "开除理由")]
        public String FireDeclaration { get; set; }

        //开除日期
        [Display(Name = "开除日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? FireDate { get; set; }

        //加入理由
        [MaxLength(200)]
        [Display(Name = "加入理由")]
        public String Declaration2 { get; set; }

        //加入日期
        [Display(Name = "加入日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? JoinDate2 { get; set; }
    }
}