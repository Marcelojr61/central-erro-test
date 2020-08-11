using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CentralDeErros.Model.Models;
using CentralDeErros.Services;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeErros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService service;
        private readonly IMapper mapper;

        public ProfileController(ProfileService service, IMapper mapper)
        {
            this.service = service; 
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProfileDTO>> GetAll()
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<ProfileDTO>>(service.List()));

            }catch(NullReferenceException e)
            {
                return NoContent();
            }
            
        } 

        [HttpGet("{id}")] 
        public ActionResult<ProfileDTO> Get(int? id) 
        {
   
                if(ModelState.IsValid)
                {
                    var profileFoundById = service.Fetch((int)id);        
                    return Ok(mapper.Map<ProfileDTO>(profileFoundById));
                }

                return NoContent();    

                  
        } 

        [HttpDelete("{id}")]
        public ActionResult<ProfileDTO> Delete(ProfileDTO entry) 
        {

            if(ModelState.IsValid)
            {
                service.Delete(mapper.Map<Model.Models.Profile>(entry)); 
                return Ok();   
            }

            return NoContent();

            
        }   


        [HttpPut("{id}")]
        public ActionResult<ProfileDTO> Update(Model.Models.Profile profile) 
        {
                if(ModelState.IsValid)
                {
                    return Ok(mapper.Map<ProfileDTO>(service.Update(profile)));
                }

                return NoContent();  
                                                
        } 

    }
} 