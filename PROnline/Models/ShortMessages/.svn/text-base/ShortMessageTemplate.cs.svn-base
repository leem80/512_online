using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.ShortMessages
{
    //短信模块
    public class ShortMessageTemplate
    {
        //短信模块ID
        public Guid ShortMessageTemplateID { get; set; }

        [Display(Name = "类型")]
        [MaxLength(50)]
        [Required]
        public String Title { get; set; }

        [Display(Name = "短信内容")]
        [MaxLength(300)]
        [Required]
        public String Content { get; set; }

        //创建者
        public Guid Creator { get; set; }

        //创建日期
        public DateTime CreateDate { get; set; }

        //修改者
        public Guid Modifier { get; set; }

        //修改日期
        public DateTime? ModifyDate { get; set; }

        //是否删除
        public Boolean isDelete { get; set; }
    }
}