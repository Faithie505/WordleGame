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
    List<String> words = new List<string>(); 
    string correctWord = "";
    private GetWordList Words;
    int timerCount = 0;
    int gameScore = 0;
    int[] checkGameOverArray = new int[5]; //checks if the words are all green
    bool checkEnd = false; //if it is false, the game continues. If it is true, the game ends
    bool chooseNewWord = true;
    bool end = false;

    string theWord = "";



    //timer variables
    private int _secondsRemaining; // Store the remaining seconds
    private System.Timers.Timer _timer;

    public GamePage()
    {
        Words = new GetWordList();
        InitializeComponent();
        StartGrid();//creates the grid of entries when the page loads
        GetSecretWord();//choses one word from the list of words
        getRow(); //finds which row of the gridthe user is on 

        


        //TIMER STUFF
        // Initialize the countdown to 60 seconds (1 minute)
        _secondsRemaining = 120;

        // Set up the timer
        _timer = new System.Timers.Timer(1000); // 1000 ms = 1 second
        _timer.Elapsed += OnTimerElapsed;
        _timer.Start();

        // Update the UI with the initial time
        UpdateTimerDisplay();
    }
    private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        // Decrease remaining time by 1 second
        _secondsRemaining--;

        // Update the UI on the main thread
        MainThread.BeginInvokeOnMainThread(() =>
        {
            UpdateTimerDisplay();
        });

        // Stop the timer when it reaches 0
        if (_secondsRemaining <= 0)
        {
            _timer.Stop();
            OnTimerFinished();
        }
    }

    private void UpdateTimerDisplay()
    {
        // Display remaining time in minutes and seconds (MM:SS)
        int minutes = _secondsRemaining / 60;
        int seconds = _secondsRemaining % 60;
        TimerLabel.Text = $" Time: {minutes:D2}:{seconds:D2}";
    }

    private void OnTimerFinished()
    {
        // Handle when the timer reaches zero (e.g., show a message)
        //TimerLabel.Text = "Time's up!";
    }

    //this methid randomly chooses one of the words in the list
    private async void GetSecretWord()
    {
        await Words.GetWords();//
        await Words.makeWordList();//
        words = Words.wordList;
        //creatse a random variable
        var random = new Random();

        
            //choses the word randomly using the variable and makes it uppercase
            correctWord = words[random.Next(words.Count)].ToUpper();
            chooseNewWord = false;

        
        theWord = Preferences.Get("SecretWord", correctWord);
        Preferences.Set("SecretWord", correctWord);
        correctWord = Preferences.Get("SecretWord", "word");
        backButton.Text = correctWord; //remove N.B N/B


    }

    //when the button is clicked, calls this method to go back to the home page
    private async void backButton_Clicked(object sender, EventArgs e)
    {
        // Pause the timer and save the current state
        if (_timer.Enabled)
        {
            _timer.Stop();
        }

        // Optionally, save the remaining time to Preferences or a similar storage
        Preferences.Set("RemainingTime", _secondsRemaining);
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

        currentEntry.Text = e.NewTextValue.ToUpper();
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
                gameScore += 5;
                checkGameOverArray[j] = 1;//if the letter is right, saves number as one

                //frame.TextColor = Colors.White;
                await Task.Delay(200);
                
            }
            //if the letter is correct but in the wrong place
            else if (correctWord.Contains(guess[j]))
            {
                inEntry.TextColor = Colors.White;
                frame.BackgroundColor = Colors.Yellow;
                gameScore += 2;
                checkGameOverArray[j] = 0;//if not, saves as 0
               // myEntry.TextColor = Colors.White;
                await Task.Delay(200);
            }
            else
            {
                inEntry.TextColor = Colors.White;
                frame.BackgroundColor = Colors.Gray;
                gameScore += 0;
                checkGameOverArray[j] = 0;//if not, saves as 0

                //myEntry.TextColor = Colors.White;
                await Task.Delay(200);
            }
            j++;
            score.Text = "SCORE: "+ gameScore.ToString();
            EndGame();
        }

        if(checkEnd == true)
        {
            gameGrid.IsEnabled = false;
        }
    }

    //when the content page appears (when the page loads), calls this method
    private void gameContentPage(object sender, EventArgs e)
    {
        // Load the remaining time from Preferences
        _secondsRemaining = Preferences.Get("RemainingTime", 60); // Default to 60 seconds if not saved

        // If the timer was paused, resume it
        if (_secondsRemaining > 0)
        {
            _timer.Start();
        }
    }

    private void EndGame()
    {
        
        if (checkGameOverArray[0] == 1&& checkGameOverArray[1] == 1&& checkGameOverArray[2] == 1&& checkGameOverArray[3] == 1&& checkGameOverArray[4] == 1) //if the array contains only 1s, that means all the letters in a row were correct
        {
            checkEnd = true;
            playAgain.IsVisible = true;

            
        }
        else
        {
            checkEnd = false;
            playAgain.IsVisible = false;

        }


        if (_secondsRemaining <= 0) //if it contains at least one 0, 
        {
            //checkEnd = true;
        }
    }

    private void playAgain_Clicked(object sender, EventArgs e)
    {
        end = true;
        gameGrid.IsEnabled = true;
        Frame theFrame = new Frame();
        Entry theEntry = new Entry();

        gameGrid.Children.Clear();
        gameGrid.ColumnDefinitions.Clear();
        gameGrid.RowDefinitions.Clear();
        //theEntry.TextChanged = onTex

        /*for (int i =0; i<gameGrid.Children.Count; i++)
        {
            theFrame = gameGrid.Children[i] as Frame;
            theFrame.BackgroundColor = Colors.White;
            //theEntry = theFrame.Content as Entry;

            //theEntry.Text = "";
            myEntry.Text = string.Empty;
            myEntry.TextColor = Colors.Black;

        }*/

        
      

        //gameGrid.Children[0].Focus();


        chooseNewWord = true;
        rows = 0;
        playAgain.IsVisible = false;
        GetSecretWord();
        Preferences.Set("RemainingTime", 120); // Default to 60 seconds if not saved
        _secondsRemaining = 120;
        UpdateTimerDisplay();
        StartGrid();


    }
}//end of namespace