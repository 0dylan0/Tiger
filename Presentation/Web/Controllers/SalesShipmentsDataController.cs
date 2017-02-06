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
        private readonly GoodsTypeService _goodsTypeService;
        private readonly ArrearsDataService _arrearsDataService;
        private readonly ArrearsDetailsService _arrearsDetailsService;

        public SalesShipmentsDataController(IWorkContext webWorkContext,
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
            ArrearsDetailsService arrearsDetailsService)
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
            IPagedList<SalesShipmentsData> SalesShipmentsDataList = _salesShipmentsDataService.GetList(model.Name, model.ShowInactive, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
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
        public ActionResult Add(int id = 0)
        {
            if (id == 0)
            {
                SalesShipmentsDataModel model = new SalesShipmentsDataModel();
                model.Date = DateTime.Now;
                model.ClientDataList = GetClientDataList();
                model.WarehouseList = GetWarehouseList();
                model.SpecificationList = GetSpecificationList();
                model.GoodsTypeList = GetGoodsTypeList();
                return View(model);
            }
            else
            {
                InventoryData inventoryData = _inventoryDataService.GetById(id);
                SalesShipmentsDataModel model = new SalesShipmentsDataModel()
                {
                    WarehouseID = inventoryData.WarehouseID,
                    WarehouseName = inventoryData.WarehouseName,
                    GoodsID = inventoryData.GoodsID,
                    GoodsName = inventoryData.GoodsName,
                    Unit = inventoryData.Unit,
                    Specification = inventoryData.Specification,
                    GoodsType = inventoryData.GoodsType,
                    Brand = inventoryData.Brand,
                    Cost = inventoryData.CostPrice,
                    InventoryDataID = inventoryData.ID,
                    OldQuantity = inventoryData.InventoryQuantity
                };
                model.Date = DateTime.Now;
                model.ClientDataList = GetClientDataList();
                model.WarehouseList = GetWarehouseList();
                model.SpecificationList = GetSpecificationList();
                model.GoodsTypeList = GetGoodsTypeList();
                return View(model);
            }

        }
        [HttpPost]
        public ActionResult Add(SalesShipmentsDataModel model)
        {
            if (ModelState.IsValid)
            {
                SalesShipmentsData SalesShipments = model.MapTo<SalesShipmentsDataModel, SalesShipmentsData>();
                int salesShipmentsDataID = _salesShipmentsDataService.Insert(SalesShipments);
                InventoryData inventoryData = new InventoryData()
                {
                    ID = model.InventoryDataID,
                    WarehouseID = model.WarehouseID,
                    WarehouseName = model.WarehouseName,
                    GoodsID = model.GoodsID,
                    GoodsName = model.GoodsName,
                    Unit = model.Unit,
                    Specification = model.Specification,
                    GoodsType = model.GoodsType,
                    Brand = model.Brand,
                    InventoryQuantity = model.OldQuantity - model.Quantity,
                    CostPrice = ((model.Quantity != 0) ? (model.Sum / Convert.ToDecimal(model.Quantity)) : 0),
                    InventorySum = model.Sum,

                    PurchaseDate = DateTime.Now,
                    ShipmentsDate = DateTime.Now,
                    LastInventoryDate = DateTime.Now,
                    FinalSaleDate = DateTime.Now
                };
                _inventoryDataService.Update(inventoryData);

                if (model.ArrearsAmount != 0 && model.ArrearsAmount != null)
                {
                    //添加欠款信息
                    ArrearsInset(model, salesShipmentsDataID);
                }

                SuccessNotification("添加成功");
                return RedirectToAction("Index");
            }

            model.ClientDataList = GetClientDataList();
            model.WarehouseList = GetWarehouseList();
            model.SpecificationList = GetSpecificationList();
            model.GoodsTypeList = GetGoodsTypeList();
            return View(model);
        }

        public void ArrearsInset(SalesShipmentsDataModel model, int salesShipmentsDataID)
        {

            int resID = _arrearsDataService.GetByClientDataIDAndDate(model.ClientDataID, model.Date);
            if (resID == 0)
            {
                ArrearsData arrearsData = new ArrearsData()
                {
                    ClientDataID = model.ClientDataID,
                    ClientDataName = model.ClientDataName,
                    ArrearsAmount = model.ArrearsAmount,
                    Date = model.Date,
                    Sum = model.ArrearsAmount

                };
                int arrearsID = _arrearsDataService.Insert(arrearsData);
                ArrearsDetails arrearsDetails = new ArrearsDetails()
                {
                    Quantity = model.Quantity,
                    UnitPrice = model.UnitPrice,
                    SalesShipmentsDataID = salesShipmentsDataID,
                    Sum = model.ArrearsAmount,
                    ArrearsAmount = model.ArrearsAmount,
                    ArrearsID = arrearsID,
                    GoodsID = model.GoodsID,
                    GoodsName = model.GoodsName
                };
                _arrearsDetailsService.Insert(arrearsDetails);

                _salesShipmentsDataService.InsertArrearsID(arrearsID, salesShipmentsDataID);
            }
            else
            {
                ArrearsDetails arrearsDetails = new ArrearsDetails()
                {
                    Quantity = model.Quantity,
                    UnitPrice = model.UnitPrice,
                    SalesShipmentsDataID = salesShipmentsDataID,
                    Sum = model.ArrearsAmount,
                    ArrearsAmount = model.ArrearsAmount,
                    ArrearsID = resID,
                    GoodsID = model.GoodsID,
                    GoodsName = model.GoodsName
                };
                _arrearsDetailsService.Insert(arrearsDetails);

                //为主表添加欠款
                ArrearsData arrearsData = _arrearsDataService.GetById(resID);
                decimal? ArrearsAmount = arrearsData.ArrearsAmount + model.ArrearsAmount;
                decimal? Sum = arrearsData.Sum + model.ArrearsAmount;
                _arrearsDataService.UpdateArrearsAmountAndSum(ArrearsAmount, Sum, resID);
            }



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
                SuccessNotification("修改成功");
                return RedirectToAction("Index");

            }

            model.ClientDataList = GetClientDataList();
            model.WarehouseList = GetWarehouseList();
            model.SpecificationList = GetSpecificationList();
            model.GoodsTypeList = GetGoodsTypeList();
            return View(model);
        }

        public ActionResult Refund(int id, int inventoryDataID, int quantity)
        {
            var selesShipment = _salesShipmentsDataService.GetById(id);
            _salesShipmentsDataService.Refund(id);

            var inventoryID = _inventoryDataService.GetById(inventoryDataID);
            InventoryData inventoryData = new InventoryData()
            {
                ID = inventoryID.ID,
                InventoryQuantity = inventoryID.InventoryQuantity + quantity,
            };
            _inventoryDataService.Refund(inventoryData);

            SuccessNotification("退货成功");
            return RedirectToAction("Index");
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
            return _goodsTypeService.GetGoodsTypeList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Name,
            }).ToList();
        }
    }
}