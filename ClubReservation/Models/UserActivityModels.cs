using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubReservation.Models
{
    //用户活动表
    [Table("user_activity")]
    public class UserActivity
    {
        [Key]
        public int Id { get; set; }

        //user_id
        [Column("user_id")]
        public int UserId { get; set; }

        //activity_id
        [Column("activity_id")]
        public int ActivityId { get; set; }

        //报名时间
        [DisplayName("报名时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm:ss")]
        public DateTime EnrollTime { get; set; }

        //活动提醒状态
        [DisplayName("活动提醒状态")]
        public int State { get; set; }

        //虚拟外键 
        public virtual Activity Activity { get; set; }

        //虚拟外键
        public virtual User User { get; set; }
    }
}