using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROnline.Models.Resources
{
    public class ResourceFile
    {
        public Guid Id { get; set; }
        public Guid? ResourceId { get; set; }
        public Guid? FileId { get; set; }
        public bool IsUpload { set; get; }
        public String Url { get; set; }
        public String Name { get; set; }
        public String MIME { get; set; }

    }

    public class DownloadModel
    {
        public String Name{get;set;}
        public String Url{get;set;}

    }
}