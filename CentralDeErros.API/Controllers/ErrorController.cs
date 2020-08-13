using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CentralDeErros.Core.Extensions;
using CentralDeErros.Model.Models;
using CentralDeErros.Services;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeErros.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ErrorService _service;
        private readonly IMapper _mapper;

        public ErrorController(ErrorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<ErrorDTO>(_service.Fetch(id)));
        }

        [HttpGet]
        public IActionResult List(int? start, int? end)
        {
            return Ok(_mapper.Map<IEnumerable<ErrorDTO>>(_service.List(start, end)));
        }


        [HttpPost]
        public IActionResult Post([FromBody]ErrorEntryDTO entry)
        {

            try
            {
                var newEntry = _service.Register(_mapper.Map<Error>(entry));
                return Ok(_mapper.Map<ErrorDTO>(newEntry));
            }
            catch (Exception exc)
            {

                return BadRequest(exc.Message);
            }
        }

        [ClaimsAuthotize("Admin","Update")]
        [HttpPut]
        public IActionResult Put(ErrorEntryDTO entry)
        {
            if (entry.Id.HasValue && _service.CheckId<Error>(entry.Id.Value))
            {
                _service.Update(_mapper.Map<Error>(entry));
                return Ok();
            }


            return NotFound();
        }

        [ClaimsAuthotize("Admin","Delete")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            
            return Ok();
        }

        [ClaimsAuthotize("Admin", "Delete")]
        [HttpDelete]
        public IActionResult Delete(ErrorEntryDTO entry)
        {
            _service.Delete(_mapper.Map<Error>(entry));

            return Ok();
        }
        
    }
}
