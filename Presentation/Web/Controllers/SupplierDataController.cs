using Core;
using Core.Domain.Common;
using Core.Page;
using Services.Common;
using Services.Localization;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Extensions;
using Web.Framework.Controllers;
using Web.Framework.Json;
using Web.Framework.Page;
using Web.Models;

namespace Web.Controllers
{
    public class SupplierDataController : BaseController
    {

        private readonly IWorkContext _webWorkContext;
        private readonly SupplierDataService _supplierDataService;
        private readonly LocalizationService _localizationService;
        public SupplierDataController(IWorkContext webWorkContext,
            SupplierDataService supplierDataService,
            LocalizationService localizationService)
        {
            _webWorkContext = webWorkContext;
            _supplierDataService = supplierDataService;
            _localizationService = localizationService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            SupplierDataListModel model = new SupplierDataListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, SupplierDataListModel model)
        {
            IPagedList<SupplierData> UserList = _supplierDataService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.SupplierData = UserList.MapTo<IList<SupplierData>, IList<SupplierDataModel>>();

            var results = new DataTable<SupplierDataModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = UserList.TotalCount,
                RecordsFiltered = UserList.TotalCount,
                Data = model.SupplierData
            };

            return Json(new PlainJsonResponse(results));
        }

        [HttpGet]
        public ActionResult Add()
        {
            SupplierDataModel model = new SupplierDataModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(SupplierDataModel model)
        {
            if (ModelState.IsValid)
            {
                SupplierData Goods = model.MapTo<SupplierDataModel, SupplierData>();
                _supplierDataService.Insert(Goods);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = _supplierDataService.GetUserById(id);
            var res = user.MapTo<SupplierData, SupplierDataModel>();
            return View(res);

        }
        [HttpPost]
        public ActionResult Edit(SupplierDataModel model)
        {
            if (ModelState.IsValid)
            {
                SupplierData goodsData = model.MapTo<SupplierDataModel, SupplierData>();
                _supplierDataService.Update(goodsData);
                SuccessNotification($"{_localizationService.GetResource("UpdateSuccess") + model.SupplierName}");
                return RedirectToAction("Index");

            }
            return View(model);
        }
    }
}