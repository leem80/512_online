using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Letters
{
    //站内信
    public class Letter
    {
        //站内信ID
        public Guid LetterID { get; set; }

        //标题
        [Display(Name = "主题")]
        [MaxLength(20)]
        [Required(ErrorMessage="主题不能为空")]
        public String Title { get; set; }

        //内容
        [MaxLength(200)]
        [Display(Name = "内容")]
        [Required(ErrorMessage = "内容不能为空")]
        public String Content { get; set; }

        //发送者
        public Guid SenderID { get; set; }
        public virtual User Sender { get; set; }

        //接收者
        public Guid ReceiverID { get; set; }
        public virtual User Receiver { get; set; }

        //发送日期
        [Display(Name = "发送日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime SenderDate { get; set; }

        //是否已读（字符串）：已读、未读
        public String IsRead { get; set; }
    }
}