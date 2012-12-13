using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;

namespace PROnline.Models.Goodnesses
{
    //风采
    public class Goodness
    {
        //风采ID
        public Guid GoodnessID { get; set; }

        //标题
        public String Title { get; set; }

        //内容
        public String Content { get; set; }

        public Guid GoodnessTypeID { get; set; }

        //风采类型
        public virtual GoodnessType GoodnessType { get; set; }

        //审核状态(草稿|待审核|通过|未通过)
        public String VerifyingState { get; set; }

        //未通过原因（审核意见）
        public String CauseOfDisapproval { get; set; }

        //浏览数
        public int BrowseringNum { get; set; }

        //审核者
        public User Verifier { get; set; }

        //审核日期
        public DateTime VerifyingDate { get; set; }

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