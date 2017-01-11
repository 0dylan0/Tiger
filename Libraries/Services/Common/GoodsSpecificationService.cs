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
    public class GoodsSpecificationService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public GoodsSpecificationService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(GoodsSpecification GoodsSpecification)
        {
            var sql = $@"insert into GoodsSpecification(
                    Name)
			        VALUES (
                    @Name)";
            _context.Execute(sql, new
            {
                Name = GoodsSpecification.Name
            });
        }

        public void Update(GoodsSpecification GoodsSpecification)
        {
            var sql = $@"update GoodsSpecification set
                    Name=@Name
                    where ID=@ID";
            _context.Execute(sql, new
            {
                ID = GoodsSpecification.ID,
                Name = GoodsSpecification.Name
            });
        }

        public GoodsSpecification GetById(int id)
        {
            var sql = @"select * from GoodsSpecification  where id = @id";
            return _context.QuerySingle<GoodsSpecification>(sql, new
            {
                id = id
            });
        }
        public IPagedList<GoodsSpecification> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from GoodsSpecification ";
            var Parameter = new DynamicParameters();
            if (!string.IsNullOrEmpty(textQuery))
            {
                sql += " where Name like @textQuery";
                textQuery = textQuery.Contains("%") ? textQuery : $"%{textQuery}%";
                Parameter.Add("textQuery", textQuery);
            }
            return new SqlPagedList<GoodsSpecification>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

        public List<GoodsSpecification> GetGoodsSpecificationList()
        {
            string sql = @"select id as code, name from GoodsSpecification";

            return _context.Query<GoodsSpecification>(sql).ToList();
        }

        public void Delete(int id)
        {
            var sql = $@"delete from GoodsSpecification where id=@id";

            _context.Execute(sql, new { id = id });
        }
    }
}
