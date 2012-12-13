using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models;
using PROnline.Models.Files;
using System.IO;
using PROnline.Src;

namespace PROnline.Controllers.Files
{
    public static class MIME
    {
        private static Dictionary<String, String> container;

        static MIME(){
            container= new Dictionary<string, string>();
            container.Add("swf", "application/x-shockwave-flash");
        }
        public static String get(String key)
        {
            if (container.ContainsKey(key))
            {
                return container[key];
            }
            else
            {
                return "application/octet-stream";
            }
        }


    }

    public class FilesController : Controller
    {
        //
        // GET: /Files/

        private PROnlineContext db = new PROnlineContext();

        public ActionResult Index()
        {
            return View();
        }



        public FileStreamResult GetFile(Guid uuid)
        {
            UploadFile file = db.UploadFiles. Find(uuid);
            String filePath = file.Path;
            String rootPath=MvcApplication.FileRootPath;
            FileStream stream=new FileStream(rootPath+filePath,FileMode.Open);
            Response.AddHeader("Content-Length", file.Length.ToString());
            if (file.Extension != "swf")
            {
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.FileName);
            }
            return new FileStreamResult(stream, MIME.get(file.Extension));
            
        }

        [HttpPost]
        public ActionResult Save(HttpPostedFileBase upload)
        {
            Guid guid=Utils.SaveFile("\\default\\",upload);
            String path= "/Files/GetFile?uuid="+guid.ToString();
            String callback = Request.Params["CKEditorFuncNum"]; 
            ViewBag.FilePath = path;
            ViewBag.CallBack = callback;
            return View();

        }

    }
}
