using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using System.Web.Mvc;

namespace PROnline.Models.Users
{
    //督导专家
    public class Supervisor
    {
        public Guid SupervisorID { get; set; }

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
        [Display(Name = "性别")]
        public String Sex { set; get; }

        //民族
        [Display(Name = "民族")]
        [Required(ErrorMessage = "民族必须填写")]
        public String Nationality { set; get; }

        //政治面貌
        [Display(Name = "政治面貌")]
        [Required(ErrorMessage = "政治面貌必须填写")]
        public String PoliticalExperience { get; set; }

        //生日
        [Display(Name = "生日")]
        [Required(ErrorMessage = "生日必须填写")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        //所在地
        [Display(Name = "所在地")]
        [Required(ErrorMessage = "所在地必须填写")]
        public String HomeTown { get; set; }

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

        //毕业院校
        [Display(Name = "毕业院校")]
        public String University { get; set; }

        //学历
        [Display(Name = "学历")]
        public String Degree { get; set; }

        //专业
        [Display(Name = "专业")]
        public String Major { get; set; }

        //职业
        [Display(Name = "职业")]
        public String Career { get; set; }

        //个人简介
        [Display(Name = "个人简介")]
        public String Introduction { get; set; }

        //心理咨询师级别
        [Display(Name = "心理咨询师级别")]
        public String PDegree { get; set; }
    }
}