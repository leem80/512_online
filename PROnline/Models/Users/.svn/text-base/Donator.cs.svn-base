using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using System.Web.Mvc;

namespace PROnline.Models.Users
{
    //爱心人士
    public class Donator
    {
        public Guid DonatorID { get; set; }

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

        //生日
        [Display(Name="生日")]
        [DisplayFormat(ApplyFormatInEditMode=true,DataFormatString="{0:yyyy-MM-dd}")]
        [Required(ErrorMessage="生日必须填写")]
        public DateTime Birthday { get; set; }


        //电子邮箱
        [Email(ErrorMessage = "电子邮箱格式不正确")]
        [Display(Name = "电子邮箱")]
        public String EMail { get; set; }

        //固定电话
        [Display(Name = "固定电话")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public String Telephone { get; set; }

        //移动电话
        [Display(Name = "移动电话")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        [Required(ErrorMessage = "移动电话必须填写")]
        public String MobileTelephone { get; set; }

        //QQ
        [Display(Name="QQ")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public String QQ { get; set; }

        //地址
        [Display(Name = "地址")]
        [Required(ErrorMessage = "地址必须填写")]
        public String Address { get; set; }

        //职业
        [Display(Name="职业")]
        [Required(ErrorMessage = "职业必须填写")]
        public String Occupation { get; set; }



        //是否参加过类似活动
        [Display(Name = "是否参加过类似的爱心捐助")]
        public Boolean isHaveExperience { get; set; }

        //希望采用何种方式与我们联系（可多选）
        [Display(Name = "希望采用何种方式与我们联系（可多选）")]
        [Required(ErrorMessage = "请选择联系方式")]
        public String Connection { get; set; }
    }
}