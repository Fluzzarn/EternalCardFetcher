using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EternalCardFetcher
{
    class OfflineEternalBot : IRedditBot
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        List<EternalCard> _cards;
        private string _syntaxRegex;

        public void LoadJSON(string jsonFile)
        {
            _cards = JsonConvert.DeserializeObject<List<EternalCard>>(File.ReadAllText(jsonFile));
            logger.Info("Loaded cards.json");
        }

        public void ConsumeComments(string subreddit)
        {

            _syntaxRegex = @"\d{1,2}\s[A-Z].*[a-z]\s";
            string message = "";
            int numMatches = 0;
            int totalAmount = 0;
            foreach (Match match in Regex.Matches(subreddit, _syntaxRegex))
            {
                string amountString = match.Value.Substring(0, 2);
                int amount = Convert.ToInt32(amountString);
                totalAmount += amount;
                string cardName = match.Value.Trim(' ').Trim(' ').Substring(2);
                EternalCard card = _cards.Where((x) => x.Name == cardName).FirstOrDefault();
                if (card != null)
                {
                    numMatches++;
                    message += cardName + Environment.NewLine;


                    //message += @"[" + cardName + "](" + card.ImageUrl + ") - [EWC](https://eternalwarcry.com/cards/details/" + card.SetNumber + "-" + card.EternalID + "/" + card.Name.ToLower().Replace(' ', '-') + ")  " + Environment.NewLine;
                }
            }

            if (totalAmount == 45 || totalAmount == 75)
            {
                Console.WriteLine(message);

            }
        }
    }
}
