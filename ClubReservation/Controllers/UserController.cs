using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity.Infrastructure;
using ClubReservation.Models;
using ClubReservation.Filters;
using ClubReservation.Helpers;

namespace ClubReservation.Controllers
{
    public class UserController : Controller
    {
        private ClubReservationContext db = new ClubReservationContext();
        private ActionHelper ah = new ActionHelper();


        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            Session.Remove("CurrentUser");//清空当前用户信息缓存
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var pwd = UserHelper.GetMD5(login.Password);
                var user = db.Users.FirstOrDefault(u => u.EmployeeCode == login.EmployeeCode && u.Password == pwd);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(login.EmployeeCode, false);
                    Session.Remove("CurrentUser");//删除已保存的用户信息
                    return RedirectToAction("Home", "User");
                }
                else
                {
                    TempData["Error"] = "用户名或密码不正确";
                    return View();
                }
            }
            return View();
        }
        //退出登陆
        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            Session.Remove("CurrentUser");//清空当前用户信息缓存
            return View("Login");
        }
        //用户管理首页
        // GET: /User/
        public ActionResult Index()
        {
            return View(ah.GetUsersByRole());
        }

        //用户详细信息
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound("没有找到该用户");
            }
            return View(user);
        }

        //新用户创建
        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        //新用户创建
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            var tempUser = db.Users.FirstOrDefault(u => u.EmployeeCode == user.EmployeeCode);
            if (tempUser == null)
            {
                user.RoleId = MasterData.RoleAU;
                user.Password = UserHelper.GetMD5(user.Password);
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    TempData["Success"] = "新建成功";
                    return RedirectToAction("Details", new{ id=user.Id});
                }
                TempData["Error"] = "新建失败";
                return View(user);
            }
            else
            {
                TempData["Error"] = "员工工号已存在";
                return View(user);
            }
        }

        //用户编辑
        // GET: /User/Edit/5
        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound("没有找到该用户");
            }
            ViewBag.Clubs = ah.GetClubsByRole();
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            //TODO:最大加入俱乐部数
            if (ModelState.IsValid)
            {
                //先设置用户的权限为“普通用户”，如果加入的俱乐部里有“管理员”就设为管理员权限
                user.RoleId = MasterData.RoleAU;
                //参加 数据库保存
                foreach (Club c in ah.GetClubsByRole())
                {
                    //查询用户是否已加入次俱乐部
                    var cu = db.ClubUsers.FirstOrDefault(u =>u.UserId == user.Id && u.ClubId == c.Id);
                    string join_number = Request.Form["join_" + c.Id.ToString()];
                    int j = (join_number == null) ? 0 : int.Parse(join_number);
                    //参加俱乐部
                    if (j == 1)
                    {
                        if (cu == null)
                        {
                            cu = new ClubUser();
                        }
                        cu.ClubId = c.Id;
                        cu.UserId = user.Id;
                        cu.JoinTime = DateTime.Now;
                        string role_number = Request.Form["role_" + c.Id.ToString()];
                        cu.RoleId = (role_number == null) ? 0 : int.Parse(role_number);
                        if (cu.RoleId == MasterData.RoleCM)
                        {
                            user.RoleId = MasterData.RoleCM;
                        }
                        if (cu.Id == 0)
                        {
                            //俱乐部人数+1
                            c.Number = c.Number - 1;
                            db.ClubUsers.Add(cu);
                        }
                    }
                    else
                    {
                        //取消参加俱乐部
                        if (cu != null)
                        {
                            //俱乐部人数-1
                            c.Number = c.Number - 1;
                            db.ClubUsers.Remove(cu);
                        }
                    }
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "编辑成功";
                return RedirectToAction("Details", new { id=user.Id});
            }
            ViewBag.Clubs = ah.GetClubsByRole();
            TempData["Error"] = "编辑失败";
            return View(user);
        }

        /// <summary>
        /// 删除用户信息,并删除UserActivity、ClubUser中的记录，俱乐部人数-1
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id=0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return null;
            }
            foreach (var c in user.ClubUsers.ToList())
            {
                //俱乐部人数-1
                c.Club.Number = c.Club.Number - 1;
                db.ClubUsers.Remove(c);
            }
            foreach (var u in user.UserActivities.ToList())
            {
                db.UserActivities.Remove(u);
            }
            db.Users.Remove(user);
            var result = db.SaveChanges();
            if (result >= 1)
            {
                return PartialView("_UserTable", ah.GetUsersByRole());
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        [HttpPost]
        public int ResetPassword(int id=0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return 204;
            }
            var result=db.Entry(user).GetValidationResult();
            if (!result.IsValid)
            {
                return 202;
            }
            user.Password = UserHelper.GetMD5("000000");
            db.Entry(user).State = EntityState.Modified;
            var r = db.SaveChanges();
            return (r == 1) ? 200 : 201;
        }
        [HttpPost]
        public ActionResult GetUserPowers(int id=0)
        {
            User user = db.Users.Find(id);
            if (user!= null)
            {
                ViewBag.Clubs = ah.GetClubsByRole();
                return PartialView("_ClubTable", user);
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        public int SetUserPowers(string obj, int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return 204;
            }
            var result = db.Entry(user).GetValidationResult();
            if (!result.IsValid)
            {
                return 202;
            }
            //先设置用户的权限为“普通用户”，如果加入的俱乐部里有“管理员”就设为管理员权限
            user.RoleId = MasterData.RoleAU;
            //参加 数据库保存
            foreach (Club c in ah.GetClubsByRole())
            {
                //查询用户是否已加入此俱乐部
                var cu = db.ClubUsers.FirstOrDefault(u => u.UserId == user.Id && u.ClubId == c.Id);
                string num = GetJsonValue(obj, "join_" + c.Id.ToString());
                int j = (num == null) ? 0 : int.Parse(num);
                //参加俱乐部
                if (j == 1)
                {
                    if (cu == null)
                    {
                        cu = new ClubUser();
                    }
                    cu.ClubId = c.Id;
                    cu.UserId = user.Id;
                    cu.JoinTime = DateTime.Now;
                    string num2 = GetJsonValue(obj, "role_" + c.Id.ToString());
                    cu.RoleId = (num2 == null) ? 0 : int.Parse(num2);
                    if (cu.RoleId == MasterData.RoleCM)
                    {
                        user.RoleId = MasterData.RoleCM;
                    }
                    if (cu.Id == 0)
                    {
                        //俱乐部人数+1
                        c.Number = c.Number + 1;
                        db.ClubUsers.Add(cu);
                    }
                }
                else
                {
                    //取消参加俱乐部，俱乐部人数-1
                    if (cu != null)
                    {
                        c.Number = c.Number - 1;
                        db.ClubUsers.Remove(cu);
                    }
                }
            }
            int re = db.SaveChanges();
            return (re > 0) ? 200 : 201;
        }

        public ActionResult Input()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Input(InputFile inputFile)
        {
            HttpPostedFileBase file = Request.Files["File"];
            if (file == null || file.ContentLength <= 0)
            {
                TempData["Error"] = "文件不能为空";
                return View();
            }
            string fileName = System.IO.Path.GetFileName(file.FileName);
            int fileSize = file.ContentLength;//获取上传文件的大小单位为字节byte
            string fileEx = System.IO.Path.GetExtension(fileName);//获取上传文件的扩展名
            //string noFileName = System.IO.Path.GetFileNameWithoutExtension(fileName);//无扩展名的文件名
            int maxSize = 4000 * 1024;//定义上传文件的最大空间大小为4M
            string fileType = ".xlsx";//定义上传文件的类型字符串
            if (!fileType.Contains(fileEx))
            {
                TempData["Error"] = "文件类型不对，只能导入xlsx格式的文件";
                return View();
            }
            if (fileSize > maxSize)
            {
                TempData["Error"] = "上传文件超过4M，不能上传";
                return View();
            }
            string saveName = DateTime.Now.ToString("yyyyMMddhhmmss") + fileName;
            string savePath = Server.MapPath("~/Content/files/") + saveName;
            file.SaveAs(savePath);
            DataTable dt = ah.ReadExcel(savePath);
            Dictionary<int, DbEntityValidationResult> results = new Dictionary<int, DbEntityValidationResult>();
            bool isFailed = false;//是否失败
            int index = 1;
            //List<User> users = new List<User>();
            Dictionary<int, User> users = new Dictionary<int, User>();
            foreach (DataRow row in dt.Rows)
            {
                index++;
                User user=ah.CreateUser(row);
                if (user.Id==0)
                {
                    db.Users.Add(user);
                }
                else
                {
                    users.Add(index,user);
                }
                var result = db.Entry(user).GetValidationResult();
                if (!result.IsValid)
                {
                    //导入信息有错误
                    isFailed = true;
                    results.Add(index, result);
                }
            }
            //导入信息有错误
            if (isFailed)
            {
                ViewBag.Users = users;
                ViewBag.Errors = results;
                TempData["ErrorCount"]="共"+results.Count.ToString()+"条";
                TempData["Error"] = "导入失败";
                return View();
            }
            else
            {
                int count = db.SaveChanges();
                if (count >= 1)
                {
                    TempData["SuccessCount"] = "共" + count.ToString() + "条";
                    TempData["Success"] = "导入成功";
                }
                else
                {
                    TempData["Error"] = "导入失败";
                }
                ViewBag.Users = users;
                return View();
            }
        }
        /// <summary>
        /// 下载Excel模板
        /// </summary>
        /// <returns></returns>
        public FileResult Download()
        {
            string path = "~/Content/";
            string name="user.xlsx";
            return File(path+name, "text/plain", name);
        }

        /// <summary>
        /// 解析字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetJsonValue(string str, string key)
        {
            int i=str.IndexOf(key);
            int j = str.IndexOf('&', i);
            int s_i=i+key.Length+1;
            return str.Substring(s_i, j - s_i);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}