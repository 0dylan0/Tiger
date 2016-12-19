using System;
using System.Collections.Generic;
using static System.Configuration.ConfigurationManager;

namespace Web.Framework.Configuration
{
    public class CookieNameConfiguration
    {
        public static string AuthenticationCookieName
        {
            get
            {
                return AppSettings["kunlun:AuthenticationCookieName"];
            }
        }

        public static string AntiForgeryCookieName
        {
            get
            {
                return AppSettings["kunlun:AntiForgeryCookieName"];
            }
        }
    }
}
