using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OMSproject.DTO;

namespace OMSproject.Models.ViewModels
{
    public class OrderClientViewModel
    {
        [Key]
        public int OrderId { get; set; }

        public float Total_price { get; set; }

        [Required(ErrorMessage = "You must enter sell price")]
        public float SellPrice { get; set; }

        [Required(ErrorMessage = "You must enter the address")]
        public string? Address { get; set; }

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
        public string Phone2 { get; set; } = string.Empty;


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/YYYY}")]
        public DateTime DateOFOrder { get; set; } = System.DateTime.Now;

        public string? OrderStatus { get; set; }

        public string? Notes { get; set; }

        public int Client_id { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "You must enter the client name")]
        public string? ClientName { get; set; }

        public List<ClientDTO>? Clients { get; set; } = new List<ClientDTO>();

        public List<ItemDTO>? Items { get; set; } = new List<ItemDTO>();

        public List<string>? Status { get; set; } = new List<string>();
    }
      
}
