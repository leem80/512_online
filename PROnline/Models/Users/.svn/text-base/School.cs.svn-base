using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PROnline.Models.Service;

namespace PROnline.Models.Users
{
    //学校实体
    public class School
    {
        public Guid SchoolID {get; set;}

        [MaxLength(1000)]
        [Display(Name = "学校编号")]
        [Required(ErrorMessage = "学校编号必须填写")]
        [RegularExpression("[0-9,a-z,A-Z]{3,10}",ErrorMessage="只能是3到11位数字或者英文字母")]
        public String SchoolNo { get; set; }

        [MaxLength(20)]
        [Display(Name = "学校名称")]
        [Required(ErrorMessage = "学校名称必须填写")]
        [Remote("SchoolNameAvailable", "School", AdditionalFields = "SchoolID", ErrorMessage = "学校名称已经存在")]
        public String SchoolName { get; set; }

        [MaxLength(1000)]
        [Display(Name = "学校介绍")]
        public String SchoolIntro { get; set; }

        [MaxLength(100)]
        [Display(Name = "备注")]
        public String SchoolCommnet { get; set; }

        //创建者ID
        public Guid CreatorID { get; set; }

        //创建时间
        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }

        //修改者ID
        public Guid ModifierID { get; set; }

        //修改时间
        [Display(Name = "修改日期")]
        public DateTime? ModifyDate { get; set; }

        //是否删除
        [Display(Name = "是否删除")]
        public Boolean isDeleted { get; set; }

        public virtual List<Student> Students { get; set; }
        [NotMapped]
        public List<Pairs> Pairs { get;set; }
        [NotMapped]
        public int StudentCount { get; set; }

        [NotMapped]
        public float TotalServiceTime { get; set; }
    }
} 