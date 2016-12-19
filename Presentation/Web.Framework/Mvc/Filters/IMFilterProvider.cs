using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Framework.Mvc.Filters
{
    public class IMFilterProvider : System.Web.Mvc.IFilterProvider
    {
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filterProviders = EngineContext.Current.ContainerManager.Resolve<IEnumerable<IFilterProvider>>();
            return filterProviders.Select(x => new Filter(x, FilterScope.Action, null));
        }
    }
}
