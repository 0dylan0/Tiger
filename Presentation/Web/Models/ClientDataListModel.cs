using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ClientDataListModel
    {
        [DisplayName("客户名")]
        public string Name { get; set; }

        public IList<ClientDataModel> ClientData { get; set; }
    }
}