﻿using Core;
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
    public class PurchaseDataController : BaseController
    {

        private readonly IWorkContext _webWorkContext;
        private readonly PurchaseDataService _purchaseDataService;
        private readonly LocalizationService _localizationService;
        public PurchaseDataController(IWorkContext webWorkContext,
            PurchaseDataService purchaseDataService,
            LocalizationService localizationService)
        {
            _webWorkContext = webWorkContext;
            _purchaseDataService = purchaseDataService;
            _localizationService = localizationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            PurchaseDataListModel model = new PurchaseDataListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, PurchaseDataListModel model)
        {
            IPagedList<PurchaseData> UserList = _purchaseDataService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.PurchaseData = UserList.MapTo<IList<PurchaseData>, IList<PurchaseDataModel>>();

            var results = new DataTable<PurchaseDataModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = UserList.TotalCount,
                RecordsFiltered = UserList.TotalCount,
                Data = model.PurchaseData
            };

            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            PurchaseDataModel model = new PurchaseDataModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(PurchaseDataModel model)
        {
            if (ModelState.IsValid)
            {
                PurchaseData Purchase = model.MapTo<PurchaseDataModel, PurchaseData>();
                _purchaseDataService.Insert(Purchase);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var purchase = _purchaseDataService.GetById(id);
            var res = purchase.MapTo<PurchaseData, PurchaseDataModel>();
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(PurchaseDataModel model)
        {
            if (ModelState.IsValid)
            {
                PurchaseData goodsData = model.MapTo<PurchaseDataModel, PurchaseData>();
                _purchaseDataService.Update(goodsData);
                SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") + model.GoodsName}");
                return RedirectToAction("Index");

            }
            return View(model);
        }

    }
}