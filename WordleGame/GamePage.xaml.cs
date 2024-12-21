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
    private async void backButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    //creates a grid and each row and column has an entry
    private void StartGrid()
    {
        gameGrid.AddRowDefinition(new RowDefinition());
        gameGrid.AddColumnDefinition(new ColumnDefinition());

        //the rows 
        for (int i = 0; i < 6; i++)
        {
            //the columns
            for (int j = 0; j < 5; j++)
            {
                //sets myEntry to a new Entry instance
                myBorder = new Border();
                myEntry = new Entry();
                myEntry.Placeholder = " ";
                myEntry.FontSize = 30;
                myEntry.HorizontalTextAlignment = TextAlignment.Center;
                myEntry.TextChanged += OnTextChanged; 
                myEntry.MaxLength = 1;
               // myEntry.BackgroundColor = Colors.Black;

                myBorder.StrokeThickness = 2;
                myBorder.Stroke = Colors.Black;
                myBorder.Content = myEntry;

                //each entry is added to each row and column of the grid
                gameGrid.Add(myEntry, j, i);
            }
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
       // test.Text = "hii";
        //Takes in what ever entry the user is typing in
        var currentEntry = (Entry)sender;
        //if the entry was empty and the new text value isnt empty (if the user types something)
       // if (string.IsNullOrEmpty(e.OldTextValue) && !string.IsNullOrEmpty(currentEntry.Text))
       // {
            if(currentEntry == gameGrid.Children[0])//if the current enrty is the first entry in a row
            {
            test.Text = "hii";
            gameGrid.Children[1].Focus(); // automatically focuses on the next entry for the user to type in
            }
            else if (currentEntry == gameGrid.Children[1])//if the current entry is the second entry in a row
            {
            test.Text = "hii";
            gameGrid.Children[2].Focus(); // automatically focuses on the next entry for the user to type in
            }
            else if (currentEntry == gameGrid.Children[2])//if the current entry is the third entry in a row
            {
            test.Text = "hii";
            gameGrid.Children[3].Focus(); // automatically focuses on the next entry for the user to type in
            }
            else if (currentEntry == gameGrid.Children[3])//if the current enrty is the second entry in a row
            {
            test.Text = "hii";
            gameGrid.Children[4].Focus(); // automatically focuses on the next entry for the user to type in
            }
            else if (currentEntry == gameGrid.Children[4])//if the current enrty is the second entry in a row
            {
            test.Text = "hii";
            gameGrid.Children[4].Focus(); // automatically focuses on the next entry for the user to type in
            }


      //  }
    }




}