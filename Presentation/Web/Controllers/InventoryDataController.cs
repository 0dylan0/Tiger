using Core;
using Core.Domain;
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
    public class InventoryDataController : BaseController
    {
        private readonly IWorkContext _webWorkContext;
        private readonly InventoryDataService _inventoryDataService;
        private readonly LocalizationService _localizationService;
        private readonly CommonController _commonController;
        private readonly WarehouseService _warehouseService;
        private readonly PurchaseDataService _purchaseDataService;

        public InventoryDataController(IWorkContext webWorkContext,
            InventoryDataService inventoryDataService,
            LocalizationService localizationService,
            CommonController commonController,
            WarehouseService warehouseService,
            PurchaseDataService purchaseDataService)
        {
            _webWorkContext = webWorkContext;
            _inventoryDataService = inventoryDataService;
            _localizationService = localizationService;
            _commonController = commonController;
            _warehouseService = warehouseService;
            _purchaseDataService = purchaseDataService;
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

        public ActionResult InventoryDataDetails(int id)
        {
            var inventoryData = _inventoryDataService.GetById(id);
            var model = inventoryData.MapTo<InventoryData, InventoryDataModel>();

            return Json(new JsonResponse<string>(RenderPartialViewToString("InventoryDataPartial", model)));
        }

        public ActionResult TransferCargo(int id)
        {

            var inventoryData = _inventoryDataService.GetById(id);
            var model = inventoryData.MapTo<InventoryData, InventoryDataModel>();
            model.WarehouseList = _commonController.GetWarehouseList();
            return Json(new JsonResponse<string>(RenderPartialViewToString("TransferCargoPartial", model)));

        }

        public ActionResult ClickTransferCargo(int id, int newNum, int newWarehouseID)
        {
            //添加验证判断库存是否够调用
            if (false)//暂时不允许使用此功能
            {
                //更改之前的库存信息
                var inventoryData = _inventoryDataService.GetById(id);
                inventoryData.InventoryQuantity = inventoryData.InventoryQuantity - newNum;
                inventoryData.InventorySum = inventoryData.CostPrice * inventoryData.InventoryQuantity;
                inventoryData.ShipmentsQuantity = inventoryData.ShipmentsQuantity - newNum;
                inventoryData.RemainingQuantity = inventoryData.RemainingQuantity - newNum;
                _inventoryDataService.Update(inventoryData);


                //添加新的库存信息
                var newWarehouse = _warehouseService.GetById(newWarehouseID);
                InventoryData newInventoryData = new InventoryData()
                {
                    WarehouseID = newWarehouseID,
                    WarehouseName = newWarehouse.Name,
                    GoodsID = inventoryData.GoodsID,
                    GoodsName = inventoryData.GoodsName,
                    PurchaseDate = inventoryData.PurchaseDate,
                    ShipmentsDate = inventoryData.ShipmentsDate,

                    Unit = inventoryData.Unit,
                    Specification = inventoryData.Specification,
                    GoodsType = inventoryData.GoodsType,
                    Brand = inventoryData.Brand,

                    InventoryQuantity = newNum,
                    CostPrice = inventoryData.CostPrice,
                    InventorySum = inventoryData.CostPrice * newNum,
                    LastInventoryDate = inventoryData.LastInventoryDate,

                    FinalSaleDate = inventoryData.FinalSaleDate,
                    SupplierID = inventoryData.SupplierID,
                    SupplierName = inventoryData.SupplierName,
                    SupplierAddress = inventoryData.SupplierAddress,

                    Active = inventoryData.Active,
                    ShipmentsQuantity = inventoryData.ShipmentsQuantity - newNum,
                    RemainingQuantity = inventoryData.RemainingQuantity - newNum

                };
                _inventoryDataService.Insert(newInventoryData);

                //添加到调货信息一条记录


                SuccessNotification("调用成功");
                return RedirectToAction("Index");
            }
            ErrorNotification("调用数量大于库存剩余，请重新选择");
            return RedirectToAction("Index");
        }

    }
}