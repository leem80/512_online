using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Guides
{
    public class AboutUs
    {
        public Guid ID { get; set; }
        [Display(Name="标题"),Required]
        public String Title { get; set; }
        
        public DateTime CreateDate { get; set; }
        [Display(Name="显示序号")]
        public int Index { get; set; }
        [Display(Name = "内容")]
        public String Content { get; set; }
        [Display(Name="是否公开")]
        public bool IsPublic { get; set; }
    }
}
 