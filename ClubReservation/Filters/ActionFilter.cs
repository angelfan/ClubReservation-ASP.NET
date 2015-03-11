using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClubReservation.Models;
using System.Web.Mvc;
using ClubReservation.Helpers;
namespace ClubReservation.Filters
{
    public class ExceptionFillter : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// 发生异常时执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (!filterContext.IsChildAction && (!filterContext.ExceptionHandled && filterContext.HttpContext.IsCustomErrorEnabled))
            {
                Exception innerException = filterContext.Exception;
                if ((new HttpException(null, innerException).GetHttpCode() == 500))
                {
                    string controllerName = (string)filterContext.RouteData.Values["controller"];
                    string actionName = (string)filterContext.RouteData.Values["action"];
                    HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                    ViewResult result = new ViewResult
                    {
                        ViewName = "Error",
                        MasterName = null,
                        ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                        TempData = filterContext.Controller.TempData
                    };
                    filterContext.Result = result;
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 500;
                    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                }
            }
        }
    }
    public class ActionFillter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //执行action后执行这个方法 比如做操作日志
            // 记录错误日志
            LogHelper.Write(filterContext);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string[] exceptPath = { "/", "user/login", "/user/login", "user/loginout", "/user/loginout" };
            //执行action前执行这个方法,比如做身份验证
            var path = filterContext.HttpContext.Request.Path.ToLower();
            if(exceptPath.Contains(path)){
                return;
            }
            if(!filterContext.HttpContext.User.Identity.IsAuthenticated){
                filterContext.Result = new RedirectResult("~/User/Login");
                return;
            }
            if (PowerHelper.ValidatePower(filterContext))
            {
                return;
            }
            else
            {
                throw new Exception("对不起，没有权限进行此操作");
            }
        }
    }
    public class ResultFillter : FilterAttribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //执行完action后跳转后执行
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //执行完action后跳转前执行
        }
    }
}