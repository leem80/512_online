using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROnline.Models
{
    public class Error
    {
        /// <summary>
        /// 网页标题
        /// </summary>
        public string ErroCode { get; set; }
        public string Heading { get; set; }

        /// <summary>
        /// 网页内容
        /// </summary>
        public string Message { get; set; }

    }
}