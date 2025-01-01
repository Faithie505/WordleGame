using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace WordleGame;

public partial class SettingsPage : ContentPage
{
    //variables
    public double fontSizeValue;
    private readonly ViewModel _viewModel;
    public SettingsPage()
    {
        InitializeComponent();
        _viewModel = new ViewModel();
        BindingContext = _viewModel;
    }


    /*private void fontSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        int value = Convert.ToInt32(e.NewValue);
        fontSize.FontSize = value;
        darkMode.FontSize = value;
        sound.FontSize = value;

    }*/

    private async void backButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

   

    private void FontSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        //music.Text = Convert.ToString(e.NewValue);
        //sound.Text = Convert.ToString(fontSlider.Value);
        int value = Convert.ToInt32(e.NewValue);
        Preferences.Set("fontValue", value);

    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {

        // Save the settings when the button is clicked
        _viewModel.SaveSettings();
        _viewModel.GetSettings();

        showColours();
        // Optionally, display a message or navigate back
        DisplayAlert("Settings", "Settings have been saved.", "OK");
        

    }

    private void showColours()
    {
        if (_viewModel.IsDarkTheme == true)
        {
            //this.BackgroundColor = Colors.White;
            settingContentPage.BackgroundColor = Colors.Black;
            darkMode.TextColor = Colors.White;
            sound.TextColor = Colors.White;
            fontSize.TextColor = Colors.White;
            music.TextColor = Colors.White;
        }
        else if (_viewModel.IsDarkTheme == false)
        {
            settingContentPage.BackgroundColor = Colors.White;
            darkMode.TextColor = Colors.Black;
            sound.TextColor = Colors.Black;
            fontSize.TextColor = Colors.Black;
            music.TextColor = Colors.Black;

        }
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        if (_viewModel.IsDarkTheme == true)
        {
            //this.BackgroundColor = Colors.White;
            settingContentPage.BackgroundColor = Colors.Black;
            darkMode.TextColor = Colors.White;
            sound.TextColor = Colors.White;
            fontSize.TextColor = Colors.White;
            music.TextColor = Colors.White;
        }
        else if (_viewModel.IsDarkTheme == false)
        {
            settingContentPage.BackgroundColor = Colors.White;
            darkMode.TextColor = Colors.Black;
            sound.TextColor = Colors.Black;
            fontSize.TextColor = Colors.Black;
            music.TextColor = Colors.Black;

        }

    }
}

