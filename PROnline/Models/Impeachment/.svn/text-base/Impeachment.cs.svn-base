using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;

namespace PROnline.Models.Impeachment
{
    //举报
    public class Impeachment
    {
        //举报ID
        public Guid ImpeachmentID { get; set; }

        //URL
        public String URL {get; set;}

        //问题描述
        public String Description {get; set;}

        //处理方式：无效、删除、修改
        public String DealType{get; set;}

        //处理结果
        public String Result { get; set; }

        //举报者
        public User ImpeachmentUser { get; set; }

        //举报日期
        public DateTime ImpeachmentDate{get; set;}

        //处理者
        public User Operator { get; set; }

        //处理时间
        public DateTime OperateDate { get; set; }
    }
}