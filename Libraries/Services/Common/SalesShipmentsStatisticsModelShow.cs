using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class SalesShipmentsStatisticsModelShow
    {
        public int GoodsID { get; set; }

        public string GoodesName { get; set; }

        //客户ID
        public int ClientDataID { get; set; }

        public string ClientDataName { get; set; }


        //总金额
        public decimal TotalSum { get; set; }

        //总成本
        public decimal TotalCost { get; set; }

        //总利润
        public decimal TotalProfit { get; set; }

        //总数量
        public int TotalNum { get; set; }

        //销售日期
        public DateTime Date { get; set; }
    }
}
