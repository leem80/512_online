using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models.Manage;
using PROnline.Src;

namespace PROnline.Controllers.Manage
{
    public class ConfigController : Controller
    {
        //
        // GET: /Config/

        public ActionResult Edit()
        {
            var configBean = new ConfigBean();
            configBean.TableSize = MvcApplication.TableSize;
            configBean.ListSize = MvcApplication.ListSize;
            configBean.SMSCon = MvcApplication.SMSCom;
            return View(configBean);
        }

        [HttpPost]
        public ActionResult Edit(ConfigBean config)
        {

            Config.setValue("sms_com",config.SMSCon);
            Config.setValue("table_size", config.TableSize);
            Config.setValue("list_size", config.ListSize);
            MvcApplication.SMSCom = config.SMSCon;
            MvcApplication.TableSize = config.TableSize;
            MvcApplication.ListSize = config.ListSize;
            MvcApplication.SMSStatus = SMS.Connect();
            return RedirectToAction("Edit","Config");
        }

        public ActionResult TestMSG()
        {

            return View();
        }

        [HttpPost]
        public ActionResult TestMSG(string number, string msg)
        {
            String msgx = SMS.SendMessage(number, msg);
            return Content(msgx);
        }

    }
}
