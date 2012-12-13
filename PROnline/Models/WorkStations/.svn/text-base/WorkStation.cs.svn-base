using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using System.Web.Mvc;
using PROnline.Models.Teams;
using PROnline.Models.Service;

namespace PROnline.Models.WorkStations
{
    public class WorkStation
    {
        //工作站ID
        public Guid WorkStationID { get; set; }

        //工作站名称
        [Required(ErrorMessage = "工作站名称必须填写")]
        [Display(Name = "工作站名称")]
        [MaxLength(30)]
        [Remote("NameCheck", "WorkStation", ErrorMessage = "工作站名不能重复")]
        public String WorkStationName { get; set; }

        //工作站负责人
        [Required(ErrorMessage = "站长名称必须填写")]
        [Display(Name = "站长名称")]
        [MaxLength(10)]
        public String WorkStationerName { get; set; }

        //负责人联系电话
        [Required(ErrorMessage = "联系方式必须填写")]
        [Display(Name = "联系方式")]
        public String Telephone { get; set; }

        //创建者
        public Guid CreatorID { get; set; }

        //工作站创建日期
        [Display(Name = "工作站创建日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateDate { get; set; }

        //修改者ID
        public Guid ModifierID { get; set; }

        //修改时间
        [Display(Name = "修改日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ModifyDate { get; set; }

        //是否删除
        public Boolean isDeleted { get; set; }

        public virtual List<Team> Teams { get; set; }

        //志愿者人数
        [NotMapped]
        public int Vcount { get; set; }


        //今日辅导
        [NotMapped]
        public int PcountToday { get; set; }

        //总服务时间
        [NotMapped]
        public float TotalServiceTime { get; set; }

        [NotMapped]
        public List<Pairs> Pairs { get; set; }
    }
}