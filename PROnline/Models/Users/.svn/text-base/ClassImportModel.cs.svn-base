using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Users
{
    public class ClassImportModel
    {
        public String Name { get; set; }
        public String Sex { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDay { get; set; }

        [Display(Name = "民族")]
        public String People { get; set; }

        //职务：班长等
        [Display(Name = "职务")]
        public String Career { get; set; }

        //家庭人口数
        [Display(Name = "家庭人口数")]
        public int HomeNum { get; set; }

        //个人特长
        [Display(Name = "个人特长")]
        public String Hobby { get; set; }

        //家庭通讯方式
        [Display(Name = "家庭通讯方式")]
        public String Telephone { get; set; }

        public Boolean IsAlone { get; set; }

        //家里是否具备上网条件
        [Display(Name = "家里是否具备上网条件")]
        public Boolean CanSurf { get; set; }



        public string DayOfWeek { get; set; }


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

        public String UserName { get; set; }
    }
}