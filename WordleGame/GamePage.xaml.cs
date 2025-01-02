
using Microsoft.Maui.Controls;
//using Microsoft.Maui.Controls.Compatibility;
//using System.Diagnostics.Metrics;

namespace WordleGame;

public partial class GamePage : ContentPage
{
    //variables
    Entry myEntry = new Entry();
    Border myBorder = new Border();
    Frame frame;
    int rows = 0;
    int count;
    // list of words
    List<String> words = new List<string>(); 
    string correctWord = "";
    private GetWordList Words;
    int timerCount = 0;
    int gameScore = 0;
    int[] checkGameOverArray = new int[5]; //checks if the words are all green
    bool checkEnd = false; //if it is false, the game continues. If it is true, the game ends
    bool chooseNewWord = true;
    bool end = false;
    int correctColour;

    string correctWordTwo;

    string theWord = "";

    bool newGame = true; //checks to see if the player is playing a new game, and if so, it is set to treu and a new word is chosen
    private ViewModel _viewModel;



    //timer variables
    private int _secondsRemaining; // Store the remaining seconds
    private System.Timers.Timer _timer;

    public GamePage()
    {
        Words = new GetWordList();
        InitializeComponent();

        _viewModel = new ViewModel();
        BindingContext = _viewModel; //sets the data binding to the ViewModel object 

        AndroidSettings();
        StartGrid();//creates the grid of entries when the page loads
        GetSecretWord();//choses one word from the list of words
        getRow(); //finds which row of the gridthe user is on 
        setBackgroundColour();
        





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

    private void AndroidSettings() //this alters all the esettings if the phone is an android phone
    {
        if(DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            buttonGrid.IsVisible = false; //makesthe gris of letters not visible, as if the user is using a phone, they will already have thir own keyboard
            phoneSetting.IsVisible = true;
            gameGrid.HeightRequest = 350;
            gameGrid.WidthRequest = 300;


        }
    }

    private void setBackgroundColour() //the background colour and text colour change in this method depending on if the dark mode switch is toggled or not
    {
        if (_viewModel.IsDarkTheme == true) //if the switch is toggled to dark mode
        {
            gameScrollView.BackgroundColor = Color.FromArgb("#181818");
            TimerLabel.TextColor = Colors.White;
            score.TextColor = Colors.White;
            for (int i = 0; i < gameGrid.Children.Count; i++)
            {
                if (gameGrid.Children[i] is Frame frame)
                {
                    if (frame.Content is Entry entry)
                    {
                        entry.TextColor = Colors.White;
                        
                        entry.BackgroundColor = Color.FromArgb("#181818");
                        frame.BackgroundColor = Color.FromArgb("#181818");

                    }
                }
            }
            //chnages the background colour of all the keyboard buttons to light gray if the dark mode switch is on
            for (int i = 0; i < buttonGrid.Children.Count; i++)
            {
                if (buttonGrid.Children[i] is Button button)
                {
                    button.BackgroundColor = Colors.Gray;
                }
            }
        }
        else if(_viewModel.IsDarkTheme == false)
        {
            gameScrollView.BackgroundColor = Colors.White;
            TimerLabel.TextColor = Colors.Black;
            score.TextColor = Colors.Black;
            for (int i = 0; i < gameGrid.Children.Count; i++)
            {
                if (gameGrid.Children[i] is Frame frame)
                {
                    if (frame.Content is Entry entry)
                    {
                        entry.TextColor = Colors.Black;

                        entry.BackgroundColor = Colors.White;
                        frame.BackgroundColor = Colors.White;
                    }
                }
            }

            for(int i = 0; i< buttonGrid.Children.Count; i++)
            {
                if(buttonGrid.Children[i] is Button button)
                {
                    button.BackgroundColor = Colors.LightGray;

                }
            }
        }
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

    //when the timer reaches 0
    private void OnTimerFinished()
    {
        // Handle when the timer reaches zero (e.g., show a message)
        //TimerLabel.Text = "Time's up!";
        //DisplayAlert("Game Over", "Time's up! You didn't guess the word in time.", "OK");
       // gameGrid.IsEnabled = false;
        //playAgain.IsVisible = true;
    } 

    //this methid randomly chooses one of the words in the list
    private async void GetSecretWord()
    {

        await Words.GetWords();
        await Words.makeWordList();
        //makes the list words equal to the list wordlist, which stores the words in the ViewModel
        words = Words.wordList;
        //Preferences.Set("NewWordBool", newGame); //saves this to preferences
        if (newGame == true)
        {
            //calls the methods in the ViewModel page to create a list of 3000 5 letter words using the github api
            
            //creatse a random variable
            var random = new Random();
            //choses the word randomly using the variable and makes it uppercase
            correctWord = words[random.Next(words.Count)].ToUpper();
            
            //chooseNewWord = false;
            newGame = false; //sets it to false so it doesnt change the word when the user goes to a different page
        }

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
        //Preferences.Set("RemainingTime", _secondsRemaining);
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
                myEntry.FontSize = Preferences.Get("FontSize", 0.0);
                myEntry.HorizontalTextAlignment = TextAlignment.Center;
                myEntry.TextChanged += MoveToNext;
                myEntry.MaxLength = 1;
                myEntry.Completed += MoveToPrevious;

                //creates a frame that contains the entry insinde and has a border
                frame.Content = myEntry;
                frame.BorderColor = Colors.Gray;
                frame.BackgroundColor = Colors.White;
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
        getRow();
        var button = sender as Button;
        Frame a = new Frame();
        Frame b = new Frame();
        Frame c = new Frame();
        Frame d = new Frame();
        Frame f = new Frame();

        Entry g = new Entry();
        Entry h = new Entry();
        Entry i = new Entry();
        Entry j = new Entry();
        Entry k = new Entry();


        // Frame frame = new Frame();
        //frame =gameGrid.Children as Frame;
        //Entry entry = new Entry();
        if (gameGrid.Children is Frame frame)
            if (frame.Content is Entry entry)
            {
                if (entry.IsFocused == true)//if the current entry that is being typed in is entry (user backspaces and wants to rewrite leytter)
                {
                    tester.Text = "In the Entry!";
                    entry.Text = button.Text; //whatever letter is in the button is saved in the entry
                }
            }

        //making the frames equal to what row/column the user is on
        a = gameGrid.Children[count] as Frame;
        b = gameGrid.Children[count + 1] as Frame;
        c = gameGrid.Children[count + 2] as Frame;
        d = gameGrid.Children[count + 3] as Frame;
        f = gameGrid.Children[count + 4] as Frame;

        //making the contents of the framse in ro a entry
        g = a.Content as Entry;
        h = b.Content as Entry;
        i = c.Content as Entry;
        j = d.Content as Entry;
        k = f.Content as Entry;



        if (button != null)
        {
            // Append the button text (number) to the Entry text
            //NumberEntry.Text += button.Text;
            if (g.Text == null || g.Text == string.Empty)
            {
                g.Text = button.Text;
                h.Focus();
            }

            else if (h.Text == null || h.Text == string.Empty)
            {
                h.Text = button.Text;
                i.Focus();
            }
            else if (i.Text == null || i.Text == string.Empty)
            {
                i.Text = button.Text;
                j.Focus();
            }
            else if (j.Text == null || j.Text == string.Empty)
            {
                j.Text = button.Text;
                k.Focus();
            }
            else if (k.Text == null || k.Text == string.Empty)
            {
                k.Text = button.Text;
                k.Focus();
            }


        }
    }

    private void enterButton_Clicked(object sender, EventArgs e)
    {
        getRow();

        //var theWord = gameGrid.Children.
       // var currentEntry = sender as Entry;
        string currentGuess = "";
        Entry gridText = new Entry();
        Entry theEntry = new Entry();
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
                    
                    
                        gridText.Text += entry.Text; //each text in the entries in the grid is saved into gridText (even empty entries)
                    
                        
                }
                
            }
        }

        for(int i = count; i<count +5; i++) //e.g. if the user is on the second row of the grid, count will be 5 (the 6th entry box which is the first entry box in the 2nd row)
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
            gameGrid.Children[count + 5].Focus(); //focuses on the next columns
        }
        

    }

    //checks the letters and gives them a colour
    private async void GetColour(string guess)
    {
        getRow();
        string letter = "";
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
                inEntry.BackgroundColor = Color.FromArgb("#6ca965");
                frame.BackgroundColor = Color.FromArgb("#6ca965"); //green
                gameScore += 5;
                correctColour = 0;
                checkGameOverArray[j] = 1;//if the letter is right, saves number as one
                

                //frame.TextColor = Colors.White;
                await Task.Delay(200);
                
            }
            //if the letter is correct but in the wrong place
            else if (correctWord.Contains(guess[j]))
            {
                inEntry.TextColor = Colors.White;
                frame.BackgroundColor = Color.FromArgb("#c8b653");  //yellow
                inEntry.BackgroundColor = Color.FromArgb("#c8b653");
                gameScore += 2;
                correctColour = 1;
                checkGameOverArray[j] = 0;//if not, saves as 0
                
                // myEntry.TextColor = Colors.White;
                await Task.Delay(200);
            }
            else
            {
                inEntry.TextColor = Colors.White;
                frame.BackgroundColor = Color.FromArgb("#2f303a"); //gray
                inEntry.BackgroundColor = Color.FromArgb("#2f303a");
                correctColour = 2;

                gameScore += 0;
                checkGameOverArray[j] = 0;//if not, saves as 0

                //myEntry.TextColor = Colors.White;
                await Task.Delay(200);
            }
            letter = guess[j].ToString(); //saves the letter that is being chceked
            j++;
            score.Text = "SCORE: "+ gameScore.ToString();

            changeButtonColour(letter); //calls method

            EndGame();
        }

        if(checkEnd == true)
        {
            gameGrid.IsEnabled = false;
        }
    }

    private void changeButtonColour(string letter) //this meathod changes the colour of the text button depending on the correct letter
    {
        for (int i = 0; i < buttonGrid.Children.Count; i++) //loops for all the buttons in the grid
        {
            if (buttonGrid.Children[i] is Button button) //this is true
            {
                if(button.Text == letter) //if the text in the button is the same as the text the user entered i.e. 'A'
                {
                    if(correctColour == 0)
                    {
                        button.BackgroundColor = Color.FromArgb("6ca965"); //if the letter is correct in the right palce
                    }
                    else if (correctColour == 1)
                    {
                        button.BackgroundColor = Color.FromArgb("#c8b653");//if the letter is correct in the wrong place
                    }
                    else if (correctColour == 2)
                    {
                        button.BackgroundColor = Color.FromArgb("#2f303a");//if the letter is not correct
                    }


                }
            }
        }
    }

   

    private void EndGame()
    {
        //if all the words are right, displays the play again button and sets check end to true
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
        //when the game is reset, sets new game to true, allowign a new word to be chosen foom the word list
        Preferences.Set("NewWordBool", true);
        newGame = Preferences.Get("NewWordBool", newGame);
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


        chooseNewWord = true;
        rows = 0;
        playAgain.IsVisible = false;
        GetSecretWord();
        Preferences.Set("RemainingTime", 120); // Default to 60 seconds if not saved
        _secondsRemaining = 120;
        UpdateTimerDisplay();
        StartGrid();
        setBackgroundColour();
        //when a new game is being played, saves the details 
        for (int i = 0; i < gameGrid.Children.Count; i++)
        {
            if (gameGrid.Children[i] is Frame frame)
            {
                if (frame.Content is Entry entry)
                {
                    // Save text of each entry
                    Preferences.Set($"entryText_{i}", entry.Text);
                    // Preferences.Set($"frameColor_{i}", frame.BackgroundColor.ToHex()); // Save frame color

                }
            }
        }
        
    }


    




    //when the content page appears (when the page loads), calls this method
    private void gameContentPage(object sender, EventArgs e)
    {
        // Load the remaining time from Preferences
       // _secondsRemaining = Preferences.Get("RemainingTime", 60); // Default to 60 seconds if not saved

        // If the timer was paused, resume it
        if (_secondsRemaining > 0)
        {
            _timer.Start();
        }
        
        newGame = Preferences.Get("NewWordBool", false); // now when the page is reloaded, the variable will be set to false and a new word willnot be chosen
        correctWord = Preferences.Get("NewWord", correctWord);

        setBackgroundColour();
        // Restore Entry Texts and Frame Colors
        for (int i = 0; i < gameGrid.Children.Count; i++)
        {
            if (gameGrid.Children[i] is Frame frame)
            {
                if (frame.Content is Entry entry)
                {

                    frame.BorderColor = Colors.Gray;

                    //retrivese the saved text
                    string savedText = Preferences.Get($"entryText_{i}", string.Empty);
                    entry.Text = savedText;

                    // Retrieve and set the saved frame color
                   string savedColor = Preferences.Get($"frameColor_{i}", entry.BackgroundColor.ToArgbHex());
                   entry.BackgroundColor = Color.FromArgb(savedColor);

                    // Restore other game state variables
                    gameScore = Preferences.Get("gameScore", 0);
                    rows = Preferences.Get("rows", 0);
                    //_secondsRemaining = Preferences.Get("secondsRemaining", 120);
                    // checkEnd = Preferences.Get("checkEnd", false);

                }
            }
        }
        // tester.Text = apple;

        


    }
    //when the game page disappears (when the user switches tio another page)
    public void gamePageDisappearing(object sender, EventArgs e)
    {

        for (int i = 0; i < gameGrid.Children.Count; i++)
        {
            if (gameGrid.Children[i] is Frame frame)
            {
                if (frame.Content is Entry entry)
                { 
                    // Save text of each entry
                   Preferences.Set($"entryText_{i}", entry.Text);
                   Preferences.Set($"frameColor_{i}", entry.BackgroundColor.ToArgbHex()); // Save frame color

                }
            }
        }

        Preferences.Set("NewWordBool", newGame); 

        //correctWordTwo = correctWord;
        Preferences.Set("NewWord", correctWord); //saves this to preferences

        Preferences.Set("gameScore", gameScore);
        Preferences.Set("rows", rows);
        //Preferences.Set("secondsRemaining", _secondsRemaining);
        //Preferences.Set("checkEnd", checkEnd);
    }
}//end of namespace