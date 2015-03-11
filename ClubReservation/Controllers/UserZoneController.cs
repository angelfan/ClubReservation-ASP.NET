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
    public class UserZoneController : Controller
    {

        private ClubReservationContext db = new ClubReservationContext();
  
        //主页显示
        public ActionResult Index()
        {
            //活动状态为：取消报名 即将开始 正在进行 活动结束
            var cuser = new CurrentUser().User;
            var user = db.Users.Find(cuser.Id);//根据当前登陆获得该用户
            ViewBag.TimeNow = DateTime.Now;      
            ViewBag.userActivities = user.UserActivities;//取得该用户参加的活动//取得该用户参加的活动
            return View();
        }

        public ActionResult Statics()
        {
            return View();
        }
    
        //修改密码 ajax
        [HttpPost]
        public ActionResult ChangePwd()
        {
            var cuser = new CurrentUser().User;
            var user = db.Users.Find(cuser.Id);//根据当前登陆获得该用户
            string pwd = Request["newPwd"].ToString();//接受js传递过来的"id"
            if (pwd.Length >= 6 || pwd.Length <= 16) {
                user.Password = UserHelper.GetMD5(pwd);
                db.SaveChanges();
                return Json("200");
            } 
            else
            {
                return Json("201");
            }
        }

        //取消报名 ajax
        public ActionResult CancelEnroll(int id = 0)
        {
            var user = new CurrentUser().User;
            UserActivity userActivity = db.UserActivities.Find(id);
            Activity activity = userActivity.Activity;  //找到该活动  
            if (activity.EnrollEndTime > DateTime.Now)  //判断有没有到报名截止时间
            {
                //如果没到报名截止时间
                activity.EnrollNo = activity.EnrollNo - 1; //活动报名人数减一
                db.UserActivities.Remove(userActivity);  //user_activity表中该条数据删除
                var result = db.SaveChanges();//保存数据 返回值2
            
                if (result > 1 )
                {
                    ViewBag.userActivities = user.UserActivities.ToList();//取得该用户参加的活动
                    return PartialView("_readyTable"); //保存成功 返回html
                }
                else
                {
                    return null;
                }
            }
            else 
            {
                //如果到达报名结束时间
                return null;
            }
        }
        public ActionResult SuperZone(int id=0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound("没有找到该用户");
            }
            return View(user);
        }
        //超级管理员信息修改
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuperZone(User user)
        {
            user.RoleId = MasterData.RoleSA;
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "编辑成功";
                return View(user);
            }
            TempData["Error"] = "编辑失败";
            return View(user);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
      
    }
}
