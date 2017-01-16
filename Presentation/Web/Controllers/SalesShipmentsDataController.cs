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
    public class SalesShipmentsDataController : BaseController
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

        public SalesShipmentsDataController(IWorkContext webWorkContext,
            SalesShipmentsDataService salesShipmentsDataService,
            LocalizationService localizationService,
            SupplierDataService supplierDataService,
            WarehouseService warehouseService,
            InventoryDataService inventoryDataService,
            GoodsSpecificationService goodsSpecificationService,
            ClientTypeService clientTypeService,
            ClientDataService clientDataService)
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
            model.ClientDataList = GetClientDataList();
            model.WarehouseList = GetWarehouseList();
            model.SpecificationList = GetSpecificationList();
            model.GoodsTypeList = GetGoodsTypeList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(SalesShipmentsDataModel model)
        {
            if (ModelState.IsValid)
            {
                SalesShipmentsData SalesShipments = model.MapTo<SalesShipmentsDataModel, SalesShipmentsData>();
                _salesShipmentsDataService.Insert(SalesShipments);
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
                    InventoryQuantity = model.OldQuantity - model.Quantity,
                    CostPrice = (( model.Quantity != 0) ? (model.Sum / Convert.ToDecimal(model.Quantity)) : 0),
                    InventorySum = model.Sum,

                    PurchaseDate = DateTime.Now,
                    ShipmentsDate = DateTime.Now,
                    LastInventoryDate = DateTime.Now,
                    FinalSaleDate = DateTime.Now
                };
                _inventoryDataService.Update(inventoryData);
                return RedirectToAction("Index");
            }

            model.ClientDataList = GetClientDataList();
            model.WarehouseList = GetWarehouseList();
            model.SpecificationList = GetSpecificationList();
            model.GoodsTypeList = GetGoodsTypeList();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var salesShipments = _salesShipmentsDataService.GetById(id);
            var model = salesShipments.MapTo<SalesShipmentsData, SalesShipmentsDataModel>();

            model.OldQuantity = model.Quantity;
            model.ClientDataList = GetClientDataList();
            model.WarehouseList = GetWarehouseList();
            model.SpecificationList = GetSpecificationList();
            model.GoodsTypeList = GetGoodsTypeList();
            return View(model);

        }
        [HttpPost]
        public ActionResult Edit(SalesShipmentsDataModel model)
        {
            if (ModelState.IsValid)
            {
                SalesShipmentsData salesShipments = model.MapTo<SalesShipmentsDataModel, SalesShipmentsData>();
                _salesShipmentsDataService.Update(salesShipments);


                var inventoryDataID = _inventoryDataService.GetById(model.InventoryDataID);
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
                    InventoryQuantity = inventoryDataID.InventoryQuantity + (model.Quantity - model.OldQuantity),
                    CostPrice = ((model.Quantity != 0) ? (model.Sum / Convert.ToDecimal(model.Quantity)) : 0),
                    InventorySum = model.Sum,

                    PurchaseDate = DateTime.Now,
                    ShipmentsDate = DateTime.Now,
                    LastInventoryDate = DateTime.Now,
                    FinalSaleDate = DateTime.Now
                };
                _inventoryDataService.Update(inventoryData);
                SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") + model.GoodsName}");
                return RedirectToAction("Index");

            }

            model.ClientDataList = GetClientDataList();
            model.WarehouseList = GetWarehouseList();
            model.SpecificationList = GetSpecificationList();
            model.GoodsTypeList = GetGoodsTypeList();
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var selesShipment = _salesShipmentsDataService.GetById(id);
            var model = selesShipment.MapTo<SalesShipmentsData, SalesShipmentsDataModel>();

            return Json(new JsonResponse<string>(RenderPartialViewToString("SalesShipmentsDataPartial", model)));
        }

        public List<SelectListItem> GetClientDataList()
        {
            return _clientDataService.GetClientDataList().Select(o => new SelectListItem
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
            return _clientTypeService.GetClientTypeList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Name,
            }).ToList();
        }
    }
}