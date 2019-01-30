using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;

namespace C.B.Common.logger {
    public class Logger {
        private static readonly log4net.ILog log;

        static Logger () {
            //log = log4net.LogManager.GetLogger (typeof (Logger));

            // var repository = log4net.LogManager.CreateRepository ("NETCoreRepository");
            // XmlConfigurator.Configure (repository, new FileInfo ("configurations/log4net.config"));
            // log = log4net.LogManager.GetLogger (repository.Name, (typeof (Logger)));

            var repository = log4net.LogManager.CreateRepository ("NETCoreRepository");
            log4net.Config.XmlConfigurator.Configure (repository, new System.IO.FileInfo ($"{Directory.GetCurrentDirectory()}/configurations/log4net.config"));
            log = log4net.LogManager.GetLogger (repository.Name, (typeof (Logger)));
        }

        public static log4net.ILog Current () {
            return log;
        }

    }
}