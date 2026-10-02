using BCrypt.Net;
using Microsoft.Maui.Controls;
using PeakMind.Models;
using PeakMind.Services;
using System;


namespace PeakMind.Pages
{
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }
        
        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text?.Trim() ?? string.Empty;
            var password = PasswordEntry.Text?.Trim() ?? string.Empty;
            var confirmPassword = ConfirmPasswordEntry.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || password != confirmPassword)
            {
                await DisplayAlertAsync("Error", "Please fill all fields correctly and match passwords.", "OK");
                return;
            }

            var db = DatabaseService.Instance;
            var existingUser = await db.GetUserByNameAsync(username);
            if (existingUser != null)
            {
                await DisplayAlertAsync("Error", "Username already exists.", "OK");
                return;
            }

            var user = new User
            {
                Name = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            await db.SaveUserAsync(user);

            await DisplayAlertAsync("Success", "Account created. Please login.", "OK");
            await Shell.Current.GoToAsync("MainPage"); 
        }
    }
}