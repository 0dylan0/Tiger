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
    public class SupplierDataService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public SupplierDataService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void Insert(SupplierData SupplierData)
        {
            var sql = $@"insert into SupplierData(
                    SupplierName,
                    CompanyName,
                    Contacts,
                    SupplierType,
                    Area,
                    Address,
                    Phone,
                    Telephone,
                    Arrears,
                    RepaymentDate,
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
                    @SupplierName,
                    @CompanyName,
                    @Contacts,
                    @SupplierType,
                    @Address,
                    @Area,
                    @Phone,
                    @Telephone,
                    @Arrears,
                    @RepaymentDate,
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
                SupplierName = SupplierData.SupplierName,
                CompanyName = SupplierData.CompanyName,
                Contacts=SupplierData.Contacts,
                SupplierType = SupplierData.SupplierType,
                Address = SupplierData.Address,
                Area = SupplierData.Area,
                Phone = SupplierData.Phone,
                Telephone = SupplierData.Telephone,
                Arrears = SupplierData.Arrears,
                RepaymentDate = SupplierData.RepaymentDate,
                Banks = SupplierData.Banks,
                AccountName = SupplierData.AccountName,
                BankAccount = SupplierData.BankAccount,
                TaxIdentificationNumber = SupplierData.TaxIdentificationNumber,
                Seq = SupplierData.Seq,
                Remarks1 = SupplierData.Remarks1,
                Remarks2 = SupplierData.Remarks2,
                Remarks3 = SupplierData.Remarks3,
                Remarks4 = SupplierData.Remarks4
            });
        }

        public void Update(SupplierData SupplierData)
        {
            var sql = $@"update SupplierData set
                    SupplierName=@SupplierName,
                    CompanyName=@CompanyName,
                    Contacts=@Contacts,
                    SupplierType=@SupplierType,
                    Area=@Area,
                    Address=@Address,
                    Phone=@Phone,
                    Telephone=@Telephone,
                    Arrears=@Arrears,
                    RepaymentDate=@RepaymentDate,
                    Banks=@Banks,
                    AccountName=@AccountName,
                    BankAccount=@BankAccount,
                    TaxIdentificationNumber=@TaxIdentificationNumber,
                    Seq=@Seq,
                    Remarks1=@Remarks1,
                    Remarks2=@Remarks2,
                    Remarks3=@Remarks3,
                    Remarks4=@Remarks4)where ID=@ID ";
            _context.Execute(sql, new
            {
                SupplierName = SupplierData.SupplierName,
                CompanyName = SupplierData.CompanyName,
                SupplierType = SupplierData.SupplierType,
                Address = SupplierData.Address,
                Area = SupplierData.Area,
                Phone = SupplierData.Phone,
                Telephone = SupplierData.Telephone,
                Arrears = SupplierData.Arrears,
                RepaymentDate = SupplierData.RepaymentDate,
                Banks = SupplierData.Banks,
                AccountName = SupplierData.AccountName,
                BankAccount = SupplierData.BankAccount,
                TaxIdentificationNumber = SupplierData.TaxIdentificationNumber,
                Seq = SupplierData.Seq,
                Remarks1 = SupplierData.Remarks1,
                Remarks2 = SupplierData.Remarks2,
                Remarks3 = SupplierData.Remarks3,
                Remarks4 = SupplierData.Remarks4
            });

        }

        public SupplierData GetUserById(int id)
        {
            var sql = @"select * from SupplierData  where id = @id";
            return _context.QuerySingle<SupplierData>(sql, new
            {
                id = id
            });
        }
        public IPagedList<SupplierData> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from SupplierData";
            var Parameter = new DynamicParameters();
            //Parameter.Add("textQuery", textQuery);
            return new SqlPagedList<SupplierData>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

        //获取供应商数据 填充下拉框
        public List<GetList> GetSupplierList()
        {
            string sql = @"select ID as code,SupplierName as name from SupplierData";

            return _context.Query<GetList>(sql).ToList();
        }
    }
}
