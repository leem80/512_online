using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Resources
{
    //收藏夹资源
    //注：可以直接删除
    public class FavoriteResource
    {
        //收藏夹资源ID
        public Guid FavoriteResourceID { get; set; }

        //所属收藏夹
        [Display(Name="收藏夹")]
        [Required(ErrorMessage="需要选择一个收藏夹")]
        public Guid FavoriteId { get; set; }
        public virtual Favorite Favorite { get; set; }

        [Required(ErrorMessage = "名称必须填写")]
        [Display(Name = "标题")]
        public String Title { get; set; }
        [Display(Name = "资源地址")]
        public String Url { get; set; }

        //备注
        [Display(Name = "备注")]
        public String Commnet { get; set; }

        //创建者
        public Guid UserId { get; set; }
        public virtual User Creator { get; set; }

        //创建时间
        public DateTime CreateDate { get; set; }
    }
}