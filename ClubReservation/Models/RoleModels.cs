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
    [Table("role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("角色名")]
        public string RoleName { get; set; }
        public virtual ICollection<RolePower> RolePowers { get; set; }
    }
}