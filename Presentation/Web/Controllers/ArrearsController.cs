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
    public class ArrearsController : BaseController
    {
        private readonly IWorkContext _webWorkContext;
        private readonly ArrearsDataService _arrearsDataService;
        private readonly LocalizationService _localizationService;
        private readonly CommonController _commonController;
        private readonly ArrearsDetailsService _arrearsDetailsService;

        public ArrearsController(IWorkContext webWorkContext,
            ArrearsDataService arrearsDataService,
            LocalizationService localizationService,
            CommonController commonController,
            ArrearsDetailsService arrearsDetailsService)
        {
            _webWorkContext = webWorkContext;
            _arrearsDataService = arrearsDataService;
            _localizationService = localizationService;
            _commonController = commonController;
            _arrearsDetailsService = arrearsDetailsService;
        }

        // GET: Arrears
        public ActionResult Index()
        {
            ArrearsDataListModel model = new ArrearsDataListModel();
            var name = _webWorkContext.CurrentUser.Name;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo, ArrearsDataListModel model)
        {
            IPagedList<ArrearsData> UserList = _arrearsDataService.GetList(model.Name, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.sortExpression);
            model.ArrearsDataList = UserList.MapTo<IList<ArrearsData>, IList<ArrearsDataModel>>();

            var results = new DataTable<ArrearsDataModel>()
            {
                Draw = pageInfo.Draw + 1,
                RecordsTotal = UserList.TotalCount,
                RecordsFiltered = UserList.TotalCount,
                Data = model.ArrearsDataList
            };

            return Json(new PlainJsonResponse(results));
        }

        public ActionResult Edit(int id)
        {
            var arrearsData = _arrearsDataService.GetById(id);
            var model = arrearsData.MapTo<ArrearsData, ArrearsDataModel>();
            List<ArrearsDetails>  res= _arrearsDetailsService.GetByArrearsId(id).ToList();
            var arrearsDetails = res.MapTo<List<ArrearsDetails>, List<ArrearsDetailsModel>>();
            model.ArrearsDetailsModels = new List<ArrearsDetailsModel>();
            model.ArrearsDetailsModels.AddRange(arrearsDetails);
            return View(model);

        }
        [HttpPost]
        public ActionResult Edit(int arrearsDetailsId, decimal arrears,int arrearsID,string remarks)
        {
            if (ModelState.IsValid)
            {
                _arrearsDetailsService.UpdateArrears(arrearsDetailsId, arrears, arrearsID, remarks);
                return Json(new JsonResponse(JsonResponseStatus.success), JsonRequestBehavior.AllowGet);
            }       
            return View(_arrearsDataService.GetById(arrearsID));
        }
    }
}