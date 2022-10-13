using OMSproject.Models;

namespace OMSproject.DTO
{
    public class Home
    {

        public int InProgress { get; set; }
        public int New { get; set; }
        public int Canceled { get; set; }
        public int Delivred { get; set; }

        public List<Order> order { get; set; } = new List<Order>();

    }
}
