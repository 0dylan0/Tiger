using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ArrearsDataModel
    {
        public int ID { get; set; }

        [DisplayName("客户编号")]
        public int ClientDataID { get; set; }

        [DisplayName("客户姓名")]
        public string ClientDataName { get; set; }

        [DisplayName("欠款额")]
        public decimal ArrearsAmount { get; set; }

        [DisplayName("欠款日期")]
        public DateTime Date { get; set; }

        [DisplayName("总欠款额")]
        public decimal Sum { get; set; }

        public List<ArrearsDetailsModel> ArrearsDetailsModels { get; set; }
    }
}