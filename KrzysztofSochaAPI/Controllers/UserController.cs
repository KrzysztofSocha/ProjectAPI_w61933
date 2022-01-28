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
                throw new Exception(ex.Message);

            }

        }
        [HttpPut("update/{id}")]
        [Authorize]
        public ActionResult Update([FromBody] UpdateUserDto input,[FromRoute] int id)
        {

            try
            {
                var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var output = _userAppService.UpdateUser(id,input);
                return Ok(output);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
        [HttpDelete("delete/{id}")]
       
        public ActionResult Delete([FromRoute] int id)
        {

            try
            {
                
                var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var result =_userAppService.DeleteUser(id,User,userId).Result;
               
                return Ok(result);               

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }

        }
        [HttpPut("resetpassword")]
        [Authorize(Roles = "Admin")]
        public ActionResult ResetPassword([FromBody] ResetPasswordDto input)
        {

            try
            {
                var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var result = _userAppService.ResetUserPassword(input, userId).Result;

                return Ok(result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
    }
}

