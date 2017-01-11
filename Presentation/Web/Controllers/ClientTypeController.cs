using Core;
using Core.Domain.Common;
using Core.Page;
using Services.Common;
using Services.Localization;
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
    public class ClientTypeController : BaseController
    {
        private readonly IWorkContext _webWorkContext;
        private readonly ClientTypeService _clientTypeService;
        private readonly LocalizationService _localizationService;


        public ClientTypeController(IWorkContext webWorkContext,
            ClientTypeService clientTypeService,
            LocalizationService localizationService)
        {
            _webWorkContext = webWorkContext;
            _clientTypeService = clientTypeService;
            _localizationService = localizationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ClientTypeListModel model = new ClientTypeListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, ClientTypeListModel model)
        {
            IPagedList<ClientType> clientTypeList = _clientTypeService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.ClientType = clientTypeList.MapTo<IList<ClientType>, IList<ClientTypeModel>>();

            var results = new DataTable<ClientTypeModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = clientTypeList.TotalCount,
                RecordsFiltered = clientTypeList.TotalCount,
                Data = model.ClientType
            };
            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            ClientTypeModel model = new ClientTypeModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ClientTypeModel model)
        {
            if (ModelState.IsValid)
            {
                ClientType clientType = model.MapTo<ClientTypeModel, ClientType>();
                _clientTypeService.Insert(clientType);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var clientType = _clientTypeService.GetById(id);
            var res = clientType.MapTo<ClientType, ClientTypeModel>();
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(ClientTypeModel model)
        {
            if (ModelState.IsValid)
            {
                ClientType clientType = model.MapTo<ClientTypeModel, ClientType>();
                _clientTypeService.Update(clientType);
                SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") }");
                return RedirectToAction("Index");

            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var res = _clientTypeService.GetById(id);
            _clientTypeService.Delete(id);
            SuccessNotification($"{"删除成功" + res.Name}");
            return RedirectToAction("Index");
        }
    }
}