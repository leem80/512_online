using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PROnline.Src;
using PROnline.Models.Users;
using PROnline.Controllers;

namespace PROnline
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        public static String FileRootPath;
        public static int TableSize = 5;
        public static int ListSize = 5;
        public static int NavBarSize=5;
        public static int TopCount = 10;
        public static bool debug = false;
        public static int SMSStatus = 0;
        public static int SMSCom=3;
        public static String ServerPath = "";




        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new []{"Users"}
            );

        }

        protected void Application_Start()
        {
            //AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", true);

            AreaRegistration.RegisterAllAreas();
            ServerPath = Server.MapPath("~/");
            FileRootPath = ServerPath + "Uploads";
            Menu.loadMenu(ServerPath);
            System.Data.Entity.Database.SetInitializer(new PROnline.DAL.DataInit());
            //SMSStatus=SMS.Connect();
            //SMSCom=Config.getIntValue("sms_com");
            TableSize = Config.getIntValue("table_size");
            ListSize = Config.getIntValue("list_size");
            //Start Task
            new Task().StartTask();
            GlobalFilters.Filters.Add(new HandleErrorAttribute()); 
            RegisterGlobalFilters(GlobalFilters.Filters);
            
            RegisterRoutes(RouteTable.Routes);

        }


        
        //处理错误
        protected void Application_Error(object sender, EventArgs e)
        {
            if (!HttpContext.Current.IsCustomErrorEnabled)
            {
                return;
            }

            var exception = Server.GetLastError();
            if (true)
            {
                var httpException = new HttpException(null, exception);

                var routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                routeData.Values.Add("action", "Index");
                routeData.Values.Add("httpException", httpException);

                Server.ClearError();

                var errorController = ControllerBuilder.Current.GetControllerFactory().CreateController(
                    new RequestContext(new HttpContextWrapper(Context), routeData), "Error");

                errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
            else
            {
                return;
            }
        }

    }
}