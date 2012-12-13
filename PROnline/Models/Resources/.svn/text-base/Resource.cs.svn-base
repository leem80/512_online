using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Resources
{
    //资源
    public class Resource
    {
        //资源ID
        public Guid ResourceID { get; set; }

        //资源标题
        [Display(Name="资源标题")]
        [Required(ErrorMessage="必须填写")]
        public String Title { get; set; }

        //介绍
        [Display(Name = "资源内容")]
        public String Content { get; set; }

        //备注
        [Display(Name = "备注")]
        public String Comment { get; set; }

        [Display(Name = "作者")]
        public String Author { get; set; }

        public DateTime CreationDate { get; set; }

        //资源类型
        [Display(Name = "资源类型")]
        [Required(ErrorMessage = "必须填写")]
        public Guid ResourceTypeId { get; set; }

        public virtual ResourceType ResourceType { get; set; }

        //审核状态
        public string VerifyStateId { get; set; }
        public virtual VerifyState VerifyState { get; set; }

        //审核者
        public Guid VerifierId { get; set; }

        //审核日期
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime VerifyingDate { get; set; }

        //创建者
        public Guid CreatorId { get; set; }
        public virtual User Creator { get; set; }

        //创建时间
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateDate { get; set; }

        //修改者
        public Guid  ModifierId{get;set;}

        //修改时间
        public DateTime ModifyDate { get; set; }

        //是否删除
        public Boolean isDeleted { get; set; }

        //附件
        public virtual List<ResourceFile> Filexs { get; set; }

        
    }
}