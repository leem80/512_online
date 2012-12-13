using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models;

namespace PROnline.DAL
{
    public interface IDataSetup
    {
        void init(PROnlineContext context);
    }
}