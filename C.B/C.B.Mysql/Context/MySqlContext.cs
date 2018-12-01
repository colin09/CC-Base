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

        public MySqlContext()
        {
            _config = ConfigBuilder.Configuration;
            _connStr = _config.GetConnectionString("MySqlConnectionString");
        }

        public MySqlContext(IConfiguration config)
        {
            _config = config;
            _connStr = _config.GetConnectionString("MySqlConnectionString");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var dbConnStr = ConfigManager.HuiConnStr;
            optionsBuilder.UseMySQL(_connStr);
        }

        public DbSet<SysUser> Section { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<StoreEntity>(entity => StoreEntity.buildAction(entity));
        }
    }
}
