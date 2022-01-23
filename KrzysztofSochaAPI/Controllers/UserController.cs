using KrzysztofSochaAPI.Services.User;
using KrzysztofSochaAPI.Services.User.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            try
            {
                _userAppService.RegisterUser(input);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new (ex.Message);
                
            }
           
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto input)
        {
            
            try
            {
                string token = _userAppService.GenerateJwt(input);
                return Ok(token);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);

            }

        }
    }
}

