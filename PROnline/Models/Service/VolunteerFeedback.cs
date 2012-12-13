using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Service
{
    public class VolunteerFeedback
    {
        //志愿者反馈表ID
        public Guid VolunteerFeedbackID { get; set; }

        //对应的志愿者
        public Guid volunteerID { get; set; }
        public virtual Volunteer volunteer { get; set; }


        //对应的预约
        public Guid PairAppointmentID {get; set;}
        public virtual PairAppointment PairAppointment{get; set;}

        //辅导效果
        [Display(Name = "辅导效果")]
        [MaxLength(20)]
        [Required]
        public String Results { get; set; }

        //心得体会
        [Display(Name = "辅导情况")]
        [MaxLength(1000)]
        public String Feelings { get; set; } 
    }

    }
