using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using ClubReservation.Models;
using ClubReservation.Filters;
using ClubReservation.Helpers;

namespace ClubReservation.Helpers
{
    public class PageHelper
    {
        private ClubReservationContext db = new ClubReservationContext();

        public string[] GetUserActivityCount(User user)
        {
            string[] result=new string[3];
            if (user == null)
            {
                return result;
            }
            //用户所有参加的活动
            var ycj = (from ua in db.UserActivities
                       where ua.UserId == user.Id
                       select ua).Count();
            //用户加入俱乐部以来所有举办的活动
            var num = (from a in db.Activities
                       from cu in db.ClubUsers
                       where a.ClubId == cu.ClubId && cu.UserId == user.Id
                       select a).Distinct().Count();
            result[0] = ycj.ToString();
            result[1] = (num - ycj).ToString();
            result[2] = num.ToString();
            return result;
        }

        /// <summary>
        /// 获得用户加入俱乐部后参加活动比例
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetUserActivityPercent(User user)
        {
            if (user == null)
            {
                return null;
            }
            var v1=(from a in db.Activities
                   from cu in db.ClubUsers
                   where a.ClubId==cu.ClubId && cu.UserId==user.Id && a.CreateDate>=cu.JoinTime
                   select a).Distinct().Count();
            int count=user.UserActivities.Count;
            float? result = (count == 0||v1==0) ? null : ((float?)count / v1);
            float f;
            if (result == null)
            {
                return null;
            }
            else
            {
                f = (float)result;
            }
            return f.ToString("P");
        }
        /// <summary>
        /// 判断当前用户是否是该俱乐部的管理员
        /// </summary>
        /// <param name="clubId">clubId</param>
        /// <returns>返回true | false</returns>
        public bool IsClubManage(int clubId)
        {
            var user = new CurrentUser().User;
            var cu = user.ClubUsers.FirstOrDefault(c => c.ClubId == clubId && c.RoleId == MasterData.RoleCM);
            return (cu==null)? false : true;
        }
        /// <summary>
        /// 获取管理员所管理的部门申请数据
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public List<Club> GetExamineByUser(User user)
        {
            if (user == null)
            {
                return null;
            }
            var clubs = (from c in db.Clubs
                       from cu in db.ClubUsers
                       from e in db.Examines
                       where c.Id == cu.ClubId && cu.UserId == user.Id && cu.RoleId == MasterData.RoleCM &&
                        c.Id==e.ClubId && e.State==MasterData.ExamineUD
                       select c).Distinct();
            return clubs.ToList();
        }
        /// <summary>
        /// 获取活动提醒
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public List<Activity> GetActivityByUser(User user)
        {
            if (user == null)
            {
                return null;
            }
            var activities = from a in db.Activities
                                 from ua in db.UserActivities
                                 where a.Id == ua.ActivityId && ua.UserId == user.Id && a.StartTime < DateTime.Now
                                 select a;
            return activities.ToList();
        }
        /// <summary>
        /// 获取申请加入俱乐部信息
        /// </summary>
        /// <returns></returns>
        public List<Examine> GetUserExamine(User user)
        {
            if (user == null)
            {
                return null;
            }
            var examines = from e in db.Examines
                           where e.UserId == user.Id && (e.State == MasterData.ExaminePA || e.State==MasterData.ExamineNP)
                           select e;
            return examines.ToList();
        }
        /// <summary>
        /// 获取俱乐部参加比例
        /// </summary>
        /// <param name="club"></param>
        /// <returns></returns>
        public string GetClubJoinPerCent(Club club)
        {
            var result = (from a in db.Activities
                          where a.ClubId==club.Id
                         select new { num=(float?)a.EnrollNo/a.MaxNumber}).Average(a=>a.num);
            float f;
            if (result == null)
            {
                return null;
            }
            else
            {
                f = (float)result;
            }
            return f.ToString("P");
        }
    }
    public class ActionHelper
    {
        private ClubReservationContext db = new ClubReservationContext();
        /// <summary>
        /// 根据权限查找用户
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsersByRole()
        {
            List<User> list = new List<Models.User>();
            var user = new CurrentUser().User;
            if (user == null)
            {
                return list;
            }
            if (user.RoleId == MasterData.RoleSA)
            {
                var users = (from u in db.Users
                            where u.RoleId != MasterData.RoleSA
                            select u).Distinct().ToList();
                return users;
            }
            if (user.RoleId == MasterData.RoleCM)
            {
                var clubs =  from c in db.Clubs
                             from cu in db.ClubUsers
                             where c.Id == cu.ClubId && cu.UserId == user.Id && cu.RoleId == MasterData.RoleCM
                             select c;
                var users = (from u in db.Users
                             from cu in db.ClubUsers
                             from c in clubs
                             where (u.Id == cu.UserId && cu.ClubId == c.Id) || (u.Id != cu.Id)
                             select u).Distinct().ToList();
                return users;
            }
            return list;
        }

        /// <summary>
        /// 根据权限查找俱乐部
        /// </summary>
        /// <returns></returns>
        public List<Club> GetClubsByRole()
        {
            List<Club> list = new List<Models.Club>();
            var user = new CurrentUser().User;
            if (user == null)
            {
                return list;
            }
            if (user.RoleId == MasterData.RoleSA)
            {
                return db.Clubs.ToList();
            }
            if (user.RoleId == MasterData.RoleCM)
            {
                var clubs = (from c in db.Clubs
                             from cu in db.ClubUsers
                             where c.Id == cu.ClubId && cu.UserId == user.Id && cu.RoleId == MasterData.RoleCM
                             select c).Distinct().ToList();
                return clubs;
            }
            return list;
        }

        public DataTable ReadExcel(string filepath)
        {

            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";" + "Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";" + "Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
                DataSet myDataSet = new DataSet();
                myCommand.Fill(myDataSet, "user");
                DataTable table = myDataSet.Tables["user"].DefaultView.ToTable();
                conn.Close();
                return table;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 把列保存为User对象
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public User CreateUser(DataRow row)
        {
            string employeeCode=row[0].ToString();
            User user = db.Users.FirstOrDefault(u => u.EmployeeCode == employeeCode);
            if (user == null)
            {
                user = new User();
                user.EmployeeCode = employeeCode;
            }
            user.Password = UserHelper.GetMD5("000000");
            user.Name = row[1].ToString();
            user.Email = row[2].ToString();
            user.Tel = row[3].ToString();
            user.Phone = row[4].ToString();
            if (row[5].ToString().Equals("男"))
            {
                user.Sex = 0;
            }
            else if (row[5].ToString().Equals("女"))
            {
                user.Sex = 1;
            }
            user.EntryTime = row[6].ToString();
            user.Team = row[7].ToString();
            user.RoleId = MasterData.RoleAU;
            if (user.Id != 0) {
                db.Entry(user).State = EntityState.Modified;
            }
            return user;
        }
    }
}