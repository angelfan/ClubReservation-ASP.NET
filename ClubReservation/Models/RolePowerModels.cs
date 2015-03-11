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
    //角色权限表
    [Table("role_power")]
    public class RolePower
    {
        [Key]
        public int Id { get; set; }
        [Column("power_id")]
        public int PowerId { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
        [DisplayName("创建日期")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm:ss")]
        public DateTime CreateDate { get; set; }
        public virtual Role Role { get; set; }
        public virtual Power Power { get; set; }
    }
}