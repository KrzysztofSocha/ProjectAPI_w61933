using AutoMapper;
using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Enums;
using KrzysztofSochaAPI.Exceptions;
using KrzysztofSochaAPI.Services.Address.Dto;
using KrzysztofSochaAPI.Services.Order.Dto;
using KrzysztofSochaAPI.Services.UserContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Order
{
    public class OrderAppService : IOrderAppService
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        private readonly IUserContextAppService _userContextAppService;
        public OrderAppService(ProjectDbContext context,
          IMapper mapper,
          IUserContextAppService userContextAppService)
        {
            _context = context;
            _mapper = mapper;
            _userContextAppService = userContextAppService;
        }
        public Task CancelOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateOrderAsync(CreateOrderInputDto input)
        {
            var user = await _context.Users
                .Include(x => x.Address)
                .Include(x => x.ClothesToBuy)
                .ThenInclude(x => x.Clothes)
                .FirstOrDefaultAsync(x => x.Id == _userContextAppService.GetUserId
                && x.IsDeleted == false);

            if (user is null)
                throw new NotFoundException("Błąd podczas pobierania danych użytkownika");
            if (user.ClothesToBuy.Count == 0)
                throw new NotFoundException("Aby złożyć zamówienie najpierw dodaj ubranie do koszyka");

            var delivery = await _context.Deliveries.FirstOrDefaultAsync(x => x.Type == input.DeliveryType);
            try
            {
                var newOrder = new Models.Order()
                {
                    CreationTime = DateTime.Now,
                    PurchaserId = user.Id,
                    Status = OrderStatus.InPreparation,
                    Delivery = delivery

                };
                var orderedClothes = new List<Models.OrderClothes>();
                decimal sum = 0;
                foreach (var item in user.ClothesToBuy)
                {
                    orderedClothes.Add(_mapper.Map<Models.OrderClothes>(item));
                    sum += item.Quantity * item.Clothes.Price;
                }
                if (sum > 150)
                    newOrder.FreeDelivery = true;
                var output = new CreateOrderOutputDto();
                output.User = _mapper.Map<GetUserOrderDto>(user);
                if (input.CustomDeliveryAddress)
                {
                    var customAddress = _mapper.Map<Models.Address>(input.DeliveryAddress);
                    await _context.Addresses.AddAsync(customAddress);
                    newOrder.DeliveryAddress = customAddress;
                    output.User.AddressDelivery = input.DeliveryAddress;
                }
                else
                {
                    newOrder.DeliveryAddress = user.Address;
                    output.User.AddressDelivery = _mapper.Map<AddressDto>(user.Address);
                }
                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();                
                
                if (newOrder.FreeDelivery)
                    output.DeliveryPrice = 0;
                else
                    output.DeliveryPrice = delivery.Price;

                output.ClothesAmount = sum;
                output.TotalAmount = output.ClothesAmount + output.ClothesAmount;
                output.Id = newOrder.Id;

                
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas dodwania zamówienia: {ex.Message}");
            }
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
