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
    public class GoodsTypeController : BaseController
    {
        private readonly IWorkContext _webWorkContext;
        private readonly GoodsTypeService _goodsTypeService;
        private readonly LocalizationService _localizationService;


        public GoodsTypeController(IWorkContext webWorkContext,
            GoodsTypeService goodsTypeService,
            LocalizationService localizationService)
        {
            _webWorkContext = webWorkContext;
            _goodsTypeService = goodsTypeService;
            _localizationService = localizationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            GoodsTypeListModel model = new GoodsTypeListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, GoodsTypeListModel model)
        {
            IPagedList<GoodsType> GoodsTypeList = _goodsTypeService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.GoodsType = GoodsTypeList.MapTo<IList<GoodsType>, IList<GoodsTypeModel>>();

            var results = new DataTable<GoodsTypeModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = GoodsTypeList.TotalCount,
                RecordsFiltered = GoodsTypeList.TotalCount,
                Data = model.GoodsType
            };
            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            GoodsTypeModel model = new GoodsTypeModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(GoodsTypeModel model)
        {
            if (ModelState.IsValid)
            {
                GoodsType goodsType = model.MapTo<GoodsTypeModel, GoodsType>();
                _goodsTypeService.Insert(goodsType);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var goodsType = _goodsTypeService.GetById(id);
            var res = goodsType.MapTo<GoodsType, GoodsTypeModel>();
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(GoodsTypeModel model)
        {
            if (ModelState.IsValid)
            {
                GoodsType goodsType = model.MapTo<GoodsTypeModel, GoodsType>();
                _goodsTypeService.Update(goodsType);
                SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") }");
                return RedirectToAction("Index");

            }
            return View(model);
        }


        public ActionResult Delete(int id)
        {
            var res = _goodsTypeService.GetById(id);
            _goodsTypeService.Delete(id);
            SuccessNotification($"{"删除成功" + res.Name}");
            return RedirectToAction("Index");
        }

    }
}