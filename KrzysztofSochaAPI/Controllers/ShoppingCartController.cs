using KrzysztofSochaAPI.Services.ShoppingCart;
using KrzysztofSochaAPI.Services.ShoppingCart.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Controllers
{
    [Route("api/shopping/cart")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartAppService _shoppingCartAppService;
        public ShoppingCartController(IShoppingCartAppService shoppingCartAppService)
        {
            _shoppingCartAppService = shoppingCartAppService;
        }
        [HttpPost("add/item")]
        [Authorize(Roles = "User")]
        public async Task <ActionResult> AddItem([FromForm] AddShoppingCartItemDto input)
        {
            await _shoppingCartAppService.AddItemToShoppingCartAsync(input);
            return Ok();
        }
        [HttpGet("get")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> GetUserShoppingCart()
        {
            var result =await _shoppingCartAppService.GetUserShopingCartListAsync();
            return Ok(result);
        }
        
        [HttpDelete("delete/{clothesId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> DeleteItemFromShoppingCart([FromRoute] int clothesId)
        {
            await _shoppingCartAppService.RemoveItemFromShoppingCartAsync(clothesId);
            return Ok();
        }
        [HttpDelete("clear")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> ClearShoppingCartList()
        {
            await _shoppingCartAppService.ClearShopingCartListAsync();
            return Ok();
        }
    }
}
