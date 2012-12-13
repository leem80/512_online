using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PROnline.Models.Users;
using System.Web.Mvc;

namespace PROnline.Models.HeartWishes
{
    //心声类型
    public class HeartWishType
    {
        //心声类型ID
        public Guid HeartWishTypeID { get; set; }

        //心声类型
        [Remote("HeartWishTypeNameAvailable", "HeartWishType", AdditionalFields = "HeartWishTypeID", ErrorMessage = "类型不能重复")]
        [Display(Name = "心声类型")]
        [Required(ErrorMessage="名称不能为空")]
        [MaxLength(20)]
        public String TypeName { get; set; }

        //心声类型简介
        [Display(Name = "介绍")]
        [MaxLength(100)]
        public String Introduction { get; set; }

        //心声类型备注
        [Display(Name = "备注")]
        [MaxLength(50)]
        public String Comment { get; set; }

        //允许回复的用户类型:所有用户/督导/督导与志愿者
        [Display(Name = "回复用户")]
        public String ReplyUserType { get; set; }

        //创建者ID
        public Guid CreatorID { get; set; }

        //创建时间
        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }

        //修改者ID
        public Guid ModifierID { get; set; }

        //修改时间
        [Display(Name = "修改日期")]
        public DateTime? ModifyDate { get; set; }

        //是否删除
        [Display(Name = "是否删除")]
        public Boolean isDeleted { get; set; }
    }
}