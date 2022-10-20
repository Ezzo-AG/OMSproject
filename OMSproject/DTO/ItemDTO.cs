using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSproject.DTO
{
    public class ItemDTO
    {
        [Key]
        public int? OrderId { get; set; }

        
        public int? Product_Id { get; set; }

        public string ColorName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Quantity feild is required")]
        public int Quantity { get; set; }

        public float Price { get; set; }
        public List<ProductDTO>? Products { get; set; } = new List<ProductDTO>();

        public List<ColorDTO>? Colors { get; set; } = new List<ColorDTO>();

        public bool IsHidden { get; set; } = false;
    }
}
