using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Web.Mvc;
using PROnline.Models;
using PROnline.Models.Users;
using PROnline.Src;

namespace PROnline.Controllers.Users
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        PROnlineContext db = new PROnlineContext();

        //显示用户当前的状态
        public ActionResult Status()
        {
            User usr = Utils.CurrentUser(this);
            if (usr!=null)
            {

                PROnlineContext db = new PROnlineContext();
                ViewBag.MsgCount = db.Letter.Where(r => r.ReceiverID == usr.Id).Where(r => r.IsRead == "未读").Count().ToString();
  

            }
            return View();

        }


        public  ActionResult UserMessage(){
            User usr = Utils.CurrentUser(this);
            return View(usr);
        }

        //用户注销
        public ActionResult Logout()
        {
            Session.RemoveAll();
            HttpCookie cookiex = new HttpCookie("user");
            cookiex.HttpOnly = false;
            cookiex.Expires = DateTime.Now.AddYears(-1);
            Response.AppendCookie(cookiex);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            ViewBag.session = Session["user"];
            return View();
        }


        public ActionResult Logon()
        {
            return View();
        }


        [NonAction]
        public ActionResult UserInfo()
        {
            if (Session["uid"] != null)
            {
                return View();
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult Logon(LogonModel logon)
        {
            User user = new User();
            user.Id = Guid.NewGuid();
            user.UserName = logon.UserName;
            user.Password = Utils.GetMd5(logon.Password);
            user.CreateDate = DateTime.Now;
            user.ModifyDate = DateTime.Now;
            user.LastLoginDate = DateTime.Now;
            user.UserType = null;
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoginMini()
        {
            string from = Request.Params["from"]; 
            User user = Utils.CurrentUser(this);
            ViewBag.user = user;
            ViewBag.from = from;
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            String password = Utils.GetMd5(login.Password);
            String name = login.UserName;
            List<User> user = db.Users.Where(t => t.UserName == name).Where(t => t.isDeleted == false)
                .Where(t => t.Password == password).ToList();
            if (user.Count == 0)
            {
                ModelState.AddModelError("Error", "用户名或密码错误！");
            }
            else
            {
                Boolean isVerify = user[0].isVerify;
                if ((user[0].RoleId == "volunteer" || user[0].RoleId == "supervisor" || user[0].RoleId == "donator") && !isVerify)
                {
                    ModelState.AddModelError("Error", "用户" + user[0].UserName + "正在审核中！");
                } 
                else
                {
                    String roleId = user[0].RoleId;
                    List<String> limit;

                    limit = db.RoleMenu.Where(r => r.RoleId == roleId).Select(r => r.menuId).ToList();

                    Session["uid"] = user[0].Id;
                    Session["user"] = user[0];
                    Session["menu"] = limit;
                    Session["role"] = user[0].RoleId;
                    if (login.Cookie == "cookie")
                    {
                        HttpCookie cookie = new HttpCookie("user");
                        cookie.Expires = DateTime.Now.AddDays(7);
                        cookie["user"] = user[0].Id.ToString();
                        cookie["password"] = user[0].Password;
                        Response.Cookies.Add(cookie);
                    }
                    if (Request.Params["from"] != "")
                    {
                        return Redirect(Request.Params["from"]);
                    }
                    //return RedirectToAction("Index", "Home");
                    return RedirectToAction("Index", "WorkSpace");
                }
            }
            return View(login);
        }

    }
}
