using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROnline.Models.Resources
{
    public class ResourceDiscussion
    {
        public Guid Id{get;set;}
        public Guid ResourceId { get; set; }
        public virtual Resource Resource { get; set; }
        public Guid UserId { get; set; }
        public String UserName { get; set; }
        public String Dicussion { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual List<RDReply> Reply { get; set; }

    }
}