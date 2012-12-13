using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Resources
{
    public class RDReply
    {
        public Guid Id { get; set; }
        public Guid ResourceDiscussionId { get; set; }
        public virtual ResourceDiscussion ResourceDiscussion { get; set; }

        [Display(Name="评论时间")]
        public DateTime ReplyTime { get; set; }
        public Guid ReplyerId { get; set; }
        [Display(Name="回复人")]
        public string UserName { get; set; }
        [Display(Name="回复内容")]
        public String ReplyContent { get; set; }
    }
}