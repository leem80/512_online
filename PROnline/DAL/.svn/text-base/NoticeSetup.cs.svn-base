using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Notices;

namespace PROnline.DAL
{
    public class NoticeSetup:IDataSetup
    {
        public void init(Models.PROnlineContext context)
        {
            List<NoticeType> types = new List<NoticeType>{
                new NoticeType{NoticeTypeID=Guid.NewGuid(),TypeName="志愿者招募",Introduction="志愿者招募", CreateDate=DateTime.Now,isDeleted=false,ModifyDate=DateTime.Now}
            };
            foreach(NoticeType type in types){
                context.NoticeType.Add(type);
            }
            context.SaveChanges();
        }
    }
}