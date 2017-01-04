using Core;
using Services.Common;
using Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework.Controllers;

namespace Web.Controllers
{
    public class GoodsSpecificationController : BaseController
    {

        private readonly IWorkContext _webWorkContext;
        private readonly GoodsDataService _goodsDataService;
        private readonly LocalizationService _localizationService;
        public GoodsSpecificationController(IWorkContext webWorkContext,
            GoodsDataService goodsDataService,
            LocalizationService localizationService)
        {
            _webWorkContext = webWorkContext;
            _goodsDataService = goodsDataService;
            _localizationService = localizationService;
        }

        // GET: GoodsSpecification
        public ActionResult Index()
        {
            return View();
        }
    }
}