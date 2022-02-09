using KrzysztofSochaAPI.Services.Order;
using KrzysztofSochaAPI.Services.Order.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }
        [HttpPost("create")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Create([FromForm]CreateOrderInputDto input)
        {
            var result =await _orderAppService.CreateOrderAsync(input);
            return Ok(result);
        }
        [HttpGet("get/{orderId}")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult> GetById([FromRoute] int orderId)
        {
            var result =await _orderAppService.GetOrderByIdAsync(orderId);
            return Ok(result);
        }
        [HttpGet("get")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Get()
        {
            var result = await _orderAppService.GetUserOrdersAsync();
            return Ok(result);
        }
    }
}
