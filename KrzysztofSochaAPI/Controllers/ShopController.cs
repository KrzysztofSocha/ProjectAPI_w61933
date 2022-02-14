using KrzysztofSochaAPI.Services.Shop;
using KrzysztofSochaAPI.Services.Shop.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class ShopController : Controller
    {
        private readonly IShopAppService _shopAppService;
        public ShopController(IShopAppService shopAppService)
        {
            _shopAppService = shopAppService;
        }
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateOrUpdateShopDto input)
        {
            await _shopAppService.CreateShopAsync(input);
            return Ok();
        }
        [HttpGet("get/manage/informations/{shopId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult> GetAllInformations([FromRoute] int shopId)
        {
            var result = await _shopAppService.GetShopFullInformationsAsync(shopId);
            return Ok(result);
        }
        [HttpGet("get/all")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _shopAppService.GetShopsAsync();
            return Ok(result);
        }
        [HttpGet("get/{shopId}")]
        public async Task<ActionResult> GetById([FromRoute] int shopId)
        {
            var result = await _shopAppService.GetShopByIdAsync(shopId);
            return Ok(result);
        }
        [HttpPut("update/{shopId}")]
        public async Task<ActionResult> Update([FromRoute] int shopId,[FromBody]CreateOrUpdateShopDto input)
        {
            await _shopAppService.UpdateShopAsync(shopId,input);
            return Ok();
        }
        [HttpDelete("delete/{shopId}")]
        public async Task<ActionResult> Delete([FromRoute] int shopId)
        {
            await _shopAppService.DeleteShopAsync(shopId);
            return Ok();
        }

    }
}
