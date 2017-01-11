using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public class SupplierData
    {
        public int ID { get; set; }

        //供应商名称
        public string SupplierName { get; set; }

        //公司名称
        public string CompanyName { get; set; }

        //联系人
        public string Contacts { get; set; }

        //供应商类别
        public string SupplierType { get; set; }

        //区域
        public string Area { get; set; }

        //地址
        public string Address { get; set; }

        //手机
        public string Phone { get; set; }

        //座机
        public string Telephone { get; set; }


        //欠款
        public decimal Arrears { get; set; }

        //还款日期
        public DateTime RepaymentDate { get; set; }

        //开户行
        public string Banks { get; set; }

        //开户名
        public string AccountName { get; set; }

        //银行账号
        public string BankAccount { get; set; }

        //税号
        public string TaxIdentificationNumber { get; set; }

        //排序号
        public int Seq { get; set; }

        //备注1
        public string Remarks1 { get; set; }

        //备注2
        public string Remarks2 { get; set; }

        //备注3
        public string Remarks3 { get; set; }

        //备注4
        public string Remarks4 { get; set; }
    }
}
