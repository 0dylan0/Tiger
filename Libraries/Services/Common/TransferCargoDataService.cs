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
    public class TransferCargoDataService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public TransferCargoDataService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(TransferCargoData TransferCargoData)
        {
            var sql = $@"insert into TransferCargoData(
                    Goods_ID,
                    Goods_Name,
                    Supplier_ID,
                    Supplier_Name,
                    
                    OldWarehouse_ID,
                    OldWarehouse_Name,
                    OldQuantity,

                    NewWarehouse_ID,
                    NewWarehouse_Name,
                    NewQuantity,
                    Date)
			        VALUES (
                    @GoodsID,
                    @GoodsName,                  
                    @SupplierID,
                    @SupplierName,

                    @OldWarehouseID,
                    @OldWarehouseName,
                    @OldQuantity,
                    
                    @NewWarehouseID,
                    @NewWarehouseName,
                    @NewQuantity,
                    @Date)";
            _context.Execute(sql, new
            {
                GoodsID = TransferCargoData.GoodsID,
                GoodsName = TransferCargoData.GoodsName,
                SupplierID = TransferCargoData.SupplierID,
                SupplierName = TransferCargoData.SupplierName,

                OldWarehouseID = TransferCargoData.OldWarehouseID,
                OldWarehouseName = TransferCargoData.OldWarehouseName,
                OldQuantity = TransferCargoData.OldQuantity,

                NewWarehouseID = TransferCargoData.NewWarehouseID,
                NewWarehouseName = TransferCargoData.NewWarehouseName,
                NewQuantity = TransferCargoData.NewQuantity,

                Date= TransferCargoData.Date
            });
        }

        public void Update(TransferCargoData TransferCargoData)
        {
            var sql = $@"update TransferCargoData set
                    Goods_ID=@GoodsID,
                    Goods_Name=@GoodsName,
                    Supplier_ID=@SupplierID,
                    Supplier_Name=@SupplierName,

                    OldWarehouse_ID=@OldWarehouseID,
                    OldWarehouse_Name=@OldWarehouseName,
                    OldQuantity=@OldQuantity,

                    NewWarehouse_ID=@NewWarehouseID,
                    NewWarehouse_Name=@NewWarehouseName,
                    NewQuantity=@NewQuantity,
                    Date=@Date  
                    where ID=@ID";
            _context.Execute(sql, new
            {
                GoodsID = TransferCargoData.GoodsID,
                GoodsName = TransferCargoData.GoodsName,
                SupplierID = TransferCargoData.SupplierID,
                SupplierName = TransferCargoData.SupplierName,

                OldWarehouseID = TransferCargoData.OldWarehouseID,
                OldWarehouseName = TransferCargoData.OldWarehouseName,
                OldQuantity = TransferCargoData.OldQuantity,

                NewWarehouseID = TransferCargoData.NewWarehouseID,
                NewWarehouseName = TransferCargoData.NewWarehouseName,
                NewQuantity = TransferCargoData.NewQuantity,

                Date = TransferCargoData.Date
            });

        }

        public TransferCargoData GetById(int id)
        {
            var sql = @"select * from TransferCargoData  where id = @id";
            return _context.QuerySingle<TransferCargoData>(sql, new
            {
                id = id
            });
        }

        public IPagedList<TransferCargoData> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from TransferCargoData  ";
            var Parameter = new DynamicParameters();
            if (!string.IsNullOrEmpty(textQuery))
            {
                sql += "where Goods_Name like @textQuery";
                textQuery = textQuery.Contains("%") ? textQuery : $"%{textQuery}%";
                Parameter.Add("textQuery", textQuery);
            }

            return new SqlPagedList<TransferCargoData>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

        public void Delete(int id)
        {
            var sql = $@"delete from TransferCargoData where id=@id";

            _context.Execute(sql, new { id = id });
        }

    }
}
