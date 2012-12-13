using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PROnline.Models.Users;

namespace PROnline.Models.Activities
{
    //活动
    public class Activity
    {
        //活动ID
        public Guid ActivityID { get; set; }
        
        //活动名称
        [Required(ErrorMessage="活动名称必须填写")]
        [Display(Name = "活动名称")]
        [MaxLength(30)]
        public String ActivityName { get; set; }

        //活动开始日期
        [Required(ErrorMessage = "活动开始日期必须填写")]
        [Display(Name = "活动开始日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ActivityStartDate { get; set; }

        //活动地点
        [Required(ErrorMessage = "活动地点必须填写")]
        [Display(Name = "活动地点")]
        [MaxLength(50)]
        public String ActivityAddr { get; set; }

        //活动负责人
        [Required(ErrorMessage = "活动负责人必须填写")]
        [Display(Name = "活动负责人")]
        public String PersonInCharge { get; set; }

        //负责人联系电话
        [Required(ErrorMessage = "负责人联系电话必须填写")]
        [Display(Name = "联系电话")]
        [RegularExpression("[0-9]+", ErrorMessage = "只能是数字")]
        public String Telephone { get; set; }

        //活动安排
        [Display(Name = "活动安排")]
        [MaxLength(2000)]
        public String ActivityArrangement { get; set; }

        //审核状态
        //NO:草稿
        //SUBMIT:提交
        //SUCCESS：审核通过
        //FAILURE：审核未通过 
        [Display(Name = "审核状态")]
        public string VerifyState { get; set; }
        //public virtual VerifyState VerifyState { get; set; }

        //未通过原因
        [Display(Name = "未通过原因")]
        public String Cause { get; set; }

        //活动人员列表
        public virtual List<ActivityMember> memberList { get; set; }

        //创建者
        public Guid CreatorID { get; set; }

        //创建时间
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateDate { get; set; }

        //修改者ID
        public Guid ModifierID { get; set; }

        //修改时间
        [Display(Name = "修改日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ModifyDate { get; set; }

        //参与的学生人数
        public int StudentCount { get; set; }

        //参与的志愿者人数
        public int VolunteerCount { get; set; }

        //是否删除
        public Boolean isDeleted { get; set; }
    }
}