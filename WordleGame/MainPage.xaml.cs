

namespace WordleGame
{
    public partial class MainPage : ContentPage
    {
        //variables
        private string username = "";
        private ViewModel _viewModel;
       // public ContentPage contentPage;



        public MainPage()
        {
            //contentPage = mainContentPage;
            InitializeComponent();
            //sets the databinding to the viewmodel class
            _viewModel = new ViewModel();
            BindingContext = _viewModel;
            //calls logIn method when page loads
            LogIn();
            ButtonAnimations();
            AndroidSettings();
        }

        private void AndroidSettings()
        {
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                vertStack.Spacing = 50;
                playButton.HeightRequest = 70;
                statsButton.HeightRequest = 70;
                settings.HeightRequest = 70;
                ruleButton.HeightRequest = 70;

            }
        }



        //this method asks the user to enter their username so then there stats can be saved and viewed
        private async void LogIn()
        {
            await Task.Delay(1000);
            //username = await DisplayPromptAsync("Log In", "Please enter your username", accept: "Enter");
        }
        


        private async void ButtonAnimations()
        {
            int num = 0;
            await Task.Delay(2600);
            while (num == 0)
            {
                await Task.WhenAll(playButton.TranslateTo(0, 6, 1500, Easing.CubicInOut), ruleButton.TranslateTo(0, 6, 1500, Easing.CubicInOut), statsButton.TranslateTo(0, 6, 1500, Easing.CubicInOut), settings.TranslateTo(0, 6, 1500, Easing.CubicInOut));
                await Task.WhenAll(playButton.TranslateTo(0, -6, 1500, Easing.CubicInOut), ruleButton.TranslateTo(0, -6, 1500, Easing.CubicInOut), statsButton.TranslateTo(0, -6, 1500, Easing.CubicInOut), settings.TranslateTo(0, -6, 1500, Easing.CubicInOut));
            }
        }

        private async void PlayButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
        }

        private async void ruleButton_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void statsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatisticsPage());
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            // test();
            //Preferences.Get("FontSize", 0);

            playButton.FontSize = Preferences.Get("FontSize", 0.0);
            statsButton.FontSize = Preferences.Get("FontSize", 0.0);
            settings.FontSize = Preferences.Get("FontSize", 0.0);
            ruleButton.FontSize = Preferences.Get("FontSize", 0.0);

           // _viewModel.SaveSettings();
            _viewModel.GetSettings();


            if (_viewModel.IsDarkTheme == true)
            {
                //scrollView.BackgroundColor = Colors.Black;
                darkMode();
            }
            else if (_viewModel.IsDarkTheme == false)
            {
                //scrollView.BackgroundColor = Colors.White;
                lighMode();
            }

        }

        private void darkMode()
        {
            scrollView.BackgroundColor = Color.FromArgb("#15202b");

            playButton.BackgroundColor = Color.FromArgb("#22303c");
            statsButton.BackgroundColor = Color.FromArgb("#22303c");
            settings.BackgroundColor = Color.FromArgb("#22303c");
            ruleButton.BackgroundColor = Color.FromArgb("#22303c");

            playButton.BorderColor = Color.FromArgb("#8899ac");
            statsButton.BorderColor = Color.FromArgb("#8899ac");
            settings.BorderColor = Color.FromArgb("#8899ac");
            ruleButton.BorderColor = Color.FromArgb("#8899ac");

            playButton.BorderWidth = 2;
            statsButton.BorderWidth = 2;
            settings.BorderWidth = 2;
            ruleButton.BorderWidth = 2;

            playButton.TextColor = Colors.White;
            statsButton.TextColor = Colors.White;
            settings.TextColor = Colors.White;
            ruleButton.TextColor = Colors.White;
        }


        private void lighMode()
        {
            scrollView.BackgroundColor = Color.FromArgb("#e8f5e4");

            playButton.BackgroundColor = Color.FromArgb("#8899ac");
            statsButton.BackgroundColor = Color.FromArgb("#8899ac");
            settings.BackgroundColor = Color.FromArgb("#8899ac");
            ruleButton.BackgroundColor = Color.FromArgb("#8899ac");

            playButton.BorderColor = Color.FromArgb("#22303c");
            statsButton.BorderColor = Color.FromArgb("#22303c");
            settings.BorderColor = Color.FromArgb("#22303c");
            ruleButton.BorderColor = Color.FromArgb("#22303c");

            playButton.BorderWidth = 2;
            statsButton.BorderWidth = 2;
            settings.BorderWidth = 2;
            ruleButton.BorderWidth = 2;

            playButton.TextColor = Colors.White;
            statsButton.TextColor = Colors.White;
            settings.TextColor = Colors.White;
            ruleButton.TextColor = Colors.White;
            title.TextColor = Colors.Black;
        }
    }

}
