

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
            //test();
            //calls logIn method when page loads
            LogIn();
            ButtonAnimations();
        }

        private void test()
        {
           // Preferences.Set("fontValue", 5);
            //_viewModel.SaveSettings();
            _viewModel.GetSettings();
            DisplayAlert("alert", "alert", "no");
            
        }


        //this method asks the user to enter their username so then there stats can be saved and viewed
        private async void LogIn()
        {
            await Task.Delay(1000);
            //username = await DisplayPromptAsync("Log In", "Please enter your username", accept: "Enter");
        }

        //chooses a random word from the list and makes it the correct word
        


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
                scrollView.BackgroundColor = Colors.Black;
            }
            else if (_viewModel.IsDarkTheme == false)
            {
                scrollView.BackgroundColor = Colors.White;

            }

            



        }
    }

}
