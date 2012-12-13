using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PROnline.Models.Users
{
    public class Role
    {
        //角色ID
        [Display(Name = "角色ID")]
        public String Id { get; set; }
        //中文名
        [Display(Name = "注释")]
        public String Note { get; set; }
        //是否删除
        public bool isDelete { get; set; }

        //是否删除
        public bool CanDelete { get; set; }
        //创建日期
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreateDate { get; set; }
        public bool ModifyDate { get; set; }


        //默认角色
        //超级管理员
        public static string ADMIN = "admin";
        //访客
        public static string PUBLIC = "public";
        //学生
        public static string STUDENT = "student";
        //老师
        public static string TEACHER = "teacher";
        //家长
        public static string PARENT = "parent";
        //系统管理员
        public static string MANAGER = "manager";
        //督导专家
        public static string SUPERVISOR = "supervisor";
        //志愿者
        public static string VOLUNTEER = "volunteer";
        //学校管理员
        public static string SCHOOL_MANAGER = "school_manager";
        //爱心人士
        public static string DONATOR = "donator";
        //小组长
        public static string TEAM_LEADER = "team_leader";
        //服务组织
        public static string SERVICE_MANAGER = "service_manager";

        //项目管理
        public static string PROJECT_MANAGER = "project_manager";

    }
}