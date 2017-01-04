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
    public class ClientTypeService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public ClientTypeService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(ClientType ClientType)
        {
            var sql = $@"insert into ClientType(
                    Name)
			        VALUES (
                    @Name)";
            _context.Execute(sql, new
            {
                Name = ClientType.Name
            });
        }

        public void Update(ClientType ClientType)
        {
            var sql = $@"update ClientType set
                    Name=@Name
                    where ID=@ID";
            _context.Execute(sql, new
            {
                ID = ClientType.ID,
                Name = ClientType.Name
            });
        }

        public ClientType GetById(int id)
        {
            var sql = @"select * from ClientType  where id = @id";
            return _context.QuerySingle<ClientType>(sql, new
            {
                id = id
            });
        }
        public IPagedList<ClientType> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from ClientType";
            var Parameter = new DynamicParameters();
            return new SqlPagedList<ClientType>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

        public List<GetList> GetWarehouseList()
        {
            string sql = @"select id as code, name from ClientType";

            return _context.Query<GetList>(sql).ToList();
        }
    }
}
