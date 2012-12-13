using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Users;
using PROnline.Models;
using PROnline.Models.Files;
using System.IO;
using System.Web.Routing;
using PROnline.Controllers;

namespace PROnline.Src
{
    public class Utils
    {

        //将字符串截断为指定长度,并在后面附加符号
        public static String title(object str, int limit, string replace)
        {

            if (str != null && str.ToString().Length > limit)
            {
                return str.ToString().Substring(0, limit) + replace;
            }
            else if (str != null && str.ToString().Length <= limit)
            {
                return str.ToString();
            }
            else
            {
				return " ";
			}

        }

        public static String title(object str)
        {
            return title(str, 12, "..");
        }

        public static bool IsDateTime(String source)
        {
            try
            {
                DateTime time = Convert.ToDateTime(source);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public static String GetMd5(String input)
        {

            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5");

        }


        public static bool hasAuth(HttpSessionStateBase session,String menuName)
        {
            if (MvcApplication.debug || session["role"].Equals("admin"))
            {
                return true;
            }
            return getMenuLimit(session).Contains(menuName);

        }

        public static List<String> getMenuLimit(HttpSessionStateBase session)
        {
            User user = CurrentUser(session);
            List<String> limit=null;
            if (MvcApplication.debug)
            {
                session["debug"] = "debug";
            }

            if (session["role"] == null)
            {
                session["role"] = "public";
            }
            if (session["menu"] == null)
            {
                PROnlineContext context = new PROnlineContext();
                if(user != null){
                    limit = context.RoleMenu.Where(r => r.RoleId == user.RoleId).Select(r => r.menuId).ToList();
                }else{
                    limit = context.RoleMenu.Where(r => r.RoleId == "public").Select(r => r.menuId).ToList();
                }
                session["menu"] = limit;

            }
            else if(session["menu"] != null)
            {
                limit = (List<String>)session["menu"];
            }


            return limit;
        }

        public static bool DeleteFile(Guid id)
        {
            String root = MvcApplication.FileRootPath;
            PROnlineContext db = new PROnlineContext();
            UploadFile file = db.UploadFiles.Find(id);
            if (file != null)
            {
                String abPath = MvcApplication.FileRootPath + file.Path;
                File.Delete(abPath);
                return true;
                
            }
            return false;
        }



        public static Guid SaveFile(string rootPath,HttpPostedFileBase file)
        {
            String root=MvcApplication.FileRootPath;
            String filePath=root+rootPath;
            PROnlineContext db=new PROnlineContext();
            if(file.ContentLength>0){
                String fileName=file.FileName;
                int ext=fileName.LastIndexOf(".");
                String extention=ext>0?fileName.Substring(ext+1):"";
                String fileXX = Guid.NewGuid() + "." + extention;
                file.SaveAs(filePath+fileXX);
                UploadFile filex=new UploadFile();
                Guid fileId=Guid.NewGuid();
                filex.Id=fileId;
                filex.FileName=fileName;
                filex.Extension=extention;
                filex.Path=rootPath+fileXX;
                filex.Length = file.ContentLength;
                filex.CreateTime = DateTime.Now;
                db.UploadFiles.Add(filex);
                db.SaveChanges();
                return fileId;
            }

            return Guid.Empty;


        }


        public static IQueryable<T> PageIt<T>(Controller controller, IQueryable<T> query)
        {
            int size = MvcApplication.ListSize;
            int start = 0;
            String startStr = controller.Request.Params["start"];
            if (startStr == null)
                start = 1;
            else
                start = Int32.Parse(startStr);
            int count = query.Count();
            controller.ViewBag.count = count / size + 1;
            controller.ViewBag.start = start;

            return query.Skip((start-1) * size).Take(size);
        }

        public static IQueryable<T> PageIt<T>(Controller controller,int size, IQueryable<T> query)
        {
            int start = 0;
            String startStr = controller.Request.Params["start"];
            if (startStr == null)
                start = 1;
            else
                start = Int32.Parse(startStr);
            int count = query.Count();
            controller.ViewBag.count = count / size + 1;
            controller.ViewBag.start = start;

            return query.Skip((start - 1) * size).Take(size);
        }


        public static User CurrentUser(HttpSessionStateBase session){

            if (session != null && session["user"] != null)
            {
                return (User)session["user"];
            }
            else
            {
                //throw new AccountException();
                return null;
                
            }
        }

        public static User CurrentUser(Controller current){
            return CurrentUser(current.Session);
            

        }


    }
}