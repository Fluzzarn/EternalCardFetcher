using RedditSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EternalCardFetcher
{
    class EternalBot
    {

        string _subreddit = "";

        public EternalBot(string subreddit)
        {
            _subreddit = subreddit;
        }

        public void StartBot()
        {
            var webAgent = new BotWebAgent("EternalCardBot")
        }

        public void ConsumeComments()
        {
            
        }
    }
}
