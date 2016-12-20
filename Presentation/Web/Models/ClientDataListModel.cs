using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ClientDataListModel
    {
        public string Name { get; set; }

        public IList<ClientDataModel> ClientData { get; set; }
    }
}