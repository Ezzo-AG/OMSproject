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

        [Required(ErrorMessage = "You must enter phone number")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Column(TypeName = "varchar(100)")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must enter client name")]
        [Column(TypeName = "varchar(100)")]
        public string ClientName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(100)")]
        public string? Claasification { get; set; } = string.Empty;

        IList<Order>? orders { get; set; } 
        ICollection<Order>? Orders { get; set; }

        [NotMapped]
        public List<string>? ClaasificationList { get;private set; } = new List<string>();

    }
}
