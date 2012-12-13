using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROnline.Controllers
{
    public class AccountException:Exception
    {
        public String Url{get;set;}
    }
}