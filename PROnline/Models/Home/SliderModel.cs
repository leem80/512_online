using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using PROnline.Models.Users;


namespace PROnline.Models.Home
{
    public class SliderModel
    {
        public Guid Id { get; set; }
        [Display(Name="内容")]
        public String Content { get; set; }

        [Display(Name = "连接")]
        public String Url { get;set; }

        [Display(Name = "标题")]
        public String Title { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public bool isDeleted { get; set; }
    }


    public class SliderShow
    {
        public String content { get; set; }
        public String content_button { get; set; }

    }


}