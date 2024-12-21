namespace WordleGame;

public partial class GamePage : ContentPage
{
    //variables
    Entry myEntry = new Entry();
    Border myBorder = new Border();
    int rows = 0;
    int count;
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
                myEntry.TextChanged += MoveToNext; 
                myEntry.MaxLength = 1;
                myEntry.Completed += MoveToPrevious;
               // myEntry.BackgroundColor = Colors.Black;

                myBorder.StrokeThickness = 2;
                myBorder.Stroke = Colors.Black;
                myBorder.Content = myEntry;

                //each entry is added to each row and column of the grid
                gameGrid.Add(myEntry, j, i);
            }
        }
    }

    //when the text in an entry changes(when the user trypes something),  calls this method 
    //the method types the letter into an entry and focuses on the next entry
    private void MoveToNext(object sender, TextChangedEventArgs e)
    {

        //Takes in what ever entry the user is typing in
        var currentEntry = (Entry)sender;
        //depending what row the user is on, makes count equal to the numer of the first column in that row
        if (rows == 0)
            count = 0;
        else if (rows == 1)
            count = 5;
        else if (rows == 2)
            count = 10;
        else if (rows == 3)
            count = 15;
        else if (rows == 4)
            count = 20;
        else if (rows == 5)
            count = 25;
        //if the entry was empty and the new text value isnt empty (if the user types something)
        if (string.IsNullOrEmpty(e.OldTextValue) && !string.IsNullOrEmpty(currentEntry.Text))
       {
            if(currentEntry == gameGrid.Children[count])//if the current enrty is the first entry in a row
            {
            gameGrid.Children[count+1].Focus(); // automatically focuses on the next entry for the user to type in
            }
            else if (currentEntry == gameGrid.Children[count+1])//if the current entry is the second entry in a row
            {
            gameGrid.Children[count+2].Focus(); // automatically focuses on the next entry for the user to type in
            }
            else if (currentEntry == gameGrid.Children[count + 2])//if the current entry is the third entry in a row
            {
            gameGrid.Children[count+3].Focus(); // automatically focuses on the next entry for the user to type in
            }
            else if (currentEntry == gameGrid.Children[count+ 3])//if the current enrty is the second entry in a row
            {
            gameGrid.Children[count+4].Focus(); // automatically focuses on the next entry for the user to type in
            }
            else if (currentEntry == gameGrid.Children[count + 4])//if the current enrty is the second entry in a row
            {
            gameGrid.Children[count+4].Focus(); // automatically focuses on the next entry for the user to type in
            }
        }
    }

    private void MoveToPrevious(object sender, EventArgs e)
    {
        int theNumber = 0;
        var currentEntry = sender as Entry;
        if (currentEntry.Text == null || currentEntry.Text == string.Empty)
        {
            if (rows == 0)
                theNumber = 0;
            else if (rows == 1)
                theNumber = 5;
            else if (rows == 2)
                theNumber = 10;
            else if (rows == 2)
                theNumber = 15;
            if (currentEntry == gameGrid.Children[theNumber + 1])
            {
                gameGrid.Children[theNumber].Focus();
            }
            else if (currentEntry == gameGrid.Children[theNumber + 2])
            {
                gameGrid.Children[theNumber + 1].Focus();
            }
            else if (currentEntry == gameGrid.Children[theNumber + 3])
            {
                gameGrid.Children[theNumber + 2].Focus();
            }
            else if (currentEntry == gameGrid.Children[theNumber + 4])
            {
                gameGrid.Children[theNumber + 3].Focus();
            }
        }
    }




}