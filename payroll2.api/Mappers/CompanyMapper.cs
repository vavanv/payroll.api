using System;

using AutoMapper;

using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.Models;

namespace Payroll2.Api.Mappers
{
    internal sealed class CompanyMapper : ICompanyMapper
    {
        private readonly IMapper _mapper;

        public CompanyMapper()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Company, CompanyModel>();
                configuration.CreateMap<CompanyModel, Company>()
                    .ForMember(i => i.BusinessType, c => c.Ignore())
                    .ForMember(i => i.Province, c => c.Ignore());
            });

            mapperConfiguration.AssertConfigurationIsValid();

            _mapper = mapperConfiguration.CreateMapper();
        }

        public CompanyModel MapFrom(Company itemEntity)
        {
            return _mapper.Map<CompanyModel>(itemEntity);
        }

        public Company MapFrom(CompanyModel model, Company itemEntity)
        {
            return _mapper.Map(model, itemEntity);
        }
    }
}