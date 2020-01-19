using System;
using System.IO;
using C.B.Common.helper;
using Microsoft.Extensions.Configuration;

namespace C.B.Common.Config {

    public class ConfigBuilder {
        static ConfigBuilder () {
            //默认使用运行目录中的 appsettings.json 文件作为配置文件。
            var builder = new ConfigurationBuilder ()
                .SetBasePath (Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "configurations"))
                .AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true);

            string environment = Environment.GetEnvironmentVariable ("ASPNETCORE_ENVIRONMENT");
            if (environment.IsNotEmpty ()) {
                builder = builder.AddJsonFile ($"appsettings.{environment}.json", optional : true);
            }
            Configuration = builder.Build ();
        }

        public static IConfigurationRoot Configuration { get; private set; }

    }

}

/**
 * 
 * SetBasePath extension method is defined in Config.FileExtensions.
 * 
 * need to add a dependency to Microsoft.Extensions.Configuration.FileExtensions package.
 * 
 * To resolve AddJsonFile add dependency to Microsoft.Extensions.Configuration.Json
 * 
 * 
 */