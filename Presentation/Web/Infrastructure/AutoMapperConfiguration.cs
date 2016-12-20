using AutoMapper;
using Core.Domain;
using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Infrastructure
{
    public static class AutoMapperConfiguration
    {
        private static MapperConfiguration _mapperConfiguration;

        private static IMapper _mapper;

        public static void Init()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<Hotel, HotelModel>();
                //cfg.CreateMap<User, UserEditPasswordModel>();
                cfg.CreateMap<UserModel, Core.Domain.Common.Users>();
                cfg.CreateMap<Core.Domain.Common.Users, UserModel>();
                cfg.CreateMap<GoodsDataModel, GoodsData>();
                cfg.CreateMap<GoodsData, GoodsDataModel>();
                cfg.CreateMap<ClientDataModel, ClientData>();
                cfg.CreateMap<ClientData, ClientDataModel>();



            });

            _mapper = _mapperConfiguration.CreateMapper();

        }

        public static IMapper Mapper => _mapper;

        public static MapperConfiguration MapperConfiguration => _mapperConfiguration;
    }
}