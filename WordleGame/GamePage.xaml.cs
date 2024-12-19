namespace WordleGame;

public partial class GamePage : ContentPage
{
	//variables
	Entry myEntry = new Entry();
    Border myBorder = new Border();
	public GamePage()
	{
		InitializeComponent();
        StartGrid();

    }

	//when the button is clicked, calls this method to go back to the home page
    /*private async void backButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
    }*/

	//creates a grid and each row and column has an entry
	private void StartGrid()
	{
		gameGrid.AddRowDefinition(new RowDefinition());
        gameGrid.AddColumnDefinition(new ColumnDefinition());

		//the rows 
		for(int i = 0; i< 6; i++)
		{
			//the columns
			for(int j = 0; j<5; j++)
			{
                //sets myEntry to a new Entry instance
                myBorder = new Border();
                myEntry = new Entry();
                myEntry.Placeholder = " ";
                myEntry.MaximumWidthRequest = 50;
                myEntry.MinimumWidthRequest = 49;
                myEntry.FontSize = 30;
                myEntry.HorizontalTextAlignment = TextAlignment.Center;
               // myEntry.TextChanged += OnTextChanged;
                myEntry.MaxLength = 1;
                
               
                myBorder.StrokeThickness = 3;
                myBorder.Stroke = Colors.Black;
               // myBorder.BackgroundColor = Colors.Black;
                myBorder.Content = myEntry;

                // myEntry.Completed += MoveFocusToPreviousEntry;

                //each entry is added to each row and column of the grid
                gameGrid.Add(myBorder, j, i);
            }
        }



    }
}