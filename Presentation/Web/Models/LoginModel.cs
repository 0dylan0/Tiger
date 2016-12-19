using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Framework;

namespace Web.Models
{
    public class LoginModel
    {
        [ResourceDisplayName("UserName")]
        public string UserName { get; set; }

        [ResourceDisplayName("Password")]
        public string Password { get; set; }

        [ResourceDisplayName("AutoLogin")]
        public bool RememberMe { get; set; }

        public bool IsFromOtherSystem { get; set; }

        public string LanguageCode { get; set; }

        public string HotelCode { get; set; }

        public string ReturnUrl { get; set; }
    }
}