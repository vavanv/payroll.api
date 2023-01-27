using System;

using AutoMapper;

using Payroll2.Api.DataAccess.Enums;
using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.Models;

namespace Payroll2.Api.Mappers
{
    internal sealed class UserMapper : IUserMapper
    {
        private readonly IMapper _mapper;

        public UserMapper()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
                configuration.CreateMap<User, UserModel>()
                    .ForMember(desc => desc.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(desc => desc.IsAdministrator,
                        opt => opt.MapFrom(src => src.RightId == (int) Right.Administrator))
            );
            mapperConfiguration.AssertConfigurationIsValid();
            _mapper = mapperConfiguration.CreateMapper();
        }

        public UserModel MapFrom(User itemEntity)
        {
            return _mapper.Map<UserModel>(itemEntity);
        }
    }
}