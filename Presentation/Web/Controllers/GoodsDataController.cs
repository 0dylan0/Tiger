using Core;
using Core.Domain.Common;
using Core.Page;
using Services.Common;
using Services.Localization;
using Services.Users;
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
    public class GoodsDataController : BaseController
    {
        private readonly IWorkContext _webWorkContext;
        private readonly GoodsDataService _goodsDataService;
        private readonly LocalizationService _localizationService;
        private readonly CommonController _commonController;
        private readonly WarehouseService _warehouseService;

        public GoodsDataController(IWorkContext webWorkContext,
            GoodsDataService goodsDataService,
            LocalizationService localizationService,
            CommonController commonController,
            WarehouseService warehouseService)
        {
            _webWorkContext = webWorkContext;
            _goodsDataService = goodsDataService;
            _localizationService = localizationService;
            _commonController = commonController;
            _warehouseService = warehouseService;
        }

        // GET: GoodsData
        [HttpGet]
        public ActionResult Index()
        {
            GoodsDataListModel model = new GoodsDataListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, GoodsDataListModel model)
        {
            IPagedList<GoodsData> UserList = _goodsDataService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.GoodsData = UserList.MapTo<IList<GoodsData>, IList<GoodsDataModel>>();

            var results = new DataTable<GoodsDataModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = UserList.TotalCount,
                RecordsFiltered = UserList.TotalCount,
                Data = model.GoodsData
            };

            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            GoodsDataModel model = new GoodsDataModel();
            model.WarehouseList = GetWarehouseList();
            model.GoodTypeList = _commonController.GetGoodsTypeList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(GoodsDataModel model)
        {


            if (ModelState.IsValid)
            {
                GoodsData Goods = model.MapTo<GoodsDataModel, GoodsData>();
                _goodsDataService.Insert(Goods);
                return RedirectToAction("Index");
            }
            model.WarehouseList = GetWarehouseList();
            model.GoodTypeList = _commonController.GetGoodsTypeList();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = _goodsDataService.GetUserById(id);
            var res = user.MapTo<GoodsData, GoodsDataModel>();
            res.WarehouseList = GetWarehouseList();
            res.GoodTypeList = _commonController.GetGoodsTypeList();
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(GoodsDataModel model)
        {
            if (ModelState.IsValid)
            {
                GoodsData goodsData = model.MapTo<GoodsDataModel, GoodsData>();
                _goodsDataService.Update(goodsData);
                SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") + model.GoodsName}");
                return RedirectToAction("Index");

            }
            model.WarehouseList = GetWarehouseList();
            model.GoodTypeList = _commonController.GetGoodsTypeList();
            return View(model);
        }

        public List<SelectListItem> GetWarehouseList()
        {
            return _warehouseService.GetWarehouseList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Name,
            }).ToList();
        }

    }
}