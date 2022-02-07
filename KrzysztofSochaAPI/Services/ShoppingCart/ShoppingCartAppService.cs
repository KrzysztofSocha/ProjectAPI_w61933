using AutoMapper;
using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Exceptions;
using KrzysztofSochaAPI.Services.ShoppingCart.Dto;
using KrzysztofSochaAPI.Services.UserContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.ShoppingCart
{
    public class ShoppingCartAppService : IShoppingCartAppService
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextAppService _userContextAppService;
        public ShoppingCartAppService(ProjectDbContext context,
          IMapper mapper,
          IUserContextAppService userContextAppService)
        {
            _context = context;
            _mapper = mapper;
            _userContextAppService = userContextAppService;
        }
        public async Task AddItemToShoppingCartAsync(AddShoppingCartItemDto input)
        {
            var user = await _context.Users.Include(x => x.ClothesToBuy).FirstOrDefaultAsync(x => x.Id == _userContextAppService.GetUserId
              && x.IsDeleted == false);
            if (user is null)
                throw new NotFoundException("Błąd podczas pobierania danych użytkownika");
            var clothes = await _context.Clothes.FirstOrDefaultAsync(x => x.Id == input.ClothesId && x.IsAvailability == true);
            if (clothes is null)
                throw new NotFoundException("Błąd podczas pobierania ubrania");
            if (input.Quantity <= 0)
                throw new BadRequestException("Niepoprawna ilość ubrań");
            try
            {
                var item = user.ClothesToBuy.FirstOrDefault(x => x.ClothesId == input.ClothesId && x.Size == input.Size);
                if (item is not null)
                    item.Quantity += input.Quantity;
                else
                {
                    var newShoppingCartItem = _mapper.Map<AddShoppingCartItemDto, Models.ShoppingCartItem>(input);
                    newShoppingCartItem.UserId = user.Id;
                    user.ClothesToBuy.Add(newShoppingCartItem);
                }
               
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas dodawania ubrania do koszyka: {ex.Message}");
            }
        }

        public async Task ClearShopingCartListAsync()
        {
            var user = await _context.Users.Include(x => x.ClothesToBuy).FirstOrDefaultAsync(x => x.Id == _userContextAppService.GetUserId
             && x.IsDeleted == false);
            if (user is null)
                throw new NotFoundException("Błąd podczas pobierania danych użytkownika");
            try
            {
                user.ClothesToBuy.Clear();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd usuwania pozycji z koszyka: {ex.Message}");
            }
        }

        public async Task<GetShopingCartOutputDto> GetUserShopingCartListAsync()
        {
            var user = await _context.Users
                .Include(x => x.ClothesToBuy)
                .ThenInclude(x => x.Clothes)
                .FirstOrDefaultAsync(x => x.Id == _userContextAppService.GetUserId
                 && x.IsDeleted == false);
            if (user is null)
                throw new NotFoundException("Błąd podczas pobierania danych użytkownika");
            try
            {
                var clothesList = new List<GetShopingCartClothesDto>();
                decimal totalSum = 0;
                foreach (var item in user.ClothesToBuy)
                {
                    var clothesOutput = _mapper.Map<Models.Clothes, GetShopingCartClothesDto>(item.Clothes);
                    clothesOutput.Quantity = item.Quantity;
                    clothesOutput.Size = item.Size;
                    clothesOutput.SumPrice = item.Quantity * item.Clothes.Price;
                    totalSum += clothesOutput.SumPrice;
                    clothesList.Add(clothesOutput);
                }
                var output = new GetShopingCartOutputDto()
                {
                    ShoppingCartList = clothesList,
                    NumberOfItem=clothesList.Count,
                    TotalSumPrice=totalSum
                };
                return output;
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas pobierania koszyka: {ex.Message}");
            }
        }

        public async Task RemoveItemFromShoppingCartAsync(int clothesId)
        {
            var user = await _context.Users.Include(x => x.ClothesToBuy).FirstOrDefaultAsync(x => x.Id == _userContextAppService.GetUserId
              && x.IsDeleted == false);
            if (user is null)
                throw new NotFoundException("Błąd podczas pobierania danych użytkownika");
            var itemToRemove =user.ClothesToBuy.FirstOrDefault(x => x.ClothesId == clothesId);
            if (itemToRemove is null)
                throw new NotFoundException("Błąd podczas pobierania pozycji z koszyka użytkownika");
            try
            {
                if (itemToRemove.Quantity > 1)
                    itemToRemove.Quantity -= 1;
                else
                    user.ClothesToBuy.Remove(itemToRemove);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas usuwania pozycji z koszyka: {ex.Message}");
            }
        }
    }
}
