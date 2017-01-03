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
    public class WarehouseController : BaseController
    {

        private readonly IWorkContext _webWorkContext;
        private readonly PurchaseDataService _purchaseDataService;
        private readonly LocalizationService _localizationService;
        private readonly GoodsDataService _goodsDataService;
        private readonly SupplierDataService _supplierDataService;
        private readonly WarehouseService _warehouseService;
        
        public WarehouseController(IWorkContext webWorkContext,
            PurchaseDataService purchaseDataService,
            LocalizationService localizationService,
            GoodsDataService goodsDataService,
            SupplierDataService supplierDataService,
            WarehouseService warehouseService)
        {
            _webWorkContext = webWorkContext;
            _purchaseDataService = purchaseDataService;
            _localizationService = localizationService;
            _goodsDataService = goodsDataService;
            _supplierDataService = supplierDataService;
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            WarehouseListModel model = new WarehouseListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, WarehouseListModel model)
        {
            IPagedList<Warehouse> WarehouseList = _warehouseService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.Warehouse = WarehouseList.MapTo<IList<Warehouse>, IList<WarehouseModel>>();

            var results = new DataTable<WarehouseModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = WarehouseList.TotalCount,
                RecordsFiltered = WarehouseList.TotalCount,
                Data = model.Warehouse
            };
            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            WarehouseModel model = new WarehouseModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(WarehouseModel model)
        {
            if (ModelState.IsValid)
            {
                Warehouse Warehouse = model.MapTo<WarehouseModel, Warehouse>();
                _warehouseService.Insert(Warehouse);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var warehouse = _warehouseService.GetById(id);
            var res = warehouse.MapTo<Warehouse, WarehouseModel>();
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(WarehouseModel model)
        {
            if (ModelState.IsValid)
            {
                Warehouse warehouse = model.MapTo<WarehouseModel, Warehouse>();
                _warehouseService.Update(warehouse);
                SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") }");
                return RedirectToAction("Index");

            }
            return View(model);
        }
    }
}