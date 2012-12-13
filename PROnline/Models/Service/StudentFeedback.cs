using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Service
{
    //学生反馈表
    public class StudentFeedback
    {
        //学生反馈表ID
        public Guid StudentFeedbackID { get; set; }

        //对应的学生
        public Guid StudentID { get; set; }
        public virtual Student Student { get; set; }

        //对应的志愿者
        //public Guid VolunteerID { get; set; }
      //  public virtual Volunteer Volunteer { get; set; }

        //对应的预约
        public Guid PairAppointmentID {get; set;}
        public virtual PairAppointment PairAppointment{get; set;}

        //收获
        [Display(Name = "收获")]
        [MaxLength(20)]
        [Required]
        public String Results { get; set; }

        //心得体会
        [Display(Name = "心得体会")]
        [MaxLength(1000)]
        public String Feelings { get; set; } 
    }
}