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
        public async Task<ActionResult> CreateClothes([FromBody] AddClothesInputDto input)
        {
            await _clothesAppService.CreateClothesAsync(input);
            return Ok();
        }
        [HttpPost("add/image")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> AddImage([FromForm] AddImageToClothesInputDto input)
        {
            await _clothesAppService.AddImageToClothesAsync(input);
            return Ok();
        }
        [HttpDelete("delete/image/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult DeleteImage([FromRoute] int id)
        {
             _clothesAppService.DeleteImageClothes(id);
            return Ok();
        }
        [HttpDelete("archive/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Archive([FromRoute] int id)
        {
            _clothesAppService.ArchiveClothes(id);
            return Ok();
        }
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateClothesInputDto input)
        {
            _clothesAppService.UpdateClothes(id, input);
            return Ok();
        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var result = await _clothesAppService.GetClothesByIdAsync(id);
            return Ok(result);
        }
        [HttpGet("get")]
        public async Task<ActionResult> GetAsync([FromQuery] GetClothesInputDto input)
        {
            var result = await _clothesAppService.GetClothesAsync(input);
            return Ok(result);
        }
    }
}
