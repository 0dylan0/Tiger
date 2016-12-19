using Core;
using Core.Domain.Common;
using Core.Page;
using Services.Localization;
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
    public class UserController :BaseController
    {
        private readonly IWorkContext _webWorkContext;
        private readonly UserService _userService;
        private readonly LocalizationService _localizationService;
        public UserController(IWorkContext webWorkContext,
            UserService userService,
            LocalizationService localizationService)
        {
            _webWorkContext = webWorkContext;
            _userService = userService;
            _localizationService = localizationService;
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
                SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") + model.Name}");
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
                        if (!String.IsNullOrEmpty(loginModel.ReturnUrl) && Url.IsLocalUrl(loginModel.ReturnUrl))
                        {
                            return Redirect("~/Hotel/Index?returnUrl=" + HttpUtility.UrlEncode(loginModel.ReturnUrl));
                        }
                        return Redirect("~/Hotel");                
            }
            return View(loginModel);
        }

    }
}