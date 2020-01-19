using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace C.B.Common.Config {
    public class AppSettingConfig {
        #region  .  base  

        private static IConfigurationRoot Configuration = ConfigBuilder.Configuration;
        public static T Get<T> (string key, T defaultValue = default (T)) where T : struct {
            return Configuration.GetValue (key, defaultValue);
        }
        public static string Get (string key, string defaultValue = "") {
            return Configuration.GetValue (key, defaultValue);
        }
        public static IConfigurationSection GetSection (string key) {
            var section = Configuration.GetSection (key);
            if (!section.Exists ()) return null;
            return section;
        }

        #endregion

        public static string TDES_Key {
            get { return ""; /* ConfigurationManager.AppSettings["TDES_Key"]; */ }
        }
        public static string TDES_IV {
            get { return ""; /* ConfigurationManager.AppSettings["TDES_IV"];*/ }
        }
        public static string MgConn => Get ("MgConn");
        public static string MgDBName => Get ("MgDBName");
        public static string MgPrefix => Get ("MgPrefix");

        public static string WebPort => Get ("WebPort");

        public static string VideoThumbTime => Get ("VideoThumbTime");

        /*
        public static string Get (string key) => Configuration[key];
        */

    }
}