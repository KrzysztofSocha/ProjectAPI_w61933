using AutoMapper;
using KrzysztofSochaAPI.Authorization;
using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Exceptions;
using KrzysztofSochaAPI.Services.Shop.Dto;
using KrzysztofSochaAPI.Services.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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
        private readonly IAuthorizationService _authorizationService;


        public ShopAppService(ProjectDbContext context,
          IMapper mapper,
          IUserContextAppService userContextAppService,
          IAuthorizationService authorizationService)
        {
            _context = context;
            _mapper = mapper;
            _userContextAppService = userContextAppService;
            _authorizationService = authorizationService;
        }
        public async Task CreateShopAsync(CreateOrUpdateShopDto input)
        {
            try
            {
                var newShop = _mapper.Map<CreateOrUpdateShopDto, Models.Shop>(input);
                newShop.ManagerId = (int)_userContextAppService.GetUserId;
                var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextAppService.User,
                    newShop,
                    new ResourceOperationRequirement(ResourceOperation.Create));
                if (!authorizationResult.Succeeded)
                {
                    throw new ForbiddenException("Nie masz uprawnień do tej operacji.");
                }
                await _context.Shops.AddAsync(newShop);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas dodwania sklepu: {ex.Message}");
            }
            
        }

        public async Task DeleteShopAsync(int shopId)
        {
            try
            {
                var shopToDelete = await _context.Shops.FirstOrDefaultAsync(x => x.Id == shopId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextAppService.User,
                   shopToDelete,
                   new ResourceOperationRequirement(ResourceOperation.Delete));
                if (!authorizationResult.Succeeded)
                {
                    throw new ForbiddenException("Nie masz uprawnień do tej operacji.");
                }
                _context.Shops.Remove(shopToDelete);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas usuwania sklepu: {ex.Message}");
            }
        }

        public async Task<GetShopOutputDto> GetShopByIdAsync(int shopId)
        {            
            try
            {
                var shop = await _context.Shops.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == shopId);
                var output = _mapper.Map<GetShopOutputDto>(shop);
                return output;
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas wyświetlania sklepu: {ex.Message}");
            }
        }

        public async Task<GetShopFullInformationsDto> GetShopFullInformationsAsync(int shopId)
        {
            try
            {
                var shop = await _context.Shops.Include(x => x.Address)
                    .Include(x=>x.Manager)
                    .ThenInclude(x=>x.Address)                    
                    .FirstOrDefaultAsync(x => x.Id == shopId);
                var output = _mapper.Map<GetShopFullInformationsDto>(shop);
                return output;
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas wyświetlania sklepu: {ex.Message}");
            }
        }

        public async Task<List<GetShopOutputDto>> GetShopsAsync()
        {
            try
            {
                var shops = await _context.Shops.Include(x => x.Address).ToListAsync();
                var output = _mapper.Map <List<GetShopOutputDto>>(shops);
                return output;
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas wyświetlania sklepów: {ex.Message}");
            }
        }

        public async Task UpdateShopAsync(int shopId,CreateOrUpdateShopDto input)
        {
            try
            {

                var shop = await _context.Shops.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == shopId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextAppService.User,
                    shop,
                    new ResourceOperationRequirement(ResourceOperation.Update));
                if (!authorizationResult.Succeeded)
                {
                    throw new ForbiddenException("Nie masz uprawnień do tej operacji.");
                }
                shop = _mapper.Map(input, shop);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas modyfikacji informacji o sklepie: {ex.Message}");
            }
        }
    }
}
