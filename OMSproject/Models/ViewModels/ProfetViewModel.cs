using System.ComponentModel.DataAnnotations;


namespace OMSproject.Models.ViewModels
{
    public class ProfetViewModel
    {
        public int? OrderId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; }
        public float OrderCoast { get; set; }
        public float OrderSellPrice { get; set; }
        public float Profet { get; set; }
    }
}
