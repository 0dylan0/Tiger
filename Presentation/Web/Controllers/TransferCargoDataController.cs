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
    public class TransferCargoDataController : BaseController
    {
        private readonly IWorkContext _webWorkContext;
        private readonly PurchaseDataService _purchaseDataService;
        private readonly LocalizationService _localizationService;
        private readonly GoodsDataService _goodsDataService;
        private readonly SupplierDataService _supplierDataService;
        private readonly WarehouseService _warehouseService;
        private readonly InventoryDataService _inventoryDataService;
        private readonly GoodsSpecificationService _goodsSpecificationService;
        private readonly ClientTypeService _clientTypeService;
        private readonly SalesShipmentsDataService _salesShipmentsDataService;
        private readonly GoodsTypeService _goodsTypeService;
        private readonly TransferCargoDataService _transferCargoDataService;


        public TransferCargoDataController(IWorkContext webWorkContext,
            PurchaseDataService purchaseDataService,
            LocalizationService localizationService,
            GoodsDataService goodsDataService,
            SupplierDataService supplierDataService,
            WarehouseService warehouseService,
            InventoryDataService inventoryDataService,
            GoodsSpecificationService goodsSpecificationService,
            ClientTypeService clientTypeService,
            SalesShipmentsDataService salesShipmentsDataService,
            GoodsTypeService goodsTypeService,
            TransferCargoDataService transferCargoDataService)
        {
            _webWorkContext = webWorkContext;
            _purchaseDataService = purchaseDataService;
            _localizationService = localizationService;
            _goodsDataService = goodsDataService;
            _supplierDataService = supplierDataService;
            _warehouseService = warehouseService;
            _inventoryDataService = inventoryDataService;
            _goodsSpecificationService = goodsSpecificationService;
            _clientTypeService = clientTypeService;
            _salesShipmentsDataService = salesShipmentsDataService;
            _goodsTypeService = goodsTypeService;
            _transferCargoDataService = transferCargoDataService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            TransferCargoDataListModel model = new TransferCargoDataListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, TransferCargoDataListModel model)
        {
            IPagedList<TransferCargoData> TransferCargoDataList = _transferCargoDataService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.TransferCargoDataList = TransferCargoDataList.MapTo<IList<TransferCargoData>, IList<TransferCargoDataModel>>();

            var results = new DataTable<TransferCargoDataModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = TransferCargoDataList.TotalCount,
                RecordsFiltered = TransferCargoDataList.TotalCount,
                Data = model.TransferCargoDataList
            };
            return Json(new PlainJsonResponse(results));
        }
    }
}