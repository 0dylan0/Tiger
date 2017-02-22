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
    public class SalesShipmentsStatisticsController : BaseController
    {
        private readonly IWorkContext _webWorkContext;
        private readonly SalesShipmentsDataService _salesShipmentsDataService;
        private readonly LocalizationService _localizationService;
        private readonly SupplierDataService _supplierDataService;
        private readonly WarehouseService _warehouseService;
        private readonly InventoryDataService _inventoryDataService;
        private readonly GoodsSpecificationService _goodsSpecificationService;
        private readonly ClientTypeService _clientTypeService;
        private readonly ClientDataService _clientDataService;
        private readonly GoodsTypeService _goodsTypeService;
        private readonly ArrearsDataService _arrearsDataService;
        private readonly ArrearsDetailsService _arrearsDetailsService;
        private readonly SalesShipmentsStatisticsService _salesShipmentsStatisticsService;
        private readonly GoodsDataService _goodsDataService;

        public SalesShipmentsStatisticsController(IWorkContext webWorkContext,
            SalesShipmentsDataService salesShipmentsDataService,
            LocalizationService localizationService,
            SupplierDataService supplierDataService,
            WarehouseService warehouseService,
            InventoryDataService inventoryDataService,
            GoodsSpecificationService goodsSpecificationService,
            ClientTypeService clientTypeService,
            ClientDataService clientDataService,
            GoodsTypeService goodsTypeService,
            ArrearsDataService arrearsDataService,
            ArrearsDetailsService arrearsDetailsService,
            SalesShipmentsStatisticsService salesShipmentsStatisticsService,
            GoodsDataService goodsDataService)
        {
            _webWorkContext = webWorkContext;
            _salesShipmentsDataService = salesShipmentsDataService;
            _localizationService = localizationService;
            _supplierDataService = supplierDataService;
            _warehouseService = warehouseService;
            _inventoryDataService = inventoryDataService;
            _goodsSpecificationService = goodsSpecificationService;
            _clientTypeService = clientTypeService;
            _clientDataService = clientDataService;
            _goodsTypeService = goodsTypeService;
            _arrearsDataService = arrearsDataService;
            _arrearsDetailsService = arrearsDetailsService;
            _salesShipmentsStatisticsService = salesShipmentsStatisticsService;
            _goodsDataService = goodsDataService;
        }

        // GET: SalesShipmentsStatistics
        [HttpGet]
        public ActionResult Index()
        {
            SalesShipmentsStatisticsListModel model = new SalesShipmentsStatisticsListModel();
            model.GoodsList = GetGoodsList();
            model.ClientDataList = GetClientDataList();
            model.FromDate = DateTime.Now;
            model.ToDate = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, SalesShipmentsStatisticsListModel model)
        {
            IPagedList<SalesShipmentsStatisticsModelShow> salesShipmentsStatistics = _salesShipmentsStatisticsService.GetList(
                model.GoodsID,
                model.ClientDataID,
                model.FromDate,
                model.ToDate, 
                pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.SalesShipmentsStatistics = salesShipmentsStatistics.MapTo<IList<SalesShipmentsStatisticsModelShow>, IList<SalesShipmentsStatisticsModel>>();

            var results = new DataTable<SalesShipmentsStatisticsModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = salesShipmentsStatistics.TotalCount,
                RecordsFiltered = salesShipmentsStatistics.TotalCount,
                Data = model.SalesShipmentsStatistics
            };

            return Json(new PlainJsonResponse(results));
        }

        public SalesShipmentsStatisticsModel Calculation(IList<SalesShipmentsDataModel> model)
        {

            SalesShipmentsStatisticsModel SalesShipmentsStatistics = new SalesShipmentsStatisticsModel()
            {

            };

            return new SalesShipmentsStatisticsModel();
        }

        public List<SelectListItem> GetClientDataList()
        {
            return _clientDataService.GetClientDataList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Code,
            }).ToList();
        }

        public List<SelectListItem> GetGoodsList()
        {
            return _goodsDataService.GetGoodsList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Code,
            }).ToList();

        }
    }
}