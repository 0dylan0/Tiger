using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Configuration
{
    public class SettingService
    {
        private readonly DapperRepository _repository;

        public SettingService(DapperRepository repository)
        {
            _repository = repository;
        }

        public string GetSettingByKey(string key, string hotelCode = "")
        {
            return _repository.SqlQuery<string>("select dbo.fn_get_sysparam_value(@key, @hotelCode)", new { key = key, hotelCode = hotelCode }).FirstOrDefault();

        }
    }
}
