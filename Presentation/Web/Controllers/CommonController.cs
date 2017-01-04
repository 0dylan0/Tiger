using Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework.Controllers;
using Web.Framework.Json;
using Web.Models;

namespace Web.Controllers
{
    public class CommonController : BaseController
    {
        // GET: Common
        public ActionResult SelectInventoryDataByRadio()
        {
            var model = new SearchInventoryDataModel();
            model.ChoiceTypes = ChoiceType.Radio;          
            return Json(new JsonResponse<string>(RenderPartialViewToString("_SelectInventoryDataPartial", model)));
        }
    }
}