using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.HeartWishes
{
    //心声回复
    public class HeartWishReply
    {
        //心声回复ID
        public Guid HeartWishReplyID { get; set; }

        //心声ID
        public Guid HeartWishID { get; set; }

        //心声回复内容
        [Required]
        public String ReplyContent { get; set; }

        //回复者ID
        public Guid ReplierID { get; set; }
        public virtual User Replier { get; set; }

        //回复日期
        public DateTime ReplyDate { get; set; }
        

        //审核状态:待审核、通过、未通过
        public String VerifyingState { get; set; }

        //审核者ID
        public Guid? VerifierID { get; set; }

        //审核日期
        public DateTime? VerifyingDate { get; set; }

        //是否删除
        public Boolean isDeleted { get; set; }
    }
}