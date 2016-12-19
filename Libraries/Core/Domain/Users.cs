using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class User : BaseEntity
    {
        public string Code
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Des
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        //删除标志，1：已被删除；0：正常
        public string IsDel
        {
            get;
            set;
        }
        //所属酒店
        public string HotelCode
        {
            get;
            set;
        }

        public string AutoViewHotel
        {
            get;
            set;
        }

        public virtual Hotel Hotel
        {
            get;
            set;
        }


        //用户可操作酒店代码
        private ICollection<UserHotelRange> _userHotelRanges;

        private ICollection<UserHotelView> _userHotelViews;

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

        public string UserGrade { get; set; }
        public string DepGrade { get; set; }
        public string DepCode { get; set; }
    }
}
