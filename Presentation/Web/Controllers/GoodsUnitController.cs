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
    public class GoodsUnitController : BaseController
    {
        private readonly IWorkContext _webWorkContext;
        private readonly GoodsUnitService _goodsUnitService;
        private readonly LocalizationService _localizationService;


        public GoodsUnitController(IWorkContext webWorkContext,
            GoodsUnitService goodsUnitService,
            LocalizationService localizationService)
        {
            _webWorkContext = webWorkContext;
            _goodsUnitService = goodsUnitService;
            _localizationService = localizationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            GoodsUnitListModel model = new GoodsUnitListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, GoodsUnitListModel model)
        {
            IPagedList<GoodsUnit> goodsUnitList = _goodsUnitService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.GoodsUnit = goodsUnitList.MapTo<IList<GoodsUnit>, IList<GoodsUnitModel>>();

            var results = new DataTable<GoodsUnitModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = goodsUnitList.TotalCount,
                RecordsFiltered = goodsUnitList.TotalCount,
                Data = model.GoodsUnit
            };
            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            GoodsUnitModel model = new GoodsUnitModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(GoodsUnitModel model)
        {
            if (ModelState.IsValid)
            {
                GoodsUnit goodsUnit = model.MapTo<GoodsUnitModel, GoodsUnit>();
                _goodsUnitService.Insert(goodsUnit);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var goodsUnit = _goodsUnitService.GetById(id);
            var res = goodsUnit.MapTo<GoodsUnit, GoodsUnitModel>();
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(GoodsUnitModel model)
        {
            if (ModelState.IsValid)
            {
                GoodsUnit goodsUnit = model.MapTo<GoodsUnitModel, GoodsUnit>();
                _goodsUnitService.Update(goodsUnit);
                SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") }");
                return RedirectToAction("Index");

            }
            return View(model);
        }


        public ActionResult Delete(int id)
        {
            var res = _goodsUnitService.GetById(id);
            _goodsUnitService.Delete(id);
            SuccessNotification($"{"删除成功" + res.Name}");
            return RedirectToAction("Index");
        }
    }
}