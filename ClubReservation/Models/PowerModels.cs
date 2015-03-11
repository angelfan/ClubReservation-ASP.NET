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
    //权限
    [Table("power")]
    public class Power
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("权限名称")]
        public string Name { get; set; }
        [DisplayName("控制器")]
        public string Controller { get; set; }
        [DisplayName("操作")]
        public string Action { get; set; }
        [DisplayName("菜单等级")]
        public int Level { get; set; }
        [DisplayName("父菜单")]
        public int Parents { get; set; }
    }
}