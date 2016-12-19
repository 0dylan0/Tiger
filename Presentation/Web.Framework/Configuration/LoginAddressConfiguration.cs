using System;
using System.Collections.Generic;
using System.Linq;
using static System.Configuration.ConfigurationManager;
using Core.Extensions;

namespace Web.Framework.Configuration
{
    public static class LoginAddressConfiguration
    {
        private const string KUNLUN_SETTING_KEY_START = "kunlun:";
        private const string LOGIN_SETTING_KEY_END = "LoginAddress";
        private const string CRO_KEY = "kunlun:CROLoginAddress";
        private const string CCM_KEY = "kunlun:CCMLoginAddress";
        private const string ECRS_KEY = "kunlun:ECRSLoginAddress";
        private const string SOC_KEY = "kunlun:SOCLoginAddress";


        public static IEnumerable<string> GetAllAddress()
        {
            return AppSettings.AllKeys
                        .Where(k => k.StartsWith(KUNLUN_SETTING_KEY_START) && k.EndsWith(LOGIN_SETTING_KEY_END))
                        .Select(k => AppSettings[k]);
        }

        public static string CROLoginAddress
        {
            get
            {
                return AppSettings[CRO_KEY];
            }
        }

        public static string CROHost
        {
            get
            {
                if (ECRSLoginAddress == null)
                {
                    return null;
                }

                return ConvertToSchemeAndAuthorityPath(CROLoginAddress);
            }
        }

        public static string CCMLoginAddress
        {
            get
            {
                return AppSettings[CCM_KEY];
            }
        }

        public static string CCMHost
        {
            get
            {
                if (ECRSLoginAddress == null)
                {
                    return null;
                }

                return ConvertToSchemeAndAuthorityPath(CCMLoginAddress);
            }
        }

        public static string ECRSLoginAddress
        {
            get
            {
                return AppSettings[ECRS_KEY];
            }
        }

        public static string ECRSHost
        {
            get
            {
                if (ECRSLoginAddress == null)
                {
                    return null;
                }

                return ConvertToSchemeAndAuthorityPath(ECRSLoginAddress);
            }
        }

        public static string SOCLoginAddress
        {
            get
            {
                return AppSettings[SOC_KEY];
            }
        }
        public static string RMSHost
        {
            get
            {
                if (SOCLoginAddress == null)
                {
                    return null;
                }

                return ConvertToSchemeAndAuthorityPath(SOCLoginAddress);
            }
        }

        public static string FindHost(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (String.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(url));
            }

            var host = GetAuthority(url);
            return GetAllAddress().Select(a => ConvertToSchemeAndAuthorityPath(a)).FirstOrDefault(h => h.EndsWith(host));
        }

        private static string ConvertToSchemeAndAuthorityPath(string url)
        {
            var uri = new Uri(url);
            return uri.GetSchemeAndAuthorityPath();
        }

        private static string GetAuthority(string url)
        {
            var uri = new Uri(url);
            return uri.Authority;
        }
    }
}
