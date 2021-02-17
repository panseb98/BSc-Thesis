using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorization.Models;
using Authorization.Services;
using JobFinder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthorizationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("RegisterNewUser")]
        public async Task<IActionResult> RegisterNewUser(RegisterUser model)
        {
            var result = await _authService.RegisterNewUser(model);
            return Ok(new Response<LoggedUser>(result));
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            var result = await _authService.SingInUser(model);
            return Ok(new Response<LoggedUser>(result));
        }
    }
}