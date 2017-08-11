using Newtonsoft.Json;
using RedditSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EternalCardFetcher
{
    class EternalBot
    {
        List<EternalCard> _cards;
        List<string> _repliedToComments;

        string _username = "";
        string _password = "";
        string _publicKey = "";
        string _privateKey = "";

        RedditSharp.Things.Subreddit _subreddit;

        string _repliedCommentsFile = "replied_comments.txt";

        string _syntaxRegex = @"\[\[.*?\]\]";

        public EternalBot(string username, string password, string publicKey, string privateKey)
        {
            _repliedToComments = new List<string>();
            _username = username;
            _password = password;
            _publicKey = publicKey;
            _privateKey = privateKey;
            _cards = new List<EternalCard>();
            LoadRepliedComments();
            LoadJSON("eternal-cards.json");
        }

        private void LoadRepliedComments()
        {
            if (!File.Exists(_repliedCommentsFile))
            {
                using (File.Create(_repliedCommentsFile))
                {

                }
                
            }
            foreach (var line in File.ReadAllLines(_repliedCommentsFile))
            {
                _repliedToComments.Add((line));
            }
        }

        public void StartBot()
        {
            var webAgent = new BotWebAgent(_username, _password,_publicKey , _privateKey, @"https://github.com/Fluzzarn/EternalCardFetcher/tree/master");
            var reddit = new Reddit(webAgent, true);
            _subreddit = reddit.GetSubreddit("testingground4bots");
           // _subreddit.Subscribe();
        }


        public void LoadJSON(string jsonFile)
        {
            _cards = JsonConvert.DeserializeObject<List<EternalCard>>(File.ReadAllText(jsonFile));

            Console.WriteLine(_cards.Count);
        }

        public void ConsumeComments()
        {
            foreach (var comment in _subreddit.CommentStream)
            {
                if (comment.AuthorName == "Fluzzarn" && !_repliedToComments.Contains( (comment.Id)))
                {
                    string message = "";
                    foreach (Match match in Regex.Matches(comment.Body,_syntaxRegex))
                    {
                        string cardName = match.Value.Trim('[').Trim(']');
                        Console.WriteLine(cardName);
                        EternalCard card = _cards.Where((x) => x.Name == cardName).First();
                        if (card != null)
                        {

                            message += @"[" + cardName + "](" + card.ImageUrl + ") - [EWC](https://eternalwarcry.com/cards/details/" + card.SetNumber + "-" + card.EternalID + "/" + card.Name.ToLower().Replace(' ', '-') + ")  " + Environment.NewLine;
                        }
                    }
                    comment.Reply(message);
                    File.AppendAllText(_repliedCommentsFile, comment.Id + Environment.NewLine);
                    _repliedToComments.Add((comment.Id));
                }
                Console.WriteLine(comment.Body);
            }
        }
    }
}
