using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Assessments
{
    //学业辅导评估
    public class StudyAssessment
    {
        public Guid StudyAssessmentID { get; set; }

        //学生ID
        public Guid StudentID { get; set; }
        
        //学生
        public virtual Student Student { get; set; }

        //科目
        [Display(Name = "科目")]
        public String Course { get; set; }

        //分数
        [Display(Name = "分数")]
        [Range(0,100)]
        public int Grade { get; set; }

        //进步情况
        [Display(Name = "进步情况")]
        public String Imporvement { get; set; }

        //评估
        [Display(Name = "评估")]
        public String Conclusion { get; set; }

        //下一步行动：不用学业辅导、继续学生辅导
        [Display(Name = "下一步行动")]
        public String Action { get; set; }

        //创建日期
        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }

        public Guid CreatorID { get; set; }
    }
}