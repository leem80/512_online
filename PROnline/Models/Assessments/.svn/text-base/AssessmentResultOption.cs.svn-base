using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Assessments
{
    public class AssessmentResultOption
    {
        public Guid AssessmentResultOptionID { get; set; }

        [Required]
        public Guid AssessmentOptionID { get; set; }

        public virtual AssessmentResult AssessmentResult { get; set; }

        public virtual AssessmentOption AssessmentOption { get; set; }
    }
}