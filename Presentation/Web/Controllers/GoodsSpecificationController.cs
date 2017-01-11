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
    public class GoodsSpecificationController : BaseController
    {

        private readonly IWorkContext _webWorkContext;
        private readonly GoodsSpecificationService _goodsSpecificationService;
        private readonly LocalizationService _localizationService;
        public GoodsSpecificationController(IWorkContext webWorkContext,
            GoodsSpecificationService goodsSpecificationService,
            LocalizationService localizationService)
        {
            _webWorkContext = webWorkContext;
            _goodsSpecificationService = goodsSpecificationService;
            _localizationService = localizationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            GoodsSpecificationListModel model = new GoodsSpecificationListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, GoodsSpecificationListModel model)
        {
            IPagedList<GoodsSpecification> GoodsSpecificationList = _goodsSpecificationService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.GoodsSpecification = GoodsSpecificationList.MapTo<IList<GoodsSpecification>, IList<GoodsSpecificationModel>>();

            var results = new DataTable<GoodsSpecificationModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = GoodsSpecificationList.TotalCount,
                RecordsFiltered = GoodsSpecificationList.TotalCount,
                Data = model.GoodsSpecification
            };
            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            GoodsSpecificationModel model = new GoodsSpecificationModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(GoodsSpecificationModel model)
        {
            if (ModelState.IsValid)
            {
                GoodsSpecification GoodsSpecification = model.MapTo<GoodsSpecificationModel, GoodsSpecification>();
                _goodsSpecificationService.Insert(GoodsSpecification);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var goodsSpecification = _goodsSpecificationService.GetById(id);
            var res = goodsSpecification.MapTo<GoodsSpecification, GoodsSpecificationModel>();
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(GoodsSpecificationModel model)
        {
            if (ModelState.IsValid)
            {
                GoodsSpecification goodsSpecification = model.MapTo<GoodsSpecificationModel, GoodsSpecification>();
                _goodsSpecificationService.Update(goodsSpecification);
                SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") }");
                return RedirectToAction("Index");

            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var res = _goodsSpecificationService.GetById(id);
            _goodsSpecificationService.Delete(id);
            SuccessNotification($"{"删除成功" + res.Name}");
            return RedirectToAction("Index");
        }
    }
}