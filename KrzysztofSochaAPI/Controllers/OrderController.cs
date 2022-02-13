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
        public async Task<ActionResult> Create([FromForm] CreateOrderInputDto input)
        {
            var result = await _orderAppService.CreateOrderAsync(input);
            return Ok(result);
        }
        [HttpGet("get/{orderId}")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult> GetById([FromRoute] int orderId)
        {
            var result = await _orderAppService.GetOrderByIdAsync(orderId);
            return Ok(result);
        }
        [HttpGet("get/all/user/orders")]
        [Authorize(Roles = "User")]
        public ActionResult Get()
        {
            var result = _orderAppService.GetUserOrders();
            return Ok(result);
        }
        [HttpPut("change/status")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<ActionResult> ChangeStatus([FromBody] ChangeOrderStatusInputDto input )
        {
             await _orderAppService.ChangeOrderStatusAsync(input);
            return Ok();
        }
        [HttpDelete("cancel/{orderId}")]
        [Authorize(Roles = "User")]
        public async Task <ActionResult> Cancel([FromRoute] int orderId)
        {
            await _orderAppService.CancelOrderAsync(orderId);
            return Ok();
        }
        [HttpPut("payment")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Payment([FromBody] PaymentDto input)
        {
            await _orderAppService.PayForOrderAsync(input);
            return Ok();
        }
    }
}
