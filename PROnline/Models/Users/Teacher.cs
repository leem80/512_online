using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PROnline.Models.Users
{
    //班主任
    public class Teacher
    {
        public Guid TeacherID {get; set;}

        public Guid UserID { get; set; }

        public virtual User User { get; set; }

        //所属的学生列表
        public virtual IList<Student> StudentList { get; set; }

        public Guid SchoolID { get; set; }

        //所属学校
        [Display(Name = "所属学校")]
        public virtual School School { get; set; }

        //性别
        [Display(Name = "姓别")]
        public String Gender { get; set; }
        
        //生日
        [Display(Name = "生日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        //政治面貌
        [Display(Name = "政治面貌")]
        public String PoliticalExperience { get; set; }

        //固定电话
        [Display(Name = "固定电话")]
        public String Telepone { get; set; }

        //手机
        [Display(Name = "手机")]
        public String MobileTelephone { get; set; }

        //电子邮箱
        [Display(Name = "电子邮箱")]
        public String EMail { get; set; }

        //QQ
        [Display(Name = "QQ")]
        public String QQ { get; set; }

        //地址
        [Display(Name = "地址")]
        public String Address { get; set; }
    }
}