using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public class ArrearsDetails
    {
        public int ID { get; set; }

        public int GoodesID { get; set; }

        public string GoodsName { get; set; }

        public int ArrearsID { get; set; }

        public int SalesShipmentsDataID { get; set; }

        //数量
        public decimal Quantity { get; set; }

        //单价
        public decimal UnitPrice { get; set; }

        //总金额（初始化后不要改变）
        public decimal Sum { get; set; }

        //欠款金额
        public decimal ArrearsAmount { get; set; }
    }
}
