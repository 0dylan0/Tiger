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
    public class SalesShipmentsStatisticsService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public SalesShipmentsStatisticsService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public SalesShipmentsStatisticsModelShow GetById(int id)
        {
            var sql = @"select * from SalesShipmentsData  where id = @id";
            return _context.QuerySingle<SalesShipmentsStatisticsModelShow>(sql, new
            {
                id = id
            });
        }

        public IPagedList<SalesShipmentsStatisticsModelShow> GetList(int goodsID, int clientDataID, DateTime fromDate, DateTime toDate, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql1 = @"select '' as GoodsID,'总计' as GoodesName,
                            '' as ClientDataID,'总计' as ClientDataName,
                            sum(Sum) as TotalSum,
                            sum(Quantity*Cost) as TotalCost,
                            sum(Profit) as TotalProfit,
                            sum(Quantity) as TotalNum, 
                            getdate() as Date 
                            from SalesShipmentsData 
                            where active = @ShowInactive                                                          
                            and DATEDIFF(dd, Date, @fromDate) <= 0 
                            and DATEDIFF(dd, Date, @toDate) >= 0 
                             ";
            if (goodsID != 0)
            {
                sql1 = sql1 + @" and Goods_ID=@goodsID ";
            }
            if (clientDataID != 0)
            {
                sql1 = sql1 + @" and ClientData_ID = @clientDataID ";
            }

            sql1 = sql1 + @" union select Goods_ID as GoodsID, 
                            Goods_Name as GoodesName, 
                            ClientData_ID as ClientDataID,ClientData_Name as ClientDataName, 
                            Sum as TotalSum,Quantity*Cost as TotalCost,Profit as TotalProfit, 
                            Quantity as TotalNum,Date as Date 
                            from SalesShipmentsData 
                            where active = @ShowInactive 
                            and DATEDIFF(dd, Date, @fromDate) <= 0 
                            and DATEDIFF(dd, Date, @toDate) >= 0 ";
            if (goodsID != 0)
            {
                sql1 = sql1 + @" and Goods_ID=@goodsID ";
            }
            if (clientDataID != 0)
            {
                sql1 = sql1 + @" and ClientData_ID = @clientDataID ";
            }

            var Parameter = new DynamicParameters();
            Parameter.Add("ShowInactive", "1");
            Parameter.Add("goodsID", goodsID);
            Parameter.Add("clientDataID", clientDataID);
            Parameter.Add("fromDate", fromDate);
            Parameter.Add("toDate", toDate);
            return new SqlPagedList<SalesShipmentsStatisticsModelShow>(sql1, Parameter, pageIndex, pageSize, sortExpression);
        }

    }
}
