using Core;
using Core.Domain;
using Core.Domain.Common;
using Core.Enum;
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
        private readonly GoodsDataService _goodsDataService;
        private readonly SupplierDataService _supplierDataService;
        private readonly WarehouseService _warehouseService;
        private readonly InventoryDataService _inventoryDataService;
        private readonly GoodsSpecificationService _goodsSpecificationService;
        private readonly ClientTypeService _clientTypeService;
        private readonly SalesShipmentsDataService _salesShipmentsDataService;
        private readonly GoodsTypeService _goodsTypeService;


        public PurchaseDataController(IWorkContext webWorkContext,
            PurchaseDataService purchaseDataService,
            LocalizationService localizationService,
            GoodsDataService goodsDataService,
            SupplierDataService supplierDataService,
            WarehouseService warehouseService,
            InventoryDataService inventoryDataService,
            GoodsSpecificationService goodsSpecificationService,
            ClientTypeService clientTypeService,
            SalesShipmentsDataService salesShipmentsDataService,
            GoodsTypeService goodsTypeService)
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
            IPagedList<PurchaseData> PurchaseDataList = _purchaseDataService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.PurchaseData = PurchaseDataList.MapTo<IList<PurchaseData>, IList<PurchaseDataModel>>();

            var results = new DataTable<PurchaseDataModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = PurchaseDataList.TotalCount,
                RecordsFiltered = PurchaseDataList.TotalCount,
                Data = model.PurchaseData
            };
            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            PurchaseDataModel model = new PurchaseDataModel();
            model.Date = DateTime.Now;
            model.GoodsList = GetGoodsList();
            model.SupplierList = GetSupplierList();
            model.WarehouseList = GetWarehouseList();
            model.SpecificationList = GetSpecificationList();
            model.GoodsTypeList = GetGoodsTypeList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(PurchaseDataModel model)
        {
            if (ModelState.IsValid)
            {
                PurchaseData Purchase = model.MapTo<PurchaseDataModel, PurchaseData>();

                InventoryData inventoryData = new InventoryData()
                {
                    WarehouseID = model.WarehouseID,
                    WarehouseName = model.WarehouseName,
                    GoodsID = model.GoodsID,
                    GoodsName = model.GoodsName,
                    Unit = model.Unit,
                    Specification = model.Specification,
                    GoodsType = model.GoodsType,
                    Brand = model.Brand,
                    InventoryQuantity = model.Quantity,
                    CostPrice = ((model.Quantity != 0) ? (model.Sum / Convert.ToDecimal(model.Quantity)) : 0),
                    InventorySum = model.Sum,
                    SupplierID = model.SupplierID,
                    SupplierName = model.SupplierName,
                    SupplierAddress = model.SupplierAddress,

                    PurchaseDate = DateTime.Now,
                    ShipmentsDate = DateTime.Now,
                    LastInventoryDate = DateTime.Now,
                    FinalSaleDate = DateTime.Now,
                    Active="1",
                    ShipmentsQuantity=0,
                    RemainingQuantity= model.Quantity
                };
                //添加货物库存表返回ID
                int inventoryDataID = _inventoryDataService.Insert(inventoryData);
                Purchase.InventoryDataID = inventoryDataID;
                Purchase.Active = "1";

                _purchaseDataService.Insert(Purchase);

                return RedirectToAction("Index");
            }

            model.GoodsList = GetGoodsList();
            model.SupplierList = GetSupplierList();
            model.WarehouseList = GetWarehouseList();
            model.SpecificationList = GetSpecificationList();
            model.GoodsTypeList = GetGoodsTypeList();

            return View(model);
        }

        public ActionResult Edit(int id, bool IfInventoryData=true)
        {
            var purchase = new PurchaseData();
            if (IfInventoryData)
            {
                 purchase = _purchaseDataService.GetById(id);
            }
            else
            {
                purchase = _purchaseDataService.GetByInventoryDataID(id);
            }
            

            if (_salesShipmentsDataService.GetByInventoryDataID(purchase.InventoryDataID)==null)
            {
                var model = purchase.MapTo<PurchaseData, PurchaseDataModel>();
                model.GoodsList = GetGoodsList();
                model.SupplierList = GetSupplierList();
                model.WarehouseList = GetWarehouseList();
                model.SpecificationList = GetSpecificationList();
                model.GoodsTypeList = GetGoodsTypeList();
                return View(model);
            }
            ErrorNotification("已经存在销售出货记录"+purchase.GoodsName+"不允许修改");
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Edit(PurchaseDataModel model)
        {
            if (ModelState.IsValid)
            {
                PurchaseData purchase = model.MapTo<PurchaseDataModel, PurchaseData>();
                InventoryData inventoryData = new InventoryData()
                {
                    WarehouseID = model.WarehouseID,
                    WarehouseName = model.WarehouseName,
                    GoodsID = model.GoodsID,
                    GoodsName = model.GoodsName,
                    Unit = model.Unit,
                    Specification = model.Specification,
                    GoodsType = model.GoodsType,
                    Brand = model.Brand,
                    InventoryQuantity = model.Quantity,
                    CostPrice = ((model.Quantity != 0) ? (model.Sum / Convert.ToDecimal(model.Quantity)) : 0),
                    InventorySum = model.Sum,
                    SupplierID = model.SupplierID,
                    SupplierName = model.SupplierName,
                    SupplierAddress = model.SupplierAddress,

                    PurchaseDate = DateTime.Now,
                    ShipmentsDate = DateTime.Now,
                    LastInventoryDate = DateTime.Now,
                    FinalSaleDate = DateTime.Now,
                    Active = "1",
                    ShipmentsQuantity = 0,
                    RemainingQuantity = model.Quantity
                };
                //修改货物库存表返回ID
                _inventoryDataService.Update(inventoryData);
                purchase.Active = "1";
                _purchaseDataService.Update(purchase);
                SuccessNotification("修改成功"+model.GoodsName);
                return RedirectToAction("Index");

            }
            model.GoodsList = GetGoodsList();
            model.SupplierList = GetSupplierList();
            model.WarehouseList = GetWarehouseList();
            model.SpecificationList = GetSpecificationList();
            model.GoodsTypeList = GetGoodsTypeList();

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var res = _purchaseDataService.GetById(id);

            if (_salesShipmentsDataService.GetByInventoryDataID(res.InventoryDataID) == null)
            {
                _inventoryDataService.Delete(id);
                _purchaseDataService.Delete(id);
                SuccessNotification($"{"删除成功" + res.GoodsName}");
                return RedirectToAction("Index");
            }
            ErrorNotification("已经存在销售出货记录" + res.GoodsName + "不允许修改");
            return RedirectToAction("Index");


        }

        public ActionResult PurchaseDataDetails(int id)
        {
            var purchase = _purchaseDataService.GetById(id);
            var model = purchase.MapTo<PurchaseData, PurchaseDataModel>();
            model.GoodsList = GetGoodsList();
            model.SupplierList = GetSupplierList();
            model.WarehouseList = GetWarehouseList();
            model.SpecificationList = GetSpecificationList();
            model.GoodsTypeList = GetGoodsTypeList();

            return Json(new JsonResponse<string>(RenderPartialViewToString("PurchaseDataPartial", model)));
        }

        public List<SelectListItem> GetGoodsList()
        {
            return _goodsDataService.GetGoodsList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Code,
            }).ToList();

        }
        public List<SelectListItem> GetSupplierList()
        {
            return _supplierDataService.GetSupplierList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Code,
            }).ToList();
        }

        public List<SelectListItem> GetWarehouseList()
        {
            return _warehouseService.GetWarehouseList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Code,
            }).ToList();
        }

        public List<SelectListItem> GetSpecificationList()
        {
            return _goodsSpecificationService.GetGoodsSpecificationList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Name,
            }).ToList();
        }

        public List<SelectListItem> GetGoodsTypeList()
        {
            return _goodsTypeService.GetGoodsTypeList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Name,
            }).ToList();
        }

    }
}