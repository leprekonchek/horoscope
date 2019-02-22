using System.Windows.Controls;

namespace _01_Lopukhina_Horoscope
{
    public partial class HoroscopeControl : UserControl
    {
        public HoroscopeControl()
        {
            InitializeComponent();
            DataContext = new HoroscopeViewModel();
        }
    }
}
