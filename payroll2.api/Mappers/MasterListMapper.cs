using System;
using System.Collections.Generic;

using AutoMapper;

using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.Models;

namespace Payroll2.Api.Mappers
{
    internal sealed class MasterListMapper : IMasterListMapper
    {
        private readonly IMapper _mapper;

        public MasterListMapper()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<MasterList, MasterListModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.MasterListTypeId))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            });

            mapperConfiguration.AssertConfigurationIsValid();

            _mapper = mapperConfiguration.CreateMapper();
        }

        public MasterListModel MapFrom(MasterList itemEntity)
        {
            return _mapper.Map<MasterListModel>(itemEntity);
        }

        public IEnumerable<MasterListModel> MapFrom(IEnumerable<MasterList> entities)
        {
            return _mapper.Map<List<MasterListModel>>(entities);
        }
    }
}