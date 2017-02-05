using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public class ArrearsData
    {
        public int ID { get; set; }

        public int ClientDataID { get; set; }

        public string ClientDataName { get; set; }

        public decimal ArrearsAmount { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }
    }
}
