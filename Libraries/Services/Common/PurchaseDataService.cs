﻿using Core.Domain.Common;
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

    public class PurchaseDataService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public PurchaseDataService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(PurchaseData PurchaseData)
        {
            var sql = $@"insert into PurchaseData(
                    Goods_ID,
                    Goods_Name,
                    Date,
                    Unit,
                    Specification,
                    GoodsType,
                    Brand,
                    Quantity,
                    UnitPrice,
                    Sum,
                    Total,
                    Remarks,
                    Supplier_ID,
                    Supplier_Name,
                    Supplier_Address,
                    Warehouse_ID,
                    Warehouse_Name,
                    InventoryData_ID,
                    Active,
                    Freight)
			        VALUES (
                    @GoodsID,
                    @GoodsName,
                    @Date,
                    @Unit,
                    @Specification,
                    @GoodsType,
                    @Brand,
                    @Quantity,
                    @UnitPrice,
                    @Sum,
                    @Total,
                    @Remarks,
                    @SupplierID,
                    @SupplierName,
                    @SupplierAddress,
                    @WarehouseID,
                    @WarehouseName,
                    @InventoryDataID,
                    @Active,
                    @Freight)";
            _context.Execute(sql, new
            {
                GoodsID= PurchaseData.GoodsID,
                GoodsName = PurchaseData.GoodsName,
                Date=PurchaseData.Date,
                Unit = PurchaseData.Unit,
                Specification = PurchaseData.Specification,
                GoodsType = PurchaseData.GoodsType,
                Brand = PurchaseData.Brand,
                Quantity = PurchaseData.Quantity,
                UnitPrice = PurchaseData.UnitPrice,
                Sum= PurchaseData.Sum,
                Total= PurchaseData.Total,
                Remarks= PurchaseData.Remarks,
                SupplierID = PurchaseData.SupplierID,
                SupplierName = PurchaseData.SupplierName,
                SupplierAddress = PurchaseData.SupplierAddress,
                WarehouseID=PurchaseData.WarehouseID,
                WarehouseName=PurchaseData.WarehouseName,
                InventoryDataID = PurchaseData.InventoryDataID,
                Active=PurchaseData.Active,
                Freight=PurchaseData.Freight
            });
        }

        public void Update(PurchaseData PurchaseData)
        {
            var sql = $@"update PurchaseData set
                    Goods_ID=@GoodsID,
                    Goods_Name=@GoodsName,
                    Date=@Date,
                    Unit=@Unit,
                    Specification=@Specification,
                    GoodsType=@GoodsType,
                    Brand=@Brand,
                    Quantity=@Quantity,
                    UnitPrice=@UnitPrice,
                    Sum=@Sum,
                    Total=@Total,
                    Remarks=@Remarks,
                    Supplier_ID=@SupplierID,
                    Supplier_Name=@SupplierName,
                    Supplier_Address=@SupplierAddress,
                    Warehouse_ID=@WarehouseID,
                    Warehouse_Name=@WarehouseName,
                    InventoryData_ID=@InventoryDataID,
                    Active=@Active,
                    Freight=@Freight 
                    where ID=@ID";
            _context.Execute(sql, new
            {
                ID = PurchaseData.ID,
                GoodsID = PurchaseData.GoodsID,
                GoodsName = PurchaseData.GoodsName,
                Date = PurchaseData.Date,
                Unit = PurchaseData.Unit,
                Specification = PurchaseData.Specification,
                GoodsType = PurchaseData.GoodsType,
                Brand = PurchaseData.Brand,
                Quantity = PurchaseData.Quantity,
                UnitPrice = PurchaseData.UnitPrice,
                Sum = PurchaseData.Sum,
                Total = PurchaseData.Total,
                Remarks = PurchaseData.Remarks,
                SupplierID = PurchaseData.SupplierID,
                SupplierName = PurchaseData.SupplierName,
                SupplierAddress = PurchaseData.SupplierAddress,
                WarehouseID =PurchaseData.WarehouseID,
                WarehouseName=PurchaseData.WarehouseName,
                InventoryDataID = PurchaseData.InventoryDataID,
                Active = PurchaseData.Active,
                Freight = PurchaseData.Freight
            });

        }

        public PurchaseData GetById(int id)
        {
            var sql = @"select * from PurchaseData  where id = @id";
            return _context.QuerySingle<PurchaseData>(sql, new
            {
                id = id
            });
        }

        public PurchaseData GetByInventoryDataId(int id)
        {
            var sql = @"select * from PurchaseData  where InventoryData_ID = @id";
            var purchaseData = _context.QueryFirstOrDefault<PurchaseData>(sql, new { id = id });
            if (purchaseData!=null)
            {
                return purchaseData;
            }
            else
            {
                var sql2 = @"select OldInventoryData_ID from TransferCargoData  where InventoryData_ID = @id";
                var OldInventoryDataID = _context.QuerySingle<int>(sql2, new { id = id });
                return _context.QuerySingle<PurchaseData>(sql, new { id = OldInventoryDataID });
            }
             
        }

        public PurchaseData GetByInventoryDataID(int id)
        {
            var sql = @"select * from PurchaseData  where InventoryData_ID = @id";
            return _context.QueryFirstOrDefault<PurchaseData>(sql, new
            {
                id = id
            });
        }

        public PurchaseData GetTransferCargoDataOldIdByInventoryDataID(int id)
        {
            var sql = @"select OldInventoryData_ID from TransferCargoData  where InventoryData_ID = @id";
            var OldInventoryDataID = _context.QuerySingle<int>(sql, new { id = id });
            var sql1 = @"select * from PurchaseData  where InventoryData_ID = @id";
            return _context.QueryFirstOrDefault<PurchaseData>(sql1, new
            {
                id = OldInventoryDataID
            });
        }

        public IPagedList<PurchaseData> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from PurchaseData where Active='1' ";
            var Parameter = new DynamicParameters();
            if(!string.IsNullOrEmpty(textQuery))
            {
                sql += " and Goods_Name like @textQuery";
                textQuery = textQuery.Contains("%") ? textQuery : $"%{textQuery}%";
                Parameter.Add("textQuery", textQuery);
            }

            return new SqlPagedList<PurchaseData>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

        public void Delete(int id)
        {
            var sql = $@"delete from PurchaseData where id=@id";

            _context.Execute(sql, new { id = id });
        }

    }
}
