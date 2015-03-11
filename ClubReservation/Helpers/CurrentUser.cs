using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClubReservation.Models;

namespace ClubReservation.Helpers
{
    public class CurrentUser
    {
        private ClubReservationContext db = new ClubReservationContext();
        public CurrentUser(){
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new Exception("用户未登陆");
            }
            if (HttpContext.Current.Session["CurrentUser"] == null)
            {
                var ecode=HttpContext.Current.User.Identity.Name;
                this.User=db.Users.FirstOrDefault(u=>u.EmployeeCode==ecode);
                var a = (from p in db.Powers
                         from rp in db.RolePowers
                         where p.Id == rp.PowerId && rp.RoleId == this.User.RoleId
                         select p).Distinct();
                this.ActionPermission = (from p in db.Powers
                                         from rp in db.RolePowers
                                         where p.Id == rp.PowerId && rp.RoleId == this.User.RoleId
                                         select p).Distinct().ToList();
                this.MenuPermission = this.ActionPermission.Where(p => p.Parents == 0).ToList();
                HttpContext.Current.Session["CurrentUser"] = this;
            }
            else
            {
                var u = (CurrentUser)HttpContext.Current.Session["CurrentUser"];
                this.User = u.User;
                this.MenuPermission = u.MenuPermission;
                this.ActionPermission = u.ActionPermission;
            }
        }
        public User User { get; set; }
        public List<Power> MenuPermission{get;set;}
        public List<Power>  ActionPermission{get;set;}
    }

}