using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSproject.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? OrderId { get; set; }

        public int Client_id { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? Address { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOFOrder { get; set; } = System.DateTime.Now;

        public  float Total_price  { get; set; }

        public float SellPrice { get; set; }


        [Required(ErrorMessage = "You must enter phone number")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Column(TypeName = "nvarchar(100)")]
        public string Phone { get; set; } = string.Empty;


        //[Required(ErrorMessage = "You must enter phone number")]
        [Display(Name = "Phone2")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Column(TypeName = "nvarchar(100)")]
        public string? Phone2 { get; set; } = string.Empty;


        [Column(TypeName = "nvarchar(150)")]
        public string? OrderStatus { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? Notes { get; set; }


        [ForeignKey("Client_id")]
        public Client? Client { get; set; }

        //public virtual List<Client>? clients { get; set; }

        public ICollection<Product>? Products { get; set;  }

        public virtual List<OrderDetails>? OrderDetails { get; set; } = new List<OrderDetails>();

    }
}
