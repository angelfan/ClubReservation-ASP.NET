using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClubReservation.Helpers
{
    public class MasterData
    {
        //管理员角色
        /// <summary>
        /// 超级管理员
        /// </summary>
        public static int RoleSA = 1;
        /// <summary>
        /// 俱乐部管理员
        /// </summary>
        public static int RoleCM = 2;
        /// <summary>
        /// 普通用户
        /// </summary>
        public static int RoleAU = 3;
        /// <summary>
        /// 最大加入俱乐部数
        /// </summary>
        public static int MaxJoin = 2;

        //活动状态
        public static int ActivityEnable = 0;
        public static int ActivityDisable = 1;

        //用户加入俱乐部审核
        /// <summary>
        /// 未审核
        /// </summary>
        public static int ExamineUD = 0;
        /// <summary>
        /// 未通过
        /// </summary>
        public static int ExamineNP = 1;
        /// <summary>
        /// 审核通过
        /// </summary>
        public static int ExaminePA = 2;

        //俱乐部统计类型
        /// <summary>
        /// 每天
        /// </summary>
        public static string StatisticsDay = "day";
        /// <summary>
        /// 每星期
        /// </summary>
        public static string StatisticsWeek = "week";
        /// <summary>
        /// 每月
        /// </summary>
        public static string StatisticsYear = "year";

    }
}