using AutoMapper;
using CentralDeErros.Services;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using CentralDeErros.Core.Extensions;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace CentralDeErros.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserService _service;
        private readonly IMapper _mapper;
        private readonly TokenGeneratorService _tokenGeneratorService;

        public AuthController(UserService service, IMapper mapper, 
                              SignInManager<IdentityUser> signInManager, 
                              UserManager<IdentityUser> userManager, 
                              TokenGeneratorService tokenGeneratorService)

        {
            _service = service;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenGeneratorService = tokenGeneratorService;
        }

        [HttpPost("createaccount")]
        public async Task<ActionResult> Post([FromBody] RegisterUserDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _mapper.Map<IdentityUser>((value));

            var result = await _userManager.CreateAsync(user, value.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Created(nameof(Post), await _tokenGeneratorService.TokenGenerator(user.Email));
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, true);

            if (result.Succeeded) return Ok(await _tokenGeneratorService.TokenGenerator(loginDTO.Email));

            if (result.IsLockedOut) return BadRequest(new { Message = "Limite de tentativas excedido! Tente novamente mais tarde." });

            return BadRequest( new { Message = "Usuário ou senha incorretos" });
        }
    }
}
