using Newtonsoft.Json;
using RedditSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NLog;

namespace EternalCardFetcher
{
    class EternalBot
    {
        static Logger logger = LogManager.GetCurrentClassLogger(); 
        List<EternalCard> _cards;
        List<string> _repliedToComments;

        string _username = "";
        string _password = "";
        string _publicKey = "";
        string _privateKey = "";

        RedditSharp.Things.Subreddit _subreddit;

        string _repliedCommentsFile = "replied_comments.txt";

        string _syntaxRegex = @"\[\[.*?\]\]";
        Reddit reddit;

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
                logger.Info("Had to recreate saved comments file");
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
             reddit = new Reddit(webAgent, true);

            logger.Info("Logged into reddit");
        }


        public void LoadJSON(string jsonFile)
        {
            _cards = JsonConvert.DeserializeObject<List<EternalCard>>(File.ReadAllText(jsonFile));
            logger.Info("Loaded cards.json");
        }

        public void ConsumeComments(string subreddit)
        {
            _subreddit = reddit.GetSubreddit(subreddit);
            foreach (var comment in _subreddit.CommentStream)
            {
                if (!_repliedToComments.Contains( (comment.Id)))
                {
                    string message = "";
                    int numMatches = 0;
                    foreach (Match match in Regex.Matches(comment.Body,_syntaxRegex))
                    {
                        string cardName = match.Value.Trim('[').Trim(']');
                        EternalCard card = _cards.Where((x) => x.Name == cardName).FirstOrDefault();
                        if (card != null)
                        {
                            numMatches++;
                            message += @"[" + cardName + "](" + card.ImageUrl + ") - [EWC](https://eternalwarcry.com/cards/details/" + card.SetNumber + "-" + card.EternalID + "/" + card.Name.ToLower().Replace(' ', '-') + ")  " + Environment.NewLine;
                        }
                    }
                    message += "^^^[[cardname]] ^^^to ^^^call";
                    try
                    {
                        if (numMatches >= 1)
                        {
                            comment.Reply(message);
                        }
                        File.AppendAllText(_repliedCommentsFile, comment.Id + Environment.NewLine);
                        _repliedToComments.Add((comment.Id));
                        logger.Info("Processed Comment " + comment.Id + " by " + comment.AuthorName + ", there were " + numMatches + " matches");
                    }
                    catch (RedditException ex)
                    {

                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                    }

                }
                
            }
        }
    }
}
