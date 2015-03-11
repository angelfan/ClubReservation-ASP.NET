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
    //俱乐部表
    [Table("club")]
    public class Club
    {
        //主键
        [Key]
        public int Id { get; set; }

        //俱乐部名称
        [DisplayName("俱乐部名称")]
        [Required(ErrorMessage = "请输入俱乐部名称")]
        [StringLength(20, ErrorMessage = "最多输入20个字符")]
        public string ClubName { get; set; }

        //俱乐部介绍
        [DisplayName("俱乐部介绍")]
        [Required(ErrorMessage = "请输入俱乐部介绍")]
        [StringLength(200, ErrorMessage = "最多输入200个字符")]
        public string ClubInfo { get; set; }

        //俱乐部邮箱
        [DisplayName("邮件列表")]
        [Required(ErrorMessage = "请输入邮件列表")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "请输入正确邮箱格式")]
        public String Email { get; set; }

        //俱乐部人数
        [DisplayName("人数")]
        public int Number { get; set; }

        //俱乐部更新者
        [DisplayName("更新者")]
        [Column("user_id")]
        public int UserId { get; set; }

        //俱乐部创建时间
        [DisplayName("创建时间")]
        public DateTime CreateDate { get; set; }

        //俱乐部更新时间
        [DisplayName("更新时间")]
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 俱乐部活动列表
        /// </summary>
        public virtual ICollection<Activity> Activities { get; set; }
        /// <summary>
        /// 俱乐部用户列表
        /// </summary>
        public virtual ICollection<ClubUser> ClubUsers { get; set; }
        /// <summary>
        /// 俱乐部资产列表
        /// </summary>
        public virtual ICollection<Property> Properties { get; set; }
        /// <summary>
        /// 俱乐部申请信息列表
        /// </summary>
        public virtual ICollection<Examine> Examines { get; set; }
    }
}