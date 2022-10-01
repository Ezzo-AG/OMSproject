using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSproject.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int? OrderId { get; set; }

        public int Client_id { get; set; }

        public string? Address { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOFOrder { get; set; } = System.DateTime.Now;

        public  float Total_price  { get; set; }

        public float SellPrice { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string? OrderStatus { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string? Notes { get; set; }


        [ForeignKey("Client_id")]
        public Client? Client { get; set; }

        //public virtual List<Client>? clients { get; set; }

        public ICollection<Product>? Products { get; set;  }

        public virtual List<OrderDetails>? OrderDetails { get; set; } = new List<OrderDetails>();

    }
}
