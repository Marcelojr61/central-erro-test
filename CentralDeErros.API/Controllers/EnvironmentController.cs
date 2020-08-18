﻿using AutoMapper;
using CentralDeErros.Core.Extensions;
using CentralDeErros.Services;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


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
        public ActionResult<EnvironmentDTO> GetEnviromentById(int? id)
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

        [ClaimsAuthorize("Admin","Delete")]
        [HttpDelete("{id}")]
        public void DeleteEnvironmentById(int? id)
        {
            _service.Delete((int)id);
        }

        [ClaimsAuthorize("Admin","Update")]
        [HttpPut("{id}")]
        public ActionResult<EnvironmentDTO> UpdateEnvironment(int? id, [FromBody] EnvironmentDTO environment)
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
                    (_mapper.Map<Model.Models.Environment>(environment))));
            }
        }

        [ClaimsAuthorize("Admin","Create")]
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