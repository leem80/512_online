using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using PROnline.Models.Users;
using System.Security.Cryptography;
using PROnline.Src;
using PROnline.Models;
using PROnline.Models.Resources;
using PROnline.Models.Notices;
using PROnline.Models.WorkStations;
using PROnline.Models.Teams;
using PROnline.Models.Service;

namespace PROnline.DAL
{
    //测试数据
    public class TestSetup:IDataSetup
    {
        public void init(Models.PROnlineContext context)
        {
            //the second administrator

            var user1 = new User();
            user1.CreateDate = DateTime.Now;
            user1.ModifyDate = DateTime.Now;
            user1.LastLoginDate = DateTime.Now;
            user1.UserName = "admin1";
            user1.RealName = "管理员1";
            user1.UserTypeId = UserType.ADMIN;
            user1.Password = Utils.GetMd5("123456");
            user1.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e7");
            user1.RoleId = Role.ADMIN;
            user1.isDeleted = false;
            user1.isHaveTraining = false;
            user1.isVerify = true;

            Administrator admin1 = new Administrator();
            admin1.AdministratorID = Guid.NewGuid();
            admin1.User = user1;
            admin1.Birthday = DateTime.Now;
            admin1.Gender = "男";
            admin1.MobileTelephone = "12121212";
            admin1.Address = "北京市";
            context.Administrator.Add(admin1);
            context.SaveChanges();

            var user2 = new User();
            user2.CreateDate = DateTime.Now;
            user2.ModifyDate = DateTime.Now;
            user2.LastLoginDate = DateTime.Now;
            user2.UserName = "manager1";
            user2.RealName = "系统管理员1";
            user2.UserTypeId = UserType.MANAGER;
            user2.Password = Utils.GetMd5("123456");
            user2.Id = Guid.NewGuid();
            user2.RoleId = Role.MANAGER;
            user2.isDeleted = false;
            user2.isHaveTraining = false;
            user2.isVerify = true;

            Administrator admin2 = new Administrator();
            admin2.AdministratorID = Guid.NewGuid();
            admin2.User = user2;
            admin2.Birthday = DateTime.Now;
            admin2.Gender = "男";
            admin2.MobileTelephone = "12121212";
            admin2.Address = "北京市";
            context.Administrator.Add(admin2);
            context.SaveChanges();

            var user3 = new User();
            user3.CreateDate = DateTime.Now;
            user3.ModifyDate = DateTime.Now;
            user3.LastLoginDate = DateTime.Now;
            user3.UserName = "project_manager1";
            user3.RealName = "项目管理员1";
            user3.UserTypeId = UserType.MANAGER;
            user3.Password = Utils.GetMd5("123456");
            user3.Id = Guid.NewGuid();
            user3.RoleId = Role.PROJECT_MANAGER;
            user3.isDeleted = false;
            user3.isHaveTraining = false;
            user3.isVerify = true;

            Administrator admin3 = new Administrator();
            admin3.AdministratorID = Guid.NewGuid();
            admin3.User = user3;
            admin3.Birthday = DateTime.Now;
            admin3.Gender = "男";
            admin3.MobileTelephone = "12121212";
            admin3.Address = "北京市";
            context.Administrator.Add(admin3);
            context.SaveChanges();

            var user4 = new User();
            user4.CreateDate = DateTime.Now;
            user4.ModifyDate = DateTime.Now;
            user4.LastLoginDate = DateTime.Now;
            user4.UserName = "service_manager1";
            user4.RealName = "服务管理员1";
            user4.UserTypeId = UserType.MANAGER;
            user4.Password = Utils.GetMd5("123456");
            user4.Id = Guid.NewGuid();
            user4.RoleId = Role.SERVICE_MANAGER;
            user4.isDeleted = false;
            user4.isHaveTraining = false;
            user4.isVerify = true;

            Administrator admin4 = new Administrator();
            admin4.AdministratorID = Guid.NewGuid();
            admin4.User = user4;
            admin4.Birthday = DateTime.Now;
            admin4.Gender = "男";
            admin4.MobileTelephone = "12121212";
            admin4.Address = "北京市";
            context.Administrator.Add(admin4);
            context.SaveChanges();

            //导入测试数据
            //志愿者
            var vol1 = new User();

            vol1.CreateDate = DateTime.Now;
            vol1.ModifyDate = DateTime.Now;
            vol1.LastLoginDate = DateTime.Now;
            vol1.UserName = "vol1";
            vol1.RealName = "志愿者1";
            vol1.UserTypeId = UserType.VOLUNTEER;
            vol1.Password = Utils.GetMd5("123456");
            vol1.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e6");
            vol1.RoleId = "volunteer";
            vol1.isDeleted = false;
            vol1.isHaveTraining = false;
            vol1.isVerify = true;

            Volunteer volunteer1 = new Volunteer();
            volunteer1.VolunteerID = Guid.NewGuid();
            volunteer1.User = vol1;
            volunteer1.FirtPassword = Utils.GetMd5("123456");
            volunteer1.ConfirmPassword = Utils.GetMd5("123456");
            volunteer1.Birthday = DateTime.Now;
            volunteer1.Nationality = "汉";
            volunteer1.Sex = "女";
            volunteer1.MobileTelephone = "12345678901";
            volunteer1.TrainingType = "心理辅导";
            volunteer1.HomeTown = "北京市大兴区";
            volunteer1.PoliticalExperience = "团员";
            volunteer1.IsStudent = true;
            volunteer1.IsExperenced = true;
            volunteer1.PDegree = "一级";
            volunteer1.DayOfWeek = "星期一";
            volunteer1.Time = "9";
            volunteer1.isHaveTeam = false;
            context.Volunteer.Add(volunteer1);
            context.SaveChanges();


            var vol2 = new User();

            vol2.CreateDate = DateTime.Now;
            vol2.ModifyDate = DateTime.Now;
            vol2.LastLoginDate = DateTime.Now;
            vol2.UserName = "vol2";
            vol2.RealName = "志愿者2";
            vol2.UserTypeId = UserType.VOLUNTEER;
            vol2.Password = Utils.GetMd5("123456");
            vol2.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e8");
            vol2.RoleId = "volunteer";
            vol2.isDeleted = false;
            vol2.isHaveTraining = false;
            vol2.isVerify = true;

            Volunteer volunteer2 = new Volunteer();
            volunteer2.VolunteerID = Guid.NewGuid();
            volunteer2.User = vol2;
            volunteer2.FirtPassword = Utils.GetMd5("123456");
            volunteer2.ConfirmPassword = Utils.GetMd5("123456");
            volunteer2.Birthday = DateTime.Now;
            volunteer2.Nationality = "汉";
            volunteer2.Sex = "女";
            volunteer2.MobileTelephone = "1212121212";
            volunteer2.TrainingType = "心理辅导";
            volunteer2.HomeTown = "北京市大兴区";
            volunteer2.PoliticalExperience = "团员";
            volunteer2.IsStudent = true;
            volunteer2.IsExperenced = true;
            volunteer2.PDegree = "一级";

            volunteer2.DayOfWeek = "星期二";
            volunteer2.Time = "19";

            volunteer2.isHaveTeam = false;
            context.Volunteer.Add(volunteer2);
            context.SaveChanges();

            var vol3 = new User();

            vol3.CreateDate = DateTime.Now;
            vol3.ModifyDate = DateTime.Now;
            vol3.LastLoginDate = DateTime.Now;
            vol3.UserName = "vol3";
            vol3.RealName = "志愿者3";
            vol3.UserTypeId = UserType.VOLUNTEER;
            vol3.Password = Utils.GetMd5("123456");
            vol3.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e9");
            vol3.RoleId = "volunteer";
            vol3.isDeleted = false;
            vol3.isHaveTraining = false;
            vol3.isVerify = true;

            Volunteer volunteer3 = new Volunteer();
            volunteer3.VolunteerID = Guid.NewGuid();
            volunteer3.User = vol3;
            volunteer3.FirtPassword = Utils.GetMd5("123456");
            volunteer3.ConfirmPassword = Utils.GetMd5("123456");
            volunteer3.Birthday = DateTime.Now;
            volunteer3.Nationality = "汉";
            volunteer3.Sex = "男";
            volunteer3.MobileTelephone = "12345678901";
            volunteer3.TrainingType = "心理辅导";
            volunteer3.HomeTown = "北京市大兴区";
            volunteer3.PoliticalExperience = "团员";
            volunteer3.IsStudent = true;
            volunteer3.IsExperenced = true;
            volunteer3.PDegree = "一级";
            volunteer3.DayOfWeek = "星期六";
            volunteer3.Time = "19";

            volunteer3.isHaveTeam = true;
            context.Volunteer.Add(volunteer3);
            context.SaveChanges();

            var vol4 = new User();

            vol4.CreateDate = DateTime.Now;
            vol4.ModifyDate = DateTime.Now;
            vol4.LastLoginDate = DateTime.Now;
            vol4.UserName = "vol4";
            vol4.RealName = "志愿者4";
            vol4.UserTypeId = UserType.VOLUNTEER;
            vol4.Password = Utils.GetMd5("123456");
            vol4.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7f1");
            vol4.RoleId = "volunteer";
            vol4.isDeleted = false;
            vol4.isHaveTraining = false;
            vol4.isVerify = true;

            Volunteer volunteer4 = new Volunteer();
            volunteer4.VolunteerID = Guid.NewGuid();
            volunteer4.User = vol4;
            volunteer4.FirtPassword = Utils.GetMd5("123456");
            volunteer4.ConfirmPassword = Utils.GetMd5("123456");
            volunteer4.Birthday = DateTime.Now;
            volunteer4.Nationality = "蒙";
            volunteer4.Sex = "男";
            volunteer4.MobileTelephone = "12345678901";
            volunteer4.TrainingType = "心理辅导";
            volunteer4.HomeTown = "北京市大兴区";
            volunteer4.PoliticalExperience = "团员";
            volunteer4.IsStudent = true;
            volunteer4.IsExperenced = true;
            volunteer4.PDegree = "一级";
            volunteer4.DayOfWeek = "星期二";

            volunteer4.Time = "19";

            volunteer4.isHaveTeam = false;
            context.Volunteer.Add(volunteer4);
            context.SaveChanges();

            var vol5 = new User();

            vol5.CreateDate = DateTime.Now;
            vol5.ModifyDate = DateTime.Now;
            vol5.LastLoginDate = DateTime.Now;
            vol5.UserName = "vol5";
            vol5.RealName = "志愿者5";
            vol5.UserTypeId = UserType.VOLUNTEER;
            vol5.Password = Utils.GetMd5("123456");
            vol5.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7f2");
            vol5.RoleId = "volunteer";
            vol5.isDeleted = false;
            vol5.isHaveTraining = false;
            vol5.isVerify = true;

            Volunteer volunteer5 = new Volunteer();
            volunteer5.VolunteerID = Guid.NewGuid();
            volunteer5.User = vol5;
            volunteer5.FirtPassword = Utils.GetMd5("123456");
            volunteer5.ConfirmPassword = Utils.GetMd5("123456");
            volunteer5.Birthday = DateTime.Now;
            volunteer5.Nationality = "汉";
            volunteer5.Sex = "男";
            volunteer5.MobileTelephone = "12345678901";
            volunteer5.TrainingType = "心理辅导";
            volunteer5.HomeTown = "北京市大兴区";
            volunteer5.PoliticalExperience = "团员";
            volunteer5.IsStudent = true;
            volunteer5.IsExperenced = true;
            volunteer5.PDegree = "一级";
            volunteer5.DayOfWeek = "星期二";

            volunteer5.Time = "19";

            volunteer5.isHaveTeam = true;
            context.Volunteer.Add(volunteer5);
            context.SaveChanges();

            var vol6 = new User();

            vol6.CreateDate = DateTime.Now;
            vol6.ModifyDate = DateTime.Now;
            vol6.LastLoginDate = DateTime.Now;
            vol6.UserName = "vol6";
            vol6.RealName = "志愿者6";
            vol6.UserTypeId = UserType.VOLUNTEER;
            vol6.Password = Utils.GetMd5("123456");
            vol6.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7f3");
            vol6.RoleId = "volunteer";
            vol6.isDeleted = false;
            vol6.isHaveTraining = false;
            vol6.isVerify = true;

            Volunteer volunteer6 = new Volunteer();
            volunteer6.VolunteerID = Guid.NewGuid();
            volunteer6.User = vol6;
            volunteer6.FirtPassword = Utils.GetMd5("123456");
            volunteer6.ConfirmPassword = Utils.GetMd5("123456");
            volunteer6.Birthday = DateTime.Now;
            volunteer6.Nationality = "汉";
            volunteer6.Sex = "男";
            volunteer6.MobileTelephone = "12345678901";
            volunteer6.TrainingType = "心理辅导";
            volunteer6.HomeTown = "北京市大兴区";
            volunteer6.PoliticalExperience = "团员";
            volunteer6.IsStudent = true;
            volunteer6.IsExperenced = true;
            volunteer6.PDegree = "一级";
            volunteer6.DayOfWeek = "星期二";
            volunteer6.Time = "19";

            volunteer6.isHaveTeam = true;
            context.Volunteer.Add(volunteer6);
            context.SaveChanges();
            //vol7是小组长
            var vol7 = new User();

            vol7.CreateDate = DateTime.Now;
            vol7.ModifyDate = DateTime.Now;
            vol7.LastLoginDate = DateTime.Now;
            vol7.UserName = "vol7";
            vol7.RealName = "志愿者7";
            vol7.UserTypeId = UserType.TEAM_LEADER;
            vol7.Password = Utils.GetMd5("123456");
            vol7.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7f4");
            vol7.RoleId = "team_leader";
            vol7.isDeleted = false;
            vol7.isHaveTraining = false;
            vol7.isVerify = true;

            Volunteer volunteer7 = new Volunteer();
            volunteer7.VolunteerID = Guid.NewGuid();
            volunteer7.User = vol7;
            volunteer7.FirtPassword = Utils.GetMd5("123456");
            volunteer7.ConfirmPassword = Utils.GetMd5("123456");
            volunteer7.Birthday = DateTime.Now;
            volunteer7.Nationality = "汉";
            volunteer7.Sex = "男";
            volunteer7.MobileTelephone = "12345678901";
            volunteer7.TrainingType = "心理辅导";
            volunteer7.HomeTown = "北京市大兴区";
            volunteer7.PoliticalExperience = "团员";
            volunteer7.IsStudent = true;
            volunteer7.IsExperenced = true;
            volunteer7.PDegree = "一级";
            volunteer7.DayOfWeek = "星期二";
            volunteer7.Time = "19";

            volunteer7.isHaveTeam = true;
            context.Volunteer.Add(volunteer7);
            context.SaveChanges();

            //工作站
            WorkStation ws1 = new WorkStation();
            ws1.WorkStationID = Guid.NewGuid();
            ws1.WorkStationName = "呼和浩特站";
            ws1.WorkStationerName = "志愿者7";
            ws1.Telephone = "123123123123";
            ws1.CreatorID = vol7.Id;
            ws1.CreateDate = DateTime.Now;
            ws1.isDeleted = false;

            context.WorkStation.Add(ws1);
            context.SaveChanges();

                        //小组
            Team team1 = new Team();
            team1.TeamID = Guid.NewGuid();
            team1.TeamName = "呼和浩特";
            team1.Introduction = "很好！";
            team1.Comment = "很好!";
            team1.Type = "心理辅导";

            team1.CreatorID = vol7.Id;
            team1.CreateDate = DateTime.Now;
            team1.WorkStationID = ws1.WorkStationID;
            team1.WorkStation = ws1;
            team1.isDeleted = false;
            context.Team.Add(team1);

            //小组长
            TeamLeader tl1 = new TeamLeader();
            tl1.TeamLeaderID = Guid.NewGuid();
            tl1.TeamID = team1.TeamID;

            tl1.VolunteerID = volunteer7.VolunteerID;
            tl1.Volunteer = volunteer7;
            tl1.State = "正常";
            tl1.JoinDate = DateTime.Now;

            Volunteer volunteer = context.Volunteer.Find(tl1.VolunteerID);
            volunteer.TeamID = tl1.TeamID;
            context.Entry(volunteer).State = EntityState.Modified;

            context.TeamLeader.Add(tl1);
            context.SaveChanges();

            team1.TeamLeaderID = tl1.TeamLeaderID;
            context.SaveChanges();


            //组员
            TeamMember tm1 = new TeamMember();
            tm1.TeamMemberID = Guid.NewGuid();
            tm1.TeamID = team1.TeamID;
            tm1.Team = team1;
            tm1.VolunteerID = volunteer6.VolunteerID;
            tm1.Volunteer = volunteer6;
            tm1.State = "正常";
            tm1.JoinDate = DateTime.Now;
            volunteer = context.Volunteer.Find(tm1.VolunteerID);
            volunteer.TeamID = tm1.TeamID;
            context.Entry(volunteer).State = EntityState.Modified;
            context.TeamMember.Add(tm1);
            context.SaveChanges();

            TeamMember tm2 = new TeamMember();
            tm2.TeamMemberID = Guid.NewGuid();
            tm2.TeamID = team1.TeamID;
            tm2.Team = team1;
            tm2.VolunteerID = volunteer5.VolunteerID;
            tm2.Volunteer = volunteer5;
            tm2.State = "正常";
            tm2.JoinDate = DateTime.Now;
            volunteer = context.Volunteer.Find(tm2.VolunteerID);
            volunteer.TeamID = tm1.TeamID;
            context.Entry(volunteer).State = EntityState.Modified;
            context.TeamMember.Add(tm2);
            context.SaveChanges();

            TeamMember tm3 = new TeamMember();
            tm3.TeamMemberID = Guid.NewGuid();
            tm3.TeamID = team1.TeamID;
            tm3.Team = team1;
            tm3.VolunteerID = volunteer3.VolunteerID;
            tm3.Volunteer = volunteer3;
            tm3.State = "正常";
            tm3.JoinDate = DateTime.Now;
            volunteer = context.Volunteer.Find(tm1.VolunteerID);
            volunteer.TeamID = tm1.TeamID;
            context.Entry(volunteer).State = EntityState.Modified;
            context.TeamMember.Add(tm3);
            context.SaveChanges();


            //学校
            var school1 = new School();
            school1.SchoolID = Guid.NewGuid();
            school1.SchoolNo = "K001";
            school1.SchoolName = "school1";
            school1.SchoolNo = "010";
            school1.SchoolIntro = "TTT";
            school1.SchoolCommnet = "MMM";
            school1.CreatorID = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7e7");
            school1.CreateDate = DateTime.Now;
            school1.isDeleted = false;

            context.School.Add(school1);
            context.SaveChanges();
            
            //学生
            var stu1 = new User();

            stu1.CreateDate = DateTime.Now;
            stu1.ModifyDate = DateTime.Now;
            stu1.LastLoginDate = DateTime.Now;
            stu1.UserName = "student1";
            stu1.RealName = "学生1";
            stu1.UserTypeId = UserType.STUDENT;
            stu1.Password = Utils.GetMd5("123456");
            stu1.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7c2");
            stu1.RoleId = "student";
            stu1.isDeleted = false;
            stu1.isVerify = true;

            var student1 = new Student();
            student1.StudentID = Guid.NewGuid();
            student1.SchoolID = school1.SchoolID;
            student1.User = stu1;
            student1.State = "心理辅导";
            student1.Birthday = DateTime.Now;

            student1.Sex = "男";
            student1.People = "汉";
            student1.Career = "无";
            student1.HomeNum = 3;
            student1.Hobby = "足球";
            student1.Telephone = "123";
            student1.CanSurf = true;
            student1.SurfDayOfWeek = "星期一";
            student1.SurfTime = "19";
            student1.Hurt = "无";
            student1.Die = "无";
            student1.Lose = "无";
            student1.IsAlone = true;
            student1.Math = "很好";
            student1.Chinese = "很好";
            student1.English = "很好";
            student1.Physics = "很好";
            student1.Chemistry = "很好";
            student1.Sw = "很好";
            student1.Dl = "很好";
            student1.Ls = "很好";

            context.Student.Add(student1);
            context.SaveChanges();


            var stu2 = new User();

            stu2.CreateDate = DateTime.Now;
            stu2.ModifyDate = DateTime.Now;
            stu2.LastLoginDate = DateTime.Now;
            stu2.UserName = "student2";
            stu2.RealName = "学生2";
            stu2.UserTypeId = UserType.STUDENT;
            stu2.Password = Utils.GetMd5("123456");
            stu2.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7c7");
            stu2.RoleId = "student";
            stu2.isDeleted = false;
            stu2.isVerify = true;

            var student2 = new Student();
            student2.StudentID = Guid.NewGuid();
            student2.SchoolID = school1.SchoolID;
            student2.User = stu2;
            student2.State = "心理辅导";
            student2.Birthday = DateTime.Now;

            student2.Sex = "男";
            student2.People = "汉";
            student2.Career = "无";
            student2.HomeNum = 3;
            student2.Hobby = "足球";
            student2.Telephone = "123";
            student2.CanSurf = true;
            student2.SurfDayOfWeek = "星期一";
            student2.SurfTime = "19";
            student2.Hurt = "无";
            student2.Die = "无";
            student2.Lose = "无";
            student2.IsAlone = true;
            student2.Math = "很好";
            student2.Chinese = "很好";
            student2.English = "很好";
            student2.Physics = "很好";
            student2.Chemistry = "很好";
            student2.Sw = "很好";
            student2.Dl = "很好";
            student2.Ls = "很好";

            context.Student.Add(student2);
            context.SaveChanges();

            var stu3 = new User();

            stu3.CreateDate = DateTime.Now;
            stu3.ModifyDate = DateTime.Now;
            stu3.LastLoginDate = DateTime.Now;
            stu3.UserName = "student3";
            stu3.RealName = "学生3";
            stu3.UserTypeId = UserType.STUDENT;
            stu3.Password = Utils.GetMd5("123456");
            stu3.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7d3");
            stu3.RoleId = "student";
            stu3.isDeleted = false;
            stu3.isVerify = true;

            var student3 = new Student();
            student3.StudentID = Guid.NewGuid();
            student3.SchoolID = school1.SchoolID;
            student3.User = stu3;
            student3.State = "心理辅导";
            student3.Birthday = DateTime.Now;
            student3.Sex = "男";
            student3.People = "汉";
            student3.Career = "无";
            student3.HomeNum = 3;
            student3.Hobby = "足球";
            student3.Telephone = "123";
            student3.CanSurf = true;
            student3.SurfDayOfWeek = "星期一";
            student3.SurfTime = "19";
            student3.Hurt = "无";
            student3.Die = "无";
            student3.Lose = "无";
            student3.IsAlone = true;
            student3.Math = "很好";
            student3.Chinese = "很好";
            student3.English = "很好";
            student3.Physics = "很好";
            student3.Chemistry = "很好";
            student3.Sw = "很好";
            student3.Dl = "很好";
            student3.Ls = "很好";

            context.Student.Add(student3);
            context.SaveChanges();

            var stu4 = new User();

            stu4.CreateDate = DateTime.Now;
            stu4.ModifyDate = DateTime.Now;
            stu4.LastLoginDate = DateTime.Now;
            stu4.UserName = "student4";
            stu4.RealName = "学生4";
            stu4.UserTypeId = UserType.STUDENT;
            stu4.Password = Utils.GetMd5("123456");
            stu4.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7d4");
            stu4.RoleId = "student";
            stu4.isDeleted = false;
            stu4.isVerify = true;

            var student4 = new Student();
            student4.StudentID = Guid.NewGuid();
            student4.SchoolID = school1.SchoolID;
            student4.User = stu4;
            student4.State = "心理辅导";
            student4.Birthday = DateTime.Now;
            student4.Sex = "男";
            student4.People = "汉";
            student4.Career = "无";
            student4.HomeNum = 3;
            student4.Hobby = "足球";
            student4.Telephone = "123";
            student4.CanSurf = true;
            student4.SurfDayOfWeek = "星期一";
            student4.SurfTime = "19";
            student4.Hurt = "无";
            student4.Die = "无";
            student4.Lose = "无";
            student4.IsAlone = true;
            student4.Math = "很好";
            student4.Chinese = "很好";
            student4.English = "很好";
            student4.Physics = "很好";
            student4.Chemistry = "很好";
            student4.Sw = "很好";
            student4.Dl = "很好";
            student4.Ls = "很好";

            context.Student.Add(student4);
            context.SaveChanges();

            var stu5 = new User();

            stu5.CreateDate = DateTime.Now;
            stu5.ModifyDate = DateTime.Now;
            stu5.LastLoginDate = DateTime.Now;
            stu5.UserName = "student5";
            stu5.RealName = "学生5";
            stu5.UserTypeId = UserType.STUDENT;
            stu5.Password = Utils.GetMd5("123456");
            stu5.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7d5");
            stu5.RoleId = "student";
            stu5.isDeleted = false;
            stu5.isVerify = true;

            var student5 = new Student();
            student5.StudentID = Guid.NewGuid();
            student5.SchoolID = school1.SchoolID;
            student5.User = stu5;
            student5.State = "心理辅导";
            student5.Birthday = DateTime.Now;
            student5.Sex = "男";
            student5.People = "汉";
            student5.Career = "无";
            student5.HomeNum = 3;
            student5.Hobby = "足球";
            student5.Telephone = "123";
            student5.CanSurf = true;
            student5.SurfDayOfWeek = "星期一";
            student5.SurfTime = "19";
            student5.Hurt = "无";
            student5.Die = "无";
            student5.Lose = "无";
            student5.IsAlone = true;
            student5.Math = "很好";
            student5.Chinese = "很好";
            student5.English = "很好";
            student5.Physics = "很好";
            student5.Chemistry = "很好";
            student5.Sw = "很好";
            student5.Dl = "很好";
            student5.Ls = "很好";

            context.Student.Add(student5);
            context.SaveChanges();

            var stu6 = new User();

            stu6.CreateDate = DateTime.Now;
            stu6.ModifyDate = DateTime.Now;
            stu6.LastLoginDate = DateTime.Now;
            stu6.UserName = "student6";
            stu6.RealName = "学生6";
            stu6.UserTypeId = UserType.STUDENT;
            stu6.Password = Utils.GetMd5("123456");
            stu6.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7d6");
            stu6.RoleId = "student";
            stu6.isDeleted = false;
            stu6.isVerify = true;

            var student6 = new Student();
            student6.StudentID = Guid.NewGuid();
            student6.SchoolID = school1.SchoolID;
            student6.User = stu6;
            student6.State = "心理辅导";
            student6.Birthday = DateTime.Now;
            student6.Sex = "男";
            student6.People = "汉";
            student6.Career = "无";
            student6.HomeNum = 3;
            student6.Hobby = "足球";
            student6.Telephone = "123";
            student6.CanSurf = true;
            student6.SurfDayOfWeek = "星期一";
            student6.SurfTime = "19";
            student6.Hurt = "无";
            student6.Die = "无";
            student6.Lose = "无";
            student6.IsAlone = true;
            student6.Math = "很好";
            student6.Chinese = "很好";
            student6.English = "很好";
            student6.Physics = "很好";
            student6.Chemistry = "很好";
            student6.Sw = "很好";
            student6.Dl = "很好";
            student6.Ls = "很好";

            context.Student.Add(student6);
            context.SaveChanges();

            var stu7 = new User();

            stu7.CreateDate = DateTime.Now;
            stu7.ModifyDate = DateTime.Now;
            stu7.LastLoginDate = DateTime.Now;
            stu7.UserName = "student7";
            stu7.RealName = "学生7";
            stu7.UserTypeId = UserType.STUDENT;
            stu7.Password = Utils.GetMd5("123456");
            stu7.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7d7");
            stu7.RoleId = "student";
            stu7.isDeleted = false;
            stu7.isVerify = true;

            var student7 = new Student();
            student7.StudentID = Guid.NewGuid();
            student7.SchoolID = school1.SchoolID;
            student7.User = stu7;
            student7.State = "心理辅导";
            student7.Birthday = DateTime.Now;
            student7.Sex = "男";
            student7.People = "汉";
            student7.Career = "无";
            student7.HomeNum = 3;
            student7.Hobby = "足球";
            student7.Telephone = "123";
            student7.CanSurf = true;
            student7.SurfDayOfWeek = "星期一";
            student7.SurfTime = "19";
            student7.Hurt = "无";
            student7.Die = "无";
            student7.Lose = "无";
            student7.IsAlone = true;
            student7.Math = "很好";
            student7.Chinese = "很好";
            student7.English = "很好";
            student7.Physics = "很好";
            student7.Chemistry = "很好";
            student7.Sw = "很好";
            student7.Dl = "很好";
            student7.Ls = "很好";

            context.Student.Add(student7);
            context.SaveChanges();
            //家长

            var par1 = new User();

            par1.CreateDate = DateTime.Now;
            par1.ModifyDate = DateTime.Now;
            par1.LastLoginDate = DateTime.Now;
            par1.UserName = "parent1";
            par1.RealName = "家长1";
            par1.UserTypeId = UserType.PARENT;
            par1.Password = Utils.GetMd5("123456");
            par1.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7c3");
            par1.RoleId = "parent";
            par1.isDeleted = false;
            par1.isVerify = true;

            var parent1 = new Parent();
            parent1.User = par1;
            parent1.ParentID = Guid.NewGuid();
            parent1.Student = student1;
            parent1.Birthday = DateTime.Now;
            parent1.WorkPlace = "IBM";
            parent1.Telepone = "1234567";
            parent1.MobileTelephone = "123456";

            context.Parent.Add(parent1);
            context.SaveChanges();


            var par2 = new User();

            par2.CreateDate = DateTime.Now;
            par2.ModifyDate = DateTime.Now;
            par2.LastLoginDate = DateTime.Now;
            par2.UserName = "parent2";
            par2.RealName = "家长2";
            par2.UserTypeId = UserType.PARENT;
            par2.Password = Utils.GetMd5("123456");
            par2.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7c8");
            par2.RoleId = "parent";
            par2.isDeleted = false;
            par2.isVerify = true;

            var parent2 = new Parent();
            parent2.User = par2;
            parent2.ParentID = Guid.NewGuid();
            parent2.Student = student2;
            parent2.Birthday = DateTime.Now;
            parent2.WorkPlace = "IBM";
            parent2.Telepone = "1234567";
            parent2.MobileTelephone = "123456";

            context.Parent.Add(parent2);
            context.SaveChanges();

            //督导
            var sup1 = new User();
            sup1.CreateDate = DateTime.Now;
            sup1.ModifyDate = DateTime.Now;
            sup1.LastLoginDate = DateTime.Now;
            sup1.UserName = "supervisor1";
            sup1.RealName = "督导1";
            sup1.UserTypeId = UserType.SUPERVISOR;
            sup1.Password = Utils.GetMd5("123456");
            sup1.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7c4");
            sup1.RoleId = "supervisor";
            sup1.isDeleted = false;
            sup1.isHaveTraining = false;
            sup1.isVerify = true;

            var supervisor1 = new Supervisor();
            supervisor1.SupervisorID = Guid.NewGuid();
            supervisor1.FirtPassword = Utils.GetMd5("123456");
            supervisor1.ConfirmPassword = Utils.GetMd5("123456");
            supervisor1.User = sup1;
            supervisor1.Birthday = DateTime.Now;
            supervisor1.PoliticalExperience = "党员";
            supervisor1.Telepone = "123456789";
            supervisor1.MobileTelephone = "123456";
            supervisor1.Sex = "男";
            supervisor1.Nationality = "汉";
            supervisor1.HomeTown = "北京";

            context.Supervisor.Add(supervisor1);
            context.SaveChanges();

            var sup2 = new User();
            sup2.CreateDate = DateTime.Now;
            sup2.ModifyDate = DateTime.Now;
            sup2.LastLoginDate = DateTime.Now;
            sup2.UserName = "supervisor2";
            sup2.RealName = "督导2";
            sup2.UserTypeId = UserType.SUPERVISOR;
            sup2.Password = Utils.GetMd5("123456");
            sup2.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7c5");
            sup2.RoleId = "supervisor";
            sup2.isDeleted = false;
            sup2.isHaveTraining = false;
            sup2.isVerify = true;
            

            var supervisor2 = new Supervisor();
            supervisor2.SupervisorID = Guid.NewGuid();
            supervisor2.User = sup2;
            supervisor2.FirtPassword = Utils.GetMd5("123456");
            supervisor2.ConfirmPassword = Utils.GetMd5("123456");
            supervisor2.Birthday = DateTime.Now;
            supervisor2.PoliticalExperience = "群众";
            supervisor2.Telepone = "123456789";
            supervisor2.MobileTelephone = "123456";
            supervisor2.Sex = "男";
            supervisor2.Nationality = "汉";
            supervisor2.HomeTown = "上海";

            context.Supervisor.Add(supervisor2);
            context.SaveChanges();

            var sup3 = new User();
            sup3.CreateDate = DateTime.Now;
            sup3.ModifyDate = DateTime.Now;
            sup3.LastLoginDate = DateTime.Now;
            sup3.UserName = "supervisor3";
            sup3.RealName = "督导3";
            sup3.UserTypeId = UserType.SUPERVISOR;
            sup3.Password = Utils.GetMd5("123456");
            sup3.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7d8");
            sup3.RoleId = "supervisor";
            sup3.isDeleted = false;
            sup3.isHaveTraining = false;
            sup3.isVerify = true;

            var supervisor3 = new Supervisor();
            supervisor3.SupervisorID = Guid.NewGuid();
            supervisor3.FirtPassword = Utils.GetMd5("123456");
            supervisor3.ConfirmPassword = Utils.GetMd5("123456");
            supervisor3.User = sup3;
            supervisor3.Birthday = DateTime.Now;
            supervisor3.PoliticalExperience = "党员";
            supervisor3.Telepone = "123456789";
            supervisor3.MobileTelephone = "123456";
            supervisor3.Sex = "女";
            supervisor3.Nationality = "回";
            supervisor3.HomeTown = "成都";

            context.Supervisor.Add(supervisor3);
            context.SaveChanges();

            var sup4 = new User();
            sup4.CreateDate = DateTime.Now;
            sup4.ModifyDate = DateTime.Now;
            sup4.LastLoginDate = DateTime.Now;
            sup4.UserName = "supervisor4";
            sup4.RealName = "督导4";
            sup4.UserTypeId = UserType.SUPERVISOR;
            sup4.Password = Utils.GetMd5("123456");
            sup4.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7d9");
            sup4.RoleId = "supervisor";
            sup4.isDeleted = false;
            sup4.isHaveTraining = false;
            sup4.isVerify = true;

            var supervisor4 = new Supervisor();
            supervisor4.SupervisorID = Guid.NewGuid();
            supervisor4.FirtPassword = Utils.GetMd5("123456");
            supervisor4.ConfirmPassword = Utils.GetMd5("123456");
            supervisor4.User = sup4;
            supervisor4.Birthday = DateTime.Now;
            supervisor4.PoliticalExperience = "团员";
            supervisor4.Telepone = "123456789";
            supervisor4.MobileTelephone = "123456";
            supervisor4.Sex = "男";
            supervisor4.Nationality = "汉";
            supervisor4.HomeTown = "北京";

            context.Supervisor.Add(supervisor4);
            context.SaveChanges();

            var sup5 = new User();
            sup5.CreateDate = DateTime.Now;
            sup5.ModifyDate = DateTime.Now;
            sup5.LastLoginDate = DateTime.Now;
            sup5.UserName = "supervisor5";
            sup5.RealName = "督导5";
            sup5.UserTypeId = UserType.SUPERVISOR;
            sup5.Password = Utils.GetMd5("123456");
            sup5.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7a1");
            sup5.RoleId = "supervisor";
            sup5.isDeleted = false;
            sup5.isHaveTraining = false;
            sup5.isVerify = true;

            var supervisor5 = new Supervisor();
            supervisor5.SupervisorID = Guid.NewGuid();
            supervisor5.FirtPassword = Utils.GetMd5("123456");
            supervisor5.ConfirmPassword = Utils.GetMd5("123456");
            supervisor5.User = sup5;
            supervisor5.Birthday = DateTime.Now;
            supervisor5.PoliticalExperience = "党员";
            supervisor5.Telepone = "123456789";
            supervisor5.MobileTelephone = "123456";
            supervisor5.Sex = "男";
            supervisor5.Nationality = "汉";
            supervisor5.HomeTown = "北京";

            context.Supervisor.Add(supervisor5);
            context.SaveChanges();


            var sup6 = new User();
            sup6.CreateDate = DateTime.Now;
            sup6.ModifyDate = DateTime.Now;
            sup6.LastLoginDate = DateTime.Now;
            sup6.UserName = "supervisor6";
            sup6.RealName = "督导6";
            sup6.UserTypeId = UserType.SUPERVISOR;
            sup6.Password = Utils.GetMd5("123456");
            sup6.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7a2");
            sup6.RoleId = "supervisor";
            sup6.isDeleted = false;
            sup6.isHaveTraining = false;
            sup6.isVerify = true;

            var supervisor6 = new Supervisor();
            supervisor6.SupervisorID = Guid.NewGuid();
            supervisor6.FirtPassword = Utils.GetMd5("123456");
            supervisor6.ConfirmPassword = Utils.GetMd5("123456");
            supervisor6.User = sup6;
            supervisor6.Birthday = DateTime.Now;
            supervisor6.PoliticalExperience = "党员";
            supervisor6.Telepone = "123456789";
            supervisor6.MobileTelephone = "123456";
            supervisor6.Sex = "男";
            supervisor6.Nationality = "汉";
            supervisor6.HomeTown = "北京";

            context.Supervisor.Add(supervisor6);
            context.SaveChanges();

            var sup7 = new User();
            sup7.CreateDate = DateTime.Now;
            sup7.ModifyDate = DateTime.Now;
            sup7.LastLoginDate = DateTime.Now;
            sup7.UserName = "supervisor7";
            sup7.RealName = "督导7";
            sup7.UserTypeId = UserType.SUPERVISOR;
            sup7.Password = Utils.GetMd5("123456");
            sup7.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7a3");
            sup7.RoleId = "supervisor";
            sup7.isDeleted = false;
            sup7.isHaveTraining = false;
            sup7.isVerify = true;

            var supervisor7 = new Supervisor();
            supervisor7.SupervisorID = Guid.NewGuid();
            supervisor7.FirtPassword = Utils.GetMd5("123456");
            supervisor7.ConfirmPassword = Utils.GetMd5("123456");
            supervisor7.User = sup7;
            supervisor7.Birthday = DateTime.Now;
            supervisor7.PoliticalExperience = "党员";
            supervisor7.Telepone = "123456789";
            supervisor7.MobileTelephone = "123456";
            supervisor7.Sex = "男";
            supervisor7.Nationality = "汉";
            supervisor7.HomeTown = "北京";

            context.Supervisor.Add(supervisor7);
            context.SaveChanges();
           //公告类型
            NoticeType noticeType1 = new NoticeType();
            noticeType1.NoticeTypeID = Guid.NewGuid();
            noticeType1.TypeName = "心理康复";
            noticeType1.Introduction = "心理康复治疗相关";
            noticeType1.CreateDate = DateTime.Now;
            noticeType1.ModifyDate = DateTime.Now;
            noticeType1.isDeleted = false;

            context.NoticeType.Add(noticeType1);
            context.SaveChanges();

            //公告
            Notice notice1 = new Notice();
            notice1.NoticeID = Guid.NewGuid();
            notice1.Title = "心理康复治疗相关的情况1";
            notice1.Content = "很多很多";
            notice1.CreateDate = DateTime.Now;
            notice1.ModifyDate = DateTime.Now;
            notice1.NoticeTypeID = noticeType1.NoticeTypeID;
            notice1.isDeleted = false;

            context.Notice.Add(notice1);
            context.SaveChanges();

            Notice notice2 = new Notice();
            notice2.NoticeID = Guid.NewGuid();
            notice2.Title = "心理康复治疗相关的情况2";
            notice2.Content = "很多很多";
            notice2.CreateDate = DateTime.Now;
            notice2.ModifyDate = DateTime.Now;
            notice2.NoticeTypeID = noticeType1.NoticeTypeID;
            notice2.isDeleted = false;

            context.Notice.Add(notice2);
            context.SaveChanges();

            //公告
            Notice notice3 = new Notice();
            notice3.NoticeID = Guid.NewGuid();
            notice3.Title = "心理康复治疗相关的情况3";
            notice3.Content = "很多很多";
            notice3.CreateDate = DateTime.Now;
            notice3.ModifyDate = DateTime.Now;
            notice3.NoticeTypeID = noticeType1.NoticeTypeID;
            notice3.isDeleted = false;

            context.Notice.Add(notice3);
            context.SaveChanges();

            //配对数据
            Pairs pairs = new Pairs();
            pairs.PairsID = Guid.NewGuid();
            pairs.SupervisorID=supervisor2.SupervisorID;
            pairs.VolunteerID = volunteer5.VolunteerID;
            pairs.StudentID =student2.StudentID;

            pairs.StartDate =DateTime.Parse("2011-5-1 20:00:00");
            pairs.EndDate = DateTime.Parse("2011-9-1 20:00:00");

            pairs.State = "正常";
            pairs.CreateDate = DateTime.Now;
            pairs.CeaseDate = DateTime.Parse("2000-1-1 00:00:00");
            
            context.Pairs.Add(pairs);
            context.SaveChanges();

            DateTime start = pairs.StartDate;
            DateTime end = pairs.EndDate;
            DateTime temp = start;

            //插入预约
            while (start.CompareTo(end) < 0)
            {
                PairAppointment pa = new PairAppointment();
                pa.PairAppointmentID = Guid.NewGuid();
                //分配视讯服务器

                pa.ServerName = "深圳市服务器";

                pa.AppointmentID = 1;
                pa.Name = "test";
                
                pa.Pairs = pairs;
                pa.State = "正常";
                pa.StartDate = start;
                pa.startweekday = start.DayOfWeek.ToString();
                pa.EndDate = start.AddHours(2);
                //pa.endweekday = pa.EndDate.DayOfWeek.ToString();
                context.PairAppointment.Add(pa);
                context.SaveChanges();

                for (int i = 0; i < 3; i++)
                {

                    AppointmentMember member = new AppointmentMember();
                    member.AppointmentMemberID = Guid.NewGuid();
                    member.AppointmentHash = Guid.NewGuid().ToString();
                    member.PairAppointmentID = pa.PairAppointmentID;
                    member.StartRealDate = start.AddMinutes(10);
                    member.EndRealDate = start.AddHours(1).AddMinutes(30);

                    if (i == 0)
                    {
                        // mxCommand.Parameters.Add("@firstname", sName);
                        member.UserID = sup2.Id;

                    }
                    else if (i == 1)
                    {
                        // mxCommand.Parameters.Add("@firstname", stuName);
                        member.UserID = stu2.Id;
                    }
                    else
                    {
                        // mxCommand.Parameters.Add("@firstname", vName);
                        member.UserID = vol2.Id;

                    }
                    context.AppointmentMember.Add(member);
                    context.SaveChanges();
                }
                start = start.AddDays(1);
            }



            //资源类型
/*            var resource = new ResourceType();
            resource.Id = Guid.NewGuid();
            resource.TypeName = "动漫";
            resource.Parent = null;
            resource.Introduction = "huoying";

            context.ResourceType.Add(resource);
            context.SaveChanges();
*/
            //爱心人士
            /*var don1 = new User();

            don1.CreateDate = DateTime.Now;
            don1.ModifyDate = DateTime.Now;
            don1.LastLoginDate = DateTime.Now;
            don1.UserName = "donator1";
            don1.RealName = "爱心人士1";
            don1.UserTypeId = UserType.DONATOR;
            don1.Password = Utils.GetMd5("123456");
            don1.Id = new Guid("a8514e9c-eabf-412e-a744-b2c6fdb3c7a9");
            don1.RoleId = "donator";
            don1.isDeleted = false;

            Donator donator1 = new Donator();       
            donator1.DonatorID = Guid.NewGuid();
            donator1.User = don1;
            donator1.FirtPassword = Utils.GetMd5("123456");
            donator1.ConfirmPassword = Utils.GetMd5("123456");
            donator1.EMail = "jiang@163.com";
            donator1.Telephone = "12345678";

            context.Donator.Add(donator1);
            context.SaveChanges();*/

        }
    }
}