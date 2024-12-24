namespace WordleGame;

public partial class StatisticsPage : ContentPage
{
	public StatisticsPage()
	{
		InitializeComponent();
        string savedText = Preferences.Get("SavedEntryText", string.Empty); // Default to empty string if no value is found
        testerLabel.Text = savedText; // Set the Entry text to the saved value
    }

}