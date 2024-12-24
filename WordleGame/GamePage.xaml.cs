using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;

namespace WordleGame;

public partial class GamePage : ContentPage
{
    //variables
    Entry myEntry = new Entry();
    Border myBorder = new Border();
    Frame frame;
    int rows = 0;
    int count;
    //test list of words
    List<String> words = new List<string>(); //new List<String>() { "apple", "spade", "chase", "range", "dance", "argue" };
    //the right word
    string correctWord = "";
    private GetWordList Words;
     
    public GamePage()
    {
        Words = new GetWordList();
        InitializeComponent();
        StartGrid();//creates the grid of entries when the page loads
        GetSecretWord();
        getRow();
    }

    //this methid randomly chooses one of the words in the list

    private async void GetSecretWord()
    {
        await Words.GetWords();
        await Words.makeWordList();
        words = Words.wordList;

        var random = new Random();

        correctWord = words[random.Next(words.Count)];
        backButton.Text = correctWord;
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
                myEntry = new Entry();
                frame = new Frame();
                myEntry.Placeholder = " ";
                myEntry.FontSize = 20;
                myEntry.HorizontalTextAlignment = TextAlignment.Center;
                myEntry.TextChanged += MoveToNext;
                myEntry.MaxLength = 1;
                myEntry.Completed += MoveToPrevious;

                //creates a frame that contains the entry insinde and has a border
                frame.Content = myEntry;
                frame.BorderColor = Colors.Gray;
                frame.CornerRadius = 0;
                frame.Padding = 0;
                frame.Margin = 0;

                //each frame containing an entry is added to each row and column of the grid
                gameGrid.Add(frame, j, i);
            }
        }
    }

    private void getRow()
    {
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
    }

    //when the text in an entry changes(when the user trypes something),  calls this method 
    //the method types the letter into an entry and focuses on the next entry
    private void MoveToNext(object sender, TextChangedEventArgs e)
    {
        getRow();
        //Takes in what ever entry the user is typing in
        var currentEntry = (Entry)sender;
        Preferences.Set("SavedEntryText", currentEntry.Text); // "SavedEntryText" is the key
        //makes the var parentFrame equal to the frame that contains the entry in the grid
        var parentFrame = currentEntry.Parent as Frame;


        //if the entry was empty and the new text value isnt empty (if the user types something)
        if (string.IsNullOrEmpty(e.OldTextValue) && !string.IsNullOrEmpty(currentEntry.Text))
        {
            if (parentFrame == gameGrid.Children[count])//if the current enrty is the first entry in a row
            {
                gameGrid.Children[count + 1].Focus(); // automatically focuses on the next entry for the user to type in
            }
            else if (parentFrame == gameGrid.Children[count + 1])//if the current entry is the second entry in a row
            {
                gameGrid.Children[count + 2].Focus(); // automatically focuses on the next entry for the user to type in
            }
            else if (parentFrame == gameGrid.Children[count + 2])//if the current entry is the third entry in a row
            {
                gameGrid.Children[count + 3].Focus(); // automatically focuses on the next entry for the user to type in
            }
            else if (parentFrame == gameGrid.Children[count + 3])//if the current enrty is the second entry in a row
            {
                gameGrid.Children[count + 4].Focus(); // automatically focuses on the next entry for the user to type in
            }
            else if (parentFrame == gameGrid.Children[count + 4])//if the current enrty is the second entry in a row
            {
                gameGrid.Children[count + 4].Focus(); // automatically focuses on the next entry for the user to type in
            }
        }
    }

    private void MoveToPrevious(object sender, EventArgs e)
    {
        getRow();
        var currentEntry = sender as Entry;
        var parentFrame = currentEntry.Parent as Frame;
       
        if (currentEntry.Text == null || currentEntry.Text == string.Empty)
        {
            
            if (parentFrame == gameGrid.Children[count + 1])
            {
                gameGrid.Children[count].Focus();
            }
            else if (parentFrame == gameGrid.Children[count + 2])
            {
                gameGrid.Children[count + 1].Focus();
            }
            else if (parentFrame == gameGrid.Children[count + 3])
            {
                gameGrid.Children[count + 2].Focus();
            }
            else if (parentFrame == gameGrid.Children[count + 4])
            {
                gameGrid.Children[count + 3].Focus();
            }
        }
    }

    private void OnKeyClicked(object sender, EventArgs e)
    {
        /*getRow();
        var button = sender as Button;
        Entry a = new Entry();
        Entry b = new Entry();
        Entry c = new Entry();
        Entry d = new Entry();
        Entry f = new Entry();

        


        a = gameGrid.Children[count] as Entry;
        b = gameGrid.Children[count + 1] as Entry;
        c = gameGrid.Children[count + 2] as Entry;
        d = gameGrid.Children[count + 3] as Entry;
        f = gameGrid.Children[count + 4] as Entry;
        


        if (button != null)
        {
            // Append the button text (number) to the Entry text
            //NumberEntry.Text += button.Text;
            if (a.Text == null)
            {
                a.Text = button.Text;
                b.Focus();
            }

            else if (b.Text == null || b.Text == string.Empty)
            {
                b.Text = button.Text;
                c.Focus();
            }
            else if (c.Text == null || c.Text == string.Empty)
            {
                c.Text = button.Text;
                d.Focus();
            }
            else if (d.Text == null || d.Text == string.Empty)
            {
                d.Text = button.Text;
                f.Focus();
            }
            else if (f.Text == null || f.Text == string.Empty)
            {
                f.Text = button.Text;
                f.Focus();
            }
        }*/
    }

    private void enterButton_Clicked(object sender, EventArgs e)
    {
        getRow();

        //var theWord = gameGrid.Children.
       // var currentEntry = sender as Entry;
        string currentGuess = "";
        Entry gridText = new Entry();
        Entry theEntry = new Entry();
        Frame framing;
        //var parentFrame = currentEntry.Parent as Frame;


        //if the grid isnt at 5 words???
        foreach (var item in gameGrid.Children)
        {
            // Check if the item is a Frame
            if (item is Frame frame)
            {
                // Access the Content of the Frame (which is the Entry)
                if (frame.Content is Entry entry)
                {
                    gridText.Text += entry.Text;
                        
                }
                
            }
        }

        for(int i = count; i<count +5; i++)
        {
            currentGuess += gridText.Text[i];
            tester.Text = currentGuess;
        }




                    //finds the text entered by user
                    /*for (int i = count; i < gameGrid.Children.Count; i++)
                    {
                        gridText = gameGrid.Children[i] as Entry;
                        currentGuess += gridText.Text;
                    }*/

        GetColour(currentGuess);
        rows++;

        if(count <=21)
        {
            gameGrid.Children[count + 5].Focus();
        }
        

    }

    //checks the letters and gives them a colour
    private async void GetColour(string guess)
    {
        getRow();
        string feedback = "a";
        int j = 0;
        Entry inEntry;

        for (int i = count; i < count + 5; i++)
        {
            frame = gameGrid.Children[i] as Frame;
            inEntry = frame.Content as Entry;
            

            //if the letter is in the right place
            if (guess[j] == correctWord[j])
            {
                inEntry.TextColor = Colors.White;
                frame.BackgroundColor = Colors.Green;
                //frame.TextColor = Colors.White;
                await Task.Delay(200);
            }
            //if the letter is correct but in the wrong place
            else if (correctWord.Contains(guess[j]))
            {
                inEntry.TextColor = Colors.White;
                frame.BackgroundColor = Colors.Yellow;
               // myEntry.TextColor = Colors.White;
                await Task.Delay(200);
            }
            else
            {
                inEntry.TextColor = Colors.White;
                frame.BackgroundColor = Colors.Gray;
                //myEntry.TextColor = Colors.White;
                await Task.Delay(200);
            }
            j++;
        }
    }






}//end of namespace