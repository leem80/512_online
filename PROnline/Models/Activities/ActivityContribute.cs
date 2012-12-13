using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PROnline.Models.Users;

namespace PROnline.Models.Activities
{
    public class ActivityContribute
    {
        public Guid ActivityContributeID { get; set; }

        //投稿题目
        [Display(Name = "风采主题")]
        [Required(ErrorMessage="风采主题必须填写")]
        public String Title { get; set; }

        //内容 
        [Display(Name = "风采内容")]
        public String Content { get; set; }

        //作者
        public Guid CreatorID { get; set; }

        //投稿日期
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateDate { get; set; }

        //失败原因
        [Display(Name = "审核未通过原因")]
        public String Cause { get; set; }

        //审核状态
        public String State { get; set; } 
    }
}