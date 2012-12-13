using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Assessments
{
    public class AssessmentOption
    {
        public Guid AssessmentOptionID { get; set; }

        public virtual AssessmentQuestion AssessmentQuestion { get; set; }

        //选项（无、偶尔、经常等，一般为5个）
        public String OptionTitle { get; set; }

        //分值
        [Range(0, 100,ErrorMessage="只能是0-100的整数")]
        [Required(ErrorMessage="分值必需填写")]
        public int OptionValue { get; set; }

        //顺序
        public int Index { get; set; }

        //创建者ID
        public Guid CreatorID { get; set; }

        //创建时间
        public DateTime CreateDate { get; set; }

        //修改者ID
        public Guid? ModifierID { get; set; }

        //修改时间
        public DateTime? ModifyDate { get; set; }
    }
}