using PeakMind.Pages;

namespace PeakMind
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(BurdonsTest), typeof(BurdonsTest));
            Routing.RegisterRoute(nameof(NewPage1), typeof(NewPage1));
        }
    }
}