using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Src;
using Quartz;

namespace PROnline.Models.Files
{
    public class FileTemp:IJob
    {
        public static Dictionary<String,FileElement> Container=new Dictionary<String,FileElement>();
        public static Dictionary<String, FileElement> Add(String key)
        {
            if (Container.ContainsKey(key))
            {
                Utils.DeleteFile(new Guid(key));
                Container[key] = new FileElement { FileId = new Guid(key), OutTime = DateTime.Now.AddMinutes(30) };
            }
            else
            {
                Container[key] = new FileElement { FileId = new Guid(key), OutTime = DateTime.Now.AddMinutes(30) };

            }
            return Container;
        }

        public  static void Remove(String key)
        {
            if(key!=null&&""!=key){
                Container.Remove(key);
            }
        }

        public static void Refresh()
        {
            foreach (FileElement fe in Container.Values)
            {
                if (fe.OutTime < DateTime.Now)
                {
                    Utils.DeleteFile(fe.FileId);
                }
            }
        }


        public void Execute(JobExecutionContext context)
        {
            Refresh();
        }
    }

    public class FileElement
    {
        public DateTime OutTime{get;set;}
        public Guid FileId { get; set; }
        
    }
}