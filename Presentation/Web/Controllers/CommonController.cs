using Core;
using Core.Enum;
using Services.Common;
using Services.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Framework.Controllers;
using Web.Framework.Json;
using Web.Models;

namespace Web.Controllers
{
    public class CommonController : BaseController
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
        private readonly SupplierTypeService _supplierTypeService;


        public CommonController(IWorkContext webWorkContext,
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
            SupplierTypeService supplierTypeService)
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
            _supplierTypeService = supplierTypeService;
        }

        // GET: Common
        public ActionResult SelectInventoryDataByRadio()
        {
            var model = new SearchInventoryDataModel();
            model.ChoiceTypes = ChoiceType.Radio;          
            return Json(new JsonResponse<string>(RenderPartialViewToString("_SelectInventoryDataPartial", model)));
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

        public List<SelectListItem> GetClientTypeList()
        {
            return _clientTypeService.GetClientTypeList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Name,
            }).ToList();
        }

        public List<SelectListItem> GetSupplierTypeList()
        {
            return _supplierTypeService.GetSupplierTypeList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Name,
            }).ToList();
        }

    }
}