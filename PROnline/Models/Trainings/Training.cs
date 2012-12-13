using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PROnline.Models;
using PROnline.Models.Users;

namespace PROnline.Models.Trainings
{
    //培训
    public class Training
    {
        //培训ID
        public Guid TrainingID { get; set; }

        //培训主题
        [Display(Name = "培训主题")]
        [Required(ErrorMessage = "培训主题必填")]
        public String Title { get; set; }

        //安排
        [Display(Name = "培训安排")]
        public String Arrangement { get; set; }

        //开始日期
        [Display(Name = "日期")]
        [Required(ErrorMessage = "日期必填")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        //开始时间
        [Display(Name = "开始时间")]
        public String StartTime { get; set; }

        //结束时间
        [Display(Name = "结束时间")]
        public String EndTime { get; set; }

        //培训地点
        [Display(Name = "培训地点")]
        [Required(ErrorMessage = "培训地点必填")]
        public String Locale { get; set; }
    
        //参与的督导专家
        public virtual IList<TrainingSupervisor> SupervisorList { get; set; }

        //参与的志愿者
        public virtual IList<TrainingVolunteer> VolunteerList { get; set; }

        //待审核、已通过、未通过、已取消
        public String State { get; set; }

        //培训申请人
        public Guid CreatorID { get; set; }

        //培训申请时间
        public DateTime CreateTime { get; set; }

        //督导创建的培训，需要审核；管理员创建的不需要审核,true表明已经审核过(通不通过由State标识)
        public Boolean isVerify { get; set; }
    }
}