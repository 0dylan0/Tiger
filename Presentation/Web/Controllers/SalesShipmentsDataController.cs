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
    public class SalesShipmentsDataController : BaseController
    {
        private readonly IWorkContext _webWorkContext;
        private readonly SalesShipmentsDataService _salesShipmentsDataService;
        private readonly LocalizationService _localizationService;
        public SalesShipmentsDataController(IWorkContext webWorkContext,
            SalesShipmentsDataService salesShipmentsDataService,
            LocalizationService localizationService)
        {
            _webWorkContext = webWorkContext;
            _salesShipmentsDataService = salesShipmentsDataService;
            _localizationService = localizationService;
        }

        // GET: SalesShipmentsData
        [HttpGet]
        public ActionResult Index()
        {
            SalesShipmentsDataListModel model = new SalesShipmentsDataListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, SalesShipmentsDataListModel model)
        {
            IPagedList<SalesShipmentsData> SalesShipmentsDataList = _salesShipmentsDataService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.SalesShipmentsData = SalesShipmentsDataList.MapTo<IList<SalesShipmentsData>, IList<SalesShipmentsDataModel>>();

            var results = new DataTable<SalesShipmentsDataModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = SalesShipmentsDataList.TotalCount,
                RecordsFiltered = SalesShipmentsDataList.TotalCount,
                Data = model.SalesShipmentsData
            };

            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            SalesShipmentsDataModel model = new SalesShipmentsDataModel();
            model.Date= DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(SalesShipmentsDataModel model)
        {
            if (ModelState.IsValid)
            {
                SalesShipmentsData SalesShipments = model.MapTo<SalesShipmentsDataModel, SalesShipmentsData>();
                _salesShipmentsDataService.Insert(SalesShipments);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}