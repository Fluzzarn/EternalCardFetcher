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

            var eternalBot = new EternalBot(username,password);
            eternalBot.StartBot();
            eternalBot.ConsumeComments();
            Console.ReadLine();

        }
    }
}
