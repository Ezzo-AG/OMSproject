using System.ComponentModel.DataAnnotations;

namespace OMSproject.DTO
{
    public class ProductDTO
    {
        [Key]
        public int? Product_Id { get; set; }

        public string Product_Name { get; set; } = string.Empty;

    }
}
