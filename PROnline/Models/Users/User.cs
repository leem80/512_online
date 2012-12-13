using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAnnotationsExtensions;

namespace PROnline.Models.Users
{
    //用户表
    public class User
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "用户名必须填写")]
        [Remote("UserNameAvailable", "Users",ErrorMessage = "用户名已经存在")]
        [Display(Name = "用户名")]
        public String UserName { get; set; }


        [Required(ErrorMessage = "真实姓名必须填写")]
        [Display(Name = "真实姓名")]
        public String RealName { get; set; }

        public String Password { get; set; }

        [Display(Name = "用户类型")]
        public String UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }

        //是否已经更改密码
        public Boolean IsPasswordChanged { get; set; }

        //登陆次数
        public int LoginNum { get; set; }

        //上一次登陆日期
        public DateTime? LastLoginDate { get; set; }

        //用户角色
        public String RoleId { get; set; }

        public virtual Role Role { get; set; }

        //创建日期
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateDate { get; set; }

        //更改日期
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ModifyDate { get; set; }

        //是否删除
        [Display(Name = "是否删除")]
        public Boolean isDeleted { get; set; }

        //是否已审核
        [Display(Name = "是否已审核")]
        public Boolean isVerify { get; set; }

        //是否已参加培训
        public Boolean isHaveTraining { get; set; }
    }
}