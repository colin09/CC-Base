using C.B.Common.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace C.B.Test.MySql
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
            System.Console.WriteLine($"==> 1. {_connStr}");
        }

        public MySqlContext(IConfiguration config)
        {
            _config = config;
            _connStr = _config.GetConnectionString("MySqlConnectionString");
            
            System.Console.WriteLine($"==> 2. {_connStr}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var dbConnStr = ConfigManager.HuiConnStr;
            optionsBuilder.UseMySQL(_connStr);
        }

        public DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<StoreEntity>(entity => StoreEntity.buildAction(entity));
        }
    }
}
