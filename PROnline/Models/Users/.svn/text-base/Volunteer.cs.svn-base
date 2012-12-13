using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using PROnline.Models.Teams;
using System.Web.Mvc;

namespace PROnline.Models.Users
{
    public class Volunteer
    {
        public Guid VolunteerID { get; set; }

        public Guid UserID { get; set; }

        public virtual User User { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [NotMapped]
        [Display(Name = "密码")]
        public String FirtPassword { get; set; }

        [Compare("FirtPassword", ErrorMessage = "两次输入的密码不相同")]
        [NotMapped]
        [Display(Name = "确认密码")]
        public String ConfirmPassword { get; set; }

        //性别
        [Display(Name="性别")]
        public String Sex { get; set; }

        //民族
        [Display(Name="民族")]
        [Required(ErrorMessage="民族必须填写")]
        public String Nationality { get; set; }

        //生日
        [Display(Name = "生日")]
        [Required(ErrorMessage="生日必须填写")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        //政治面貌
        [Display(Name = "政治面貌")]
        [Required(ErrorMessage="政治面貌必须填写")]
        public String PoliticalExperience { get; set; }

        //固定电话
        [Display(Name = "固定电话")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public String Telepone { get; set; }

        //移动电话
        [Display(Name = "移动电话")]
        [Required(ErrorMessage = "移动电话必须填写")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public String MobileTelephone { get; set; }

        //电子邮箱
        [Email(ErrorMessage = "电子邮箱格式不正确")]
        [Display(Name = "电子邮箱")]
        public String EMail { get; set; }

        //QQ
        [Display(Name = "QQ")]
        public String QQ { get; set; }

        //辅导类型：学生辅导、心理辅导
        [Display(Name = "辅导类型")]
        public String TrainingType { get; set; }

        //是否是在校大学生
        [Display(Name = "是否是在校大学生")]
        public Boolean IsStudent { get; set; }

        //毕业（所在）院校
        [Display(Name = "毕业（所在）院校")]
        public String University { get; set; }

        //学历
        [Display(Name = "学历")]
        public String Degree { get; set; }

        //专业
        [Display(Name = "专业")]
        public String Major { get; set; }

        //年级（在校大学生）
        [Display(Name = "年级（在校大学生,学生志愿者填写)")]
        public String Grade { get; set; }

        //职业（非在校大学生）
        [Display(Name = "职业（非在校大学生,社会志愿者填写）")]
        public String Career { get; set; }

        [Display(Name = "所在地")]
        [Required(ErrorMessage = "所在地必须填写")]
        public String HomeTown { get; set; }

        //是否参加过类似活动
        [Display(Name = "是否参加过类似活动")]
        public Boolean IsExperenced { get; set; }

        //经验与体会
        [Display(Name = "相关经历")]
        public String Experence { get; set; }

        //心理咨询师级别
        [Display(Name = "心理咨询师级别")]
        public String PDegree { get; set; }

        //心理帮助的看法
        [Display(Name = "心理帮助的看法")]
        public String Insight{get;set;}

        //自我介绍
        [Display(Name = "教育背景或接受过的相关培训情况")]
        public String Introduction { get; set; }

        //固定上网时间
        [Display(Name = "星期")]
        public String DayOfWeek { get; set; }

        //时间
        [Display(Name = "爱心时间")]
        public String Time { get; set; }

        //动机
        [Display(Name="成为志愿者的动机")]
        public String Motive { get; set; }

        //座右铭
        [Display(Name = "座右铭")]
        public String Motto { get; set; }

        //擅长领域
        [Display(Name = "擅长领域")]
        public String GoodAtField { get; set; }

        //是否已加入小组
        public Boolean isHaveTeam { get; set; }

        //加入的小组

        public Guid? TeamID { get; set; }

        public virtual Team Team { get;set; }
    }
}