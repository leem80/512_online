using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Assessments
{
    //测评结果
    public class AssessmentResult
    {
        public Guid AssessmentResultID { get; set; }

        //接受测评的学生
        public Guid StudentID { get; set; }

        public virtual Student Student { get; set; }

        //结论
        [MaxLength(100)]
        [Display(Name = "结论")]
        public String Conclusion { get; set; }

        //建议
        [MaxLength(100)]
        [Display(Name = "建议")]
        public String Advice { get; set; }

        public virtual IList<AssessmentResultOption> ResultOptionList { get; set; }

        //下一步措施：继续治疗、治愈、转院
        [MaxLength(100)]
        [Display(Name = "下一步措施")]
        public String Action { get; set; }

        public DateTime CreateDate { get; set; }
    }
}