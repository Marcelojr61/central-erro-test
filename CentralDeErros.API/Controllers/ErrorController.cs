using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CentralDeErros.Model.Models;
using CentralDeErros.Services;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeErros.API.Controllers
{
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

            //check if foreign keys exist
            var fkValidation = ValidateFK(entry);

            if (fkValidation.Count > 0)
                return BadRequest(fkValidation);
            


            var newEntry = _service.Register(_mapper.Map<Error>(entry));
            return Ok(_mapper.Map<ErrorDTO>(newEntry));
        }

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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(ErrorEntryDTO entry)
        {
            _service.Delete(_mapper.Map<Error>(entry));

            return Ok();
        }

        private ICollection<string> ValidateFK(ErrorEntryDTO entry)
        {
            List<string> messages = new List<string>();

            if (!_service.CheckId<Level>(entry.LevelId))
                messages.Add("LevelId not found");

            if (!_service.CheckId<Microsservice>(entry.MicrosserviceId))
                messages.Add("MicrosserviceId not found");

            if (!_service.CheckId<Model.Models.Environment>(entry.EnviromentId))
                messages.Add("EnviromentId not found");

            return messages;
        }
        
        
    }
}
