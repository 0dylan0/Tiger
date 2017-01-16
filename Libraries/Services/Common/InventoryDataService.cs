using Core.Domain;
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
    public class InventoryDataService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;
        private readonly TransferCargoDataService _transferCargoDataService;
        private readonly WarehouseService _warehouseService;
        private readonly DbHelper _dbHelper;

        public InventoryDataService(DapperRepository repository,
            IDbConnection context,
            TransferCargoDataService transferCargoDataService,
            WarehouseService warehouseService,
            DbHelper dbHelper)
        {
            _repository = repository;
            _context = context;
            _transferCargoDataService = transferCargoDataService;
            _warehouseService = warehouseService;
            _dbHelper = dbHelper;
        }

        public int Insert(InventoryData InventoryData)
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
                    Warehouse_Name,
                    Active,
                    ShipmentsQuantity,
                    RemainingQuantity)
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
                    @WarehouseName,
                    @Active,
                    @ShipmentsQuantity,           
                    @RemainingQuantity) select @@identity";
            return _context.QuerySingle<int>(sql, new
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
                WarehouseName = InventoryData.WarehouseName,
                Active = InventoryData.Active,
                ShipmentsQuantity = InventoryData.ShipmentsQuantity,
                RemainingQuantity = InventoryData.RemainingQuantity
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
                    PurchaseDate=@PurchaseDate,
                    ShipmentsDate=@ShipmentsDate,
                    Warehouse_ID=@WarehouseID,
                    Warehouse_Name=@WarehouseName,
                    Active=@Active,
                    ShipmentsQuantity=@ShipmentsQuantity,
                    RemainingQuantity=@RemainingQuantity 
                    WHERE ID=@ID";
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
                PurchaseDate = InventoryData.PurchaseDate,
                ShipmentsDate = InventoryData.ShipmentsDate,
                WarehouseID = InventoryData.WarehouseID,
                WarehouseName = InventoryData.WarehouseName,
                Active = InventoryData.Active,
                ShipmentsQuantity = InventoryData.ShipmentsQuantity,
                RemainingQuantity = InventoryData.RemainingQuantity,
                ID= InventoryData.ID
            });
        }

        public InventoryData GetById(int id)
        {
            var sql = @"select * from InventoryData  where id = @id";
            return _context.QuerySingleOrDefault<InventoryData>(sql, new
            {
                id = id
            });
        }
        public IPagedList<InventoryData> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from InventoryData where Active='1' ";
            var Parameter = new DynamicParameters();
            if (!string.IsNullOrEmpty(textQuery))
            {
                sql += " and Goods_Name like @textQuery";
                textQuery = textQuery.Contains("%") ? textQuery : $"%{textQuery}%";
                Parameter.Add("textQuery", textQuery);
            }
            return new SqlPagedList<InventoryData>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

        public void Delete(int id)
        {
            var sql = $@"delete from InventoryData where id=@id";

            _context.Execute(sql, new { id = id });
        }

        public bool ClickTransferCargo(int id, int newNum, int newWarehouseID)
        {
            return _dbHelper.ExecuteTransaction(
            t =>
            {
                var inventoryData=_context.QuerySingleOrDefault<InventoryData>("select * from InventoryData  where id = @id", new { id = id }, t);
                //var inventoryData = GetById(id);
                int OldQuantity = inventoryData.InventoryQuantity;
                //更改之前的库存信息               
                inventoryData.InventoryQuantity = inventoryData.InventoryQuantity - newNum;
                inventoryData.InventorySum = inventoryData.CostPrice * inventoryData.InventoryQuantity;
                inventoryData.ShipmentsQuantity = inventoryData.ShipmentsQuantity - newNum;
                inventoryData.RemainingQuantity = inventoryData.RemainingQuantity - newNum;
                //Update(inventoryData);

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
                    PurchaseDate=@PurchaseDate,
                    ShipmentsDate=@ShipmentsDate,
                    Warehouse_ID=@WarehouseID,
                    Warehouse_Name=@WarehouseName,
                    Active=@Active,
                    ShipmentsQuantity=@ShipmentsQuantity,
                    RemainingQuantity=@RemainingQuantity 
                    WHERE ID=@ID";
                _context.Execute(sql, new
                {
                    GoodsID = inventoryData.GoodsID,
                    GoodsName = inventoryData.GoodsName,
                    Unit = inventoryData.Unit,
                    Specification = inventoryData.Specification,
                    GoodsType = inventoryData.GoodsType,
                    Brand = inventoryData.Brand,
                    InventoryQuantity = inventoryData.InventoryQuantity,
                    CostPrice = inventoryData.CostPrice,
                    InventorySum = inventoryData.InventorySum,
                    LastInventoryDate = inventoryData.LastInventoryDate,
                    FinalSaleDate = inventoryData.FinalSaleDate,
                    PurchaseDate = inventoryData.PurchaseDate,
                    ShipmentsDate = inventoryData.ShipmentsDate,
                    WarehouseID = inventoryData.WarehouseID,
                    WarehouseName = inventoryData.WarehouseName,
                    Active = inventoryData.Active,
                    ShipmentsQuantity = inventoryData.ShipmentsQuantity,
                    RemainingQuantity = inventoryData.RemainingQuantity,
                    ID = inventoryData.ID
                }, t);



                //添加新的库存信息
                //var newWarehouse = _warehouseService.GetById(newWarehouseID);
                var newWarehouse = _context.QuerySingleOrDefault<Warehouse>("select * from Warehouse  where id = @id", new { id = id }, t);


                InventoryData newInventoryData = new InventoryData()
                {
                    WarehouseID = newWarehouseID,
                    WarehouseName = newWarehouse.Name,
                    GoodsID = inventoryData.GoodsID,
                    GoodsName = inventoryData.GoodsName,
                    PurchaseDate = inventoryData.PurchaseDate,
                    ShipmentsDate = inventoryData.ShipmentsDate,

                    Unit = inventoryData.Unit,
                    Specification = inventoryData.Specification,
                    GoodsType = inventoryData.GoodsType,
                    Brand = inventoryData.Brand,

                    InventoryQuantity = newNum,
                    CostPrice = inventoryData.CostPrice,
                    InventorySum = inventoryData.CostPrice * newNum,
                    LastInventoryDate = inventoryData.LastInventoryDate,

                    FinalSaleDate = inventoryData.FinalSaleDate,
                    SupplierID = inventoryData.SupplierID,
                    SupplierName = inventoryData.SupplierName,
                    SupplierAddress = inventoryData.SupplierAddress,

                    Active = inventoryData.Active,
                    ShipmentsQuantity = inventoryData.ShipmentsQuantity - newNum,
                    RemainingQuantity = inventoryData.RemainingQuantity - newNum

                };
                //Insert(newInventoryData);
                var sql1 = $@"insert into InventoryData(
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
                    Warehouse_Name,
                    Active,
                    ShipmentsQuantity,
                    RemainingQuantity)
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
                    @WarehouseName,
                    @Active,
                    @ShipmentsQuantity,           
                    @RemainingQuantity) ";
                 _context.Execute(sql1, new
                {
                    GoodsID = newInventoryData.GoodsID,
                    GoodsName = newInventoryData.GoodsName,
                    Unit = newInventoryData.Unit,
                    Specification = newInventoryData.Specification,
                    GoodsType = newInventoryData.GoodsType,
                    Brand = newInventoryData.Brand,
                    InventoryQuantity = newInventoryData.InventoryQuantity,
                    CostPrice = newInventoryData.CostPrice,
                    InventorySum = newInventoryData.InventorySum,
                    LastInventoryDate = newInventoryData.LastInventoryDate,
                    FinalSaleDate = newInventoryData.FinalSaleDate,
                    SupplierID = newInventoryData.SupplierID,
                    SupplierName = newInventoryData.SupplierName,
                    SupplierAddress = newInventoryData.SupplierAddress,
                    PurchaseDate = newInventoryData.PurchaseDate,
                    ShipmentsDate = newInventoryData.ShipmentsDate,
                    WarehouseID = newInventoryData.WarehouseID,
                    WarehouseName = newInventoryData.WarehouseName,
                    Active = newInventoryData.Active,
                    ShipmentsQuantity = newInventoryData.ShipmentsQuantity,
                    RemainingQuantity = newInventoryData.RemainingQuantity
                },t);

                //添加到调货信息一条记录
                TransferCargoData transferCargoData = new TransferCargoData()
                {
                    GoodsID = inventoryData.GoodsID,
                    GoodsName = inventoryData.GoodsName,
                    SupplierID = inventoryData.SupplierID,
                    SupplierName = inventoryData.SupplierName,
                    OldWarehouseID = inventoryData.WarehouseID,
                    OldWarehouseName = inventoryData.WarehouseName,
                    OldQuantity = OldQuantity,
                    NewWarehouseID = newWarehouseID,
                    NewWarehouseName = newWarehouse.Name,
                    NewQuantity = newNum,
                    Date = DateTime.Now
                };
                //_transferCargoDataService.Insert(transferCargoData);
                var sql2 = $@"insert into TransferCargoData(
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
                _context.Execute(sql2, new
                {
                    GoodsID = transferCargoData.GoodsID,
                    GoodsName = transferCargoData.GoodsName,
                    SupplierID = transferCargoData.SupplierID,
                    SupplierName = transferCargoData.SupplierName,

                    OldWarehouseID = transferCargoData.OldWarehouseID,
                    OldWarehouseName = transferCargoData.OldWarehouseName,
                    OldQuantity = transferCargoData.OldQuantity,

                    NewWarehouseID = transferCargoData.NewWarehouseID,
                    NewWarehouseName = transferCargoData.NewWarehouseName,
                    NewQuantity = transferCargoData.NewQuantity,

                    Date = transferCargoData.Date
                },t);

            });
        }

    }
}
