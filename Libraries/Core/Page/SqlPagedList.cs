using Core.Infrastructure;
using Dapper;
using Dapper.FluentMap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Page
{
    public class SqlPagedList<T> : List<T>, IPagedList<T>
    {
        public SqlPagedList()
        {
        }

        public SqlPagedList(string sql, object parameters, int pageIndex, int pageSize) : this(sql, parameters, pageIndex, pageSize, null)
        {
        }

        public SqlPagedList(string sql, object parameters, int pageIndex, int pageSize, string sortExpression)
        {
            DynamicParameters dynamicParameters = new DynamicParameters(parameters);

            IDbConnection connection = EngineContext.Current.Resolve<IDbConnection>();

            string calcTotalSql = $"SELECT COUNT(*) FROM ({sql}) AS Source",
                pagingSql = $@"
                    SELECT *
                    FROM(
	                    SELECT *, ROW_NUMBER() OVER (ORDER BY {ConvertSortExpression<T>(sortExpression)}) AS [row_number]
	                    FROM ({sql}) AS Source
                    ) AS T
                    WHERE T.[row_number] BETWEEN @paging_start AND @paging_end";

            int total = connection.QueryFirst<int>(calcTotalSql, dynamicParameters),
                skip = pageIndex * pageSize,
                take = skip + pageSize;

            dynamicParameters.Add("paging_start", skip + 1);
            dynamicParameters.Add("paging_end", take);

            TotalCount = total;
            TotalPages = total / pageSize;

            if (total % pageSize > 0)
            {
                TotalPages++;
            }

            PageSize = pageSize;
            PageIndex = pageIndex;
            AddRange(connection.Query<T>(String.IsNullOrEmpty(sortExpression) ? sql : pagingSql, dynamicParameters));
        }

        public int PageIndex
        {
            get;
        }

        public int PageSize
        {
            get;
        }

        public int TotalCount
        {
            get;
        }

        public int TotalPages
        {
            get;
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

        public static string ConvertSortExpression<TEntity>(string sortExpression)
        {
            if (sortExpression == null)
            {
                return null;
            }

            string[] temp = sortExpression.Split(' ');
            string propertyName = temp[0];
            string dir = temp.Length > 1 ? temp[1] : null;
            var maps = FluentMapper.EntityMaps;
            var keyType = typeof(TEntity);

            if (maps.ContainsKey(keyType))
            {
                var propertyMap = maps[keyType]?.PropertyMaps?.FirstOrDefault(p => p.PropertyInfo.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

                if (!String.IsNullOrEmpty(propertyMap?.ColumnName))
                {
                    return $"{propertyMap.ColumnName} {dir}";
                }
            }

            return $"{propertyName} {dir}";
        }
    }
}
