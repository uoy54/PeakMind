using BCrypt.Net;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using PeakMind.Models;
using PeakMind.Services;
using System;

namespace PeakMind.Pages
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text?.Trim() ?? string.Empty;
            var password = PasswordEntry.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlertAsync("Error", "Please enter username and password.", "OK");
                return;
            }

            var db = DatabaseService.Instance;
            var user = await db.GetUserByNameAsync(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                await DisplayAlertAsync("Error", "Invalid username or password.", "OK");
                return;
            }

            
            Preferences.Default.Set("UserId", user.Id);

            //await Shell.Current.GoToAsync("//MainPage");
            

            if (user.LastTestDate == null || user.LastTestDate.Value.Date != DateTime.Today)
            {

                /*Preferences.Default.Set("UserId", user.Id);
                await Shell.Current.GoToAsync(nameof(BurdonsTest));
                await DisplayAlertAsync("DEBUG", "Going to test", "OK");
                await Navigation.PushAsync(new BurdonsTest(user));
                Application.Current.MainPage = new AppShell();*/
                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("//BurdonsTest");

            }
            else 
            {
                //await Shell.Current.GoToAsync(nameof(MainPage));
                Application.Current.MainPage = new AppShell();
            }
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Register));
        }
        
    }
}