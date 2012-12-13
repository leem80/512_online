using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;

namespace PROnline.DAL
{
    public class UserSetup:IDataSetup
    {
        public void init(Models.PROnlineContext context)
        {
            var add = new List<UserType>{
                new UserType{Id="student",Note="学生"},
                new UserType{Id="teacher",Note="教师"},
                new UserType{Id="admin",Note="超级管理员"},
                new UserType{Id="parent",Note="家长"},
                new UserType{Id="manager",Note="系统管理员"},
                new UserType{Id="supervisor",Note="督导专家"},
                new UserType{Id="volunteer",Note="志愿者"},
                new UserType{Id="school_manager",Note="校方管理员"},
                new UserType{Id="donator",Note="爱心人士"},
                new UserType{Id="team_leader",Note="小组长"}

            };
            foreach (UserType rt in add)
            {
                context.UserType.Add(rt);
            }
            context.SaveChanges();
        }
    }
}