using Core;
using Core.Domain;
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
    public class InventoryDataController : BaseController
    {
        private readonly IWorkContext _webWorkContext;
        private readonly InventoryDataService _inventoryDataService;
        private readonly LocalizationService _localizationService;
        public InventoryDataController(IWorkContext webWorkContext,
            InventoryDataService inventoryDataService,
            LocalizationService localizationService)
        {
            _webWorkContext = webWorkContext;
            _inventoryDataService = inventoryDataService;
            _localizationService = localizationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            InventoryDataListModel model = new InventoryDataListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, InventoryDataListModel model)
        {
            IPagedList<InventoryData> SalesShipmentsDataList = _inventoryDataService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.InventoryData = SalesShipmentsDataList.MapTo<IList<InventoryData>, IList<InventoryDataModel>>();

            var results = new DataTable<InventoryDataModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = SalesShipmentsDataList.TotalCount,
                RecordsFiltered = SalesShipmentsDataList.TotalCount,
                Data = model.InventoryData
            };

            return Json(new PlainJsonResponse(results));
        }
    }
}