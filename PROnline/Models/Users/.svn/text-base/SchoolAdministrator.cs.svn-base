using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Users
{
    public class SchoolAdministrator
    {
        public Guid ID { get; set; }

        public Guid UserID { get; set; }

        public virtual User User { get; set; }

        //所属学校
        [Display(Name = "学校")]
        [Required(ErrorMessage="所属学校必须选择")]
        public Guid SchoolID { get; set; }

        //所属学校，只对校方管理员有效
        public virtual School School { get; set; }

        //真实姓名
        [Display(Name = "真实姓名")]
        public String RealName { get; set; }

        //生日
        [Display(Name = "生日")]
        [Required(ErrorMessage="生日必须填写")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        //性别
        [Display(Name = "性别")]
        public String Gender { get; set; }

        //固定电话
        [Display(Name = "固定电话")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public String Telephone { get; set; }

        //移动电话
        [Display(Name = "移动电话")]
        [Required(ErrorMessage = "移动电话必须填写")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public String MobileTelephone { get; set; }

        //地址
        [Display(Name = "地址")]
        public String Address { get; set; }
    }
}