﻿using System;
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
    public class UserController : ControllerBase
    {
        private readonly UserService service;
        private readonly IMapper mapper;

        public UserController(UserService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public UserController()
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll() => Ok(mapper.Map<IEnumerable<UserDTO>>(service.List()));


        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int? id) 
        {
            if(id == null) return NotFound();

            return Ok(mapper.Map<UserDTO>(service.Fetch((int)id)));
        } 


        [HttpDelete("{id}")]
        public ActionResult<UserDTO> Delete(UserDTO entry) 
        {
            if(ModelState.IsValid)
            {
                service.Delete(mapper.Map<User>(entry)); 
                return Ok();   
            }

            return NotFound();

        }   

        [HttpPut("{id}")]
        public ActionResult<UserDTO> Update(User user) 
        {
           if(ModelState.IsValid)
           {
                service.Update(user);
                return Ok();
           }

           return NotFound();                                       
        } 

        [HttpPost]
        public ActionResult<UserDTO> Create([FromBody]User value)
        {
            if(ModelState.IsValid) 
            {
                var userModel = mapper.Map<User>(value);
                service.Register(userModel);
                return Ok(mapper.Map<UserDTO>(mapper.Map<UserDTO>(userModel)));
            }

            return NotFound();

        }

    }
} 