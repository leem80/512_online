using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;

namespace PROnline.Models.Trainings
{
    //参与培训的志愿者
    public class TrainingVolunteer
    {
        public Guid TrainingVolunteerID { get; set; }

        public Guid TrainingID { get; set; }

        public virtual Training Training { get; set; }

        public Guid VolunteerID { get; set; }

        public virtual Volunteer Volunteer { get; set; } 

        //是否参与
        public Boolean IsAttend { get; set; }

        //是否已反馈
        public Boolean isFeedback { get; set; }
    }
}