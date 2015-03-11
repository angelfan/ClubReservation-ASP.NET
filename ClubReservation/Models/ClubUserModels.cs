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
    //用户俱乐部表
    [Table("club_user")]
    public class ClubUser
    {
        //主键
        [Key]
        public int Id { get; set; }

        //外键 user_id
        [Column("user_id")]
        public int UserId { get; set; }

        //外键 club_id
        [Column("club_id")]
        public int ClubId { get; set; }

        //加入俱乐部时间
        [DisplayName("加入时间")]
        [DisplayFormat(DataFormatString="yyyy-MM-dd hh:mm:ss")]
        public DateTime JoinTime { get; set; }

        //外键 role_id
        [DisplayName("用户角色")]
        [Column("role_id")]
        public int RoleId { get; set; }

        //虚拟外键
        public virtual Club Club { get; set; }

        //虚拟外键
        public virtual Role Role { get; set; }

        ////虚拟外键
        //public virtual User User { get; set; }
    }
}