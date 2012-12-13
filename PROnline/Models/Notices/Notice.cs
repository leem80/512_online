using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PROnline.Models.Users;

namespace PROnline.Models.Notices
{
    //公告
    public class Notice
    {
        //公告ID
        public Guid NoticeID { get; set; }

        //公告标题
        [MaxLength(50,  ErrorMessage = "标题长度不能大于50个字符")]
        [Display(Name = "标题")]
        [Required]
        public String Title { get; set; }

        //公告内容
        [MaxLength(1000)]
        [Display(Name = "内容")]
        public String Content { get; set; }

        //所属公告类型ID
        public Guid NoticeTypeID { get; set; }

        //所属公告类型
        [Display(Name = "公告类型")]
        public virtual NoticeType NoticeType { get; set; }

        //浏览数
        public int BrowsingNum { get; set; }

        //创建者ID
        public Guid CreatorID { get; set; }

        //创建时间
        [Display(Name = "创建日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateDate { get; set; }

        //修改者ID
        public Guid? ModifierID { get; set; }

        //修改时间
        [Display(Name = "修改日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ModifyDate { get; set; }

        //是否删除
        [Display(Name = "是否删除")]
        public Boolean isDeleted { get; set; }
    }
}