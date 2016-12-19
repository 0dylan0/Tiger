using Core.Domain.Common;
using Core.Infrastructure;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class UserHotelRangeService
    {
        private readonly IDbConnection _context;

        public UserHotelRangeService(IDbConnection context)
        {
            _context = context;
        }

        public IEnumerable<UserHotelRange> GetByUserCode(string userCode)
        {
            var sql = @"select  * from users_hotels_range r
                       left join hotels h on h.code = r.hotel_code
                        left join users u on u.code = r.user_code 
                        where r.user_code=@UserCode";
            var connection = EngineContext.Current.Resolve<IDbConnection>();
            var rangeList = _context.Query<Hotel, UserHotelRange, UserHotelRange>(sql,
                (hotel, userHotelRange) =>
                {
                    userHotelRange.Hotel = hotel;
                    return userHotelRange;
                }, new { UserCode = userCode }, splitOn: "Code");
            return rangeList;
        }

    }
}