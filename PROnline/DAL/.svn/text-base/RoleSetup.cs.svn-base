using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;

namespace PROnline.DAL
{
    public class RoleSetup:IDataSetup
    {
        public void init(Models.PROnlineContext context)
        {
            //添加角色
            var add = new List<Role>{
                new Role{Id="public",Note="公共",isDelete=false,CanDelete=false,CreateDate=DateTime.Now},
                new Role{Id="student",Note="学生",isDelete=false,CanDelete=false,CreateDate=DateTime.Now},
                new Role{Id="parent",Note="家长",isDelete=false,CanDelete=false,CreateDate=DateTime.Now},
                new Role{Id="teacher",Note="教师",isDelete=false,CanDelete=false,CreateDate=DateTime.Now},
                new Role{Id="admin",Note="超级管理员",isDelete=false,CanDelete=false,CreateDate=DateTime.Now},
                new Role{Id="manager",Note="系统管理",isDelete=false,CanDelete=false,CreateDate=DateTime.Now},
                new Role{Id="school_manager",Note="校方管理员",isDelete=false,CanDelete=false,CreateDate=DateTime.Now},
                new Role{Id="volunteer",Note="志愿者",isDelete=false,CanDelete=false,CreateDate=DateTime.Now},
                new Role{Id="supervisor",Note="督导专家",isDelete=false,CanDelete=false,CreateDate=DateTime.Now},
                new Role{Id="donator",Note="爱心人士",isDelete=false,CanDelete=false,CreateDate=DateTime.Now},
                new Role{Id="team_leader",Note="小组长",isDelete=false,CanDelete=false,CreateDate=DateTime.Now},
                new Role{Id="service_manager",Note="服务组织",isDelete=false,CanDelete=false,CreateDate=DateTime.Now},
                new Role{Id="project_manager",Note="项目管理",isDelete=false,CanDelete=false,CreateDate=DateTime.Now}
            };
            foreach (Role rl in add)
            {
                context.Role.Add(rl);
            }

            //添加公共菜单
            var addMenu = new List<RoleMenu>{
                new RoleMenu{Id=Guid.NewGuid(),RoleId=Role.PUBLIC,menuId="main"},
                new RoleMenu{Id=Guid.NewGuid(),RoleId=Role.PUBLIC,menuId="volunteerService"},
                new RoleMenu{Id=Guid.NewGuid(),RoleId=Role.PUBLIC,menuId="tutorshipArrange"},
                new RoleMenu{Id=Guid.NewGuid(),RoleId=Role.PUBLIC,menuId="donation"},
                new RoleMenu{Id=Guid.NewGuid(),RoleId=Role.PUBLIC,menuId="activity"},
                new RoleMenu{Id=Guid.NewGuid(),RoleId=Role.PUBLIC,menuId="heartWish"},
                new RoleMenu{Id=Guid.NewGuid(),RoleId=Role.PUBLIC,menuId="resourcesList"},
                new RoleMenu{Id=Guid.NewGuid(),RoleId=Role.PUBLIC,menuId="projectControl"}
            };
            foreach (RoleMenu rl in addMenu)
            {
                context.RoleMenu.Add(rl);
            }
            context.SaveChanges();
        }

    }
}