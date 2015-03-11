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
    [Table("property")]
    public class Property
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("所属俱乐部")]
        [Column("club_id")]
        public int ClubId { get; set; }
        [DisplayName("资产名")]
        [Required(ErrorMessage="请输入资产名")]
        [StringLength(40,MinimumLength=2,ErrorMessage="请输入正确资产名")]
        public string Name { get; set; }
        [DisplayName("数量")]
        [Required(ErrorMessage = "请输入数量")]
        [Range(0,99999,ErrorMessage="请输入正确数量")]
        public int Count { get; set; }
        [DisplayName("单价")]
        [Required(ErrorMessage = "请输入单价")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "请输入正确单价")]
        public float Price { get; set; }
        [DisplayName("更新时间")]
        public DateTime UpdateDate { get; set; }
        public virtual Club club { get; set; }
    }
}