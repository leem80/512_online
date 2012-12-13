using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Web.Resources;
using System.IO;
using System.Text;
using System.Web.Routing;

namespace PROnline.Src
{

    public class DMenuConfig
    {
        public String id { get;set;}
        public String target { get; set; }

    }
    public class Menu
    {
        private static String _file = "sitemap.xml";
        private static String _root = "Config";
        private static String _abPath="";
        public static Dictionary<String,MenuItem> indexContainer=new Dictionary<String,MenuItem>();
        private static List<MenuItem> rootMenu=new List<MenuItem>();
        private static List<String> fileChecked=new List<String>();
        public static void reset()
        {
            indexContainer = new Dictionary<String, MenuItem>();
            rootMenu = new List<MenuItem>();
            fileChecked = new List<String>();

        }


        public static String builHtmlMenu(String id, int up, int down, string ulId,string target)
        {
           String renderHtml="";

           StringBuilder sb = new StringBuilder();
           sb.Append("<ul ");
           if (ulId!=null)
           {
               sb.Append("id=\"" + ulId + "\"");
           }
           sb.Append(">");
            if (id == null || id == "")
            {
                List<MenuItem> menus = getMainMenu();
                for (int i = 0; i < menus.Count; i++)
                {
                    sb.Append(ToHtml(menus[i],down, target));
                }

            }
            else
            {
                MenuItem menu = getMenuById(id, up);
                if (menu != null)
                {
                    List<MenuItem> menus = menu.Children;
                    for (int i = 0; i < menus.Count; i++)
                    {
                        sb.Append(ToHtml(menus[i],down, target));
                    }
                }
            }
            sb.Append("</ul>");
            renderHtml = sb.ToString();
            return renderHtml;

        }


        public static String builHtmlMenu(List<String> limit,String id, int up, int down, string ulId, string target)
        {
            String renderHtml = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul ");
            if (ulId != null)
            {
                sb.Append("id=\"" + ulId + "\"");
            }
            sb.Append(">");
            if (id == null || id == "")
            {
                List<MenuItem> menus = getMainMenu();
                for (int i = 0; i < menus.Count; i++)
                {
                    sb.Append(ToHtml(menus[i],limit, down, target));
                }

            }
            else
            {
                MenuItem menu = getMenuById(id, up);
                if (menu != null&&limit.Contains(menu.id))
                {
                    List<MenuItem> menus = menu.Children;
                    for (int i = 0; i < menus.Count; i++)
                    {
                        sb.Append(ToHtml(menus[i],limit,down, target));
                    }
                }
            }
            sb.Append("</ul>");
            renderHtml = sb.ToString();
            return renderHtml;

        }



        public static String builSelectedHtmlMenu(string ulId,string target,String name)
        {
           String renderHtml="";

           StringBuilder sb = new StringBuilder();
           sb.Append("<ul ");
           if (ulId!=null)
           {
               sb.Append("id=\"" + ulId + "\"");
           }
           sb.Append(">");

            List<MenuItem> menus = getMainMenu();
            for (int i = 0; i < menus.Count; i++)
            {
                sb.Append(ToSelectedHtml(menus[i],target, name));
            }
            sb.Append("</ul>");
            renderHtml = sb.ToString();
            return renderHtml;

        }


        public static String builSelectedHtmlMenu(List<String> limit, string ulId, string target, String name)
        {
            String renderHtml = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul ");
            if (ulId != null)
            {
                sb.Append("id=\"" + ulId + "\"");
            }
            sb.Append(">");

            List<MenuItem> menus = getMainMenu();
            for (int i = 0; i < menus.Count; i++)
            {
                sb.Append(ToSelectedHtml(menus[i], limit,target, name));
            }
            sb.Append("</ul>");
            renderHtml = sb.ToString();
            return renderHtml;

        }



        public static String ToHtml(MenuItem item,List<String> limit,int depth, string target)
        {
            StringBuilder sb = new StringBuilder();
            iteBuildHtml(sb, item,limit, depth, target);
            return sb.ToString();
        }




        public static  String ToHtml(MenuItem item,int depth, string target)
        {
            StringBuilder sb = new StringBuilder();
            iteBuildHtml(sb, item, depth, target);
            return sb.ToString();
        }

        public static String ToSelectedHtml(MenuItem item,List<String> limit, string target, string name)
        {
            StringBuilder sb = new StringBuilder();
            iteBuildSlectedHtml(sb, item,limit, target, name);
            return sb.ToString();
        }


        public static String ToSelectedHtml(MenuItem item, string target, string name)
        {
            StringBuilder sb = new StringBuilder();
            iteBuildSlectedHtml(sb, item, target, name);
            return sb.ToString();
        }


        private static void iteBuildSlectedHtml(StringBuilder sb, MenuItem item,List<String> limit, string target, String name)
        {

            sb.Append("<li><input type=\"checkbox\" value=\"" + item.id + "\" name=\"" + name + "\" ");
                
            if(limit.Contains(item.id)){
                sb.Append(" checked=\"true\"");
            }
            sb.Append(" /><a ");
            if (target != null)
            {
                sb.Append("target=\"" + target + "\"");
            }
            sb.Append(" href=\"").Append(item.url).Append("\" >");
            sb.Append(item.note);
            sb.Append("</a>");
            if (item.Children.Count > 0)
            {
                sb.Append("<ul>");
                foreach (MenuItem temp in item.Children)
                {
                    iteBuildSlectedHtml(sb, temp,limit, target, name);
                }
                sb.Append("</ul>");
            }
            sb.Append("</li>");

        }



        private static void iteBuildSlectedHtml(StringBuilder sb, MenuItem item, string target, String name)
        {

            sb.Append("<li><input type=\"checkbox\" value=\"" + item.id + "\" name=\"" + name + "\"/><a ");
            if (target != null)
            {
                sb.Append("target=\"" + target + "\"");
            }
            sb.Append(" href=\"").Append(item.url).Append("\" >");
            sb.Append(item.note);
            sb.Append("</a>");
            if (item.Children.Count > 0)
            {
                sb.Append("<ul>");
                foreach (MenuItem temp in item.Children)
                {
                    iteBuildSlectedHtml(sb, temp, target, name);
                }
                sb.Append("</ul>");
            }
            sb.Append("</li>");

        }



        private static void iteBuildHtml(StringBuilder sb, MenuItem item,List<String> limit, int depth, string target)
        {
            if (depth > 0 && !item.isAction&& limit.Contains(item.id))
            {
                sb.Append("<li><a ");
                if (target != null)
                {
                    sb.Append("target=\"" + target + "\"");
                }
                sb.Append(" class=\"");
                //
                if (item.icon != null)
                {
                    sb.Append(item.icon);
                }
                else
                {
                    sb.Append(item.id);
                }
                //是否为当前
                if (item.current)
                {
                    sb.Append(" current ");
                }
                sb.Append("\"");


                sb.Append(" href=\"").Append(item.url).Append("\" >");
                sb.Append(item.note);
                sb.Append("</a>");
                if (item.hasChild(false) && depth - 1 > 0)
                {
                    sb.Append("<ul>");
                    foreach (MenuItem temp in item.Children)
                    {
                        iteBuildHtml(sb, temp,limit, depth - 1, target);
                    }
                    sb.Append("</ul>");
                }
                sb.Append("</li>");
            }
        }



        private static  void iteBuildHtml(StringBuilder sb, MenuItem item, int depth, string target)
        {
            if (depth > 0 && !item.isAction)
            {
                sb.Append("<li><a ");
                if (target != null)
                {
                    sb.Append("target=\"" + target + "\"");
                }
                sb.Append(" class=\"");
                //
                if (item.icon!=null)
                {
                    sb.Append(item.icon);
                }
                else
                {
                    sb.Append(item.id);
                }
                //是否为当前
                if (item.current)
                {
                    sb.Append(" current ");
                }
                sb.Append("\"");

                sb.Append(" href=\"").Append(item.url).Append("\" >");
                sb.Append(item.note);
                sb.Append("</a>");
                if (item.hasChild(false) && depth - 1 > 0)
                {
                    sb.Append("<ul>");
                    foreach (MenuItem temp in item.Children)
                    {
                        iteBuildHtml(sb, temp, depth - 1, target);
                    }
                    sb.Append("</ul>");
                }
                sb.Append("</li>");
            }
        }








        public  static void loadMenu(String serverPath){
            _abPath = serverPath + _root;
            String path = serverPath + _root +"\\"+ _file;
            if (File.Exists(path))
            {
                XElement xml = XElement.Load(path);
                foreach (XElement xel in xml.Elements("menu"))
                {
                    MenuItem item=new MenuItem();
                    item.id=xel.Attribute("id").Value;
                    item.note=xel.Attribute("note").Value;
                    item.url=xel.Attribute("url").Value;
                    rootMenu.Add(item);
                    indexContainer.Add(item.id, item);
                    XAttribute refFile=xel.Attribute("ref");
                    if(refFile!=null&&fileChecked.IndexOf(refFile.Value)==-1){
                        loadChild(item,refFile.Value);
                    }
                }
                buildTree();

            }
        }


        public static List<MenuItem> getMainMenu()
        {
            return rootMenu;
        }

        public static MenuItem getMenuById(String id)
        {

            return getMenuById(id, 0);
        }



        public static MenuItem getMenuById(String id,int upDepth)
        {
            String key=id;
            for (int i = 0; i < upDepth; i++)
            {
                if (id != null && indexContainer.ContainsKey(id))
                {
                    if (indexContainer[key].pid!=null&&indexContainer[key].pid != "")
                    {
                        key = indexContainer[key].pid;
                    }
                    
                }
            }
            if (indexContainer.ContainsKey(key))
            {
                indexContainer[key].current = true;
                return indexContainer[key];
            }
            else
            {
                return new MenuItem();
            }

        }


        public static void loadChild(MenuItem pmenu, String file)
        {
            String url = _abPath + "\\"+file;
            if (File.Exists(url))
            {
                XElement xml = XElement.Load(url);
                foreach (XElement xel in xml.Elements("menu"))
                {
                    MenuItem item = new MenuItem();
                    item.id = xel.Attribute("id").Value;
                    item.note = xel.Attribute("note").Value;
                    item.url = xel.Attribute("url").Value;
           
                    if ( xel.Attribute("actions")!= null)
                    {
                        buildAction(item, xel.Attribute("actions").Value);
                    }
                    if (xel.Attribute("pid") == null)
                    {
                        item.pid = pmenu.id;
                    }
                    else
                    {
                        item.pid = xel.Attribute("pid").Value;
                    }
                    indexContainer.Add(item.id, item);
                    XAttribute refFile = xel.Attribute("ref");
                    if (refFile != null && fileChecked.IndexOf(refFile.Value) == -1)
                    {
                        loadChild(item, refFile.Value);
                    }

                }
            }
        }

        private static void buildAction(MenuItem item, String actions)
        {
            String id = item.id;
            String[] actionArr = actions.Split(new char[] { ',' });
            for (int i = 0; i < actionArr.Length; i++)
            {
                String[] actionParam = actionArr[i].Split(new char[] { ':' });
                String tempid = id + "_" + actionParam[0];
                String tempName = actionParam[1];

                MenuItem itemx = new MenuItem { id = tempid, note = tempName,isAction=true,pid=item.id };
                indexContainer.Add(itemx.id, itemx);
            }

        }

        private static void buildTree()
        {
            foreach (MenuItem item in indexContainer.Values)
            {
                if (item.pid != null)
                {
                    if (indexContainer[item.pid] != null)
                    {
                        indexContainer[item.pid].AddChild(item);
                    }
                }
            }
        }


    }


    public class MenuItem{
        public String id { get; set; }
        public String note { get; set; }
        public String url { get; set; }
        public String pid { get; set; }
        public String icon { get; set; }
        public bool current { get; set; }
        public int order{get;set;}
        public bool isAction { get; set; }
        public List<MenuItem> Children {

            get
            {
                return _child;
            }
       }
        //是否有子菜单，includeAction：是否忽略action菜单
        public bool hasChild(bool includeAction){

            if (!includeAction && this.Children.Count > 0)
            {
                foreach (MenuItem item in _child)
                {
                    if (!item.isAction)
                    {
                        return true;
                    }
                }
            }
            else if (includeAction)
            {
                return _child.Count > 0;
            }
            return false;

        }

        private List<MenuItem> _child = new List<MenuItem>();
        public void AddChild(MenuItem child){
            _child.Add(child);
        }
    }
}