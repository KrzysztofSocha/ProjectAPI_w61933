using AutoMapper;
using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Services.Shop.Dto;
using KrzysztofSochaAPI.Services.UserContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Shop
{    
    
    public class ShopAppService : IShopAppService
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextAppService _userContextAppService;
        


        public ShopAppService(ProjectDbContext context,
          IMapper mapper,
          IUserContextAppService userContextAppService         )
        {
            _context = context;
            _mapper = mapper;
            _userContextAppService = userContextAppService;
        }
        public Task CreateShopAsync(CreateShopDto input)
        {
            throw new NotImplementedException();
        }

        public Task DelteShopAsync(int shopId)
        {
            throw new NotImplementedException();
        }

        public Task<GetShopOutputDto> GetShopByIdAsync(int shopId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetManyShopsDto>> GetShopsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateShopAsync(UpdateShopDto input)
        {
            throw new NotImplementedException();
        }
    }
}
