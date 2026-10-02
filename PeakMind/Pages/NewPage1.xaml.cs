using Microsoft.Maui.Controls;

namespace PeakMind.Pages;

public partial class NewPage1 : ContentPage, IQueryAttributable
{
    public NewPage1()
    {
        InitializeComponent();
    }

    // Получаем mood после теста
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("action"))
        {
            string action = query["action"].ToString();

            PopUpLayer1.IsVisible = false;
            PopUpLayer2.IsVisible = false;
            PopUpLayer3.IsVisible = false;

            if (action == "feed")
                PopUpLayer1.IsVisible = true;

            if (action == "shower")
                PopUpLayer2.IsVisible = true;

            if (action == "beat")
                PopUpLayer3.IsVisible = true;
        }
    }

    private async void Button_Feed(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(BurdonsTest)}?action=feed");
    }

    private async void Button_Shower(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(BurdonsTest)}?action=shower");
    }

    private async void Button_Beat(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(BurdonsTest)}?action=beat");
    }

    private void Close_Feed(object sender, EventArgs e)
    {
        PopUpLayer1.IsVisible = false;
    }

    private void Button_Wash_End(object sender, EventArgs e)
    {
        PopUpLayer2.IsVisible = false;
    }

    private void Button_Close_Beat(object sender, EventArgs e)
    {
        PopUpLayer3.IsVisible = false;
    }
}