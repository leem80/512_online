using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROnline.Models;
using PROnline.Models.Resources;
using PROnline.Src;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace PROnline.Controllers.Resources
{
    public class ResourceTypeController : Controller
    {
        //
        // GET: /ResourceType/

        PROnlineContext db = new PROnlineContext();

        public ActionResult Index()
        {
            String ParentId = Request.Params["ParentId"];
            if (ParentId == null)
            {
                var result = db.ResourceType.Where(rt => rt.ParentId == null && rt.isDeleted == false).ToList();
                ViewBag.NavData = new List<ResourceTypeNav>();
                ViewBag.isShow = false;
                return View(result);
            }
            else
            {
                List<ResourceTypeNav> output = new List<ResourceTypeNav>();
                ResourceType rt = db.ResourceType.Include("Children").Include("Parent")
                    .Where(r => r.Id == new Guid(ParentId)).Single();
                while (rt != null)
                {
                    ResourceTypeNav temp = new ResourceTypeNav { Current = rt };
                    if (rt.ParentId != null)
                    {
                        rt = db.ResourceType.Where(r => r.Id == rt.ParentId).Single();
                        temp.Sibling = (List<ResourceType>)rt.Children.Where(r => r.isDeleted == false).ToList();
                    }
                    else
                    {
                        rt = null;
                        temp.Sibling = db.ResourceType.Where(r => r.ParentId == null && r.isDeleted == false).ToList();
                    }
                    output.Add(temp);
                }
                output.Reverse();
                ViewBag.ParentId = ParentId;
                ViewBag.NavData = output;
                ViewBag.isShow = true;
                var result = db.ResourceType.Where(re => re.ParentId == new Guid(ParentId)).ToList();
                return View(result);

            }
            

        }

        public static Dictionary<String, ResourceTypeTree> ListRoot()
        {

            PROnlineContext dbx = new PROnlineContext();
            Dictionary<String, ResourceTypeTree> result = new Dictionary<string, ResourceTypeTree>();
            Dictionary<String, ResourceTypeTree> temp = new Dictionary<string, ResourceTypeTree>();
            List<ResourceType> list = dbx.ResourceType.Where(r => r.isDeleted == false).ToList();
            foreach (ResourceType type in list)
            {
                temp.Add(type.Id.ToString(), new ResourceTypeTree { name = type.TypeName, id = type.Id.ToString(), pid = type.ParentId.ToString() });
            }
            foreach (string id in temp.Keys)
            {
                string pid = temp[id].pid;
                if (pid == null || pid == "")
                {
                    result.Add(id, temp[id]);
                }
                else if (temp.ContainsKey(pid))
                {
                    temp[pid].AddChild(id, temp[id]);
                }

            }
            return result;
        }

        public static Dictionary<String, ResourceTypeTree> ListAll()
        {
            PROnlineContext dbx = new PROnlineContext();
            Dictionary<String, ResourceTypeTree> result = new Dictionary<string, ResourceTypeTree>();
            Dictionary<String, ResourceTypeTree> temp = new Dictionary<string, ResourceTypeTree>();
            List<ResourceType> list = dbx.ResourceType.Where(r => r.isDeleted == false).ToList();
            foreach (ResourceType type in list)
            {
                temp.Add(type.Id.ToString(), new ResourceTypeTree { name = type.TypeName, id = type.Id.ToString(), pid = type.ParentId.ToString() });
            }
            foreach (string id in temp.Keys)
            {
                string pid = temp[id].pid;
                if (pid == null || pid == "")
                {
                    result.Add(id, temp[id]);
                }
                else if (temp.ContainsKey(pid))
                {
                    temp[pid].AddChild(id, temp[id]);
                }

            }
            return temp;
        }


        public static String MenuTop()
        {
            Dictionary<String, ResourceTypeTree> list = ListRoot();
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (String type in list.Keys)
            {
                sb.Append("<li><a href=\"/Resources/List?tid=");
                sb.Append(list[type].id + "\" >");
                sb.Append(list[type].name);
                sb.Append("</a>");
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            return sb.ToString();
        }


        //获取路径
        //id，当前菜单id，dect 路径分割符号

        public static String getPath(String id,String dect)
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<String, ResourceTypeTree> list = ListAll();
            List<ResourceTypeTree> path = new List<ResourceTypeTree>();
            if(id!=null){
                String cid = id;
                while (list.ContainsKey(cid))
                {
                    path.Add(list[cid]);
                    ResourceTypeTree tree = list[cid];
                    cid = tree.pid;
                }
            }
            path.Reverse();
            foreach(ResourceTypeTree rt in path){
                sb.Append(dect+"<a href=\"");
                sb.Append("/Resources/List?tid=" + rt.id);
                sb.Append("\" ");
                if (id == rt.id)
                {
                    sb.Append("class='current'");
                }
                sb.Append(">" + rt.name + "</a>");
            }
            return sb.ToString();
        }

        //获取类型列表

        public static String Menu(String id)
        {
            Dictionary<String, ResourceTypeTree> list = ListAll();
            if (id != null && list.ContainsKey(id))
            {
                ResourceTypeTree tree = list[id];
                while (tree.pid != "")
                {
                    tree = list[tree.pid];
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("<ul>");
                sb.Append("<li><a href=\"/Resources/List?tid=");
                sb.Append(tree.id + "\" >");
                sb.Append(tree.name);
                sb.Append("</a>");
                if (tree.cell != null && tree.cell.Count > 0)
                {
                    buildChild(sb, tree,id);
                }
                sb.Append("</li>");
                sb.Append("</ul>");

                return sb.ToString();
            }
            else
            {
                return Menu();
            }
            
        }
        public static String Menu()
        {
            Dictionary<String, ResourceTypeTree> list = ListRoot();
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (String type in list.Keys)
            {
                sb.Append("<li><a href=\"/Resources/List?tid=");
                sb.Append(list[type].id+"\" >");
                sb.Append(list[type].name);
                sb.Append("</a>");
                if (list[type].cell != null && list[type].cell.Count > 0)
                {
                    buildChild(sb, list[type],null);
                }
                sb.Append("</li>");
            }
            sb.Append("</ul>");

            return sb.ToString();
        }

        private static void buildChild(StringBuilder sb, ResourceTypeTree typex,string current)
        {
            sb.Append("<ul>");
            foreach (String type in typex.cell.Keys)
            {
                sb.Append("<li ");
                if (current == typex.cell[type].id)
                {
                    sb.Append("class='current'");
                }
                sb.Append("><a href=\"/Resources/List?tid=");
                sb.Append(typex.cell[type].id + "\" >");
                sb.Append(typex.cell[type].name);
                sb.Append("</a>");
                if (typex.cell[type].cell != null && typex.cell[type].cell.Count > 0)
                {
                    buildChild(sb, typex.cell[type],current);
                }
                sb.Append("</li>");
            }
            sb.Append("</ul>");
        }

        //获取菜单的列表及根据传入id生成路径数组
        public static String GetAll(String valueId)
        {
            PROnlineContext dbx = new PROnlineContext();
            Dictionary<String, ResourceTypeTree> result = new Dictionary<string, ResourceTypeTree>();
            Dictionary<String, ResourceTypeTree> temp = new Dictionary<string, ResourceTypeTree>();
            List<ResourceType> list = dbx.ResourceType.Where(r=>r.isDeleted == false).ToList();
            foreach (ResourceType type in list)
            {
                temp.Add(type.Id.ToString(), new ResourceTypeTree { name=type.TypeName,id=type.Id.ToString(),pid=type.ParentId.ToString()});
            }
            foreach (string id in temp.Keys)
            {
                string pid = temp[id].pid;
                if(pid==null||pid==""){
                    result.Add(id,temp[id]);
                }
                else if (temp.ContainsKey(pid))
                {
                    temp[pid].AddChild(id,temp[id]);
                }

            }

            
            List<String> valueList = new List<String>();
            if(temp.ContainsKey(valueId)){
                ResourceTypeTree current = temp[valueId];
                valueList.Add(current.id);
                while (current.pid != "")
                {
                    current = temp[current.pid];
                    valueList.Add(current.id);
                
                }
                valueList.Reverse();
            }

            Dictionary<String, Object> lastPack = new Dictionary<string, object>();
            lastPack.Add("data", result);
            lastPack.Add("value", valueList);

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(lastPack);
            

        }

        public ActionResult Create()
        {
            String ParentId = Request.Params["ParentId"];
            ViewBag.ParentId = ParentId;

            return View();
        }

        [HttpPost]
        [Auth(MenuId="ResourceType_add")]
        public ActionResult Create(ResourceType type)
        {
            type.Id = Guid.NewGuid();
            type.CreateDate = DateTime.Now;
            type.ModifyDate = DateTime.Now;
            type.isDeleted = false;
            type.CreatorId = Utils.CurrentUser(this).Id;
            db.ResourceType.Add(type);
            db.SaveChanges();
            return RedirectToAction("Index", new { ParentId = type.ParentId.ToString() });
        }

        public ActionResult Edit(Guid id)
        {
            var ent = db.ResourceType.Find(id);
            return View(ent);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ResourceType resource = db.ResourceType.Find(id);
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            ResourceType type = db.ResourceType.Find(id);
            type.isDeleted = true;
            db.Entry(type).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { ParentTypeId = type.ParentId.ToString() });
        }

        [HttpPost]
        public ActionResult Edit(ResourceType type)
        {
            type.CreateDate = DateTime.Now;
            type.ModifyDate = DateTime.Now;
            type.isDeleted = false;
            type.CreatorId = Utils.CurrentUser(this).Id;
            db.Entry(type).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { ParentID=type.ParentId});
        }

        public ViewResult Details(Guid id)
        {
            ResourceType resourceType = db.ResourceType.Find(id);
            return View(resourceType);
        }
    }
}
