﻿using KrzysztofSochaAPI.Services.Order.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Order
{
    public interface IOrderAppService
    {
        Task CreateOrderAsync(CreateOrderInputDto input);
        Task CancelOrderAsync(int orderId);
        Task <GetOrderOutputDto>GetOrderByIdAsync(int orderId);
        Task<List<GetOrderOutputDto>> GetOrdersAsync();

    }
}
