using Core;
using Core.Domain.Common;
using Core.Page;
using Services.Localization;
using Services.Security;
using Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Extensions;
using Web.Framework.Controllers;
using Web.Framework.Json;
using Web.Framework.Page;
using Web.Models;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IWorkContext _webWorkContext;
        private readonly UserService _userService;
        private readonly LocalizationService _localizationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserValidateService _userValidateService;
        private readonly HttpContextBase _httpContext;
        private const string _hotelCookieName = "kunlun.hotel";

        public UserController(IWorkContext webWorkContext,
            UserService userService,
            LocalizationService localizationService,
            IAuthenticationService authenticationService,
            UserValidateService userValidateService,
            HttpContextBase httpContext)
        {
            _webWorkContext = webWorkContext;
            _userService = userService;
            _localizationService = localizationService;
            _authenticationService = authenticationService;
            _userValidateService = userValidateService;
            _httpContext = httpContext;
        }
        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            UserListModel model = new UserListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, UserListModel model)
        {
            IPagedList<Users> UserList = _userService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.UserData = UserList.MapTo<IList<Users>, IList<UserModel>>();

            var results = new DataTable<UserModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = UserList.TotalCount,
                RecordsFiltered = UserList.TotalCount,
                Data = model.UserData
            };

            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            UserModel model = new UserModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(UserModel model)
        {


            if (ModelState.IsValid)
            {
                Users User = model.MapTo<UserModel, Users>();
                _userService.Insert(User);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = _userService.GetUserById(id);
            var res = user.MapTo<Users, UserModel>();
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                Users opponent = model.MapTo<UserModel, Users>();
                _userService.Update(opponent);
                //SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") + model.Name}");
                return RedirectToAction("Index");

            }
            return View(model);
        }


        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            LoginModel model = new LoginModel();
            model.ReturnUrl = returnUrl;
            if (_webWorkContext.IsAlreadyLogin())
            {
                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("~/Home");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {


            if (ModelState.IsValid)
            {
                UserLoginResult result = _userValidateService.Validate(loginModel.UserName, loginModel.Password, loginModel.IsFromOtherSystem);
                switch (result)
                {
                    case UserLoginResult.Successful:
                        Core.Domain.Common.Users user = _userService.GetByCode(loginModel.UserName);
                        _authenticationService.SignIn(user, loginModel.RememberMe);

                        if (loginModel.IsFromOtherSystem)
                        {
                            if (!String.IsNullOrEmpty(loginModel.ReturnUrl) && Url.IsLocalUrl(loginModel.ReturnUrl))
                            {
                                return Redirect("~/Home/Index?returnUrl=" + HttpUtility.UrlEncode(loginModel.ReturnUrl));
                            }
                        }                       
                        return Redirect("~/Home/Index");
                    case UserLoginResult.UserNotExist:
                        ModelState.AddModelError("UserName_NotExist","登录失败");
                        break;
                    case UserLoginResult.WrongPassword:
                        ModelState.AddModelError("Password_Wrong", "密码错误");
                        break;
                }
                //Core.Domain.Common.Users user = _userService.GetByCode(loginModel.UserName);
                //_authenticationService.SignIn(user, loginModel.RememberMe);

                //if (!String.IsNullOrEmpty(loginModel.ReturnUrl) && Url.IsLocalUrl(loginModel.ReturnUrl))
                //{
                //    return Redirect("~/Hotel/Index?returnUrl=" + HttpUtility.UrlEncode(loginModel.ReturnUrl));
                //}
                //return Redirect("~/Hotel");
            }
            return View(loginModel);
        }

        public ActionResult Logout()
        {
            _authenticationService.SignOut();
            //ClearHotelCookie();
            return Redirect("~/Login");
        }

        public virtual void ClearHotelCookie()
        {
            if (_httpContext != null && _httpContext.Response != null)
            {
                _httpContext.Response.Cookies[_hotelCookieName].Expires = DateTime.Now.AddDays(-1);
            }
        }

    }
}