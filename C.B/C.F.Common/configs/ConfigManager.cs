using System;

namespace C.F.Common.configs
{
    public class ConfigManager
    {




        #region



        private static string _wordHtmlRootPath;
        public static string WordHtmlRootPath
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_wordHtmlRootPath)) return _wordHtmlRootPath;
                else
                {
                    _wordHtmlRootPath = System.Configuration.ConfigurationManager.AppSettings["WordHtmlRootPath"];
                    return _wordHtmlRootPath;
                }
            }
        }


        private static int _imageMaxWidth;
        public static int ImageMaxWidth
        {
            get
            {
                if (_imageMaxWidth > 0) return _imageMaxWidth;
                else
                {
                    var value = System.Configuration.ConfigurationManager.AppSettings["ImageMaxWidth"];
                    int val = 0;
                    if (Int32.TryParse(value, out val))
                        _imageMaxWidth = val;
                    else _imageMaxWidth = 600;
                    return _imageMaxWidth;
                }
            }
        }

        private static int _imageMaxSize;
        public static int ImageMaxSize
        {
            get
            {
                if (_imageMaxSize > 0) return _imageMaxSize;
                else
                {
                    var value = System.Configuration.ConfigurationManager.AppSettings["ImageMaxSize"];
                    int val = 0;
                    if (Int32.TryParse(value, out val))
                        _imageMaxSize = val;
                    else _imageMaxSize = 600;
                    return _imageMaxSize;
                }
            }
        }



        private static int _imageDefaultQuality;
        public static int ImageDefaultQuality
        {
            get
            {
                if (_imageDefaultQuality > 0) return _imageDefaultQuality;
                else
                {
                    var value = System.Configuration.ConfigurationManager.AppSettings["ImageDefaultQuality"];
                    int val = 0;
                    if (Int32.TryParse(value, out val))
                        _imageDefaultQuality = val;
                    else _imageDefaultQuality = 600;
                    return _imageDefaultQuality;
                }
            }
        }



        #endregion 



    }
}
