using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Services.User;
using KrzysztofSochaAPI.Services.User.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;


namespace KrzysztofSochaAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;

        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto input)
        {
            _userAppService.RegisterUser(input);
            return Ok();
        }
        [HttpPost("register/manager")]
        [Authorize(Roles = "Admin")]
        public ActionResult RegisterUserMnager([FromBody] RegisterUserDto input)
        {
            _userAppService.RegisterUserManager(input);
            return Ok();
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto input)
        {
            string token = _userAppService.GenerateJwt(input);
            return Ok(token);
        }
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateUserDto input, [FromRoute] int id)
        {
            var output = await _userAppService.UpdateUserAsync(id, input);
            return Ok(output);
        }
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            var result = await _userAppService.DeleteUserAsync(id);
            return Ok(result);
        }
        [HttpPut("reset/password")]
        [Authorize(Roles = "Admin")]
        public ActionResult ResetPassword([FromBody] ResetPasswordDto input)
        {
            var result = _userAppService.ResetUserPassword(input).Result;
            return Ok(result);
        }
        [HttpPut("change/password")]
        [Authorize]
        public ActionResult ChangePassword([FromBody] ChangePasswordDto input)
        {
            var result = _userAppService.ChangeUserPassword(input).Result;
            return Ok(result);
        }
    }
}

