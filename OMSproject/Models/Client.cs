using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSproject.Models
{
    [Table("Clients")]
    public class Client
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Client_id { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(100)")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string ClientName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(100)")]
        public string? Claasification { get; set; } = string.Empty;

        IList<Order>? orders { get; set; } 
        ICollection<Order>? Orders { get; set; }

    }
}
