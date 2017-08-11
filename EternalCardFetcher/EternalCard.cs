using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EternalCardFetcher
{

    public class EternalCard
    {
        public int SetNumber { get; set; }
        public int EternalID { get; set; }
        public string Name { get; set; }
        public string CardText { get; set; }
        public int Cost { get; set; }
        public string Influence { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public string Rarity { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }

        public EternalCard()
        {

        }
    }
}
