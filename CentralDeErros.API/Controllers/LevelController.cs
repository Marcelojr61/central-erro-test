using AutoMapper;
using CentralDeErros.Core.Extensions;
using CentralDeErros.Model.Models;
using CentralDeErros.Services;
using CentralDeErros.Services.Interfaces;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Collections.Generic;

namespace CentralDeErros.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _service;
        private readonly IMapper _mapper;

        public LevelController(ILevelService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<LevelDTO>> GetAllLevel()
        {
            return Ok
                 (_mapper.Map<IEnumerable<LevelDTO>>
                 (_service.List()));
        }

        [HttpGet("{id}")]
        public ActionResult<LevelDTO> GetLevelById(int? id)
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

        [ClaimsAuthorize("Admin", "Delete")]
        [HttpDelete("{id}")]
        public ActionResult DeleteLevelById(int? id)
        {
            if (id == null)
            {
                return NoContent();
            }

            _service.Delete((int)id);

            return Ok();
        }

        [ClaimsAuthorize("Admin", "Update")]
        [HttpPut]
        public ActionResult<LevelDTO> UpdateLevel([FromBody] LevelDTO level)
        {
            if (ModelState.IsValid)
            {
                return Ok
                    (_mapper.Map<LevelDTO>
                    (_service.RegisterOrUpdate
                    (_mapper.Map<Level>(level))));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [ClaimsAuthorize("Admin", "Create")]
        [HttpPost]
        public ActionResult<LevelDTO> SaveLevel([FromBody] LevelDTO value)
        {
            if (ModelState.IsValid)
            {
                return Ok
                    (_mapper.Map<LevelDTO>
                    (_service.RegisterOrUpdate
                    (_mapper.Map<Level>
                    ((value)))));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}