using CureWellUsingEF.Modals;
using CureWellUsingEF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CureWellUsingEF.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSpecializationController : ControllerBase
    {
        private readonly IDoctorSpecializationService service;
        public DoctorSpecializationController(IDoctorSpecializationService service)
        {
            this.service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var data = await service.GetAll();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("GetByPrimaryKey")]
        public async Task<IActionResult> GetById(int id,string specialization)
        {
            var data = await service.GetById(id, specialization);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("GetDoctorsByCode")]
        public async Task<IActionResult> GetDoctorsByCode(string code)
        {
            var data = await service.GetDoctorsByCode(code);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(DoctorSpecializationModal _data)
        {
            var data = await this.service.Create(_data);
            return Ok(data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(DoctorSpecializationModal _data, int id,string specialization)
        {
            var data = await this.service.Update(_data, id, specialization);
            return Ok(data);
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int id,string specialization)
        {
            var data = await this.service.Remove(id, specialization);
            return Ok(data);
        }
    }
}
