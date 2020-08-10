using AutoMapper;
using CentralDeErros.Model.Models;
using CentralDeErros.Services;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Text;

namespace CentralDeErros.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
   
    public class MicrosserviceController:ControllerBase
    {
        private MicrosserviceService _service;
        private IMapper _mapper;
        

        public MicrosserviceController(MicrosserviceService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/v1/microsservice
        [HttpGet]
        public ActionResult<IEnumerable<MicrosserviceDTO>> GetAllMicrosservices()
        {
            return Ok
                 (_mapper.Map<IEnumerable<MicrosserviceDTO>>
                 (_service.List()));
        }

        // GET api/v1/microsservice/{id}
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

        // DELETE api/v1/microsservice/{id}
        [HttpDelete("{id}")]
        public void DeleteEnvironmentId(int? id)
        {
            _service.Delete((int)id);
        }

        // PUT api/v1/microsservice/{id}
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
                (microsservice)));
        }

        // POST api/v1/microsservice
        [HttpPost]
        public ActionResult<MicrosserviceDTO> SaveMicrosservice([FromBody] MicrosserviceDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            return Ok(_mapper.Map<MicrosserviceDTO>
                (_service.RegisterOrUpdate
                (_mapper.Map<Microsservice>
                ((value)))));

        }
    }
}
