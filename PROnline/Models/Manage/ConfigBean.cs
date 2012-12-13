using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Manage
{
    public class ConfigBean
    {
        [Required]
        [Range(1, 30, ErrorMessage = "端口只能为1-30")]
        [Display(Name="短信猫所在COM端口")]
        public int SMSCon { get; set; }
        [Required]
        [Range(10, 50, ErrorMessage = "数量只能为10-50")]
        [Display(Name = "表格显示数量")]
        public int TableSize { get; set; }

        [Required]
        [Display(Name = "列表显示数量")]
        [Range(10,50,ErrorMessage="数量只能为10-50")]
        public int ListSize { get; set; }
    }
}