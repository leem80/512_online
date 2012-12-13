using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Resources
{
    public class Answer
    {
        public Guid AnswerId { get; set; }

        public Guid QuestionId { get; set; }


        public Guid UserId { get; set; }

        public virtual User User { get; set; }


        //答案内容
        public string AnswerContent { get; set; }

        //答案的备注
        public string Comment { get; set; }

        //答案类型：正确，错误，其他
        public string AnswerType { get; set; }

        public DateTime CreateDate { get; set; }


    }
}