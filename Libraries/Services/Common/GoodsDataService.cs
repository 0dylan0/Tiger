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
            var sql = $@"insert into GoodsData(
                    GoodsName,
                    Unit,
                    Brand,
                    GoodType,
                    DefaultPurchasePrice,
                    ActualPurchasePrice,
                    Inventory,
                    Price1,
                    Price2,
                    Price3,
                    Price4,
                    Warehouse,
                    Cost,
                    Image,
                    SingleProfit)
			        VALUES (
                    @GoodsName,
                    @Unit,
                    @Brand,
                    @GoodType,
                    @DefaultPurchasePrice,
                    @ActualPurchasePrice,
                    @Inventory,
                    @Price1,
                    @Price2,
                    @Price3,
                    @Price4,
                    @Warehouse,
                    @Cost,
                    @Image,
                    @SingleProfit)";


            _context.Execute(sql, new
            {
                GoodsName = GoodsData.GoodsName,
                Unit = GoodsData.Unit,
                Brand = GoodsData.Brand,
                GoodType = GoodsData.GoodType,
                DefaultPurchasePrice = GoodsData.DefaultPurchasePrice,
                ActualPurchasePrice = GoodsData.ActualPurchasePrice,
                Inventory = GoodsData.Inventory,
                Price1 = GoodsData.Price1,
                Price2 = GoodsData.Price2,
                Price3 = GoodsData.Price3,
                Price4 = GoodsData.Price4,
                Warehouse = GoodsData.Warehouse,
                Cost = GoodsData.Cost,
                Image = GoodsData.Image,
                SingleProfit = GoodsData.SingleProfit
            });
        }

        public void Update(GoodsData GoodsData)
        {

            var sql = $@"update GoodsData set
                    GoodsName =@GoodsName,
                    Unit=@Unit,
                    Brand=@Brand,
                    GoodType=@GoodType,
                    DefaultPurchasePrice=@DefaultPurchasePrice,
                    ActualPurchasePrice=@ActualPurchasePrice,
                    Inventory=@Inventory,
                    Price1=@Price1,
                    Price2=@Price2,
                    Price3=@Price3,
                    Price4=@Price4,
                    Warehouse=@Warehouse,
                    Cost=@Cost,
                    Image=@Image,
                    SingleProfit=@SingleProfit
                    where ID=@ID";
            _context.Execute(sql, new
            {
                ID = GoodsData.ID,
                GoodsName = GoodsData.GoodsName,
                Unit = GoodsData.Unit,
                Brand = GoodsData.Brand,
                GoodType = GoodsData.GoodType,
                DefaultPurchasePrice = GoodsData.DefaultPurchasePrice,
                ActualPurchasePrice = GoodsData.ActualPurchasePrice,
                Inventory = GoodsData.Inventory,
                Price1 = GoodsData.Price1,
                Price2 = GoodsData.Price2,
                Price3 = GoodsData.Price3,
                Price4 = GoodsData.Price4,
                Warehouse = GoodsData.Warehouse,
                Cost = GoodsData.Cost,
                Image = GoodsData.Image,
                SingleProfit = GoodsData.SingleProfit
            });

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


        public List<GetList> GetGoodsList()
        {
            string sql = @"select ID as code,GoodsName as name from GoodsData";

            return _context.Query<GetList>(sql).ToList();
        }
    }
}
