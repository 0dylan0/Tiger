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
    public class ArrearsStatisticsService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public ArrearsStatisticsService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public IPagedList<ArrearsStatisticsShow> GetList(int goodsID, int clientDataID, DateTime fromDate, DateTime toDate, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql1 = @"select  '' as ClientDataID,'总计' as ClientDataName,
                            0.00 as ArrearsDataAmount ,0.00 as ArrearsDataSum ,'' as GoodsID,'' as GoodsName,
                            sum(Quantity) as Quantity, 0.00 as UnitPrice, sum(AD.Sum) as Sum,
                            sum(AD.ArrearsAmount) as ArrearsAmount,getdate() as Date
                            from ArrearsDetails as AD  LEFT JOIN ArrearsData A on AD.Arrears_ID=A.ID
                            where  DATEDIFF(dd, Date, @fromDate) <= 0 
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

            sql1 = sql1 + @" union
                            select  ClientData_ID as ClientDataID,ClientData_Name as ClientDataName,
                            A.ArrearsAmount as ArrearsDataAmount , A.sum as ArrearsDataSum,Goods_ID as GoodsID, Goods_Name as GoodsName,
                            Quantity as Quantity,UnitPrice as UnitPrice,AD.Sum as Sum,
                            AD.ArrearsAmount as ArrearsAmount,date as Date
                            from ArrearsDetails as AD  LEFT JOIN ArrearsData A on AD.Arrears_ID=A.ID
                            where  DATEDIFF(dd, Date, @fromDate) <= 0 
                            and DATEDIFF(dd, Date, @toDate) >= 0  ";
            if (goodsID != 0)
            {
                sql1 = sql1 + @" and Goods_ID=@goodsID ";
            }
            if (clientDataID != 0)
            {
                sql1 = sql1 + @" and ClientData_ID = @clientDataID ";
            }

            var Parameter = new DynamicParameters();
            //Parameter.Add("ShowInactive", "1");
            Parameter.Add("goodsID", goodsID);
            Parameter.Add("clientDataID", clientDataID);
            Parameter.Add("fromDate", fromDate);
            Parameter.Add("toDate", toDate);
            return new SqlPagedList<ArrearsStatisticsShow>(sql1, Parameter, pageIndex, pageSize, sortExpression);
        }

    }
}
