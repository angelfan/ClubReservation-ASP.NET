using System.Web;
using System.Web.Mvc;
using ClubReservation.Filters;
namespace ClubReservation
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //异常处理
            filters.Add(new ExceptionFillter());
            //TODP:Action前后操作
            filters.Add(new ActionFillter());
        }
    }
}