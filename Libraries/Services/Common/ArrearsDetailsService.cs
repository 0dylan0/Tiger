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
    public class ArrearsDetailsService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public ArrearsDetailsService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(ArrearsDetails ArrearsDetails)
        {
            var sql = $@"insert into ArrearsDetails(
                    Goods_ID,
                    Goods_Name,
                    Arrears_ID,
                    SalesShipmentsData_ID,
                    Quantity,
                    UnitPrice,
                    ArrearsAmount,
                    Sum)
			        VALUES (
                    @GoodsID,
                    @GoodsName,
                    @ArrearsID,
                    @SalesShipmentsDataID,
                    @Quantity,
                    @UnitPrice,
                    @ArrearsAmount,
                    @Sum)";
            _context.Execute(sql, new
            {
                GoodsID = ArrearsDetails.GoodsID,
                GoodsName = ArrearsDetails.GoodsName,
                ArrearsID = ArrearsDetails.ArrearsID,
                SalesShipmentsDataID = ArrearsDetails.SalesShipmentsDataID,
                Quantity = ArrearsDetails.Quantity,
                UnitPrice = ArrearsDetails.UnitPrice,
                ArrearsAmount = ArrearsDetails.ArrearsAmount,
                Sum = ArrearsDetails.Sum
            });
        }

        public void Update(ArrearsDetails ArrearsDetails)
        {
            var sql = $@"update ArrearsDetails set
                    Goods_ID=@GoodsID,
                    Goods_Name=@GoodsName,
                    Arrears_ID=@ArrearsID,
                    SalesShipmentsData_ID=@SalesShipmentsDataID,
                    Quantity=@Quantity,
                    UnitPrice=@UnitPrice,
                    ArrearsAmount=@ArrearsAmount,
                    Sum=@Sum
                    where ID=@ID";
            _context.Execute(sql, new
            {
                GoodsID = ArrearsDetails.GoodsID,
                GoodsName = ArrearsDetails.GoodsName,
                ArrearsID = ArrearsDetails.ArrearsID,
                SalesShipmentsDataID = ArrearsDetails.SalesShipmentsDataID,
                Quantity = ArrearsDetails.Quantity,
                UnitPrice = ArrearsDetails.UnitPrice,
                ArrearsAmount = ArrearsDetails.ArrearsAmount,
                Sum = ArrearsDetails.Sum
            });
        }

        public ArrearsDetails GetById(int id)
        {
            var sql = @"select * from ArrearsDetails  where id = @id";
            return _context.QuerySingle<ArrearsDetails>(sql, new
            {
                id = id
            });
        }
        public IPagedList<ArrearsDetails> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from ArrearsDetails ";
            var Parameter = new DynamicParameters();
            if (!string.IsNullOrEmpty(textQuery))
            {
                sql += " where Goods_Name like @textQuery";
                textQuery = textQuery.Contains("%") ? textQuery : $"%{textQuery}%";
                Parameter.Add("textQuery", textQuery);
            }

            return new SqlPagedList<ArrearsDetails>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }


    }
}
