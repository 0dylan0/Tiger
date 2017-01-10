using Core.Domain.Common;
using Core.Page;
using Dapper;
using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class ClientDataService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public ClientDataService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(ClientData ClientData)
        {
            var sql = $@"insert into ClientData(
                    ClientName,
                    CompanyName,
                    ClientType,
                    Address,
                    Area,
                    Phone,
                    Telephone,
                    Arrears,
                    ReceiptDate,
                    Banks,
                    AccountName,
                    BankAccount,
                    TaxIdentificationNumber,
                    Seq,
                    Remarks1,
                    Remarks2,
                    Remarks3,
                    Remarks4)
			        VALUES (
                    @ClientName,
                    @CompanyName,
                    @ClientType,
                    @Address,
                    @Area,
                    @Phone,
                    @Telephone,
                    @Arrears,
                    @ReceiptDate,
                    @Banks,
                    @AccountName,
                    @BankAccount,
                    @TaxIdentificationNumber,
                    @Seq,
                    @Remarks1,
                    @Remarks2,
                    @Remarks3,
                    @Remarks4)";
            _context.Execute(sql, new
            {
                ClientName= ClientData.ClientName,
                CompanyName = ClientData.CompanyName,
                ClientType=ClientData.ClientType,
                Address=ClientData.Address,
                Area=ClientData.Area,
                Phone=ClientData.Phone,
                Telephone=ClientData.Telephone,
                Arrears=ClientData.Arrears,
                ReceiptDate=ClientData.ReceiptDate,
                Banks=ClientData.Banks,
                AccountName=ClientData.AccountName,
                BankAccount=ClientData.BankAccount,
                TaxIdentificationNumber=ClientData.TaxIdentificationNumber,
                Seq=ClientData.Seq,
                Remarks1=ClientData.Remarks1,
                Remarks2=ClientData.Remarks2,
                Remarks3=ClientData.Remarks3,
                Remarks4=ClientData.Remarks4
            });
        }

        public void Update(ClientData ClientData)
        {
            var sql = $@"update ClientData set
                    ClientName=@ClientName,
                    CompanyName=@CompanyName,
                    ClientType=@ClientType,
                    Address=@Address,
                    Area=@Area,
                    Phone=@Phone,
                    Telephone=@Telephone,
                    Arrears=@Arrears,
                    ReceiptDate=@ReceiptDate,
                    Banks=@Banks,
                    AccountName=@AccountName,
                    BankAccount=@BankAccount,
                    TaxIdentificationNumber=@TaxIdentificationNumber,
                    Seq=@Seq,
                    Remarks1=@Remarks1,
                    Remarks2=@Remarks2,
                    Remarks3=@Remarks3,
                    Remarks4=@Remarks4
                    where ID=@ID";
            _context.Execute(sql, new
            {
                ID = ClientData.ID,
                ClientName = ClientData.ClientName,
                CompanyName = ClientData.CompanyName,
                ClientType = ClientData.ClientType,
                Address = ClientData.Address,
                Area = ClientData.Area,
                Phone = ClientData.Phone,
                Telephone = ClientData.Telephone,
                Arrears = ClientData.Arrears,
                ReceiptDate = ClientData.ReceiptDate,
                Banks = ClientData.Banks,
                AccountName = ClientData.AccountName,
                BankAccount = ClientData.BankAccount,
                TaxIdentificationNumber = ClientData.TaxIdentificationNumber,
                Seq = ClientData.Seq,
                Remarks1 = ClientData.Remarks1,
                Remarks2 = ClientData.Remarks2,
                Remarks3 = ClientData.Remarks3,
                Remarks4 = ClientData.Remarks4
            });

        }

        public ClientData GetUserById(int id)
        {
            var sql = @"select * from ClientData  where id = @id";
            return _context.QuerySingle<ClientData>(sql, new
            {
                id = id
            });
        }
        public IPagedList<ClientData> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from ClientData ";
            var Parameter = new DynamicParameters();
            if (!string.IsNullOrEmpty(textQuery))
            {
                sql += " where ClientName like @textQuery";
                textQuery = textQuery.Contains("%") ? textQuery : $"%{textQuery}%";
                Parameter.Add("textQuery", textQuery);
            }

            return new SqlPagedList<ClientData>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

        public List<GetList> GetClientDataList()
        {
            string sql = @"select id as code, clientName as name from ClientData";

            return _context.Query<GetList>(sql).ToList();
        }
    }
}
