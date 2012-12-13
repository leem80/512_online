using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PROnline.Models.Users;

namespace PROnline.Models.ShortMessages
{
    //短信
    public class ShortMessage
    {
        //短信ID
        public Guid ShortMessageID { get; set; }

        //NA 短信来源：预约通知、多用户交互
        public String Source { get; set; }

        //发送用户
        public Guid FromUserID { get; set; }
        public virtual User FromUser { get; set; }

        //接收用户
        public Guid ToUserID { get; set; }
        public virtual User ToUser { get; set; }

        //接收者电话号码
        public String ToNum { get; set; }

        //短信内容
        public Guid ShortMessageTemplateID { get; set; }
        public virtual ShortMessageTemplate ShortMessageTemplate { get; set; }

        //发送日期
        [Display(Name = "发送日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime SendDate { get; set; }
    }
}