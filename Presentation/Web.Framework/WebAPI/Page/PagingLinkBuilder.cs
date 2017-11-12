using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Web.Framework.WebAPI.Page
{
    public class PagingLinkBuilder
    {
        public static PagingLinkBuilder<T> Build<T>(UrlHelper urlHelper, string routeName, T content, int page, int pageSize, int total)
        {
            return new PagingLinkBuilder<T>(urlHelper, routeName, content, page, pageSize, total);
        }
    }

    public class PagingLinkBuilder<T>
    {
        private const string LINK_TEMPLATE = "<{0}>; rel=\"{1}\"";
        private UrlHelper _urlHelper;
        private string _routeName;

        public PagingLinkBuilder(UrlHelper urlHelper, string routeName, T content, int page, int pageSize, int total, string pageParamName = "page", string pageSizeParamName = "pageSize")
        {
            _urlHelper = urlHelper;
            _routeName = routeName;

            Content = content;
            Page = page;
            PageSize = pageSize;
            TotalRecordsCount = total;

            PageParamName = pageParamName;
            PageSizeParamName = pageSizeParamName;
        }

        public T Content { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public int TotalRecordsCount { get; private set; }
        public int PageCount => (int)Math.Ceiling(TotalRecordsCount / (decimal)PageSize);

        public string PageParamName { get; set; }
        public string PageSizeParamName { get; set; }

        public string FirstPage
        {
            get
            {
                var link = GetLink(1);
                return String.Format(LINK_TEMPLATE, link, "first");
            }
        }

        public string PreviousPage
        {
            get
            {
                if (Page == 1 || TotalRecordsCount < Page * PageSize)
                {
                    return null;
                }

                var link = GetLink(Page - 1);
                return String.Format(LINK_TEMPLATE, link, "prev");
            }
        }

        public string NextPage
        {
            get
            {
                if (Page == PageCount || TotalRecordsCount < Page * PageSize)
                {
                    return null;
                }

                var link = GetLink(Page + 1);
                return String.Format(LINK_TEMPLATE, link, "next");
            }
        }

        public string LastPage
        {
            get
            {
                var link = GetLink(PageCount);
                return String.Format(LINK_TEMPLATE, link, "last");
            }
        }

        public IEnumerable<string> Link
        {
            get
            {
                var links = new List<string>();
                links.Add(FirstPage);

                if (!String.IsNullOrEmpty(PreviousPage))
                {
                    links.Add(PreviousPage);
                }

                if (!String.IsNullOrEmpty(NextPage))
                {
                    links.Add(NextPage);
                }

                links.Add(LastPage);

                return links;
            }
        }

        private string GetLink(int page)
        {
            var queryStringCollection = _urlHelper.Request.RequestUri.ParseQueryString();
            queryStringCollection.Set(PageParamName, page.ToString());
            queryStringCollection.Set(PageSizeParamName, PageSize.ToString());

            var routeValues = queryStringCollection.AllKeys.ToDictionary(key => key, key => (object)queryStringCollection[key]);

            return _urlHelper.Link(_routeName, routeValues);
        }
    }
}
