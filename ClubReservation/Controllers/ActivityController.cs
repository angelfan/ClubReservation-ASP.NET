using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClubReservation.Models;
using ClubReservation.Filters;
using ClubReservation.Helpers;

namespace ClubReservation.Controllers
{
    public class ActivityController : Controller
    {
        private ClubReservationContext db = new ClubReservationContext();

        //初始化活动一览页面
        // GET: /Activity/
        public ActionResult Index()
        {
            ViewBag.activities = GetActivitiesByRole();
            return View();
        }

        //详细页面
        // GET: /Activity/Details/5
        public ActionResult Details(int id = 0)
        {
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound("没有找到该活动");//返回一个错误页面
            }
            return View(activity);
        }

        //初始化创建活动页面
        // GET: /Activity/Create
        public ActionResult Create()
        {
            var user = new CurrentUser().User;
            //用户属于那些俱乐部 并且是该俱乐部的管理员
            var cu = db.ClubUsers.Where(u => u.UserId == user.Id && u.RoleId == MasterData.RoleCM);
            //获取该用户的俱乐部
            var clubs = (from c in db.Clubs
                         from u in cu
                         where c.Id == u.Club.Id
                         select c).Distinct();
            ViewBag.clubs = clubs.ToList();
            return View();
        }

        //新建活动
        // POST: /Activity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Activity activity)
        {
            var user = new CurrentUser().User;
            activity.CreateDate = DateTime.Now;
            activity.UpdateDate = DateTime.Now;
            activity.UserId = user.Id;
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                //var add_activity = db.Activities.Add(activity);
                //add_activity.CreateDate = DateTime.Now;//加入创建时间
                //add_activity.UpdateDate = DateTime.Now;//加入创建时间
                //add_activity.UserId = user.Id;//加入创建者        
                var r = db.SaveChanges();
                if (r == 1)
                {
                    TempData["messages"] = "新建成功";
                    return RedirectToAction("Details", new { id = activity.Id });
                }
            }
            //用户属于那些俱乐部 并且是该俱乐部的管理员
            var cu = db.ClubUsers.Where(u => u.UserId == user.Id && u.RoleId == MasterData.RoleCM);
            //获取该用户的俱乐部
            var clubs = (from c in db.Clubs
                         from u in cu
                         where c.Id == u.Club.Id
                         select c).Distinct();
            ViewBag.clubs = clubs.ToList();
            TempData["messages"] = "新建失败";
            return View(activity);
        }

        //初始化
        // GET: /Activity/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();//返回错误页面
            }
            return View(activity);
        }

        //编辑活动
        // POST: /Activity/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Activity activity)
        {
            var user = new CurrentUser().User;
            if (ModelState.IsValid)
            {
                activity.UpdateUserId = user.Id;
                activity.CreateDate = activity.CreateDate;
                activity.UpdateDate = DateTime.Now;
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                TempData["messages"] = "编辑成功";
                return RedirectToAction("Details", new { id = activity.Id });
            }
            TempData["messages"] = "编辑失败";
            return View(activity);
        }
        //ajax删除
        [HttpPost]
        public ActionResult Delete()
        {
            var id = Request["id"].ToString();//接受js传递过来的"id"
            int activityid = id == null ? 0 : int.Parse(id);
            Activity activity = db.Activities.Find(activityid);
            if (activity == null)
            {
                return null; //没找到 返回null
            }
            //同时也要删除user_activity表下对应的信息
            foreach (var c in activity.UserActivities.ToList())
            {
                db.UserActivities.Remove(c);
            }
            db.Activities.Remove(activity); //删除此数据
            var result = db.SaveChanges();
            if (result >= 1)
            {
                ViewBag.activities = GetActivitiesByRole();
                return PartialView("_ActivityTable", ViewBag.activities);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据用户取得活动
        /// </summary>
        /// <returns></returns>
        protected List<Activity> GetActivitiesByRole()
        {
            var user = new CurrentUser().User;
            //用户属于那些俱乐部 并且是该俱乐部的管理员
            var cu = db.ClubUsers.Where(u => u.UserId == user.Id && u.RoleId == MasterData.RoleCM);
            //查找这些俱乐部的活动
            var activities = (from c in db.Activities
                              from u in cu
                              where c.ClubId == u.Club.Id
                              select c).Distinct();
            return activities.ToList();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}