using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Trainings
{
    public class TrainingFeedback
    {
        public Guid TrainingFeedbackID { get; set; }

        public Guid TrainingID { get; set; }
        public virtual Training Training { get; set; }

        public Guid VolunteerID { get; set; }
        public virtual Volunteer Volunteer { get; set; }

        //自身收获
        [Required(ErrorMessage="该项为必填")]
        public String SelfGains { get; set; }

        //培训建议
        public String TrainingAdvice { get; set; }
    }
}