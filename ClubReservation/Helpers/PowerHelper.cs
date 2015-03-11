using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubReservation.Helpers
{
    public class PowerHelper
    {
        public static bool ValidatePower(ActionExecutingContext filterContext)
        {
            var user = new CurrentUser();
            var controllerName=filterContext.RouteData.Values["controller"].ToString();
            var actionName=filterContext.RouteData.Values["action"].ToString();
            var ac = user.ActionPermission.FirstOrDefault(a => a.Controller == controllerName && a.Action == actionName);
            if ( ac== null)
            {
                return false;
            }
            return true;
        }
    }
}