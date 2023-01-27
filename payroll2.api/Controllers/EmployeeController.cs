using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.Framework.Exception;
using Payroll2.Api.Mappers;
using Payroll2.Api.Models;
using Payroll2.Api.Services.Address;
using Payroll2.Api.Services.Employee;
using Payroll2.Api.Services.Users;

namespace Payroll2.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class EmployeeController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeMapper _mapper;
        private readonly IUserService _userService;

        public EmployeeController(IEmployeeService companyService, IAddressService addressService,
            IUserService userService,
            IEmployeeMapper mapper)
        {
            _employeeService = companyService;
            _addressService = addressService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("api/employee/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employee = await _employeeService.GetEmployeeById(id);
                    var address = await _addressService.GetAddressById(employee.AddressId);
                    var user = await _userService.GetUserById(employee.UserId);
                    var model = _mapper.MapFrom(employee, address, user);
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        [HttpPost("api/employee/delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeService.DeleteEmployeeById(id);
                    return Ok("Successfully deleted");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        [HttpPost("api/employee/restore/{id}")]
        public IActionResult Restore(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeService.DeleteEmployeeById(id, false);
                    return Ok("Successfully restored");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        [HttpPost("api/employee")]
        public IActionResult Update([FromBody] EmployeeModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (data == null) throw new BusinessException("employee Date must be provided");

                    var employee = new Employee();
                    var address = new Address();
                    var user = new User();
                    _mapper.MapFrom(data, address);
                    _mapper.MapFrom(data, user);
                    _mapper.MapFrom(data, employee);
                    _employeeService.UpdateEmployee(employee, address, user);

                    return Ok("Successfully updated");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        [HttpGet("api/employee/list")]
        public async Task<IActionResult> Get()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employees = await _employeeService.GetEmployees();
                    return Ok(_mapper.MapFrom(employees));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }

            return BadRequest(ModelState);
        }
    }
}