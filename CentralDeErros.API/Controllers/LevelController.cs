using AutoMapper;
using CentralDeErros.Model.Models;
using CentralDeErros.Services;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CentralDeErros.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    
    public class LevelController : ControllerBase
    {
        private LevelService _service;
        private IMapper _mapper;

        public LevelController(LevelService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/v1/level
        [HttpGet]
        public ActionResult<IEnumerable<LevelDTO>> GetAllLevel()
        {
            return Ok
                 (_mapper.Map<IEnumerable<LevelDTO>>
                 (_service.List()));
        }

        // GET api/v1/level/{id}
        [HttpGet("{id}")]
        public ActionResult<LevelDTO> GetEnviromentId(int? id)
        {
            if (id == null)
            {
                return NoContent();
            }
            else
            {
                return Ok
                    (_mapper.Map<LevelDTO>
                    (_service.Fetch
                    ((int)id)));
            }
        }

        // DELETE api/v1/level/{id}
        [HttpDelete("{id}")]
        public void DeleteLevelId(int? id)
        {
            _service.Delete((int)id);
        }

        // PUT api/v1/level/{id}
        [HttpPut("{id}")]
        public ActionResult<LevelDTO> UpdateLevel(int? id, Level level)
        {
            if (id == null)
            {
                return NoContent();
            }
            else
            {
                return Ok
                    (_mapper.Map<LevelDTO>
                    (_service.RegisterOrUpdate
                    ((level))));
            }

        }

        // POST api/v1/level
        [HttpPost]
        public ActionResult<LevelDTO> SaveEnvironment([FromBody] LevelDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok
                    (_mapper.Map<LevelDTO>
                    (_service.RegisterOrUpdate
                    (_mapper.Map<Level>
                    ((value)))));

            }
        }
    }
}
