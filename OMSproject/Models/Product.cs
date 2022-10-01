using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSproject.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Product_Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Product_Name { get; set; } = string.Empty;

        [Required]
        public float Cost { get; set; }

        public float? Price { get; set; }

        [Required]
        public int Total_QTY { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string? Notes { get; set; }

        public virtual List<Color> colors { get; set; } = new List<Color>();

        public ICollection<Order>? Orders { get; set; }

        public List<OrderDetails>? OrderDetails { get; set; }

    }
}
