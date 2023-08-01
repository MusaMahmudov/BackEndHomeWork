using HomeWorkPronia.Models.Common;

namespace HomeWorkPronia.Models
{
    public class Category : BaseSectionEntity
    {
        public string Name { get; set; }
        public ICollection<Product>? Products { get; set;}
    }
}
