using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PROnline.Models.Donation
{
    //捐助类型
    public class DonationType
    {
        //捐助类型ID
        public Guid DonationTypeID { get; set; }

        //类型名称
        [Remote("DonationTypeNameAvailable", "DonationType", AdditionalFields = "DonationTypeID", ErrorMessage = "类型不能重复")]
        [MaxLength(20)]
        [Required]
        [Display(Name = "捐助类型名称")]
        public String TypeName { get; set; }

        //介绍
        [MaxLength(100)]
        [Display(Name = "介绍")]
        public String Introduction { get; set; }

        //捐助列表(包含删除的)
        public virtual ICollection<DonationItem> DonationList { get; set; }

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