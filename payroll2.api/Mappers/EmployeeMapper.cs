using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Payroll2.Api.DataAccess.Enums;
using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.Models;
using Payroll2.Api.Services.MasterLists;

namespace Payroll2.Api.Mappers
{
    public class EmployeeMapper : IEmployeeMapper
    {
        private readonly IMapper _mapper;

        public EmployeeMapper(IMasterListService masterListService)
        {
            var masterListDepartment = masterListService.GetMasterListByTypeId((int) MasterListType.Department).Result;
            var masterListPosition = masterListService.GetMasterListByTypeId((int) MasterListType.Position).Result;

            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Employee, EmployeeModel>()
                    .ForMember(e => e.Department,
                        em => em.MapFrom(d => masterListDepartment.First(i => i.Id == d.DepartmentId).Description))
                    .ForMember(e => e.Position,
                        em => em.MapFrom(p => masterListPosition.First(i => i.Id == p.PositionId).Description))
                    .ForMember(e => e.Street, em => em.Ignore())
                    .ForMember(e => e.City, em => em.Ignore())
                    .ForMember(e => e.PostalCode, em => em.Ignore())
                    .ForMember(e => e.ProvinceId, em => em.Ignore())
                    .ForMember(e => e.Username, em => em.Ignore())
                    .ForMember(e => e.Password, em => em.Ignore())
                    .ForMember(e => e.UserRightId, em => em.Ignore())
                    .ForMember(e => e.UserEnabled, em => em.Ignore());

                configuration.CreateMap<EmployeeModel, Employee>()
                    .ForMember(e => e.Sin,
                        x => x.MapFrom(em => new string(em.Sin.Where(c => c != "-".ToCharArray()[0]).ToArray())))
                    .ForMember(e => e.Department, em => em.Ignore())
                    .ForMember(e => e.WageType, em => em.Ignore())
                    .ForMember(e => e.VacationPolicy, em => em.Ignore())
                    .ForMember(e => e.Address, em => em.Ignore())
                    .ForMember(e => e.Position, em => em.Ignore())
                    .ForMember(e => e.User, em => em.Ignore());

                configuration.CreateMap<EmployeeModel, Address>()
                    .ForMember(a => a.Id, x => x.MapFrom(em => em.AddressId))
                    .ForMember(a => a.City, x => x.MapFrom(em => em.City))
                    .ForMember(a => a.PostalCode,
                        x => x.MapFrom(em => new string(em.PostalCode.Where(c => !char.IsWhiteSpace(c)).ToArray())))
                    .ForMember(a => a.ProvinceId, x => x.MapFrom(em => em.ProvinceId))
                    .ForMember(a => a.Street, x => x.MapFrom(em => em.Street))
                    .ForAllOtherMembers(x => x.Ignore());

                configuration.CreateMap<Address, EmployeeModel>()
                    .ForMember(em => em.AddressId, x => x.MapFrom(a => a.Id))
                    .ForMember(em => em.Street, x => x.MapFrom(a => a.Street))
                    .ForMember(em => em.City, x => x.MapFrom(a => a.City))
                    .ForMember(em => em.ProvinceId, x => x.MapFrom(a => a.ProvinceId))
                    .ForMember(em => em.PostalCode, x => x.MapFrom(a => a.PostalCode))
                    .ForAllOtherMembers(dest => dest.Ignore());

                configuration.CreateMap<EmployeeModel, User>()
                    .ForMember(u => u.Id, x => x.MapFrom(em => em.UserId))
                    .ForMember(u => u.Username, x => x.MapFrom(em => GetUsername(em)))
                    .ForMember(u => u.Password, x => x.MapFrom(em => GetPassword(em)))
                    .ForMember(u => u.Enabled, x => x.MapFrom(em => GetUserEnabled(em)))
                    .ForMember(u => u.RightId, x => x.MapFrom(em => GetUserRights(em)))
                    .ForAllOtherMembers(x => x.Ignore());

                configuration.CreateMap<User, EmployeeModel>()
                    .ForMember(em => em.Username, x => x.MapFrom(u => u.Username))
                    .ForMember(em => em.Password, x => x.MapFrom(u => u.Password))
                    .ForMember(em => em.UserEnabled, x => x.MapFrom(u => u.Enabled))
                    .ForMember(em => em.UserRightId, x => x.MapFrom(u => u.RightId))
                    .ForAllOtherMembers(x => x.Ignore());
            });

            mapperConfiguration.AssertConfigurationIsValid();

            _mapper = mapperConfiguration.CreateMapper();
        }

        public EmployeeModel MapFrom(Employee employeeEntity)
        {
            return _mapper.Map<EmployeeModel>(employeeEntity);
        }

        public Employee MapFrom(EmployeeModel model, Employee employeeEntity)
        {
            return _mapper.Map(model, employeeEntity);
        }

        public Address MapFrom(EmployeeModel model, Address addressEntity)
        {
            return _mapper.Map(model, addressEntity);
        }

        public User MapFrom(EmployeeModel model, User userEntity)
        {
            return _mapper.Map(model, userEntity);
        }

        public IEnumerable<EmployeeModel> MapFrom(IEnumerable<Employee> employees)
        {
            return _mapper.Map<List<EmployeeModel>>(employees);
        }

        public EmployeeModel MapFrom(Employee employeeEntity, Address addressEntity, User userEntity)
        {
            var model = _mapper.Map<Employee, EmployeeModel>(employeeEntity);
            _mapper.Map(userEntity, model);
            _mapper.Map(addressEntity, model);
            return model;
        }

        private string GetUsername(EmployeeModel em)
        {
            return em.UserId == null ? em.Email : em.Username;
        }

        private string GetPassword(EmployeeModel em)
        {
            return em.UserId == null ? em.Email : em.Password;
        }

        private bool GetUserEnabled(EmployeeModel em)
        {
            return em.UserId != null && em.UserEnabled;
        }

        private int GetUserRights(EmployeeModel em)
        {
            return em.UserId == null ? (int) Right.Employee : em.UserRightId;
        }
    }
}