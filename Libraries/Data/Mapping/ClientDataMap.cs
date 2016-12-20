using Core.Domain.Common;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ClientDataMap : DommelEntityMap<ClientData>
    {
        public ClientDataMap()
        {
            ToTable("ClientData");

            Map(t => t.ID).ToColumn("ID").IsKey();
            Map(t => t.ClientName).ToColumn("ClientName");
            Map(t => t.CompanyName).ToColumn("CompanyName");
            Map(t => t.ClientType).ToColumn("ClientType");
            Map(t => t.Address).ToColumn("Address");

            Map(t => t.Area).ToColumn("Area");
            Map(t => t.Phone).ToColumn("Phone");
            Map(t => t.Telephone).ToColumn("Telephone");
            Map(t => t.Arrears).ToColumn("Arrears");

            Map(t => t.ReceiptDate).ToColumn("ReceiptDate");
            Map(t => t.Banks).ToColumn("Banks");
            Map(t => t.AccountName).ToColumn("AccountName");
            Map(t => t.BankAccount).ToColumn("BankAccount");

            Map(t => t.TaxIdentificationNumber).ToColumn("TaxIdentificationNumber");
            Map(t => t.Seq).ToColumn("Seq");
            Map(t => t.Remarks1).ToColumn("Remarks1");
            Map(t => t.Remarks2).ToColumn("Remarks2");
            Map(t => t.Remarks3).ToColumn("Remarks3");
            Map(t => t.Remarks4).ToColumn("Remarks4");

        }
    }
}
