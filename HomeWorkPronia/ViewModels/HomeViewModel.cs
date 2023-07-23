using HomeWorkPronia.Models;

namespace HomeWorkPronia.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Card> Cards { get; set; }    
    }
}
