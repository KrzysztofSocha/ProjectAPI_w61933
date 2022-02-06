using AutoMapper;
using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Exceptions;
using KrzysztofSochaAPI.Models;
using KrzysztofSochaAPI.Services.Clothes.Dto;
using KrzysztofSochaAPI.Services.UserContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Clothes
{
    public class ClothesAppService : IClothesAppService
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        private readonly IUserContextAppService _userContextAppService;
        public ClothesAppService(ProjectDbContext context,
          IMapper mapper,
          IUserContextAppService userContextAppService)
        {
            _context = context;
            _mapper = mapper;
            _userContextAppService = userContextAppService;
        }
        public async Task AddImageToClothesAsync(AddImageToClothesInputDto input)
        {
            var clothes = await _context.Clothes.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == input.ClothesId);
            if (clothes is null)
                throw new NotFoundException("Nie znaleziono ubrania o podanym numerze ID");
            try
            {
                var image = new Models.Image()
                {
                    ClothesId = input.ClothesId,
                    Name = input.Image.FileName
                };
                MemoryStream ms = new MemoryStream();
                input.Image.CopyTo(ms);
                image.ImageFile = ms.ToArray();
                ms.Close();
                ms.Dispose();
                clothes.Images.Add(image);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas dodawnia zdjęcia do ubrania: {ex.Message}");
            }
        }

        public void ArchiveClothes(int id)
        {
            var clothes = _context.Clothes.FirstOrDefault(x => x.Id == id);
            if (clothes is null)
                throw new NotFoundException("Nie znaleziono ubrania o podanym numerze ID");
            try
            {
                clothes.IsAvailability = false;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas archizowania ubrania: {ex.Message}");
            }
        }

        public async Task CreateClothesAsync(AddClothesInputDto input)
        {
            try
            {
                var newClothes = _mapper.Map<AddClothesInputDto, Models.Clothes>(input);
                newClothes.CreationTime = DateTime.Now;
                await _context.Clothes.AddAsync(newClothes);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas dodawania nowego ubrania: {ex.Message}");
            }
        }

        public void UpdateClothes(int id, UpdateClothesInputDto input)
        {
            var clothes = _context.Clothes.FirstOrDefault(x => x.Id == id);
            if (clothes is null)
                throw new NotFoundException("Nie znaleziono ubrania o podanym numerze ID");
            try
            {
                clothes = _mapper.Map(input, clothes);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas archizowania ubrania: {ex.Message}");
            }
        }
        public async Task<GetClothesOutputDto> GetClothesByIdAsync(int id)
        {
            var clothes = await _context.Clothes.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id && x.IsAvailability == true);
            if (clothes is null)
                throw new NotFoundException("Nie znaleziono ubrania o podanym numerze ID");
            try
            {
                var output = _mapper.Map<Models.Clothes, GetClothesOutputDto>(clothes);

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas pobierania ubrania: {ex.Message}");
            }
        }
        public async Task<List<GetClothesOutputDto>> GetClothesAsync(GetClothesInputDto input)
        {
            try
            {
                var clothes = await _context.Clothes.Include(x => x.Images)
                .Where(x => x.IsAvailability == true && x.Category == input.Category && x.Sex==input.Sex)
                .Skip(input.StartIndex)
                .Take(input.MaxResultCount)
                .ToListAsync();

                if (input.MaxPrice != 0)
                    clothes = clothes.Where(x => x.Price < input.MaxPrice).ToList();
                if (input.MinPrice != 0)
                    clothes = clothes.Where(x => x.Price > input.MinPrice).ToList();

                switch (input.SortingType)
                {
                    case Enums.SortingClothesType.Default:
                        clothes = clothes.OrderByDescending(x => x.CreationTime).ToList();
                        break;
                    case Enums.SortingClothesType.AscendingPrice:
                        clothes = clothes.OrderBy(x => x.Price).ToList();
                        break;
                    case Enums.SortingClothesType.DescendingPrice:
                        clothes = clothes.OrderByDescending(x => x.Price).ToList();
                        break;
                    default:
                        break;
                }

                var output = _mapper.Map<List<Models.Clothes>, List<GetClothesOutputDto>>(clothes);

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas pobierania ubrań: {ex.Message}");
            }

        }

        public void DeleteImageClothes(int id)
        {
            var image = _context.Images.FirstOrDefault(x => x.Id == id);
            if (image is null)
                throw new NotFoundException("Nie znaleziono zdjęcia o podanym numerze ID");
            try
            {
                _context.Images.Remove(image);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas usuwania zdjęcia: {ex.Message}");
            }
        }
    }

}
