using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.HeartWishes
{
    //心声
    public class HeartWish
    {
        //心声ID
        public Guid HeartWishID { get; set; }

        //标题
        [Display(Name = "标题")]
        [Required(ErrorMessage="标题必须填写")]
        public String Title { get; set; }

        //心声内容
        [Display(Name = "心声内容")]
        public String Content { get; set; }

        //所属公告类型ID
        public Guid HeartWishTypeID { get; set; }

        //所属心声类型
        public virtual HeartWishType HeartWishType { get; set; }

        //心声回复列表
        public virtual ICollection<HeartWishReply> ReplyList { get; set; }

        //浏览数
        public int BrowsingNum { get; set; }

        //审核状态:待审核、通过、未通过
        public String VerifyingState { get; set; }

        //审核者
        public Guid VerifierID { get; set; }

        //创建者ID
        public Guid CreatorID { get; set; }

        //创建时间
        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }

        //修改者ID
        public Guid? ModifierID { get; set; }

        //修改时间
        [Display(Name = "修改日期")]
        public DateTime? ModifyDate { get; set; }

        //是否删除
        [Display(Name = "是否删除")]
        public Boolean isDeleted { get; set; }
    }
}