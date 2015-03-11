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
    public class ClubController : Controller
    {
        private ClubReservationContext db = new ClubReservationContext();
        private ActionHelper ah = new ActionHelper();
        private User currentuser = new CurrentUser().User;
        /// <summary>
        /// 俱乐部一览 P:ALL
        /// </summary>
        /// <returns>全部俱乐部</returns>

        public ActionResult Index()
        {
            return View(db.Clubs.ToList());
        }

        /// <summary>
        /// 俱乐部详细 P:ALL
        /// </summary>
        /// <param name="id">俱乐部Id</param>
        /// <returns></returns>

        public ActionResult Details(int id = 0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound("没有找到该俱乐部");
            }
            return View(club);
        }

        /// <summary>
        /// 俱乐部追加 P:SA
        /// </summary>
        /// <returns></returns>

        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 俱乐部追加 P:SA
        /// </summary>
        /// <param name="club"></param>
        /// <returns>成功进入详细页，失败返回新建页</returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Club club)
        {
            club.CreateDate = DateTime.Now;
            club.UpdateDate = DateTime.Now;
            club.UserId = currentuser.Id;
            var temp = db.Clubs.FirstOrDefault(c=>c.ClubName.Equals(club.ClubName));
            if (temp != null)
            {
                TempData["Error"] = "该俱乐部已存在";
                return View(club);
            }
            if (ModelState.IsValid)
            {
                db.Clubs.Add(club);
                db.SaveChanges();
                TempData["Success"] = "新建成功";
                return RedirectToAction("Details", new { id=club.Id});
            }
            TempData["Error"] = "新建失败";
            return View(club);
        }

        /// <summary>
        /// 俱乐部编辑 P:SA,CM
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Edit(int id = 0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound("没有找到该俱乐部");
            }
            return View(club);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="club"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Club club)
        {
            club.UpdateDate = DateTime.Now;
            club.UserId = currentuser.Id;
            if (ModelState.IsValid)
            {
                db.Entry(club).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "编辑成功";
                return RedirectToAction("Details", new { id=club.Id});
            }
            TempData["Error"] = "编辑失败";
            return View(club);
        }

        /// <summary>
        /// 俱乐部删除功能
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult Delete(int id=0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null) {
                return null;
            }
            //活动
            var activities = club.Activities.ToList();
            //资产
            var properties = club.Properties.ToList();
            //用户
            var clubusers = club.ClubUsers.ToList();
            //俱乐部的活动 删除
            foreach(var a in activities){
                //参加该活动的记录删除
                foreach(var ua in a.UserActivities.ToList()){
                    db.UserActivities.Remove(ua);
                }
                db.Activities.Remove(a);
            }
            //俱乐部的资产删除
            foreach(var p in properties){
                db.Properties.Remove(p);
            }
            //俱乐部的用户记录删除
            foreach (var c in clubusers)
            {
                db.ClubUsers.Remove(c);
            }
            db.Clubs.Remove(club);
            var result = db.SaveChanges();
            if (result >= 1)
            {
                return PartialView("_ClubTable",db.Clubs.ToList());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 资产管理
        /// </summary>
        /// <param name="id">俱乐部Id</param>
        /// <returns></returns>
        public ActionResult Property(int id=0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            ViewBag.Club = club;
            return View(club.Properties.ToList());
        }
        /// <summary>
        /// 删除资产
        /// </summary>
        /// <param name="id">资产Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PropertyDelete(int id = 0)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return null;
            }
            var club = property.club;
            db.Properties.Remove(property);
            var result = db.SaveChanges();
            if (result >= 1)
            {
                return PartialView("_PropertyTable", club.Properties.ToList());
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 资产新建
        /// </summary>
        /// <param name="id">俱乐部Id</param>
        /// <returns></returns>
        public ActionResult PropertyCreate(int id=0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound("没有找到该俱乐部");
            }
            ViewBag.Club = club;
            return View();
        }
        /// <summary>
        /// 俱乐部新建
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PropertyCreate(Property property)
        {
            property.UpdateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Properties.Add(property);
                db.SaveChanges();
                TempData["Success"] = "新建成功";
                return RedirectToAction("PropertyDetails", new { id=property.Id});
            }
            TempData["Error"] = "新建失败";
            return View("PropertyCreate", new { id = property.ClubId });
        }
        /// <summary>
        /// 俱乐部资产编辑
        /// </summary>
        /// <param name="id">资产Id</param>
        /// <returns></returns>
        public ActionResult PropertyEdit(int id=0)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound("没有找到该俱乐部资产");
            }
            Club club = db.Clubs.Find(property.ClubId);
            ViewBag.Club = club;
            return View(property);
        }
        /// <summary>
        /// 俱乐部资产编辑
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PropertyEdit(Property property)
        {
            property.UpdateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "编辑成功";
                return RedirectToAction("PropertyDetails", new { id=property.Id});
            }
            TempData["Error"] = "编辑失败";
            return View("PropertyEdit", new { id = property.ClubId });
        }
        /// <summary>
        /// 俱乐部资产详细
        /// </summary>
        /// <param name="id">资产Id</param>
        /// <returns></returns>
        public ActionResult PropertyDetails(int id = 0)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound("没有找到该俱乐部资产");
            }
            ViewBag.Club = db.Clubs.Find(property.ClubId);
            return View(property);
        }
        /// <summary>
        /// 申请加入俱乐部
        /// </summary>
        /// <param name="clubId">俱乐部Id</param>
        /// <returns></returns>
        [HttpPost]
        public int ExamineCreate(int clubId = 0)
        {
            Club club = db.Clubs.Find(clubId);
            if (club == null)
            {
                return 204;
            }
            var cu = db.ClubUsers.FirstOrDefault(c => c.ClubId == clubId && c.UserId==currentuser.Id);
            if (cu != null)
            {
                return 202;
            }
            var count = db.ClubUsers.Count(c=>c.UserId==currentuser.Id);
            if(count>=MasterData.MaxJoin){
                return 203;
            }
            var ex = db.Examines.FirstOrDefault(e=>e.UserId==currentuser.Id&&e.ClubId==club.Id);
            if (ex != null)
            {
                return 205;
            }
            Examine examine = new Examine();
            examine.ClubId = club.Id;
            examine.UserId = currentuser.Id;
            examine.CreateDate = DateTime.Now;
            examine.State = MasterData.ExamineUD;
            db.Examines.Add(examine);
            int result = db.SaveChanges();
            return (result > 0) ? 200 : 201;
        }

        /// <summary>
        /// 删除申请信息 P:AU,CM
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int ExamineDelete()
        {
            var examines = from e in db.Examines
                           where e.UserId == currentuser.Id && (e.State == MasterData.ExaminePA || e.State == MasterData.ExamineNP)
                           select e;
            foreach (var item in examines.ToList())
            {
                db.Examines.Remove(item);
            }
            int result = db.SaveChanges();
            return (result > 0) ? 200 : 201;
        }

        /// <summary>
        /// 申请一览
        /// </summary>
        /// <param name="id">俱乐部Id</param>
        /// <returns></returns>
        public ActionResult Examine(int id = 0)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound("没有找到该俱乐部");
            }
            ViewBag.Club = club;
            var examines = db.Examines.Where(e=>e.ClubId==id && e.State==MasterData.ExamineUD);
            return View(examines);
        }
        /// <summary>
        /// 加入申请通过
        /// </summary>
        /// <param name="id">申请表Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExaminePass(int id = 0)
        {
            Examine examine = db.Examines.Find(id);
            if (examine == null)
            {
                return null;
            }
            Club club = db.Clubs.Find(examine.ClubId);
            if (club == null)
            {
                return null;
            }
            ClubUser clubUser = new ClubUser();
            clubUser.ClubId = examine.ClubId;
            clubUser.UserId = examine.UserId;
            clubUser.RoleId = MasterData.RoleAU;
            clubUser.JoinTime = DateTime.Now;
            club.Number = club.Number + 1;
            db.ClubUsers.Add(clubUser);
            examine.State = MasterData.ExaminePA;
            var result=db.SaveChanges();
            if (result >= 1)
            {
                var examines = db.Examines.Where(e => e.ClubId == club.Id && e.State == MasterData.ExamineUD);
                return PartialView("_ExamineTable", examines);
            }
            return null;
        }
        /// <summary>
        /// 报名退回
        /// </summary>
        /// <param name="id">报名表Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExamineReturn(int id = 0)
        {
            Examine examine = db.Examines.Find(id);
            if (examine == null)
            {
                return null;
            }
            int clubId = examine.ClubId;
            examine.State = MasterData.ExamineNP;
            var result = db.SaveChanges();
            if (result >= 1)
            {
                var examines = db.Examines.Where(e => e.ClubId == clubId && e.State == MasterData.ExamineUD);
                return PartialView("_ExamineTable", examines.ToList());
            }
            return null;
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}