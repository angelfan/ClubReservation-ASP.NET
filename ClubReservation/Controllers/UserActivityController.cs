using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClubReservation.Models;
using ClubReservation.Filters;
using ClubReservation.Helpers;
namespace ClubReservation.Controllers
{
    public class UserActivityController : Controller
    {
        private ClubReservationContext db = new ClubReservationContext();
        //
        // GET: /UserActivity/

        public ActionResult Index() 
        {
            //因为state从0变成1 要在每天的0点刷新 所以前台有可能会出现刚刚结束的活动
            //活动状态为：报名参加 人数已满 报名截止
            ViewBag.TimeNow = DateTime.Now;
            var cuser = new CurrentUser().User;
            var user = db.Users.Find(cuser.Id);
            var clubusers = db.ClubUsers.Where(c => c.UserId == user.Id);
            var act = from a in db.Activities
                           from c in clubusers
                           where a.ClubId==c.ClubId && a.State==MasterData.ActivityEnable
                           select a;//获取用户对应俱乐部的活动
            var activities = act.Distinct().ToList();
            foreach (var ua in user.UserActivities.ToList())
            {
                activities.Remove(ua.Activity);//将报名过的活动从列表去掉
            }
            return View(activities);
        }

        public ActionResult Enroll(int id)
        {
            var user = new CurrentUser().User;
            Activity activity = db.Activities.Find(id);
            if(activity.EnrollNo<activity.MaxNumber)
            {
              
                UserActivity useractivty = new UserActivity();
                useractivty.ActivityId = id;
                useractivty.UserId = user.Id;
                useractivty.EnrollTime = DateTime.Now;
                useractivty.State = 0;  //活动提醒状态设置为 ： 未提醒
                db.UserActivities.Add(useractivty); //像user_activity表插入数据
                activity.EnrollNo = activity.EnrollNo + 1;  //将活动报名总数+1
                db.Entry(activity).State = EntityState.Modified;
                var r = db.SaveChanges();
                if (r > 1)
                {
                    TempData["messages"] = "报名成功";
                    return RedirectToAction("Index", "UserZone",TempData["Messages"] as List<string>);
                }
                else
                {
                    TempData["messages"] = "报名失败";
                    return View("Index",TempData["Messages"] as List<string>);
                }
            }
            else
            {
                TempData["messages"] = "报名失败";
                return RedirectToAction("Index", "UserActivity",TempData["Messages"] as List<string>);
            }                 
        }       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
