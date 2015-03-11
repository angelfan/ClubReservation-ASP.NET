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
    //活动
    [Table("activity")]
    public class Activity
    {
        //主键
        [Key]
        public int Id { get; set; }

        //外键 club
        [DisplayName("俱乐部")]
        [Column("club_id")]
        public int ClubId { get; set; }

        //外键 user
        [DisplayName("用户")]
        [Column("user_id")]
        public int UserId { get; set; }

        //活动名称
        [DisplayName("活动名称")]
        [Required(ErrorMessage = "请输入活动名称")]
        public string ActivityName { get; set; }

        //活动地点
        [DisplayName("活动地点")]
        [Required(ErrorMessage = "请输入活动地点")]
        public string ActivityPlace { get; set; }

        //活动介绍
        [DisplayName("活动介绍")]
        [Required(ErrorMessage = "请输入活动介绍")]
        public string ActivityInfo { get; set; }

        //最低人数
        [DisplayName("人数下限")]
        [Required(ErrorMessage = "请输入人数下限")]
        public int MinNumber { get; set; }

        //最高人数
        [DisplayName("人数上限")]
        [Required(ErrorMessage = "请输入人数上限")]
        public int MaxNumber { get; set; }

        //活动是否结束的状态
        [DisplayName("活动状态")]
        public int State { get; set; }

        //开始时间
        [DisplayName("活动开始时间")]
        [Required(ErrorMessage = "请输入活动开始时间")]
        public DateTime StartTime { get; set; }

        //结束时间
        [DisplayName("活动结束时间")]
        [Required(ErrorMessage = "请输入活动结束时间")]
        public DateTime EndTime { get; set; }

        //信息修改者
        [DisplayName("活动信息修改者")]
        [Column("updateUser_id")]
        public int UpdateUserId { get; set; }

        //报名人数
        [DisplayName("已报名人数")]
        public int EnrollNo { get; set; }

        //报名截止时间
        [DisplayName("报名截止时间")]
        public DateTime EnrollEndTime { get; set; }

        //活动更新时间
        [DisplayName("活动更新时间")]
        public DateTime UpdateDate { get; set; }

        //活动创建时间
        [DisplayName("创建时间")]
        public DateTime CreateDate { get; set; }

        //一个活动只属于一个俱乐部
        public virtual Club Club { get; set; }


        //活动创建者 虚拟外键 对应 UserId
        public virtual User User { get; set; }

        //活动更新者 虚拟外键 对应 UpdateUserId
        public virtual User UpdateUser { get; set; }

        public virtual ICollection<UserActivity> UserActivities { get; set; }
    }

}