using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PROnline.Models.Users;
using System.Security.Cryptography;
using PROnline.Src;
using PROnline.Models;
using PROnline.Models.Resources;
using PROnline.Models.Notices;
using System.Data;

namespace PROnline.DAL
{
    public class DataInit :DropCreateDatabaseIfModelChanges<PROnlineContext>
    {

        protected override void Seed(PROnlineContext context)
        {
            base.Seed(context);

            new UserSetup().init(context);
            new RoleSetup().init(context);
            new NoticeSetup().init(context);
            new BaseSetup().init(context);
            new TestSetup().init(context);
            new ShortMessageTemplateSetup().init(context);
        }


    }
}