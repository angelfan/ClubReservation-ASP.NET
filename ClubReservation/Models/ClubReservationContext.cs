using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClubReservation.Models
{
    public class ClubReservationContext : DbContext
    {
        // 您可以向此文件中添加自定义代码。更改不会被覆盖。
        // 
        // 如果您希望只要更改模型架构，Entity Framework
        // 就会自动删除并重新生成数据库，则将以下
        // 代码添加到 Global.asax 文件中的 Application_Start 方法。
        // 注意: 这将在每次更改模型时销毁并重新创建数据库。
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<ClubReservation.Models.ClubReservationContext>());

        public ClubReservationContext()
            : base("name=MySqlString")
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ClubUser> ClubUsers { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<Power> Powers { get; set; }
        public DbSet<RolePower> RolePowers { get; set; }
        public DbSet<Property> Properties { get; set; }
        //用户加入俱乐部审核表
        public DbSet<Examine> Examines { get; set; }

    }
}
