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
    //用户表
    [Table("user")]
    public class User
    {
        //主键ID
        [Key]
        public int Id { get; set; }

        //工号
        [DisplayName("工号")]
        [Required(ErrorMessage = "请输入工号")]
        [StringLength(8,MinimumLength=6,ErrorMessage="请输入正确工号")]
        public string EmployeeCode { get; set; }

        //密码
        [DisplayName("密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //邮箱
        [DisplayName("邮箱")]
        [Required(ErrorMessage = "请输入邮箱")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage="请输入正确邮箱格式")]
        public string Email { get; set; }

        //电话
        [DisplayName("内部电话")]
        [Required(ErrorMessage = "请输入内部电话")]
        [RegularExpression(@"^\d{4}$",ErrorMessage="请输入正确内部电话")]
        public string Tel { get; set; }

        //手机
        [DisplayName("手机")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "请输入正确手机号码")]
        [MaxLength(11)]
        public string Phone { get; set; }

        //姓名
        [DisplayName("姓名")]
        [Required(ErrorMessage = "请输入姓名")]
        [RegularExpression(@"^([\u4E00-\u9FA5]|[a-zA-Z]){2,10}$", ErrorMessage = "请输入正确姓名")]
        public string Name { get; set; }

        //性别
        [DisplayName("性别")]
        public int Sex { get; set; }

        //入职日期
        [DisplayName("入职日期")]
        [DataType(DataType.DateTime,ErrorMessage="请输入正确的日期格式")]
        [DisplayFormat(DataFormatString="yyyy-MM-dd")]
        public string EntryTime { get; set; }

        //角色
        [DisplayName("角色")]
        public int RoleId { get; set; }

        //组别
        [DisplayName("组别")]
        [StringLength(40,ErrorMessage="最大输入40个字符")]
        public string Team { get; set; }

        //外键 role
        public virtual Role Role { get; set; }

        //关系对应
        public virtual ICollection<ClubUser> ClubUsers { get; set; }

        //关系对应
        public virtual ICollection<UserActivity> UserActivities { get; set; }
    
    }

    public class Login
    {   
        [DisplayName("工号")]
        [Required(ErrorMessage = "请输入工号")]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "请输入正确工号")]
        public string EmployeeCode { get; set; }
        [DisplayName("密码")]
        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
    public class InputFile
    {
        //[RegularExpression(@"^.*\.(?:xls|xlsx)$", ErrorMessage = "请选择正确的excel文件")]
        [DisplayName("选择文件")]
        [Required(ErrorMessage = "请选择文件")]
        [RegularExpression(@"^.*\.(?:xlsx)$", ErrorMessage = "请选择正确的xlsx文件")]
        public string File{get;set;}
    }

}