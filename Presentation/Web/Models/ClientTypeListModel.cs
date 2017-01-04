using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ClientTypeListModel
    {
        public string Name { get; set; }

        public IList<ClientTypeModel> ClientType { get; set; }
    }
}