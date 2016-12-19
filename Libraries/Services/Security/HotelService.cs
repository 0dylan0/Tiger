using Core.Domain.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Security
{
    public class HotelService
    {
        public const string HOTEL_GROUP_FLAG = "1";
        private readonly IDbConnection _context;

        public HotelService(IDbConnection context)
        {
            _context = context;
        }

        public Hotel GetByCode(string code)
        {
            return _context.QuerySingleOrDefault<Hotel>(
                @"select code,
                    name,
                    flag,
                    des,
                    remark,
                    sort_id as Seq,
                    eng_name as EngName,
                    city_code as CityCode,
                    Keyword, dt as CreatedOn,
                    DbName,
                    Stars,
                    Address,
                    province_code as ProvinceCode,
                    country_code as CountryCode,
                    group_code as HotelGroupCode,
                    total_room as TotalRoom, 
                    full_name as FullName, 
                    address_en as EngAddress, 
                    total_area as TotalArea, 
                    total_meeting_area as TotalMeetingArea, 
                    total_fb_area as TotalFbArea,
                    unite_hotels as UniteHotels, 
                    post_code as PostCode, 
                    fax, Tel, 
                    udf_1 as Udf2, 
                    udf_2 as Udf2, 
                    Email, 
                    Mobile, 
                    Currency, 
                    hotel_operation_type as HotelOperationType, 
                    internal_code as InternalCode
                from hotels
                where code=@code", new { code = code });
        }

        public List<Hotel> GetByCode(IEnumerable<string> codes)
        {
            var sql = @"
                    SELECT
                        code,
                        name
                    FROM hotels";

            if (codes != null && codes.Count() > 0)
            {
                sql += " WHERE code IN @codes";
            }

            return _context.Query<Hotel>(sql, new
            {
                codes
            }).ToList();
        }

        public List<Hotel> GetAll()
        {
            return _context.Query<Hotel>(
                @"select code,
                    name,
                    flag,
                    des,
                    remark,
                    sort_id as Seq,
                    eng_name as EngName,
                    city_code as CityCode,
                    Keyword, dt as CreatedOn,
                    DbName,
                    Stars,
                    Address,
                    province_code as ProvinceCode,
                    country_code as CountryCode,
                    group_code as HotelGroupCode,
                    total_room as TotalRoom, 
                    full_name as FullName, 
                    address_en as EngAddress, 
                    total_area as TotalArea, 
                    total_meeting_area as TotalMeetingArea, 
                    total_fb_area as TotalFbArea,
                    unite_hotels as UniteHotels, 
                    post_code as PostCode, 
                    fax, Tel, 
                    udf_1 as Udf2, 
                    udf_2 as Udf2, 
                    Email, 
                    Mobile, 
                    Currency, 
                    hotel_operation_type as HotelOperationType, 
                    internal_code as InternalCode
                from hotels").ToList();
        }

    }
}
