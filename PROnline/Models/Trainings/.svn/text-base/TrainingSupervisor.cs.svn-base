using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;

namespace PROnline.Models.Trainings
{
    //参与培训的督导专家
    public class TrainingSupervisor
    {
        public Guid TrainingSupervisorID { get; set; }

        //所属培训
        public Guid TrainingID { get; set; }

        public virtual Training Training { get; set; }

        //所属督导专家
        public Guid SupervisorID { get; set; }

        public virtual Supervisor Supervisor { get; set; }

        //是否参与
        public Boolean IsAttend { get; set; }
    }
}