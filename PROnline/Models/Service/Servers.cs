using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Service
{
    //视讯服务器
    public class Servers
    {
        //服务器ID
        public int ServersID { get; set; }

        //服务器名称，可以使用学校名称，便于关联
        [Display(Name = "服务器名称")]
        public String ServerName { get; set; }

        //服务器IP
        [Display(Name = "服务器IP")]
        public String ServerIP { get; set; }

        //服务器端口
        [Display(Name = "服务器端口")]
        public int ServerPort { get; set; }

        //是否可用，用于心跳检测
        [Display(Name = "是否可用")]
        public Boolean IsAvaiable { get; set; }

        //备注
        [Display(Name = "备注")]
        public String Comment { get; set; }       
    }
}