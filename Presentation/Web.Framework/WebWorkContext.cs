using Core;
using Core.Domain;
using Core.Domain.Common;
using Core.Domain.Localization;
using Core.Enum;
using Core.Infrastructure;
using Services.Localization;
using Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.ApplicationServices;

namespace Web.Framework
{
    public class WebWorkContext : IWorkContext
    {
        private const string _languageCookieName = "kunlun.language";

        private const string _userCookieName = "kunlun.user";

        private Language _cachedLanguage;

        private readonly IAuthenticationService _authenticationService;

        private readonly LanguageService _languageService;

        private readonly HotelAuthenticationService _hotelAuthenticationService;

        private readonly HttpContextBase _httpContext;

        private Core.Domain.Common.Users _cachedUser;

        private Guid _cachedRequestId;

        private string _cachedUserCode;

        private Hotel _cachedHotel;

        public WebWorkContext(HttpContextBase httpContext,
            IAuthenticationService authenticationService,
            LanguageService languageService,
            HotelAuthenticationService hotelAuthenticationService)
        {
            _httpContext = httpContext;
            _authenticationService = authenticationService;
            _languageService = languageService;
            _hotelAuthenticationService = hotelAuthenticationService;
        }

        protected HttpCookie GetUserCookie()
        {
            if (_httpContext == null || _httpContext.Request == null)
            {
                return null;
            }
            return _httpContext.Request.Cookies[_userCookieName];
        }

        protected void SetUserCookie(Guid userGuid)
        {
            if (_httpContext != null && _httpContext.Response != null)
            {
                var cookie = new HttpCookie(_userCookieName);
                cookie.HttpOnly = true;
                cookie.Value = userGuid.ToString();
                if (userGuid == Guid.Empty)
                {
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                }
                else
                {
                    const int cookieExpires = 30;
                    cookie.Expires = DateTime.Now.AddMinutes(cookieExpires);
                }

                _httpContext.Response.Cookies.Remove(_userCookieName);
                _httpContext.Response.Cookies.Add(cookie);
            }
        }

        public Core.Domain.Common.Users CurrentUser
        {
            get
            {
                if (_cachedUser != null)
                {
                    return _cachedUser;
                }
                _cachedUser = _authenticationService.GetAuthenticatedUser();
                if (_cachedUser == null)
                {
                    _cachedUser = new Core.Domain.Common.Users();
                }
                return _cachedUser;
            }
            set
            {
                _cachedUser = value;
            }
        }

        public Language CurrentLanguage
        {
            get
            {
                if (_cachedLanguage != null)
                {
                    return _cachedLanguage;
                }

                var allLanguages = _languageService.GetAllLanguages();

                string languageId = String.Empty;
                Language language = null;
                if (_httpContext.Request.Cookies[_languageCookieName] != null)
                {
                    languageId = _httpContext.Request.Cookies[_languageCookieName].Value;
                }

                if (!String.IsNullOrEmpty(languageId))
                {
                    language = allLanguages.FirstOrDefault(x => x.Code.Trim() == languageId);
                }

                if (language == null)
                {
                    language = allLanguages.FirstOrDefault();
                }

                _cachedLanguage = language;
                return _cachedLanguage;
            }
            set
            {
                var languageId = value?.Code;
                HttpCookie languageCookie = new HttpCookie(_languageCookieName);
                //languageCookie.Path = String.Format("/{0}/", _httpContext.Request["submit"]);
                languageCookie.HttpOnly = true;
                languageCookie.Value = languageId.ToString();
                _httpContext.Response.Cookies.Set(languageCookie);
            }
        }


        public bool IsAlreadyLogin()
        {
            bool isAlreadyLogin = false;

            //if (CurrentUser.UserHotelRanges != null && CurrentUser.UserHotelRanges.Count != 0)
            //{
            //    isAlreadyLogin = true;
            //}

            return isAlreadyLogin;
        }
        public virtual Hotel CurrentHotel
        {
            get
            {
                if (_cachedHotel != null)
                {
                    return _cachedHotel;
                }
                _cachedHotel = _hotelAuthenticationService.GetSpecifiedHotel();
                if (_cachedHotel == null)
                {
                    _cachedHotel = new Hotel();
                }
                return _cachedHotel;
            }
            set
            {
                _cachedHotel = value;
            }
        }

        public Guid CurrentRequestId => _cachedRequestId != default(Guid) ? _cachedRequestId : (_cachedRequestId = Guid.NewGuid());

        /// <summary>
        /// 当前用户 Code。对于  站点来说，会获取到当前登陆的用户 Code（若未登陆则为 null）；Web API 会返回 “ Web Api”；而 POS API 会返回 “LPS Pos Api”。
        /// </summary>
        public string CurrentUserCode
        {
            get
            {
                if (String.IsNullOrEmpty(_cachedUserCode))
                {
                    var currentEntryPoint = (EntryPoint)EngineContext.Current.Resolve(typeof(EntryPoint));
                    return _cachedUserCode = GetEntryPointUserCode(currentEntryPoint);
                }

                return _cachedUserCode;
            }
        }

        #region Private

        private string GetEntryPointUserCode(EntryPoint currentEntryPoint)
        {
            switch (currentEntryPoint)
            {
                case EntryPoint.Website:
                    return CurrentUser?.Code;

                case EntryPoint.WebApi:
                    var authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
                    var client = authenticationService.GetClient();
                    return $"[API] {client.ClientId}";

                case EntryPoint.PosApi:
                    return "Pos Api";

                case EntryPoint.WindowsService:
                    return "Windows Service";

                default:
                    return "Unknown entry point";
            }
        }

        #endregion
    }
}
