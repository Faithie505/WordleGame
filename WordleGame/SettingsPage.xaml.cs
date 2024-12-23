using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace WordleGame;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }


    private void fontSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        int value = Convert.ToInt32(e.NewValue);
        fontSize.FontSize = value;
        darkMode.FontSize = value;
        sound.FontSize = value;

    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
         if (darkSwitch.IsToggled)
         {
            verticalLayout.BackgroundColor = Colors.DarkSlateGrey;
             darkMode.TextColor = Colors.White;
             sound.TextColor = Colors.White;
             fontSize.TextColor = Colors.White;
         }
         else
         {
            verticalLayout.BackgroundColor = Colors.White;
             darkMode.TextColor = Colors.Black;
             sound.TextColor = Colors.Black;
             fontSize.TextColor = Colors.Black;
         }
    }

   
}

