using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CentralDeErros.Core.Extensions;

namespace CentralDeErros.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [ClaimsAuthorize("Admin", "Read")]
        [HttpGet]
        public ActionResult<IEnumerable<UserGetDTO>> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<UserGetDTO>>(_userManager.Users.ToList()));
        }

        [ClaimsAuthorize("Admin", "Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDTO>> GetAsync(string id)
        {
            if (ModelState.IsValid)
            {
                return Ok(_mapper.Map<UserGetDTO>(await _userManager.FindByIdAsync(id)));
            }
            return NoContent();
        }

        [ClaimsAuthorize("Admin", "Delete")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {

            if (ModelState.IsValid)
            {
                var findById = await _userManager.FindByIdAsync(id);

                var result = await _userManager.DeleteAsync(findById);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                return Ok(result);
            }
            return NoContent();
        }

        [HttpPut()]
        public async Task<ActionResult<UserGetDTO>> UpdateAsync([FromBody] UserUpdateDTO identityUser)
        {
            if (ModelState.IsValid)
            {
                var findBy = await _userManager.FindByIdAsync(identityUser.Id);

                findBy.UserName = identityUser.Email;
                findBy.Email = identityUser.Email;

                var result = await _userManager.UpdateAsync(findBy);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(_mapper.Map<UserGetDTO>(await _userManager.FindByIdAsync(identityUser.Id))); ;
            }
            return BadRequest();
        }
    }
}