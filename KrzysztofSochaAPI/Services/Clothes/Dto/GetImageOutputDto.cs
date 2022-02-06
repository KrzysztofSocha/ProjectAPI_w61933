using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Clothes.Dto
{
    public class GetImageOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageFile { get; set; }
        
    }
}
