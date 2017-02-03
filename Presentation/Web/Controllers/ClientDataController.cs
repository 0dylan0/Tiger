using Core;
using Core.Domain.Common;
using Core.Page;
using Services.Common;
using Services.Localization;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Extensions;
using Web.Framework.Controllers;
using Web.Framework.Json;
using Web.Framework.Page;
using Web.Models;

namespace Web.Controllers
{
    public class ClientDataController : BaseController
    {

        private readonly IWorkContext _webWorkContext;
        private readonly ClientDataService _clientDataService;
        private readonly LocalizationService _localizationService;
        private readonly CommonController _commonController;

        public ClientDataController(IWorkContext webWorkContext,
            ClientDataService clientDataService,
            LocalizationService localizationService,
            CommonController commonController)
        {
            _webWorkContext = webWorkContext;
            _clientDataService = clientDataService;
            _localizationService = localizationService;
            _commonController = commonController;
        }


        [HttpGet]
        public ActionResult Index()
        {
            ClientDataListModel model = new ClientDataListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, ClientDataListModel model)
        {
            IPagedList<ClientData> UserList = _clientDataService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.ClientData = UserList.MapTo<IList<ClientData>, IList<ClientDataModel>>();

            var results = new DataTable<ClientDataModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = UserList.TotalCount,
                RecordsFiltered = UserList.TotalCount,
                Data = model.ClientData
            };

            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            ClientDataModel model = new ClientDataModel();
            model.ReceiptDate = DateTime.Now;
            model.ClientTypeList = _commonController.GetClientTypeList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(ClientDataModel model)
        {
            if (ModelState.IsValid)
            {
                ClientData Goods = model.MapTo<ClientDataModel, ClientData>();
                _clientDataService.Insert(Goods);
                return RedirectToAction("Index");
            }
            model.ClientTypeList = _commonController.GetClientTypeList();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = _clientDataService.GetUserById(id);
            var res = user.MapTo<ClientData, ClientDataModel>();
            res.ClientTypeList = _commonController.GetClientTypeList();
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(ClientDataModel model)
        {
            if (ModelState.IsValid)
            {
                ClientData goodsData = model.MapTo<ClientDataModel, ClientData>();
                _clientDataService.Update(goodsData);
                SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") + model.ClientName}");
                return RedirectToAction("Index");

            }
            model.ClientTypeList = _commonController.GetClientTypeList();
            return View(model);
        }

    }
}