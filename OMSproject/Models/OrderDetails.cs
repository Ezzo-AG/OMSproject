using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSproject.Models
{
    [Table("Orderdetails")]
    public class OrderDetails
    {
        [Key]
        public int OrderId { get; set; }

        public Order? Orders { get; set; }

        [Key]
        public int ProductId { get; set; }

        
        public Product? Products { get; set; }

        [Key]
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string?  ClrName { get; set; }

        public int SubQty { get; set; }

        public float Price { get; set; }

        //public virtual List<Product>? ListProducts { get; private set; } = new List<Product>();

    }
}
