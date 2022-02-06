using AutoMapper;
using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Exceptions;
using KrzysztofSochaAPI.Models;
using KrzysztofSochaAPI.Services.Clothes.Dto;
using KrzysztofSochaAPI.Services.UserContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public void AddImageToClothes(AddImageToClothesInputDto input)
        {
            var clothes = _context.Clothes.Include(x => x.Images).FirstOrDefault(x => x.Id == input.ClothesId);
            if (clothes is null)
                throw new NotFoundException("Nie znaleziono ubrania o podanym numerze ID");
            try
            {
                var image = new Image()
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

        public void CreateClothes(AddClothesInputDto input)
        {
            try
            {
                var newClothes = _mapper.Map<AddClothesInputDto, Models.Clothes>(input);
                _context.Clothes.Add(newClothes);
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
                clothes =  _mapper.Map(input, clothes);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas archizowania ubrania: {ex.Message}");
            }
        }
    }
}
