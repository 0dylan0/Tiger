using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Framework.Mvc.Filters
{
    public class PageSumFilterProvider : IFilterProvider, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            new Task(() =>
            {
                PageVisitsStatistics.PageVisitSum(actionContext);
            }).Start();
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}
