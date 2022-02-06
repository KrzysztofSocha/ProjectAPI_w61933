using KrzysztofSochaAPI.Services.Clothes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Clothes
{
    public interface IClothesAppService
    {

        Task CreateClothesAsync(AddClothesInputDto input);
        Task AddImageToClothesAsync(AddImageToClothesInputDto input);
        void DeleteImageClothes(int id);
        void ArchiveClothes(int id);
        void UpdateClothes(int id, UpdateClothesInputDto input);
        Task<GetClothesOutputDto> GetClothesByIdAsync(int id);
        Task<List<GetClothesOutputDto>> GetClothesAsync(GetClothesInputDto input);
    }
}
