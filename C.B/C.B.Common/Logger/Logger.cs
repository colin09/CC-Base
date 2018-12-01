using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.B.Common.logger
{
    public class Logger
    {
        private static readonly log4net.ILog log;

        static Logger()
        {
            log = log4net.LogManager.GetLogger(typeof(Logger));
        }

        public static log4net.ILog Current()
        {
            return log;
        }

    }
}
