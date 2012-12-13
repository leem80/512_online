using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Users
{
    public class UserPTest
    {
        public Guid ID { get; set; }

        public Guid StudentID { get; set; }
        public virtual Student User { get; set; }

        [Display(Name = "测试时间")]
        [Required(ErrorMessage = "必须填写")]
        public DateTime TestDate { get; set; }

        [Display(Name = "想法总与别人不一样")]
        [Range(1, 10, ErrorMessage = "分数只能是1-10")]
        [Required(ErrorMessage = "必须填写")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public int Ta { get; set; }

        [Display(Name = "情绪不稳定（常发脾气或容易激动或容易哭泣）")]
        [Range(1, 10, ErrorMessage = "分数只能是1-10")]
        [Required(ErrorMessage = "必须填写")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public int Tb { get; set; }

        [Display(Name = "讨厌上学、讨厌写作业")]
        [Required(ErrorMessage = "必须填写")]
        [Range(1, 10, ErrorMessage = "分数只能是1-10")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public int Tc { get; set; }

        [Display(Name = "伤害他人或打人")]
        [Required(ErrorMessage = "必须填写")]
        [Range(1, 10, ErrorMessage = "分数只能是1-10")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public int Td { get; set; }

        [Display(Name = "从不与异性在一起")]
        [Required(ErrorMessage = "必须填写")]
        [Range(1, 10, ErrorMessage = "分数只能是1-10")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public int Te { get; set; }

        [Display(Name = "平时成绩和考试成绩差距较大")]
        [Required(ErrorMessage = "必须填写")]
        [Range(1, 10, ErrorMessage = "分数只能是1-10")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public int Tf { get; set; }

        [Display(Name = "时常与人争论、抬杠")]
        [Required(ErrorMessage = "必须填写")]
        [Range(1, 10, ErrorMessage = "分数只能是1-10")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public int Tg { get; set; }

        [Display(Name = "不喜欢与他人交往")]
        [Required(ErrorMessage = "必须填写")]
        [Range(1, 10, ErrorMessage = "分数只能是1-10")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public int Th { get; set; }

        [Display(Name = "学习成绩忽上忽下，很不稳定")]
        [Required(ErrorMessage = "必须填写")]
        [Range(1, 10, ErrorMessage = "分数只能是1-10")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public int Ti { get; set; }

        [Display(Name = "和老师发生冲突")]
        [Required(ErrorMessage = "必须填写")]
        [Range(1, 10, ErrorMessage = "分数只能是1-10")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public int Tj { get; set; }

        public int total { get; set; }

        public String result { get; set; }
    }
}