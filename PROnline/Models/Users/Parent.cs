using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Users
{
    //学生家长
    public class Parent
    {
        public Guid ParentID { get; set; }

        //对应的学生
        public Guid StudentID { get; set; }
        public virtual Student Student { get; set; }

        //对应的用户
        public Guid UserID { get; set; }
        public virtual User User { get; set; }

        //出生日期
        [Display(Name = "出生日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        //工作单位
        [Display(Name = "工作单位")]
        public String WorkPlace { get; set; }

        //职业
        [Display(Name = "职业")]
        public String Career { get; set; }

        //固定电话
        [Display(Name = "固定电话")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public String Telepone { get; set; }

        //移动电话
        [Display(Name = "移动电话")]
        [Required(ErrorMessage = "联系电话必须填写")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public String MobileTelephone { get; set; }
    }
}