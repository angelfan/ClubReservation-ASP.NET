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
    [Table("examine")]
    public class Examine
    {
        [Key]
        public int Id { get; set; }
        [Column("club_id")]
        public int ClubId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [DisplayName("申请时间")]
        public DateTime CreateDate { get; set; }
        public int State { get; set; }

        public virtual Club Club { get; set; }
        public virtual User User { get; set; }
    }
}