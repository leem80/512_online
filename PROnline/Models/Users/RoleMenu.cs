using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROnline.Models.Users
{
    //角色菜单权限
    public class RoleMenu
    {
        public Guid Id { get; set; }
        //角色
        public String RoleId { get; set; }
        public Role Role { get; set; }
        //菜单ID
        public String menuId { get; set; }
    }
}