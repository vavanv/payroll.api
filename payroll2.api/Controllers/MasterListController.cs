using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Payroll2.Api.Mappers;
using Payroll2.Api.Services.MasterLists;

namespace Payroll2.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class MasterListController : Controller
    {
        private readonly IMasterListMapper _mapper;
        private readonly IMasterListService _masterListService;

        public MasterListController(IMasterListService masterListService, IMasterListMapper mapper)
        {
            _masterListService = masterListService;
            _mapper = mapper;
        }

        [HttpGet("api/masterlist/list")]
        public async Task<IActionResult> Get()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var masterList = await _masterListService.GetMasterLists();
                    return Ok(_mapper.MapFrom(masterList));
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