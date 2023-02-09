namespace OMSproject.Models.ViewModels
{
    public class pftViewModel
    {
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndtDate { get; set; } = DateTime.Today;

        public List<ProfetViewModel> Profet { get; set; }
    }
}
