using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Resources
{
    //收藏夹
    public class Favorite
    {
        //收藏夹ID
        public Guid FavoriteID { get; set; }

        //收藏夹名称
        [Display(Name="收藏夹名称")]
        [Required(ErrorMessage="收藏夹名称必须填写")]
        public String FavoriteName { get; set; }

        //收藏夹备注
        [Display(Name = "备注")]
        public String Commnet { get; set; }

        //收藏夹包含的资源
        public virtual List<FavoriteResource> ResourceList { get; set; }

        //创建者
        public Guid CreatorId { get; set; }

        //创建时间
        [Display(Name = "创建时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateDate { get; set; }

        //修改者
        public Guid ModifierId { get; set; }


        //修改时间
        public DateTime ModifyDate { get; set; }

        //是否删除
        public Boolean isDeleted { get; set; }
    }
}