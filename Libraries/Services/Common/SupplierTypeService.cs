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
    public class SupplierTypeService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public SupplierTypeService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(SupplierType SupplierType)
        {
            var sql = $@"insert into SupplierType(
                    Name)
			        VALUES (
                    @Name)";
            _context.Execute(sql, new
            {
                Name = SupplierType.Name
            });
        }

        public void Update(SupplierType SupplierType)
        {
            var sql = $@"update SupplierType set
                    Name=@Name
                    where ID=@ID";
            _context.Execute(sql, new
            {
                ID = SupplierType.ID,
                Name = SupplierType.Name
            });
        }

        public SupplierType GetById(int id)
        {
            var sql = @"select * from SupplierType  where id = @id";
            return _context.QuerySingle<SupplierType>(sql, new
            {
                id = id
            });
        }
        public IPagedList<SupplierType> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from SupplierType ";
            var Parameter = new DynamicParameters();
            if (!string.IsNullOrEmpty(textQuery))
            {
                sql += " where Name like @textQuery";
                textQuery = textQuery.Contains("%") ? textQuery : $"%{textQuery}%";
                Parameter.Add("textQuery", textQuery);
            }
            return new SqlPagedList<SupplierType>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

        public List<GetList> GetSupplierTypeList()
        {
            string sql = @"select id as code, name from SupplierType";

            return _context.Query<GetList>(sql).ToList();
        }
    }
}
