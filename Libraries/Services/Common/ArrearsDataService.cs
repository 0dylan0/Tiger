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
    public class ArrearsDataService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public ArrearsDataService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(ArrearsData ArrearsData)
        {
            var sql = $@"insert into ArrearsData(
                    ClientData_ID,
                    ClientData_Name,
                    ArrearsAmount,
                    Date,
                    Sum)
			        VALUES (
                    @ClientDataID,
                    @ClientDataName,
                    @ArrearsAmount,
                    @Date,
                    @Sum)";
            _context.Execute(sql, new
            {
                ClientDataID = ArrearsData.ClientDataID,
                ClientDataName = ArrearsData.ClientDataName,
                ArrearsAmount = ArrearsData.ArrearsAmount,
                Date = ArrearsData.Date,
                Sum = ArrearsData.Sum
            });
        }

        public void Update(ArrearsData ArrearsData)
        {
            var sql = $@"update ArrearsData set
                    ClientData_ID=@ClientDataID,
                    ClientData_Name=@ClientDataName,
                    ArrearsAmount=@ArrearsAmount,
                    Date=@Date,
                    Sum=@Sum
                    where ID=@ID";
            _context.Execute(sql, new
            {
                ID = ArrearsData.ID,
                ClientDataID = ArrearsData.ClientDataID,
                ClientDataName = ArrearsData.ClientDataName,
                ArrearsAmount = ArrearsData.ArrearsAmount,
                Date = ArrearsData.Date,
                Sum = ArrearsData.Sum
            });

        }

        public ArrearsData GetById(int id)
        {
            var sql = @"select * from ArrearsData  where id = @id";
            return _context.QuerySingle<ArrearsData>(sql, new
            {
                id = id
            });
        }
        public IPagedList<ArrearsData> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from ArrearsData ";
            var Parameter = new DynamicParameters();
            if (!string.IsNullOrEmpty(textQuery))
            {
                sql += " where Client_Name like @textQuery";
                textQuery = textQuery.Contains("%") ? textQuery : $"%{textQuery}%";
                Parameter.Add("textQuery", textQuery);
            }

            return new SqlPagedList<ArrearsData>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

    }
}
