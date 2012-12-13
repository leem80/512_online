using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Users
{
    //系统管理员
    public class Administrator
    {
        public Guid AdministratorID { get; set; }

        public Guid UserID { get; set; }
        public virtual User User { get; set; }

        //出生日期
        [Display(Name = "出生日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        //性别
        [Display(Name = "性别")]
        public String Gender { get; set; }

        //固定电话
        [Display(Name = "固定电话")]
        public String Telephone { get; set; }

        //移动电话
        [Display(Name = "移动电话")]
        [Required(ErrorMessage = "移动电话必须填写")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public String MobileTelephone { get; set; }
        
        //地址
        [Display(Name = "地址")]
        [Required(ErrorMessage = "地址必须填写")]
        public String Address { get; set; }
    }
}