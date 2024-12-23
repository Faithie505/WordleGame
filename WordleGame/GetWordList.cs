using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleGame
{
    public class GetWordList
    {
        //variable
        public List<String> wordList;
        private HttpClient client;
        public List<String> _word = new List<String>();

        public List<String> Word
        {
            get
            {
                return _word;
            }
        }

        //constructor
        public GetWordList()
        {
            wordList = new List<String>();
            client = new HttpClient();
        }

        //the method access the file with the words and saves them to a string called content which reads it as a string
        //since the list of words are not seperated by quotes, the words are split and turned into strings and then saved in wordList
        public async Task<List<string>> GetWords()
        {
            var response = await client.GetAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/main/words.txt");
            string contents = await response.Content.ReadAsStringAsync();
            wordList = contents.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //wordList = JsonSerializer.Deserialize<List<User>>(contents);

            return new List<string>(wordList);
        }


        //this method fills the list
        public async Task makeWordList()
        {
            _word.Clear();
            await GetWords();
            foreach (var word in wordList)
            {
                _word.Add(word);
            }
        }




    }
}
