using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClubReservation.Models;
using ClubReservation.Helpers;
using ClubReservation.Filters;
namespace ClubReservation.Controllers
{
    public class StatisticsController : Controller
    {
        private ClubReservationContext db = new ClubReservationContext();
        private ActionHelper ah = new ActionHelper();
        /// <summary>
        /// 用户活动统计一览
        /// </summary>
        /// <returns></returns>

        public ActionResult Users()
        {
            //根据角色获得用户列表
            ViewBag.Users = ah.GetUsersByRole();
            return View();
        }
        public ActionResult UserDetails(int id = 0)
        {
            User user =db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound("没有找到该用户");
            }
            ViewBag.UserActivities = user.UserActivities.ToList();
            ViewBag.User = user;
            return View();
        }
        /// <summary>
        /// 俱乐部信息统计
        /// </summary>
        /// <returns></returns>
        public ActionResult Clubs()
        {
            ViewBag.Clubs = ah.GetClubsByRole();
            return View();
        }
        /// <summary>
        /// 俱乐部活动情况统计
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ClubDetails(int id = 0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound("没有找到该俱乐部");
            }
            ViewBag.Activities = club.Activities.ToList();
            ViewBag.Club = club;
            return View();
        }
        /// <summary>
        /// 获取俱乐部活动统计折线图
        /// </summary>
        /// <param name="t">折线图类型</param>
        /// <param name="id">俱乐部Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetClubById(string t, int id = 0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return Json(new { state = 204 });
            }
            if (t == MasterData.StatisticsDay)
            {
                return GetClubDay(club);
            }
            if (t == MasterData.StatisticsWeek)
            {
                return GetClubWeek(club);
            }
            if (t == MasterData.StatisticsYear)
            {
                return GetClubMonth(club);
            }
            return Json(new { state = 201 });
        }
        /// <summary>
        /// 把一天划分为时间段统计
        /// </summary>
        /// <param name="club"></param>
        /// <returns></returns>
        protected JsonResult GetClubDay(Club club)
        {
            int[] r1 = new int[4];
            int[] r2 = new int[4];
            int[] r3 = new int[4];
            string[] ticks = { "06:00-10:00", "10:00-14:00", "14:00-18:00", "18:00-22:00" };
            int[] h = { 6, 10, 14, 18, 22 };
            for (int i = 0; i < 4; i++)
            {
                var v1 = (from a in club.Activities
                          where a.StartTime.Hour >= h[i] && a.StartTime.Hour < h[i + 1]
                          select new
                          {
                              n1 = (a.EnrollNo * 100) / a.MaxNumber,
                              n2 = (a.EnrollNo * 100) / club.Number,
                              n3 = (a.MaxNumber * 100) / club.Number
                          }).ToList();
                if (v1.Count > 0)
                {
                    //参加人数/活动上限
                    r1[i] = i + (int)v1.Average(a => a.n1);
                    //参加人数/俱乐部人数
                    r2[i] = i + (int)v1.Average(a => a.n2);
                    //活动上限/俱乐部人数
                    r3[i] = i + (int)v1.Average(a => a.n3);
                }
            }
            var results = new { state = 200, ticks = ticks, d1 = r1, d2 = r2, d3 = r3 };
            return Json(results);
        }

        protected JsonResult GetClubWeek(Club club)
        {
            int[] r1 = new int[7];
            int[] r2 = new int[7];
            int[] r3 = new int[7];
            string[] ticks = { "周日", "周一", "周二", "周三", "周四", "周五", "周六" };
            int[] w = { 0, 1, 2, 3, 4, 5, 6 };
            for (int i = 0; i < 7; i++)
            {
                var v1 = (from a in club.Activities
                          where (int)a.StartTime.DayOfWeek == w[i]
                          select new
                          {
                              n1 = (a.EnrollNo * 100) / a.MaxNumber,
                              n2 = (a.EnrollNo * 100) / club.Number,
                              n3 = (a.MaxNumber * 100) / club.Number
                          }).ToList();
                if (v1.Count > 0)
                {
                    //参加人数/活动上限
                    r1[i] = i + (int)v1.Average(a => a.n1);
                    //参加人数/俱乐部人数
                    r2[i] = i + (int)v1.Average(a => a.n2);
                    //活动上限/俱乐部人数
                    r3[i] = i + (int)v1.Average(a => a.n3);
                }
            }
            var results = new { state = 200, ticks = ticks, d1 = r1, d2 = r2, d3 = r3 };
            return Json(results);
        }

        protected JsonResult GetClubMonth(Club club)
        {
            int[] r1 = new int[12];
            int[] r2 = new int[12];
            int[] r3 = new int[12];
            string[] ticks = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二" };
            int[] m = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            for (int i = 0; i < 12; i++)
            {
                var v1 = (from a in club.Activities
                          where a.StartTime.Month == m[i]
                          select new
                          {
                              n1 = (a.EnrollNo * 100) / a.MaxNumber,
                              n2 = (a.EnrollNo * 100) / club.Number,
                              n3 = (a.MaxNumber * 100) / club.Number
                          }).ToList();
                if (v1.Count > 0)
                {
                    //参加人数/活动上限
                    r1[i] = i + (int)v1.Average(a => a.n1);
                    //参加人数/俱乐部人数
                    r2[i] = i + (int)v1.Average(a => a.n2);
                    //活动上限/俱乐部人数
                    r3[i] = i + (int)v1.Average(a => a.n3);
                }
            }
            var results = new { state = 200, ticks = ticks, d1 = r1, d2 = r2, d3 = r3 };
            return Json(results);
        }
    }
}
