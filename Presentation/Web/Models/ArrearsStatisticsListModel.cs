using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class ArrearsStatisticsListModel
    {
        [DisplayName("商品")]
        public int GoodsID { get; set; }

        public IEnumerable<SelectListItem> GoodsList { get; set; }

        [DisplayName("开始日期")]
        public DateTime FromDate { get; set; }

        [DisplayName("结束日期")]
        public DateTime ToDate { get; set; }

        [DisplayName("客户")]
        public int ClientDataID { get; set; }

        public IEnumerable<SelectListItem> ClientDataList { get; set; }

        public IList<ArrearsStatisticsModel> ArrearsStatistics { get; set; }
    }
}