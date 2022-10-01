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

        public float SellPrice { get; set; }

        public string? Address { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/YYYY}")]
        public DateTime DateOFOrder { get; set; } = System.DateTime.Now;

        public string? OrderStatus { get; set; }

        public string? Notes { get; set; }

        public int Client_id { get; set; }

        public List<ClientDTO>? Clients { get; set; } = new List<ClientDTO>();

        public List<ItemDTO>? Items { get; set; } = new List<ItemDTO>();

        public List<string>? Status { get; set; } = new List<string>();
    }
      
}
