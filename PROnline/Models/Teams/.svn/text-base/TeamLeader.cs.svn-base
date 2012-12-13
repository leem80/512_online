using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Teams
{
    public class TeamLeader  
    {
        public Guid TeamLeaderID { get; set; }

        //所属小组
        public Guid TeamID { get; set; }

        //志愿者小组长ID
        public Guid VolunteerID { get; set; }

        public virtual Volunteer Volunteer { get; set; }

        //状态:正常、更换(不再担任)
        [Display(Name = "状态")]
        public String State { get; set; }

        //加入日期
        [Display(Name = "加入日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime JoinDate { get; set; }

        //更换理由
        [MaxLength(200)]
        [Display(Name = "更换理由")]
        public String ChangeCause { get; set; }

        //更换日期
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ChangeDate { get; set; }
    }
}