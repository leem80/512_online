using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Goodnesses
{
    //风采类型
    public class GoodnessType
    {
        //风采类型ID
        public Guid GoodnessTypeID { get; set; }

        //类型名称
        [Display(Name = "风采类型")]
        [MaxLength(20)]
        public String TypeName { get; set; }

        //风采类型简介
        [Display(Name = "风采简介")]
        [MaxLength(100)]
        public String Introduction { get; set; }

        //风采类型备注
        [Display(Name = "风采备注")]
        [MaxLength(100)]
        public String Comment { get; set; }

        //创建者
        public User Creator { get; set; }

        //创建时间
        public DateTime CreateDate { get; set; }

        //修改者
        public User Modifier { get; set; }

        //修改时间
        public DateTime ModifyDate { get; set; }

        //是否删除
        public Boolean isDeleted { get; set; }
    }
}