using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public class Hotel : BaseEntity
    {
        private ICollection<UserHotelRange> _userHotelRanges;

        private ICollection<UserHotelView> _userHotelViews;

        public string Code { get; set; }

        public string Name { get; set; }

        //酒店或集团的标志，1：集团；2：酒店
        public string Flag { get; set; }

        public string Des { get; set; }

        public int? Seq { get; set; }

        public string EngName { get; set; }

        public string Remark { get; set; }

        public string CityCode { get; set; }

        public string Keyword { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string Stars { get; set; }

        public string Address { get; set; }

        public int? TotalRoom { get; set; }

        public string ProvinceCode { get; set; }

        public string CountryCode { get; set; }

        public string HotelGroupCode { get; set; }

        public string FullName
        {
            get;
            set;
        }

        public string EngAddress
        {
            get;
            set;
        }

        public int? TotalArea
        {
            get;
            set;
        }

        public int? TotalMeetingArea
        {
            get;
            set;
        }

        public int? TotalFbArea
        {
            get;
            set;
        }

        public string UniteHotels
        {
            get;
            set;
        }

        public string DbName
        {
            get;
            set;
        }

        public string PostCode
        {
            get;
            set;
        }

        public string Fax
        {
            get;
            set;
        }

        public string Tel
        {
            get;
            set;
        }

        public string Udf1
        {
            get;
            set;
        }

        public string Udf2
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Mobile
        {
            get;
            set;
        }

        public string Currency
        {
            get;
            set;
        }

        public string HotelOperationType
        {
            get;
            set;
        }

        public string InternalCode
        {
            get;
            set;
        }

        public virtual ICollection<UserHotelRange> UserHotelRanges
        {
            get
            {
                return _userHotelRanges ?? (_userHotelRanges = new List<UserHotelRange>());
            }
            protected set
            {
                _userHotelRanges = value;
            }
        }

        public virtual ICollection<UserHotelView> UserHotelViews
        {
            get
            {
                return _userHotelViews ?? (_userHotelViews = new List<UserHotelView>());
            }
            protected set
            {
                _userHotelViews = value;
            }
        }
    }
}
