using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROnline.Models.Teams;
using PROnline.Models.WorkStations;
using PROnline.Models.Users;

namespace PROnline.Controllers.Users
{
    public class MemberTools
    {

        public static List<WorkStation> CombineMember(List<WorkStation> stations,List<Team> teams,List<TeamMember> teamMember)
        {
            Dictionary<String, WorkStation> stationTemp = new Dictionary<String, WorkStation>();
            Dictionary<Guid, Team> teamTemp = new Dictionary<Guid, Team>();
            foreach (WorkStation temp in stations)
            {
                stationTemp[temp.WorkStationID.ToString()] = temp;
                temp.Teams=new List<Team>();
            }
            foreach (Team temp in teams)
            {
                teamTemp[temp.TeamID] = temp;
                temp.members = new List<Volunteer>();
            }
            foreach (TeamMember temp in teamMember)
            {
                if (teamTemp.ContainsKey(temp.TeamID))
                {
                    teamTemp[temp.TeamID].members.Add(temp.Volunteer);
                }

            }

            foreach (Team temp in teams)
            {
                if (temp.WorkStationID!=null&&stationTemp.ContainsKey(temp.WorkStationID.ToString()) && temp.members.Count > 0)
                {
                    stationTemp[temp.WorkStationID.ToString()].Teams.Add(temp);
                }
            }
            List<WorkStation> result = new List<WorkStation>();
            foreach(WorkStation station in stations){
                if (station.Teams.Count > 0)
                { 
                    result.Add(station);
                }
            }

            return result;
        }



        public static List<School> CombineStudent(List<School> schools, List<Student> students)
        {
            Dictionary<Guid, School> schoolTemp = new Dictionary<Guid, School>();
            foreach (School scl in schools)
            {
                schoolTemp[scl.SchoolID] = scl;
                scl.Students = new List<Student>();
            }
            foreach (Student stu in students)
            {
                if (schoolTemp.ContainsKey(stu.SchoolID))
                {
                    schoolTemp[stu.SchoolID].Students.Add(stu);
                }
            }
            List<School> result = new List<School>();
            foreach (School scl in schools)
            {
                if (scl.Students.Count > 0)
                {
                    result.Add(scl);
                }
            }
            return result;
        }

    }
}