using ApiOnion104.Application.Abstractions.Services;
using ApiOnion104.Application.DTOs.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiOnion104.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public UsersController(IAuthenticationService service)
        {
            _service = service;
        }
        [HttpPost] 
       public async Task<IActionResult> Register([FromForm]RegisterDto dto)
        {
            await _service.Regoster(dto);
            return NoContent();
        }

    }
}
