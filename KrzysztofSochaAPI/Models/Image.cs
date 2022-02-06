using System.ComponentModel.DataAnnotations;

namespace KrzysztofSochaAPI.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] ImageFile { get; set; }
        public int ClothesId { get; set; }
        public virtual Clothes Clothes { get; set; }
    }
}