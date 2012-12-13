using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models;
using PROnline.Models.Home;
using PROnline.Src;
using System.Data;

namespace PROnline.Controllers.Home
{
    public class SliderController : Controller
    {

        PROnlineContext db = new PROnlineContext();
        //
        // GET: /Slider/


        //保存编辑器附件

        [HttpPost]
        public ActionResult EditorSave(HttpPostedFileBase upload)
        {
            Guid guid = Utils.SaveFile("\\slider\\", upload);
            String path = "/Files/GetFile?uuid=" + guid.ToString();
            String callback = Request.Params["CKEditorFuncNum"];
            ViewBag.FilePath = path;
            ViewBag.CallBack = callback;
            return View();

        }


        public ActionResult Relate()
        {
            return View();
        }


        public ActionResult Index()
        {
            var result = db.Slider.OrderBy(r => r.CreateDate).ToList();
            return View(result);
        }
        public JsonResult List()
        {
            var result = db.Slider.OrderBy(r => r.CreateDate).Select(
                r => new SliderShow
                {
                    content = r.Content,
                    content_button = r.Title
                }
                ).ToList();

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Slider/Details/5

        public ActionResult Details(Guid id)
        {
            return View();
        }

        //
        // GET: /Slider/Create

        public ActionResult Create()
        {
            return View();
        }


        //
        // POST: /Slider/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SliderModel slider)
        {
            try
            {
                if (Utils.CurrentUser(this)!=null){
                    slider.Id = Guid.NewGuid();
                    slider.CreateDate = DateTime.Now;
                    slider.UserId = Utils.CurrentUser(this).Id;
                    db.Slider.Add(slider);
                    db.SaveChanges();
                   
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Slider/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            var slider = db.Slider.Find(id);
            return View(slider);
        }

        //
        // POST: /Slider/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SliderModel slider)
        {
            try
            {
                var sliderx = db.Slider.Find(slider.Id);
                if (slider != null)
                {
                    sliderx.Title = slider.Title;
                    sliderx.Content = slider.Content;
                    db.Entry(sliderx).State = EntityState.Modified;
                    db.SaveChanges();
                }
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Slider/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Slider/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
