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
    public class GoodsDataService
    {

        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public GoodsDataService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(GoodsData GoodsData)
        {
           // var sql = $@"insert into Users(
           //         [Name],
           //         [Password])
			        //VALUES (
           //         @Name,
           //         @Password)";


           // _context.Execute(sql, new
           // {
           //     Name = GoodsData.Name,
           //     Password = GoodsData.Password
           // });
        }

        public void Update(GoodsData GoodsData)
        {
           // var sql = $@"insert into Users(
           //         [Name],
           //         [Password])
			        //VALUES (
           //         @Name,
           //         @Password)";


           // _context.Execute(sql, new
           // {
           //     Name = GoodsData.Name,
           //     Password = GoodsData.Password
           // });

        }

        public GoodsData GetUserById(int id)
        {
            var sql = @"select * from GoodsData  where id = @id";

            return _context.QuerySingle<GoodsData>(sql, new
            {
                id = id
            });
        }
        public IPagedList<GoodsData> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from GoodsData";

            var Parameter = new DynamicParameters();
            //Parameter.Add("textQuery", textQuery);
            return new SqlPagedList<GoodsData>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }
    }
}
