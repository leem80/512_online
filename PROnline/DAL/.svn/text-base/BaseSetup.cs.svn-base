using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models;
using PROnline.Models.Users;
using PROnline.Src;

namespace PROnline.DAL
{
    public class BaseSetup : IDataSetup
    {


        public void init(PROnlineContext context)
        {
            //添加超级管理员
            var user = new User();
            user.CreateDate = DateTime.Now;
            user.ModifyDate = DateTime.Now;
            user.LastLoginDate = DateTime.Now;
            user.UserName = "admin";
            user.RealName = "超级管理员";
            user.UserTypeId = UserType.ADMIN;
            user.Password = Utils.GetMd5("123456");
            user.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e5");
            user.RoleId = "admin";
            user.isDeleted = false;
            user.isVerify = true;
            user.isHaveTraining = false;
            context.Users.Add(user);
            context.SaveChanges();



            //添加审核
                var add = new List<VerifyState>{
                    new VerifyState{Id="no",Note="未审核", Index=1},
                    new VerifyState{Id="submit",Note="提交审核", Index=2},
                    new VerifyState{Id="success",Note="审核成功",Index=3},
                    new VerifyState{Id="failure",Note="审核失败",Index=5},
                    new VerifyState{Id="finish",Note="完成",Index=5},
                };
            foreach (VerifyState vf in add)
            {
                context.VerifyState.Add(vf);
            }
            context.SaveChanges();
        }
    }
}