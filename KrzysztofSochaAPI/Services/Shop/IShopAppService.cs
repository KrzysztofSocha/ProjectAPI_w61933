using KrzysztofSochaAPI.Services.Shop.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Shop
{
    interface IShopAppService
    {
        Task CreateShopAsync(CreateShopDto input);
        Task UpdateShopAsync(UpdateShopDto input);
        Task<GetShopOutputDto> GetShopByIdAsync(int shopId);
        Task<List<GetManyShopsDto>> GetShopsAsync();
        Task DelteShopAsync(int shopId);
    }
}
