using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROnline.Controllers.Users
{
    public class ImportFactory
    {
        public static IUserImport GetImport(String type)
        {
            if ("class" == type)
            {
                return new ClassImport(); 
            }

            if ("test" == type)
            {
                return new UserPTestImport(); 
            }

            return null;

        }
    }
}