using Core.Domain.Security;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Security
{
    public class LicenseService
    {
        private readonly IDbConnection _context;

        public LicenseService(IDbConnection context)
        {
            _context = context;
        }

        /// <summary>
        /// 判断指定酒店是否有某个 Module 的 License
        /// </summary>
        /// <param name="hotelCode"></param>
        /// <param name="moduleName">通过 LicenseModuleParameter 获取的 Module Code</param>
        /// <returns></returns>
        public bool CheckLicense(string hotelCode, string moduleName)
        {
            var spCheckRightResult = this._context.Query<CheckLicenseResult>("EXEC sp_get_hotel_information @hotelCode", new { hotelCode = hotelCode });

            if (spCheckRightResult.Count() == 0)
            {
                return false;
            }
            else
            {
                string expireDate = spCheckRightResult.ElementAt(0).expire_date.ToString();
                string dateNow = DateTime.Now.ToString("yyyy-MM-dd");
                //判断License的到期日期
                if (string.Compare(expireDate, dateNow) < 0)
                {
                    return false;
                }

                string[] moduleArray = spCheckRightResult.ElementAt(0).module.Split(',');
                foreach (string moduleStr in moduleArray)
                {
                    if (moduleStr.Substring(0, 3) == moduleName)
                    {
                        string moduleExpireDate = moduleStr.Substring(4, moduleStr.Length - 4).ToString();
                        //判断License中每个模块的到期日期
                        if (string.Compare(moduleExpireDate, dateNow) < 0)
                        {
                            return false;
                        }

                        return true;
                    }
                }

                return false;
            }
        }


        public DateTime? GetModuleExpireDate(string hotelCode, string moduleName)
        {
            var license = _context.Query<License>("select id,hotel_code as HotelCode,function_control as FunctionControl,Module from License where hotel_code =@hotelCode",
                new { hotelCode = hotelCode }).FirstOrDefault();

            DateTime d;
            if (license != null && GetModules(license.Module).TryGetValue(moduleName, out d))
            {
                return d;
            }

            return null;
        }

        private Dictionary<string, DateTime> GetModules(string module)
        {
            var modules = new Dictionary<string, DateTime>();

            foreach (var m in module.Split(','))
            {
                var name = m.Split('|')[0];
                var expireDate = Convert.ToDateTime(m.Split('|')[1]);

                modules.Add(name, expireDate);
            }

            return modules;
        }
    }
}

