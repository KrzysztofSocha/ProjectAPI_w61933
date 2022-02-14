using KrzysztofSochaAPI.Services.Shop.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Shop
{
    interface IShopAppService
    {
        Task CreateShopAsync(CreateOrUpdateShopDto input);
        Task UpdateShopAsync(int shopId, CreateOrUpdateShopDto input);
        Task<GetShopOutputDto> GetShopByIdAsync(int shopId);
        Task<GetShopFullInformationsDto> GetShopFullInformationsAsync(int shopId);
        Task<List<GetShopOutputDto>> GetShopsAsync();
        Task DeleteShopAsync(int shopId);
    }
}
