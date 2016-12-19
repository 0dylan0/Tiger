using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Security
{
    //TODO:要不要做License
    /// <summary>
    /// 此类为sp_get_hotel_information的返回视图，用于校验License
    /// </summary>
    class CheckLicenseResult
    {
        public string hotel_code { get; set; }
        public string hotel_ch_name { get; set; }
        public string module { get; set; }
        public string expire_date { get; set; }
    }
}
