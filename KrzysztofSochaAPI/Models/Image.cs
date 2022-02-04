namespace KrzysztofSochaAPI.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageFile { get; set; }
        public int ClothesId { get; set; }
        public virtual Clothes Clothes { get; set; }
    }
}