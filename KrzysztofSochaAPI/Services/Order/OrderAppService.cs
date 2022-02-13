using AutoMapper;
using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Enums;
using KrzysztofSochaAPI.Exceptions;
using KrzysztofSochaAPI.Services.Address.Dto;
using KrzysztofSochaAPI.Services.Order.Dto;
using KrzysztofSochaAPI.Services.ShoppingCart;
using KrzysztofSochaAPI.Services.ShoppingCart.Dto;
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
        private readonly IShoppingCartAppService _shoppingCartAppService;

        private readonly IUserContextAppService _userContextAppService;
        public OrderAppService(ProjectDbContext context,
          IMapper mapper,
          IUserContextAppService userContextAppService,
          IShoppingCartAppService shoppingCartAppService)
        {
            _context = context;
            _mapper = mapper;
            _userContextAppService = userContextAppService;
            _shoppingCartAppService = shoppingCartAppService;
        }

       
        public async Task CancelOrderAsync(int orderId)
        {            
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order is null)
                throw new NotFoundException("Nie istnieje zamówinie o podanym numerze id");
            if (order.Status != OrderStatus.InPreparation || order.PurchaserId != _userContextAppService.GetUserId)
                throw new BadRequestException("Nie możesz anulować tego zamówienia");
            try
            {
                order.Status = OrderStatus.Canceled;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas anulowania zamówienia {ex.Message}");
            }
        }

        public async Task ChangeOrderStatusAsync(ChangeOrderStatusInputDto input)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == input.OrderId);
            if (order is null)
                throw new NotFoundException("Nie istnieje zamówinie o podanym numerze id");
            if(order.Status==OrderStatus.Received)
                throw new BadRequestException("Nie możesz zmienić statusu tego zamówienia");
            try
            {
                if (input.Status == OrderStatus.Received)
                    order.ReceivedTime = DateTime.Now;
                order.Status = input.Status;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas modyfikacji statusu zamówienia {ex.Message}");
            }
        }

        public async Task<CreateOrderOutputDto> CreateOrderAsync(CreateOrderInputDto input)
        {
            #region Context
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
            #endregion
            try
            {
                var newOrder = new Models.Order()
                {
                    CreationTime = DateTime.Now,
                    PurchaserId = user.Id,
                    Status = OrderStatus.InPreparation,
                    Delivery = delivery

                };
                #region AddClothes
                var orderedClothes = new List<Models.OrderClothes>();
                decimal sum = 0;
                foreach (var item in user.ClothesToBuy)
                {
                    var clothes = _mapper.Map<Models.OrderClothes>(item);
                    sum += item.Quantity * item.Clothes.Price;
                    orderedClothes.Add(clothes);
                }
                newOrder.OrderedClothesList=orderedClothes;
                #endregion
                #region CalculateOrderPrice
                var output = new CreateOrderOutputDto();
                output.User = _mapper.Map<GetUserOrderDto>(user);
                if (sum > 150)
                {
                    newOrder.FreeDelivery = true;
                    output.DeliveryPrice = 0;
                }
                else
                    output.DeliveryPrice = delivery.Price;
                output.ClothesAmount = sum;
                output.TotalAmount = output.DeliveryPrice + output.ClothesAmount;
                #endregion
                #region CreateOrderAddress
                switch (input.DeliveryType)
                {
                    case DeliveryType.ToShop:
                        var shop = await _context.Shops.FirstOrDefaultAsync(x=>x.Id==input.TargetShopId);
                        newOrder.DeliveryAddress = shop.Address;
                        output.User.AddressDelivery = _mapper.Map<AddressDto>(shop.Address);
                        break;
                    case DeliveryType.ToHouse:
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
                        break;
                    case DeliveryType.ToParcelLocker:
                        break;
                    default:
                        break;
                }

                #endregion
                #region RegisterPayment
                newOrder.Payment = new Models.Payment()
                {
                    Amount = output.TotalAmount,
                    Status = PaymentStatus.Waiting,
                    Type = input.PaymentType
                };
                #endregion
                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();
                await _shoppingCartAppService.ClearShopingCartListAsync();
                output.Id = newOrder.Id;
                return output;

            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas dodwania zamówienia: {ex.Message}");
            }
        }

        public async Task<GetOrderOutputDto> GetOrderByIdAsync(int orderId)
        {
            
            var order = await _context.Orders.Include(x => x.Payment)                
                .Include(x => x.DeliveryAddress)
                .Include(x => x.Delivery)
                .Include(x => x.OrderedClothesList)
                .ThenInclude(x => x.OrderedClothes)
                .FirstOrDefaultAsync(x => x.Id == orderId );
            if (order is null)
                throw new NotFoundException("Błąd podczas pobierania danych zamówienia");
            try
            {
                var output = _mapper.Map<GetOrderOutputDto>(order);
                if (order.FreeDelivery)
                    output.DeliveryPrice = 0;
                
                output.Clothes= _mapper.Map<List<GetShopingCartClothesDto>>(order.OrderedClothesList);
                foreach (var item in output.Clothes)
                {
                    output.ClothesAmount += item.SumPrice;
                }
                output.TotalAmount = output.ClothesAmount + output.DeliveryPrice;
                output.QuantityItems = output.Clothes.Count;
                return output;
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas pobierania zamówienia: {ex.Message}");
            }
        }

        public  Task<List<GetManyOrdersOutputDto>> GetUserOrders()
        {
            var userOrders =  _context.Orders.Include(x => x.Payment)               
               .Include(x => x.Purchaser)
               .Include(x => x.Delivery)
               .Include(x => x.OrderedClothesList)
               .ThenInclude(x => x.OrderedClothes)
               .Where(x => x.PurchaserId == _userContextAppService.GetUserId);
            
            try
            {
                var output = _mapper.Map < List<GetManyOrdersOutputDto>>(userOrders);
                foreach (var order in userOrders)
                {
                    decimal sumOrderClothes =0;
                    foreach (var item in order.OrderedClothesList)
                    {
                        sumOrderClothes += item.OrderedClothes.Price;
                    }
                    var outputItem =output.Where(x => x.Id == order.Id).Single();
                    outputItem.QuantityItems = order.OrderedClothesList.Count;
                    outputItem.ClothesAmount = sumOrderClothes;
                    outputItem.TotalAmount = sumOrderClothes + outputItem.DeliveryPrice;
                }
                return Task.FromResult(output);
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas pobiernia zamówień użytkownika: {ex.Message}");
            }
        }

        public async Task PayForOrderAsync(PaymentDto input)
        {
            var order = await _context.Orders
                .Include(x=>x.Payment)
                .FirstOrDefaultAsync(x => x.Id == input.OrderId && x.Payment.Status == PaymentStatus.Waiting);
            if (order is null)
                throw new NotFoundException("Nie znaleziono zamówienia nieopłaconego");
            try
            {
                switch (order.Payment.Type)
                {
                    #region Blik
                    case PaymentType.Blik:                        
                        if (!System.Text.RegularExpressions.Regex.IsMatch(input.BlikCode, "^[0-9]*$"))
                            throw new BadRequestException("Niepoprawna wartość kodu Blik");
                        order.Payment.Status = PaymentStatus.Registered;
                        await _context.SaveChangesAsync();
                        await Task.Delay(5000);//Możliwość dodania mechanizmu związanego z płatnością blik
                        order.Payment.PaymentDate = DateTime.Now;
                        order.Payment.Status = PaymentStatus.Completed;
                        await _context.SaveChangesAsync();
                        break;
                    #endregion
                    #region Bank
                    case PaymentType.Bank:
                        if(!String.IsNullOrEmpty(input.BankName))
                            throw new BadRequestException("Niepoprawna nazwa banku");
                        order.Payment.Status = PaymentStatus.Registered;
                        await _context.SaveChangesAsync();
                        await Task.Delay(5000);//Możliwość dodania mechanizmu związanego z płatnością za pomocą aplikacji bankowej
                        order.Payment.PaymentDate = DateTime.Now;
                        order.Payment.Status = PaymentStatus.Completed;
                        await _context.SaveChangesAsync();
                        break;
                    #endregion
                    default:
                        throw new BadRequestException("Niepoprawna forma płatności");
                        
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas dokonywania płatności: {ex.Message}");
            }
        }
    }
}
