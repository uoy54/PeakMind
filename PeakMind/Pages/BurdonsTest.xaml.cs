using PeakMind.Models;
using System.Collections.ObjectModel;

namespace PeakMind.Pages
{
    public partial class BurdonsTest : ContentPage, IQueryAttributable
    {
        public ObservableCollection<BourdonCell> Cells { get; set; }

        private char targetLetter = 'A';
        private string actionType = "";

        public BurdonsTest()
        {
            InitializeComponent();
        }

        // Получаем выбранное действие
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("action"))
            {
                actionType = query["action"].ToString();
            }
        }
        private void StartTimer()
        {
            timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);

            timer.Tick += (s, e) =>
            {
                timeLeft--;
                TimerText = $"Time: {timeLeft}";
                OnPropertyChanged(nameof(TimerText));

                if (timeLeft <= 0)
                {
                    timer.Stop();
                    FinishTest(null, null);
                }
            };

            timer.Start();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            TimerText = $"Time: {timeLeft}";
            StartTimer();
            Cells = new ObservableCollection<BourdonCell>(
                GenerateGrid(200, targetLetter)
            );

            BindingContext = this;
        }

        static public List<BourdonCell> GenerateGrid(int count, char target)
        {
            var random = new Random();
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var list = new List<BourdonCell>();

            for (int i = 0; i < count; i++)
            {
                char letter = letters[random.Next(letters.Length)];

                list.Add(new BourdonCell
                {
                    Letter = letter.ToString(),
                    IsTarget = letter == target
                });
            }

            return list;
        }

        private void CellTapped(object sender, EventArgs e)
        {
            if (sender is Frame frame && frame.BindingContext is BourdonCell cell)
            {
                cell.IsSelected = !cell.IsSelected;
            }
        }

        private async void FinishTest(object sender, EventArgs e)
        {
           

            await Shell.Current.GoToAsync($"//{nameof(NewPage1)}?action={actionType}");
        }
        private int timeLeft = 60;
        public string TimerText { get; set; }
        private IDispatcherTimer timer;
    }
}