using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PROnline.Models.Service;

namespace PROnline.Models.Users
{
    public class Student
    {

        //姓名、性别、出生日期、民族、是否担任班干部、家庭人口数、个人特长、家庭通讯方式、家里是否具备上网条件、
        //是否留守、个人受伤情况（无|轻|重）、亲朋亡故（有|无）、财产损失（重大|中等|轻微|无）、语文（很好|较好|一般|较差）、英语、数学、物理、化学

        public Guid StudentID { get; set; }

        public Guid UserID { get; set; }
        public virtual User User { get; set; }

        //状态:区分是否接受心理辅导
        public String State { get; set; }

        public  Guid SchoolID { get; set; }
        public virtual School School { get; set; }

        //对应的父母
        public virtual IList<Parent> ParentList { get; set; } 

        //性别
        [Display(Name = "性别")]
        public String Sex { get; set; }

        //生日
        [Display(Name = "出生日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        //民族
        [Display(Name = "民族")]
        [Required(ErrorMessage = "民族必须填写")]
        public String People { get; set; }

        //职务：班长等
        [Display(Name = "职务")]
        public String Career { get; set; }

        //家庭人口数
        [Display(Name = "家庭人口数")]
        [Required(ErrorMessage = "家庭人口数必须填写")]
        public int HomeNum { get; set; }

        //个人特长
        [Display(Name = "个人特长")]
        [Required(ErrorMessage = "个人特长必须填写")]
        public String Hobby { get; set; }

        //家庭通讯方式
        [Display(Name = "家庭通讯方式")]
        [Required(ErrorMessage = "家庭通讯方式必须填写")]
        //[RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public String Telephone { get; set; }

        //家里是否具备上网条件
        [Display(Name = "家里是否具备上网条件")]
        public Boolean CanSurf { get; set; }

        //固定上网时间
        [Display(Name = "固定上网时间")]
        public String SurfDayOfWeek { get; set; }

        //固定上网时间，例如19:00:00-21:00:00
        [Display(Name = "时间")]
        public String SurfTime { get; set; }

        //个人受伤情况（无|轻|重）
        [Display(Name = "个人受伤情况")]
        public String Hurt { get; set; }

        //亲朋亡故（有|无）
        [Display(Name = "亲朋亡故情况")]
        public String Die { get; set; }

        //家庭财产损失情况
        [Display(Name = "家庭财产损失情况")]
        public String Lose { get; set; }

        //是否留守
        [Display(Name = "是否留守")]
        public Boolean IsAlone { get; set; }
       
        //学业情况
        [Display(Name = "数学")]
        public String Math { get; set; }

        [Display(Name = "语文")]
        public String Chinese { get; set; }

        [Display(Name = "英语")]
        public String English { get; set; }

        [Display(Name = "物理")]
        public String Physics { get; set; }

        [Display(Name = "化学")]
        public String Chemistry { get; set; }

        [Display(Name = "生物")]
        public String Sw { get; set; }

        [Display(Name = "地理")]
        public String Dl { get; set; }

        [Display(Name = "历史")]
        public String Ls { get; set; }

        //配对列表
        public virtual IList<Pairs> PairList { get; set; }

        public virtual IList<UserPTest> UserPTest { get; set; } 
    }
}