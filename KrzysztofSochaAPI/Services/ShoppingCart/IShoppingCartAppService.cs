using KrzysztofSochaAPI.Services.ShoppingCart.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.ShoppingCart
{
    public interface IShoppingCartAppService
    {
        Task AddItemToShoppingCartAsync(AddShoppingCartItemDto input);
        Task RemoveItemFromShoppingCartAsync(int clothesId);
        Task<GetShopingCartOutputDto> GetUserShopingCartListAsync();
        Task ClearShopingCartListAsync();
    }
}
