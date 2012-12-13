using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROnline.Models
{
    public class VerifyState
    {
        public string Id { get; set; }
        public String Note { get; set; }

        //排列顺序,用于列表输出排序
        public int Index { get; set; }

        //未提交审核,即草稿状态。创建者可以修改或者删除，参活动、培训、爱心捐助模块
        public static string NO = "no";

        //提交审核。创建者不能修改或者删除
        public static string SUBMIT = "submit";

        //审核成功
        public static string SUCCESS = "success";

        //审核失败
        public static string FAILURE = "failure"; 

        //完成(用于爱心捐助等)，参与者可以提交活动风采等
        public static string FINISH = "finish";
    }
}