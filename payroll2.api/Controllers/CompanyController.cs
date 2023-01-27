using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.Framework.Exception;
using Payroll2.Api.Mappers;
using Payroll2.Api.Models;
using Payroll2.Api.Services.Company;

namespace Payroll2.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyMapper _mapper;

        public CompanyController(ICompanyService companyService, ICompanyMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet("api/company")]
        public async Task<IActionResult> Get()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var company = await _companyService.GetCompany();
                    return Ok(_mapper.MapFrom(company));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
            }

            return BadRequest(ModelState.ToDictionary(k => k.Key,
                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
        }

        [HttpPost("api/company")]
        public IActionResult Update([FromBody] CompanyModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (data == null) throw new BusinessException("Company Date must be provided");

                    var campany = new Company();
                    _mapper.MapFrom(data, campany);
                    _companyService.UpdateCompany(campany);

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
    }
}