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


#if DEBUG
            var eternalBot = new OfflineEternalBot();
            eternalBot.LoadJSON("eternal-cards.json");
            subreddit = @"I just took this really cheap deck to masters yesterday it is really well positioned right now, I took a similar deck to masters a couple of times before set 2 got released and updated it with set 2 cards. It plays 0 legendaries, 8 rares and 4 promos. 

1 Yeti Spy (Set1 #191)
1 Argenport Stranger (Set2 #246)
1 Devour (Set1 #261)
1 East-Wind Herald (Set1 #201)
1 Gorgon Swiftblade (Set1 #377)
1 Maelstrom Bell (Set2 #109)
1 Scaly Gruan (Set1 #215)
1 Whispering Wind (Set1 #202)
1 Cobalt Acolyte (Set1 #212)
1 Crafty Yeti (Set2 #116)
1 Desperado (Set1 #273)
1 Foothills Alpha (Set2 #117)
1 Skeeter (Set2 #150)
1 Skyrider Vanguard (Set2 #120)
1 Trigger-Happy (Set2 #153)
1 Tundra Explorer (Set1 #204)
2 Cripple (Set2 #157)
2 Stonepowder Alchemist (Set2 #239)
1 Hooru Fledgling (Set1 #159)
1 Jotun Warrior (Set1 #226)
1 Rolant's Choice (Set2 #243)
1 Valkyrie Denouncer (Set2 #244)
1 Cirso's Meddling (Set2 #132)
1 Thunderstrike Dragon (Set1 #243)
1 Icequake (Set2 #136)
4 Justice Sigil (Set1 #126)
8 Primal Sigil (Set1 #187)
6 Shadow Sigil (Set1 #249)";
            eternalBot.ConsumeComments(subreddit);
#else

                        var eternalBot = new EternalBot(username,password,publicKey,privateKey);
            eternalBot.StartBot();
            eternalBot.ConsumeComments(subreddit);

#endif
            Console.ReadLine();

        }
    }
}
