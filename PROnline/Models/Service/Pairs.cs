using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PROnline.Models.Users;

namespace PROnline.Models.Service
{
    //配对,生成对子的时候，自动生成预约列表
    public class Pairs
    {
        public Guid PairsID { get; set; }

        //对应的学生
        public Guid StudentID { get; set; }

        public virtual Student Student { get; set; }

        //对应的志愿者
        public Guid VolunteerID { get; set; }

        public virtual Volunteer Volunteer { get; set; }

        //对应的督导
        public Guid SupervisorID { get; set; }

        public virtual Supervisor Supervisor { get; set; }

        //所有预约
        public virtual IList<PairAppointment> AppointmentList { get; set; }

        //正常、申请解除、解除
        public String State { get; set; }

        //解除配对时间
        public DateTime CeaseDate { get; set; }

        //对子开始日期

        [Required(ErrorMessage = "该字段为必填自段，请填写")]
        public DateTime StartDate { get; set; }

        //对子结束日期
        [Required(ErrorMessage = "该字段为必填自段，请填写")]
        public DateTime EndDate { get; set; }

        //创建日期
        public DateTime CreateDate { get; set; }
       
     //  public String test { get; set; }

        [NotMapped]
        public bool IsOK { get; set; }

        [NotMapped]
        public PairAppointment TodayAppointment { get; set; }
    }
}