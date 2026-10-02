using PeakMind.Models;
using PeakMind.Services;
using System.Collections.ObjectModel;

namespace PeakMind.Pages;

public partial class JournalPage : ContentPage
{
    public ObservableCollection<JournalEntry> Entries { get; set; }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        int id = Preferences.Default.Get("UserId", 0);
        var list = await DatabaseService.Instance.GetJournalEntriesAsync(id);

        Entries = new ObservableCollection<JournalEntry>(list);
        BindingContext = this;
    }
    public JournalPage()
    {
        InitializeComponent();
    }
   
}