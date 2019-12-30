dotnet EF Migrations


1. 添加引用  MySql.Data.EntityFrameworkCore，
2. 完成 Context 实例，有无参的构造函数，
3. 为 csproj 文件指定运行时库，即添加 <RuntimeFrameworkVersion>2.1.6</RuntimeFrameworkVersion>，实际版本使用本地已安装的版本号，
4. 添加引用 Microsoft.EntityFrameworkCore.Design，有了这个引用才能使用 Migrations cli，
5. Context 所在项目根目录下添加文件 configurations/appsettings.json 配置文件，
6. 配置文件添加 sql connString , 配置项参数名称同Context类无参构造函数使用的名称，格式如下：参数命名=MySqlConnectionString
    {
        "ConnectionStrings": {
            "MySqlConnectionString":"Server=localhost;Character Set=utf8;Database=hui;Uid=sa;Pwd=sa-1234;"
        }
    }
7. 如提示找不到表 `__EFMigrationsHistory`，自行创建即可，如下：
    CREATE TABLE `__EFMigrationsHistory` 
    ( 
        `MigrationId` nvarchar(150) NOT NULL, 
        `ProductVersion` nvarchar(32) NOT NULL, 
        PRIMARY KEY (`MigrationId`) 
    );

8. 附：执行cmd
    dotnet ef Migrations add <migrationName>
    dotnet ef database update

cli 帮助相当完善哦。