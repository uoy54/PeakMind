using PeakMind.Models;
using PeakMind.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;


namespace PeakMind.Pages
{
    public partial class MainPage : ContentPage
    {
        
        private string usersAnswer = string.Empty;

        
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GenerateAnswer(object sender, EventArgs e)
        {
            var ai = new Aiservices();

            string result = await ai.AnalyzeAsync(usersAnswer);

            await DisplayAlertAsync("Answer", result, "ok");
            int userId = Preferences.Default.Get("UserId", 0);

            var entry = new JournalEntry
            {
                UserId = userId,
                Content =usersAnswer,
                CreatedAt = DateTime.Now
            };

            await DatabaseService.Instance.SaveJournalEntryAsync(entry);

        }

        private void PlaceHolderChange_TextChanged(object sender, TextChangedEventArgs e)
        {

            usersAnswer = e.NewTextValue ?? string.Empty;
        }
        private async void GoToCatClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewPage1());
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var results = await DatabaseService.Instance.GetResultsForUserAsync(1);

            if (results.Any())
            {
                var last = results.First();

                await DisplayAlert("Last Test",
                    $"Accuracy: {last.accuracy:F1}%\nPerformance: {last.performance}",
                    "OK");
            }
        }
    }
    
}

