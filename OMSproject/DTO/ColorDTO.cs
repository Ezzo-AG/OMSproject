using System.ComponentModel.DataAnnotations;

namespace OMSproject.DTO
{
    public class ColorDTO
    {
        [Key]
        public int Product_Id { get; set; }
        public string ColorName { get; set; } = String.Empty;
    }
}
