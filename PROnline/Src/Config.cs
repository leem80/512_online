using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.IO;

namespace PROnline.Src
{
    public class Config
    {
        public static String root = "Config\\base\\config.xml";

        public static String filePath = "";

        public static XDocument GetXml()
        {
            String rootPath = MvcApplication.ServerPath;
            if (filePath == "")
            {
                filePath = rootPath + root;
            }
            if (File.Exists(filePath))
            {
                return XDocument.Load(filePath);
            }else{
                return null;
            }
        }

        public static int getIntValue(String name){
            XElement root = GetXml().Root;
            var value = root.Elements("config").Where(r=>r.Attribute("id").Value==name)
                .Single().Attribute("value").Value;
            return Convert.ToInt32(value);
        }

        public static void setValue(String name,int value){
            XDocument doc = GetXml();
            XElement root = doc.Root;
            XElement finded = root.Elements("config").Where(r => r.Attribute("id").Value == name).Single();
            if (finded != null)
            {
                finded.SetAttributeValue("value", value);
            }
            doc.Save(filePath);
        }

        public static void setValue(String name,String value){
            
        }


    }
}