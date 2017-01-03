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
    public class WarehouseService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public WarehouseService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(Warehouse Warehouse)
        {
            var sql = $@"insert into Warehouse(
                    Name)
			        VALUES (
                    @Name)";
            _context.Execute(sql, new
            {
                Name = Warehouse.Name
            });
        }

        public void Update(Warehouse Warehouse)
        {
            var sql = $@"update Warehouse set
                    Name=@Name
                    where ID=@ID";
            _context.Execute(sql, new
            {
                ID = Warehouse.ID,
                Name = Warehouse.Name
            });
        }

        public Warehouse GetById(int id)
        {
            var sql = @"select * from Warehouse  where id = @id";
            return _context.QuerySingle<Warehouse>(sql, new
            {
                id = id
            });
        }
        public IPagedList<Warehouse> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from Warehouse";
            var Parameter = new DynamicParameters();
            return new SqlPagedList<Warehouse>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

        public List<GetList> GetWarehouseList()
        {
            string sql = @"select id as code, name from Warehouse";

            return _context.Query<GetList>(sql).ToList();
        }
    }
}
