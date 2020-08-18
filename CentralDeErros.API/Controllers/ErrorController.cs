﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CentralDeErros.Core.Extensions;
using CentralDeErros.Model.Models;
using CentralDeErros.Services.Interfaces;
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
        private readonly IErrorService _service;
        private readonly IMapper _mapper;

        public ErrorController(IErrorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<ErrorDTO> Get(int id)
        {
            return Ok(_mapper.Map<ErrorDTO>(_service.Fetch(id)));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ErrorDTO>> List(int? start, int? end, bool archived = false)
        {
            return Ok(_mapper.Map<IEnumerable<ErrorDTO>>(_service.List(start, end, archived)));
        }


        [HttpPost]
        public ActionResult Post([FromBody] ErrorEntryDTO entry)
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

        [ClaimsAuthorize("Admin", "Update")]
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

        [HttpPut("archive")]
        public IActionResult Archive([FromBody] List<int> errorIdList)
        {
            var failed = new List<int>();

            if (errorIdList != null)
            {
                failed =  _service.ArchiveById(errorIdList);
            }

            if (failed != null)
            {
                return NotFound(new { failedList = failed });
            }

            return Ok();
        }

        [ClaimsAuthorize("Admin", "Delete")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);

            return Ok();
        }

        [ClaimsAuthorize("Admin", "Delete")]
        [HttpDelete]
        public IActionResult Delete(ErrorEntryDTO entry)
        {
            _service.Delete(_mapper.Map<Error>(entry));

            return Ok();
        }

    }
}
