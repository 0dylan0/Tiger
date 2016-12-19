using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;


namespace Core.Page
{
    [Serializable]
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList() { }

        public PagedList(IQueryable<T> source, int pageIndex, int pageSize) : this(source, pageIndex, pageSize, null) { }

        public PagedList(IQueryable<T> source, int pageIndex, int pageSize, string sortExpression)
        {
            if (!String.IsNullOrEmpty(sortExpression))
            {
                source = source.OrderBy(sortExpression);
            }

            int total = source.Count();
            TotalCount = total;
            TotalPages = total / pageSize;

            if (total % pageSize > 0)
            {
                TotalPages++;
            }

            PageSize = pageSize;
            PageIndex = pageIndex;
            AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        public int PageIndex
        {
            get;
            private set;
        }

        public int PageSize
        {
            get;
            private set;
        }

        public int TotalCount
        {
            get;
            private set;
        }

        public int TotalPages
        {
            get;
            private set;
        }

        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 0;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return PageIndex + 1 < TotalPages;
            }
        }
    }
}
