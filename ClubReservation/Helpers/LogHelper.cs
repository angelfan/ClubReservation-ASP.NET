using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace ClubReservation.Helpers
{
    public class LogHelper
    {
        public static void Write(ActionExecutedContext filterContext)
        {
            var dt = DateTime.Now;
            var logPath = HttpContext.Current.Server.MapPath("/log/" + dt.ToString("yyyy-MM"));
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            var logFilePath = string.Format("{0}/{1}.txt", logPath, dt.ToString("yyyy-MM-dd"));
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(logFilePath, true, Encoding.UTF8);
                writer.WriteLine("------------------------------------------------------------------------------");
                writer.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                writer.WriteLine("Ip：" + filterContext.HttpContext.Request.UserHostAddress);
                writer.WriteLine("Url：" + filterContext.HttpContext.Request.RawUrl);
                writer.WriteLine("用户名：" + new CurrentUser().User.EmployeeCode);
                if (filterContext.Exception != null)
                {
                    writer.WriteLine("错误信息：" + filterContext.Exception.Message);
                    writer.WriteLine("错误源：" + filterContext.Exception.Source);
                    writer.WriteLine("堆栈信息：" + filterContext.Exception.StackTrace);
                }
                writer.WriteLine("------------------------------------------------------------------------------");
            }
            catch
            {
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}