using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Services.Security
{
    public class HotelAuthenticationService
    {
        private const string _hotelCookieName = "kunlun.hotel";

        private readonly HttpContextBase _httpContext;

        private readonly HotelService _hotelService;

        private Hotel _hotel;

        public HotelAuthenticationService(HttpContextBase httpContext,
           HotelService hotelService)
        {
            _httpContext = httpContext;
            _hotelService = hotelService;
        }

        public virtual void SetHotelCookie(Hotel hotel, string path)
        {
            HttpCookie hotelCookie = new HttpCookie(_hotelCookieName, hotel.Code.ToString());
            hotelCookie.HttpOnly = true;
            hotelCookie.Secure = FormsAuthentication.RequireSSL;
            hotelCookie.Path = path;
            if (FormsAuthentication.CookieDomain != null)
            {
                hotelCookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(hotelCookie);
            _hotel = hotel;
        }

        public virtual HttpCookie GetHotelCookie()
        {
            if (_httpContext == null || _httpContext.Request == null)
            {
                return null;
            }
            return _httpContext.Request.Cookies[_hotelCookieName];
        }

        public virtual void ClearHotelCookie()
        {
            if (_httpContext != null && _httpContext.Response != null)
            {
                _httpContext.Response.Cookies[_hotelCookieName].Expires = DateTime.Now.AddDays(-1);
            }
        }

        public Hotel GetSpecifiedHotel()
        {
            HttpCookie cookie = GetHotelCookie();
            if (cookie == null)
            {
                return null;
            }
            return _hotelService.GetByCode(cookie.Value);
        }
    }
}
