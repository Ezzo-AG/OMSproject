using System.ComponentModel.DataAnnotations;

namespace OMSproject.DTO
{
    public class  ClientDTO
    {
        [Key]
        public int? Client_id { get; set; }

        public string? ClientName { get; set; } 
    }
}
