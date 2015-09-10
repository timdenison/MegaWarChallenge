using System.Collections.Generic;

namespace MegaWarChallenge
{
    public class Player
    {
        public string name { get; set; }
        public List<Card> hand { get; set; }

        public Player()
        {
            name = "Player 1";
            hand = new List<Card>();
        }

    }
}