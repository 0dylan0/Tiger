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
    public class GoodsUnitService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public GoodsUnitService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(GoodsUnit GoodsUnit)
        {
            var sql = $@"insert into GoodsUnit(
                    Name)
			        VALUES (
                    @Name)";
            _context.Execute(sql, new
            {
                Name = GoodsUnit.Name
            });
        }

        public void Update(GoodsUnit GoodsUnit)
        {
            var sql = $@"update GoodsUnit set
                    Name=@Name
                    where ID=@ID";
            _context.Execute(sql, new
            {
                ID = GoodsUnit.ID,
                Name = GoodsUnit.Name
            });
        }

        public GoodsUnit GetById(int id)
        {
            var sql = @"select * from GoodsUnit  where id = @id";
            return _context.QuerySingle<GoodsUnit>(sql, new
            {
                id = id
            });
        }
        public IPagedList<GoodsUnit> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from GoodsUnit";
            var Parameter = new DynamicParameters();
            return new SqlPagedList<GoodsUnit>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

        public List<GetList> GetWarehouseList()
        {
            string sql = @"select id as code, name from GoodsUnit";

            return _context.Query<GetList>(sql).ToList();
        }
    }
}
