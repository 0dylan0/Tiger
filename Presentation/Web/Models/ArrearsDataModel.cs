using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ArrearsDataModel
    {
        public int ID { get; set; }

        public int ClientDataID { get; set; }

        public string ClientDataName { get; set; }

        public decimal ArrearsAmount { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public List<ArrearsDetailsModel> ArrearsDetailsModels { get; set; }
    }
}