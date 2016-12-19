using Web.Framework.Page;
using System.Web.Mvc;

namespace Web.Framework.Mvc.ModelBinder
{
    public class PageInfoModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            PageInfo pageInfo = new PageInfo();
            string size = bindingContext.ValueProvider.GetValue("length")?.AttemptedValue;
            if (!string.IsNullOrEmpty(size))
            {
                pageInfo.PageSize = int.Parse(size);
            }
            string start = bindingContext.ValueProvider.GetValue("start")?.AttemptedValue;
            if (!string.IsNullOrEmpty(start))
            {
                pageInfo.StartRecord = int.Parse(start);
            }
            pageInfo.PageIndex = pageInfo.StartRecord / pageInfo.PageSize;
            // For sortable,
            // sortOrder indicate which column need to sort,
            // sortDir indicate the sort order of sortOrder variable.
            string sortOrder = bindingContext.ValueProvider.GetValue("order[0][column]")?.AttemptedValue;
            string sortDir = bindingContext.ValueProvider.GetValue("order[0][dir]")?.AttemptedValue;
            if (!string.IsNullOrEmpty(sortOrder) && !string.IsNullOrEmpty(sortDir))
            {
                string colName = bindingContext.ValueProvider.GetValue("columns[" + int.Parse(sortOrder) + "][data]")?.AttemptedValue;
                if (!string.IsNullOrEmpty(colName))
                {
                    pageInfo.sortExpression = colName + " " + sortDir;
                }
            }
            string draw = bindingContext.ValueProvider.GetValue("draw")?.AttemptedValue;
            if (!string.IsNullOrEmpty(draw))
            {
                pageInfo.Draw = int.Parse(draw);
            }
            return pageInfo;
        }
    }
}

