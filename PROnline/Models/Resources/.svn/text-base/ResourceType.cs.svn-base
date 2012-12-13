using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace PROnline.Models.Resources
{
    //资源类型
    public class ResourceType
    {
        //资源类型ID
        public Guid Id { get; set; }

        //类型名称
        [Display(Name = "类型名称")]
        [Required(ErrorMessage = "资源类型名称必须填写")]
        public String TypeName { get; set; }

        //资源上级类型
        public Nullable<Guid> ParentId { get; set; }
        public virtual ResourceType Parent { get; set; }

        //资源的子类型列表
        public virtual List<ResourceType> Children { get; set; }

        //资源类型简介
        [Display(Name = "简介")]
        [Required(ErrorMessage = "资源类型简介必须填写")]
        public String Introduction { get; set; }

        //资源类型备注
        [Display(Name = "备注")]
        public String Comment { get; set; }

        //创建者
        public Guid CreatorId { get; set; }

        //创建时间
        public DateTime CreateDate { get; set; }

        //修改者
        public Guid ModifierId { get; set; }

        //修改时间
        public DateTime ModifyDate { get; set; }

        //是否删除
        public Boolean isDeleted { get; set; }

        public List<Guid> GetChildren()
        {
            List<Guid> result = new List<Guid>();
            var db=new PROnlineContext();
            Stack<ResourceType> stack = new Stack<ResourceType>();
            stack.Push(this);
            while (stack.Count > 0)
            {
                ResourceType target = stack.Pop();
                target.Children = db.ResourceType.Where(r => r.ParentId == target.Id).ToList();
                result.Add(target.Id);
                if (target.Children != null && target.Children.Count > 0)
                {
                    foreach (var t in target.Children)
                        stack.Push(t);
                }
            }
            return result;
        }
    }


    //业务中的帮助Model

    public class ResourceTypeNav
    {
        public ResourceType Current { get; set; }
        public List<ResourceType> Sibling { get; set; }
    }


    public class ResourceTypeTree
    {
        public String name { get; set; }
        public String id { get; set; }
        public String pid { get; set; }

        private Dictionary<String, ResourceTypeTree> _cell;
        public Dictionary<String, ResourceTypeTree> cell
        {
            get { return _cell; }

        }

        public void AddChild(String key, ResourceTypeTree value)
        {
            if (_cell == null)
            {
                _cell = new Dictionary<string, ResourceTypeTree>();
            }

            _cell.Add(key, value);
        }
    }


}