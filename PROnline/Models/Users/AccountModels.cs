using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAnnotationsExtensions;

namespace PROnline.Models.Users
{
    #region 账户模型

    public class LoginModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "用户名不能为空")]
        [Display(Name = "用户名")]
        public String UserName { get; set; }
        [Required(ErrorMessage = "密码不能为空")]
        [Display(Name = "密码")]
        public String Password { get; set; }
        [Display(Name = "是否保留登录信息")]
        public String Cookie { get; set; }
    }


    public class LogonModel
    {

        public Guid Id { get; set; }
        [Required(ErrorMessage="用户名不能为空")]
        [Display(Name = "用户名")]
        [Remote("UserNameAvailable", "Users", ErrorMessage = "用户名重复")]
        public String UserName { get; set; }
        [Required(ErrorMessage="密码不能为空")]
        [Display(Name = "密码")]
        public String Password { get; set; }
        [EqualTo("Password",ErrorMessage="两次输入的密码不相同")]
        [Display(Name = "重复输入密码")]
        public String RePassword { get; set; }
        [Display(Name = "用户类型")]
        public String UserType { get; set; }
    }

    public class Password
    {
        [Required(ErrorMessage = "旧密码不能为空")]
        [Display(Name = "旧密码")]
        [Remote("OldPasswordAvailable", "Users", ErrorMessage = "旧密码错误")]
        public String oldPassword { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [MaxLength(10, ErrorMessage = "密码超过10位，长度过长")]
        [MinLength(5, ErrorMessage = "密码不足6位，长度过短")]
        [Display(Name = "密码")]
        public String Pass { get; set; }

        [EqualTo("Pass", ErrorMessage = "两次输入的密码不相同")]
        [Display(Name = "重复输入密码")]
        public String RePass { get; set; }
    }

    #endregion
}