namespace WordleGame;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
	}

	//when the button is clicked, calls this method to go back to the home page
    private async void backButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
    }
}