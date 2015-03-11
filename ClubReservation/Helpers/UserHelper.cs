using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace ClubReservation.Helpers
{
    public class UserHelper
    {
        public static string GetMD5(string strSource)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "MD5");
        }
    }
}