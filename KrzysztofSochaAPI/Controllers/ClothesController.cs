using KrzysztofSochaAPI.Services.Clothes;
using KrzysztofSochaAPI.Services.Clothes.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Controllers
{
    [Route("api/clothes")]
    [ApiController]
    public class ClothesController : Controller
    {
        private readonly IClothesAppService _clothesAppService;
        public ClothesController(IClothesAppService clothesAppService)
        {
            _clothesAppService = clothesAppService;
        }
        [HttpPost("create")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateClothes([FromBody] AddClothesInputDto input)
        {
            _clothesAppService.CreateClothes(input);
            return Ok();
        }
        [HttpPost("add/image")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult AddImage([FromForm] AddImageToClothesInputDto input)
        {
            _clothesAppService.AddImageToClothes(input);
            return Ok();
        }
    }
}
