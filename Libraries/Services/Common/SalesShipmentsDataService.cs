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
    public class SalesShipmentsDataService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public SalesShipmentsDataService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(SalesShipmentsData SalesShipmentsData)
        {
            var sql = $@"insert into SalesShipmentsData(
                    Goods_ID,
                    Goods_Name,
                    Date,
                    Unit,
                    Specification,
                    GoodsType,
                    Brand,
                    Quantity,
                    UnitPrice,
                    Cost,
                    Profit,
                    Sum,
                    Total,
                    Remarks,
                    Warehouse_ID,
                    Warehouse_Name，
                    InventoryData_ID,
                    Active,
                    Freight,
                    ClientData_ID,
                    ClientData_Name)
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
                    @Cost,
                    @Profit,
                    @Sum,
                    @Total,
                    @Remarks,
                    @WarehouseID,
                    @WarehouseName,
                    @InventoryDataID,
                    @Active,
                    @Freight,
                    @ClientDataID,
                    @ClientDataName)";
            _context.Execute(sql, new
            {
                GoodsID = SalesShipmentsData.GoodsID,
                GoodsName = SalesShipmentsData.GoodsName,
                Date = SalesShipmentsData.Date,
                Unit = SalesShipmentsData.Unit,
                Specification = SalesShipmentsData.Specification,
                GoodsType = SalesShipmentsData.GoodsType,
                Brand = SalesShipmentsData.Brand,
                Quantity = SalesShipmentsData.Quantity,
                UnitPrice = SalesShipmentsData.UnitPrice,
                Cost = SalesShipmentsData.Cost,
                Profit = SalesShipmentsData.Profit,
                Sum = SalesShipmentsData.Sum,
                Total = SalesShipmentsData.Total,
                Remarks = SalesShipmentsData.Remarks,
                WarehouseID = SalesShipmentsData.WarehouseID,
                WarehouseName = SalesShipmentsData.WarehouseName,
                InventoryDataID = SalesShipmentsData.InventoryDataID,
                Active = SalesShipmentsData.Active,
                Freight = SalesShipmentsData.Freight,
                ClientDataID = SalesShipmentsData.ClientDataID,
                ClientDataName = SalesShipmentsData.ClientDataName
            });
        } 

        public void Update(SalesShipmentsData SalesShipmentsData)
        {
            var sql = $@"update SalesShipmentsData set
                    Goods_ID=@GoodsID,
                    Goods_Name=@GoodsName,
                    Date=@Date,
                    Unit=@Unit,
                    Specification=@Specification,
                    GoodsType=@GoodsType,
                    Brand=@Brand,
                    Quantity=@Quantity,
                    UnitPrice=@UnitPrice,
                    Cost=@Cost,
                    Profit=@Profit,
                    Sum=@Sum,
                    Total=@Total,
                    Remarks=@Remarks,
                    ClientData_ID=@ClientDataID,
                    ClientData_Name=@ClientDataName)
                    where ID=@ID";
            _context.Execute(sql, new
            {
                GoodsID = SalesShipmentsData.GoodsID,
                GoodsName = SalesShipmentsData.GoodsName,
                Unit = SalesShipmentsData.Unit,
                Specification = SalesShipmentsData.Specification,
                GoodsType = SalesShipmentsData.GoodsType,
                Brand = SalesShipmentsData.Brand,
                Quantity = SalesShipmentsData.Quantity,
                UnitPrice = SalesShipmentsData.UnitPrice,
                Cost = SalesShipmentsData.Cost,
                Profit = SalesShipmentsData.Profit,
                Sum = SalesShipmentsData.Sum,
                Total = SalesShipmentsData.Total,
                Remarks = SalesShipmentsData.Remarks,
                ClientDataID = SalesShipmentsData.ClientDataID,
                ClientDataName = SalesShipmentsData.ClientDataName
            });

        }

        public SalesShipmentsData GetById(int id)
        {
            var sql = @"select * from SalesShipmentsData  where id = @id";
            return _context.QuerySingle<SalesShipmentsData>(sql, new
            {
                id = id
            });
        }

        public SupplierData GetByInventoryDataID(int InventoryDataID)
        {
            var sql = @"select * from SalesShipmentsData  where InventoryData_ID = @InventoryDataID";
            return _context.QuerySingleOrDefault<SupplierData>(sql, new
            {
                InventoryDataID = InventoryDataID
            });
        }

        public IPagedList<SalesShipmentsData> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from SalesShipmentsData";
            var Parameter = new DynamicParameters();
            //Parameter.Add("textQuery", textQuery);
            return new SqlPagedList<SalesShipmentsData>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

    }
}
