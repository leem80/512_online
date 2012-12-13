using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NPOI.HSSF.UserModel;

namespace PROnline.Controllers.Users
{


    public class ImportResult
    {
        public  const  String SUCCESS="success";
        public  const  String FAILURE="failure";
        public  const  String PROBLEM="problem";
        public  String Status {get;set;}
        public String Description {get;set;}
        public Object Data { get; set; }

    }


    public interface IUserImport
    {
        List<ImportResult> import(HSSFWorkbook input);
    }
}