using AutoMapper;
using CentralDeErros.Services;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using CentralDeErros.Transport.MicrosserviceDTOs;
using CentralDeErros.Core.Extensions;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using CentralDeErros.Model.Models;
using CentralDeErros.Services.Interfaces;

namespace CentralDeErros.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInUserManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMicrosserviceService _microsserviceService;
        private readonly IMapper _mapper;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public AuthController(IMicrosserviceService microsserviceService,
                              IMapper mapper,
                              SignInManager<IdentityUser> signInUserManager,
                              UserManager<IdentityUser> userManager,
                              ITokenGeneratorService tokenGeneratorService)

        {
            _microsserviceService = microsserviceService;
            _mapper = mapper;
            _signInUserManager = signInUserManager;
            _userManager = userManager;
            _tokenGeneratorService = tokenGeneratorService;
        }

        [HttpPost("CreateUserAccount")]
        public async Task<ActionResult> Post([FromBody] RegisterUserDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _mapper.Map<IdentityUser>((value));
            user.UserName = user.Email;
            var result = await _userManager.CreateAsync(user, value.Password);

            if (result.Succeeded)
            {
                await _signInUserManager.SignInAsync(user, false);
                return Created(
                    nameof(Post),
                    new { token = await _tokenGeneratorService.TokenGenerator(user.Email) }
                );
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("UserLogin")]
        public async Task<ActionResult> UserLogin(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _signInUserManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, true);

            if (result.Succeeded) return Ok(new { token = await _tokenGeneratorService.TokenGenerator(loginDTO.Email) });

            if (result.IsLockedOut) return BadRequest(new { Message = "Limite de tentativas excedido! Tente novamente mais tarde." });

            return BadRequest(new { Message = "Usuário ou senha incorretos" });
        }

        [HttpPost("MicrosserviceLogin")]
        public ActionResult MicrosserviceLogin(MicrosserviceLoginDTO microsserviceLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = _microsserviceService.ValidateMicrosserviceCredentials(_mapper.Map<Microsservice>(microsserviceLogin));
            if (result)
            {
                return Ok(_tokenGeneratorService.TokenGeneratorMicrosservice(microsserviceLogin.ClientId.ToString()));
            }
            //if (result.IsLockedOut) return BadRequest(new { Message = "Limite de tentativas excedido! Tente novamente mais tarde." });
            else
            {
                return BadRequest(new { Message = "ClientId ou ClientSecret incorretos" });
            }
        }
    }
}
