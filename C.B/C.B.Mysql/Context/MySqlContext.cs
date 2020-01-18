using C.B.Common.Config;
using C.B.MySql.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace C.B.MySql.Context
{
    class MySqlContext : DbContext
    {


        private readonly IConfiguration _config;
        private readonly string _connStr;

        private const string connString = "Server=localhost;Character Set=utf8;Database=hui;Uid=sa;Pwd=sa-1234;";

        public MySqlContext()
        {
            _config = ConfigBuilder.Configuration;
            _connStr = _config.GetConnectionString("MySqlConnectionString");
            //System.Console.WriteLine($"==> 1. {_connStr}");
        }

        public MySqlContext(IConfiguration config)
        {
            _config = config;
            _connStr = _config.GetConnectionString("MySqlConnectionString");            
            //System.Console.WriteLine($"==> 2. {_connStr}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var dbConnStr = ConfigManager.HuiConnStr;
            optionsBuilder.UseMySQL(_connStr);
        }

        public DbSet<AreaInfo> AreaInfo { get; set; }
        public DbSet<Notice> Notice { get; set; }
        public DbSet<NewsInfo> NewsInfo { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<HisEventInfo> HisEventInfo { get; set; }
        public DbSet<ResourceInfo> ResourceInfo { get; set; }
        public DbSet<ExpertInfo> ExpertInfo { get; set; }
        public DbSet<EventType> EventType { get; set; }
        public DbSet<EventInfo> EventInfo { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<Document> Document { get; set; }

        public DbSet<AuthNavs> AuthNavs { get; set; }
        public DbSet<AuthRole> AuthRole { get; set; }
        public DbSet<AuthUser> AuthUser { get; set; }
        public DbSet<AuthRoleNavs> AuthRoleNavs { get; set; }
        public DbSet<AuthUserNavs> AuthUserNavs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<StoreEntity>(entity => StoreEntity.buildAction(entity));
        }
    }
}
