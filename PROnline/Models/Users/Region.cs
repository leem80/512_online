using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Users
{
    //区域
    public class Region
    {
        public Guid Id { get; set; }
        [Display(Name="区域代码")]
        public String AreaCode { get; set; }
        [Display(Name="区域名")]
        public String Name { get; set; }
        [Display(Name="顺序")]
        public int Order { get; set; }
        public Guid ParentId { get; set; }
        public virtual Guid Parent { get; set; }
        public virtual List<Region> Children { get; set; }
    }
}