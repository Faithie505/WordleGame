namespace WordleGame
{
    public partial class MainPage : ContentPage
    {
        //variables
        private string username = "";

        public MainPage()
        {
            InitializeComponent();
            //calls logIn method when page loads
            LogIn();
        }
        

        //this method asks the user to enter their username so then there stats can be saved and viewed
        private async void LogIn()
        {
            await Task.Delay(1000);
          
             username = await DisplayPromptAsync("Log In", "Please enter your username", accept:"Enter");
        }
    }

}
