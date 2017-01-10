using Core.Domain.Common;
using Core.Page;
using Dapper;
using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class GoodsTypeService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public GoodsTypeService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(GoodsType GoodsType)
        {
            var sql = $@"insert into GoodsType(
                    Name)
			        VALUES (
                    @Name)";
            _context.Execute(sql, new
            {
                Name = GoodsType.Name
            });
        }

        public void Update(GoodsType GoodsType)
        {
            var sql = $@"update GoodsType set
                    Name=@Name
                    where ID=@ID";
            _context.Execute(sql, new
            {
                ID = GoodsType.ID,
                Name = GoodsType.Name
            });
        }

        public GoodsType GetById(int id)
        {
            var sql = @"select * from GoodsType  where id = @id";
            return _context.QuerySingle<GoodsType>(sql, new
            {
                id = id
            });
        }
        public IPagedList<GoodsType> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from GoodsType ";
            var Parameter = new DynamicParameters();
            if (!string.IsNullOrEmpty(textQuery))
            {
                sql += " where Name like @textQuery";
                textQuery = textQuery.Contains("%") ? textQuery : $"%{textQuery}%";
                Parameter.Add("textQuery", textQuery);
            }
            return new SqlPagedList<GoodsType>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

        public List<GetList> GetGoodsTypeList()
        {
            string sql = @"select id as code, name from GoodsType";

            return _context.Query<GetList>(sql).ToList();
        }
    }
}
