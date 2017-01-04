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
    public class SupplierTypeController : BaseController
    {

        private readonly IWorkContext _webWorkContext;
        private readonly SupplierTypeService _supplierTypeService;
        private readonly LocalizationService _localizationService;


        public SupplierTypeController(IWorkContext webWorkContext,
            SupplierTypeService supplierTypeService,
            LocalizationService localizationService)
        {
            _webWorkContext = webWorkContext;
            _supplierTypeService = supplierTypeService;
            _localizationService = localizationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            SupplierTypeListModel model = new SupplierTypeListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, SupplierTypeListModel model)
        {
            IPagedList<SupplierType> supplierTypeList = _supplierTypeService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.SupplierType = supplierTypeList.MapTo<IList<SupplierType>, IList<SupplierTypeModel>>();

            var results = new DataTable<SupplierTypeModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = supplierTypeList.TotalCount,
                RecordsFiltered = supplierTypeList.TotalCount,
                Data = model.SupplierType
            };
            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            SupplierTypeModel model = new SupplierTypeModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(SupplierTypeModel model)
        {
            if (ModelState.IsValid)
            {
                SupplierType supplierType = model.MapTo<SupplierTypeModel, SupplierType>();
                _supplierTypeService.Insert(supplierType);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var supplierType = _supplierTypeService.GetById(id);
            var res = supplierType.MapTo<SupplierType, SupplierTypeModel>();
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(SupplierTypeModel model)
        {
            if (ModelState.IsValid)
            {
                SupplierType supplierType = model.MapTo<SupplierTypeModel, SupplierType>();
                _supplierTypeService.Update(supplierType);
                SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") }");
                return RedirectToAction("Index");

            }
            return View(model);
        }
    }
}