using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PROnline.Models.Users;
using System.Web.Mvc;

namespace PROnline.Models.Notices
{
    //公告类型
    public class NoticeType
    {
        //公告类型ID
        public Guid NoticeTypeID { get; set; }

        //类型名称
        [Remote("NoticeTypeNameAvailable", "NoticeType", AdditionalFields = "NoticeTypeID",ErrorMessage="类型不能重复")]
        [MaxLength(20)]
        [Required]
        [Display(Name = "公告类型名称")]
        public String TypeName { get; set; }

        //介绍
        [MaxLength(100)]
        [Required]
        [Display(Name = "介绍")]
        public String Introduction { get; set; }

        //公告列表(包含删除的)
        public virtual ICollection<Notice> NoticeList { get; set; }

        //创建者ID
        public Guid CreatorID { get; set; }

        //创建时间
        [Display(Name = "创建日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required]
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