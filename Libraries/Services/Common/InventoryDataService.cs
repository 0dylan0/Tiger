using Core.Domain;
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
    public class InventoryDataService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public InventoryDataService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(InventoryData InventoryData)
        {
            var sql = $@"insert into InventoryData(
                    Goods_ID,
                    Goods_Name,
                    Unit,
                    Specification,
                    GoodsType,
                    Brand,
                    InventoryQuantity,
                    CostPrice,
                    InventorySum,
                    LastInventoryDate,
                    FinalSaleDate,
                    Supplier_ID,
                    Supplier_Name,
                    Supplier_Address,
                    PurchaseDate,
                    ShipmentsDate,
                    Warehouse_ID,
                    Warehouse_Name)
			        VALUES (
                    @GoodsID,
                    @GoodsName,
                    @Unit,
                    @Specification,
                    @GoodsType,
                    @Brand,
                    @InventoryQuantity,
                    @CostPrice,
                    @InventorySum,
                    @LastInventoryDate,
                    @FinalSaleDate,
                    @SupplierID,
                    @SupplierName,
                    @SupplierAddress,
                    @PurchaseDate,
                    @ShipmentsDate,
                    @WarehouseID,
                    @WarehouseName)";
            _context.Execute(sql, new
            {
                GoodsID = InventoryData.GoodsID,
                GoodsName = InventoryData.GoodsName,
                Unit = InventoryData.Unit,
                Specification = InventoryData.Specification,
                GoodsType = InventoryData.GoodsType,
                Brand = InventoryData.Brand,
                InventoryQuantity = InventoryData.InventoryQuantity,
                CostPrice = InventoryData.CostPrice,
                InventorySum = InventoryData.InventorySum,
                LastInventoryDate = InventoryData.LastInventoryDate,
                FinalSaleDate = InventoryData.FinalSaleDate,
                SupplierID = InventoryData.SupplierID,
                SupplierName = InventoryData.SupplierName,
                SupplierAddress = InventoryData.SupplierAddress,
                PurchaseDate = InventoryData.PurchaseDate,
                ShipmentsDate = InventoryData.ShipmentsDate,
                WarehouseID = InventoryData.WarehouseID,
                WarehouseName = InventoryData.WarehouseName
            });
        }

        public void Update(InventoryData InventoryData)
        {
            var sql = $@"update InventoryData set
                    Goods_ID=@GoodsID,
                    Goods_Name=@GoodsName,
                    Unit=@Unit,
                    Specification=@Specification,
                    GoodsType=@GoodsType,
                    Brand=@Brand,
                    InventoryQuantity=@InventoryQuantity,
                    CostPrice=@CostPrice,
                    InventorySum=@InventorySum,
                    LastInventoryDate=@LastInventoryDate,
                    FinalSaleDate=@FinalSaleDate,
                    Supplier_ID=@SupplierID,
                    Supplier_Name=@SupplierName,
                    Supplier_Address=@SupplierAddress,
                    PurchaseDate=@PurchaseDate,
                    ShipmentsDate=@ShipmentsDate,
                    Warehouse_ID=@WarehouseID,
                    Warehouse_Name=@WarehouseName)
                    where ID=@ID";
            _context.Execute(sql, new
            {
                ID = InventoryData.ID,
                GoodsID = InventoryData.GoodsID,
                GoodsName = InventoryData.GoodsName,
                Unit = InventoryData.Unit,
                Specification = InventoryData.Specification,
                GoodsType = InventoryData.GoodsType,
                Brand = InventoryData.Brand,
                InventoryQuantity = InventoryData.InventoryQuantity,
                CostPrice = InventoryData.CostPrice,
                InventorySum = InventoryData.InventorySum,
                LastInventoryDate = InventoryData.LastInventoryDate,
                FinalSaleDate = InventoryData.FinalSaleDate,
                SupplierID = InventoryData.SupplierID,
                SupplierName = InventoryData.SupplierName,
                SupplierAddress = InventoryData.SupplierAddress,
                PurchaseDate = InventoryData.PurchaseDate,
                ShipmentsDate = InventoryData.ShipmentsDate,
                WarehouseID = InventoryData.WarehouseID,
                WarehouseName = InventoryData.WarehouseName
            });
        }

        public InventoryData GetById(int id)
        {
            var sql = @"select * from InventoryData  where id = @id";
            return _context.QuerySingle<InventoryData>(sql, new
            {
                id = id
            });
        }
        public IPagedList<InventoryData> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from InventoryData";
            var Parameter = new DynamicParameters();
            //Parameter.Add("textQuery", textQuery);
            return new SqlPagedList<InventoryData>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }
    }
}
