using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using PROnline.Models.Users;
using PROnline.Models.WorkStations;
using PROnline.Models.Service;

namespace PROnline.Models.Teams
{
    //小组
    public class Team
    {
        //小组ID
        public Guid TeamID { get; set; }

        //小组名称
        [Display(Name = "小组名称")]
        [MaxLength(20)]
        [Required(ErrorMessage = "小组名称必须填写")]
        [Remote("NameCheck", "Team", ErrorMessage = "同一工作站下小组名不能重复", AdditionalFields = "WorkStationID,TeamID")]
        public String TeamName { get; set; }

        //小组介绍
        [Display(Name = "小组介绍")]
        [MaxLength(200)]
        public String Introduction { get; set; }

        //小组备注
        [Display(Name = "小组备注")]
        [MaxLength(200)]
        public String Comment { get; set; }

        //小组类型（学生辅导、心理辅导）
        [Display(Name = "小组类型")]
        [MaxLength(10)]
        public String Type { get; set; }

        //组长
        public Guid? TeamLeaderID { get; set; }
        public virtual TeamLeader TeamLeader { get; set; }

        //组员列表
        public virtual IList<TeamMember> TeamMembers { get; set; }

        //创建者
        public Guid CreatorID { get; set; }

        //创建时间
        public DateTime CreateDate { get; set; }

        //创建者
        public Guid? ModifierID { get; set; }

        //修改时间
        public DateTime? ModifyDate { get; set; }

        //所属工作站
        public Guid? WorkStationID { get; set; }
        public virtual WorkStation WorkStation { get; set; }

        //是否删除
        public Boolean isDeleted { get; set; }
        [NotMapped]
        public virtual List<Volunteer> members { get; set; }
        [NotMapped]
        public List<Pairs> Pairs { get; set; }
    }
}