using System;
using static System.Configuration.ConfigurationManager;


namespace Web.Framework.Configuration
{
    public class IdentityServerConfiguration
    {
        public static string IdentityServerUrl
        {
            get
            {
                return AppSettings["kunlun:IdentityServerUrl"];
            }
        }
    }
}
