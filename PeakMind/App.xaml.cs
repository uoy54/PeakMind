using PeakMind.Pages;

namespace PeakMind
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Login());
        }
    }
}