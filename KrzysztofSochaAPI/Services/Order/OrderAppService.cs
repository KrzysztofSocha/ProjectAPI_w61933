using KrzysztofSochaAPI.Services.Order.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Order
{
    public class OrderAppService : IOrderAppService
    {
        public OrderAppService()
        {

        }
        public Task CancelOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task CreateOrderAsync(CreateOrderInputDto input)
        {
            throw new NotImplementedException();
        }

        public Task<GetOrderOutputDto> GetOrderByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOrderOutputDto>> GetOrdersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
