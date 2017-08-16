using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EternalCardFetcher
{
    public interface IRedditBot
    {

         void ConsumeComments(string subreddit);
    }
}
