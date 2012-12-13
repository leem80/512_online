using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PROnline.Models.Users;

namespace PROnline.Models.Donation
{
    //捐助条目
    public class DonationItem
    {
        public Guid DonationItemID { get; set; }

        //标题
        [Display(Name = "捐助项目名称")]
        [Required(ErrorMessage = "捐助项目名称必须填写")]
        public String Title { get; set; }

        /*//捐助类型
        [Display(Name = "捐助类型")]
        [Required]
        public Guid DonationTypeID { get; set; }
        public virtual DonationType DonationType { get; set; }*/

        //捐助列表
        public virtual IList<Donation> DonationList { get; set; }

        //学生列表
        public virtual IList<DonationStudent> DonationStudentList { get; set; }

        //对应的捐助视讯
        public virtual IList<DonationVideo> DonationVideoList { get; set; }

        //捐助人列表
        public virtual List<DonationDonator> DonatorList { get; set; }

        //捐助内容
        [MaxLength(1000)]
        [Display(Name = "捐助内容")]
        [Required(ErrorMessage = "捐助内容必须填写")]
        public String Content { get; set; }

        //发布日期（通过审核）
        [Display(Name = "发布日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? StartDate { get; set; }

        //执行日期（已执行）
        [Display(Name = "执行日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ExcuteDate { get; set; }

        //审核状态
        [Display(Name = "审核状态")]
        public string VerifyStateId { get; set; }
        //public virtual VerifyState VerifyState { get; set; }

        //未通过原因
        [Display(Name = "未通过原因")]
        public String Cause { get; set; }
        
        //创建者ID
        public Guid CreatorID { get; set; }

        //创建时间
        [Display(Name = "创建日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateDate { get; set; }

        //修改者ID
        public Guid ModifierID { get; set; }

        //修改时间
        [Display(Name = "修改日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ModifyDate { get; set; }

        //是否删除
        [Display(Name = "是否删除")]
        public Boolean isDeleted { get; set; }
    }
}