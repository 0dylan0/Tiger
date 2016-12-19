using Web.Framework.Mvc.ModelBinder;
using System.Web.Mvc;

namespace Web.Framework.Page
{
    [ModelBinder(typeof(PageInfoModelBinder))]
    public class PageInfo
    {
        //查询当前第几页
        public int PageIndex { get; set; }

        //每页的第一条记录的索引
        public int StartRecord { get; set; }

        //每页行数
        public int PageSize { get; set; } = 10;

        //总页数
        public int TotalPages { get; set; }

        //总数据量
        public int TotalRecords { get; set; }

        //页面刷新次数
        public int Draw { get; set; }

        public string sortExpression { get; set; }
    }
}
