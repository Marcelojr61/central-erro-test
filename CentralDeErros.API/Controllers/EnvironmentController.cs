using AutoMapper;
using CentralDeErros.Core.Extensions;
using CentralDeErros.Services;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace CentralDeErros.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        private EnvironmentService _service;
        private IMapper _mapper;

        public EnvironmentController(EnvironmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EnvironmentDTO>> GetAllEnvironments()
        {
            return Ok
                (_mapper.Map<IEnumerable<EnvironmentDTO>>
                (_service.List()));  
        }

        [HttpGet("{id}")]
        public ActionResult<EnvironmentDTO> GetEnviromentId(int? id)
        {
            if (id == null)
            {
                return NoContent();
            }
            else
            {
                return Ok
                    (_mapper.Map<EnvironmentDTO>
                    (_service.Fetch
                    ((int)id)));
            }
        }

        [ClaimsAuthotize("Admin","Delete")]
        [HttpDelete("{id}")]
        public void DeleteEnvironmentId(int? id)
        {
            _service.Delete((int)id);
        }

        [ClaimsAuthotize("Admin","Update")]
        [HttpPut("{id}")]
        public ActionResult<EnvironmentDTO> UpdateEnvironment(int? id, Model.Models.Environment environment)
        {
            if (id == null)
            {
                return NoContent();
            }
            else
            {
                return Ok
                    (_mapper.Map<EnvironmentDTO>
                    (_service.RegisterOrUpdate
                    ((environment))));
            }

        }

        [ClaimsAuthotize("Admin","Create")]
        [HttpPost]
        public ActionResult<EnvironmentDTO> SaveEnvironment([FromBody] EnvironmentDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok
                (_mapper.Map<EnvironmentDTO>
                (_service.RegisterOrUpdate
                (_mapper.Map<Model.Models.Environment>
                ((value)))));
            }
        }



    }

}

