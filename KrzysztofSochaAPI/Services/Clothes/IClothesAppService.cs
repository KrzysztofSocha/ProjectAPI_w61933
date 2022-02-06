using KrzysztofSochaAPI.Services.Clothes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Clothes
{
    public interface IClothesAppService
    {

        void CreateClothes(AddClothesInputDto input);
        void AddImageToClothes(AddImageToClothesInputDto input);
        void ArchiveClothes(int id);
        void UpdateClothes(int id, UpdateClothesInputDto input);
    }
}
