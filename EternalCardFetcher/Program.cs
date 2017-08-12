using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EternalCardFetcher
{
    class Program
    {
        static void Main(string[] args)
        {

            string username = args[0];
            string password = args[1];
            string publicKey = args[2];
            string privateKey = args[3];
            string subreddit = args[4];

            var eternalBot = new EternalBot(username,password,publicKey,privateKey);
            eternalBot.StartBot();
            eternalBot.ConsumeComments(subreddit);
            Console.ReadLine();

        }
    }
}
