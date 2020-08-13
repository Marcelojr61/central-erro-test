using AutoMapper;
using CentralDeErros.Core.Extensions;
using CentralDeErros.Model.Models;
using CentralDeErros.Services;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MicrosserviceController:ControllerBase
    {
        private MicrosserviceService _service;
        private IMapper _mapper;
        private readonly TokenGeneratorService _tokenGeneratorService;

        public MicrosserviceController(MicrosserviceService service, IMapper mapper, TokenGeneratorService tokenGeneratorService)
        {
            _service = service;
            _mapper = mapper;
            _tokenGeneratorService = tokenGeneratorService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<MicrosserviceDTO>> GetAllMicrosservices()
        {
            return Ok
                 (_mapper.Map<IEnumerable<MicrosserviceDTO>>
                 (_service.List()));
        }

        [HttpGet("{id}")]
        public ActionResult<MicrosserviceDTO> GetMicrosserviceId(int? id)
        {
            if (id == null)
            {
                return NoContent();
            }
                return Ok
                    (_mapper.Map<MicrosserviceDTO>
                    (_service.Fetch
                    ((int)id)));

        }

        [ClaimsAuthotize("Admin", "Delete")]
        [HttpDelete("{id}")]
        public void DeleteEnvironmentId(int? id)
        {
            _service.Delete((int)id);
        }

        [ClaimsAuthotize("Admin", "Update")]
        [HttpPut("{id}")]
        public ActionResult<MicrosserviceDTO> UpdateMicrosservice(int? id, Microsservice microsservice)
        {
            if (id == null)
            {
                return NoContent();
            }
                return Ok
                    (_mapper.Map<MicrosserviceDTO>
                    (_service.RegisterOrUpdate
                    ((microsservice))));
        }

        [ClaimsAuthotize("Admin", "Create")]
        [HttpPost]
        public ActionResult<MicrosserviceDTO> SaveMicrosservice([FromBody] MicrosserviceDTO value)
        {
            if (!ModelState.IsValid){ return BadRequest(ModelState); }

                _mapper.Map<MicrosserviceDTO>
                (_service.RegisterOrUpdate
                (_mapper.Map < Microsservice >(value)));

                return Ok();
        }
    }
}
