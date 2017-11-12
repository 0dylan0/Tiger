using AutoMapper;
using Core.Domain;
using Core.Domain.Common;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Infrastructure
{
    public class AutoMapperConfiguration
    {
        private static MapperConfiguration _mapperConfiguration;

        private static IMapper _mapper;

        public static void Init()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<UserModel, Core.Domain.Common.Users>();
                //cfg.CreateMap<Core.Domain.Common.Users, UserModel>();
                //cfg.CreateMap<GoodsDataModel, GoodsData>();
                //cfg.CreateMap<GoodsData, GoodsDataModel>();
                cfg.CreateMap<UserEditPasswordModel, Core.Domain.Common.Users>();
                cfg.CreateMap<Core.Domain.Common.Users, UserEditPasswordModel>();
            });

            _mapper = _mapperConfiguration.CreateMapper();

        }

        public static IMapper Mapper => _mapper;

        public static MapperConfiguration MapperConfiguration => _mapperConfiguration;
    }
}