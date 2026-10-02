using Microsoft.Maui.Controls;

namespace PeakMind
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage(); // или new NavigationPage(new MainPage());
        }
    }
}