using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSproject.Models.ViewModels
{
    public class ClientViewModel
    {
        [Key]
        public int? Client_id { get; set; }

        [Required(ErrorMessage = "You must enter phone number")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number, most be like (09X-XXX-XXXX)")]
        [Column(TypeName = "varchar(100)")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must enter client name")]
        [Column(TypeName = "varchar(100)")]
        public string ClientName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(100)")]
        public string? Claasification { get; set; } = string.Empty;

        public List<string>? ClaasificationList { get; private set; } = new List<string>();

        
        public IEnumerable<OMSproject.Models.Client>? ClientsCollection { get; set; }
    }
}
