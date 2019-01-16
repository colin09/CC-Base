namespace C.B.Sqlite.Context {
    using C.B.Common.Config;
    using C.B.Sqlite.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    class SqliteContext : DbContext {

        private readonly IConfiguration _config;
        private readonly string _connStr;

        public SqliteContext () {
            _config = ConfigBuilder.Configuration;
            _connStr = _config.GetConnectionString ("SqliteConnectionString");
            //System.Console.WriteLine($"==> 1. {_connStr}");
        }

        public SqliteContext (IConfiguration config) {
            _config = config;
            _connStr = _config.GetConnectionString ("SqliteConnectionString");
            //System.Console.WriteLine($"==> 2. {_connStr}");
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite (_connStr);
        }

        public DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);

            //modelBuilder.Entity<StoreEntity>(entity => StoreEntity.buildAction(entity));
        }

    }
}