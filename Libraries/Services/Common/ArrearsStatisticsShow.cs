using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class ArrearsStatisticsShow
    {
        //客户ID
        public int ClientDataID { get; set; }

        public string ClientDataName { get; set; }


        //总欠款余额 不会计算合计
        public decimal ArrearsDataAmount { get; set; }

        //初始总欠款额 不会改变 不会计算合计
        public decimal ArrearsDataSum { get; set; }

        //商品名
        public string GoodsName { get; set; }

        //总数量
        public decimal Quantity { get; set; }

        //总单价 不会计算合计
        public decimal UnitPrice { get; set; }

        //欠款额 （每个字表中的 欠款额合计）
        public decimal Sum { get; set; }

        //欠款余额 每个字表中的 欠款额合计）
        public decimal ArrearsAmount { get; set; }

        //销售日期
        public DateTime Date { get; set; }
    }
}
