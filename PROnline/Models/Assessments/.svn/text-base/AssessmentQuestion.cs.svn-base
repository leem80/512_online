using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Assessments
{
    public class AssessmentQuestion
    {
        //评估题库
        public Guid AssessmentQuestionID { get; set; }

        //题目
        [Display(Name = "题目")]
        [Required]
        public String Title { get; set; }

        public virtual IList<AssessmentOption> AssessmentOptionList { get; set; }

        //创建者ID
        public Guid CreatorID { get; set; }

        //创建时间
        public DateTime CreateDate { get; set; }

        //修改者ID
        public Guid? ModifierID { get; set; }

        //修改时间
        public DateTime? ModifyDate { get; set; }

        //是否删除
        public Boolean isDeleted { get; set; }
    }
}