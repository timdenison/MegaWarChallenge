using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.IO;

namespace MegaWarChallenge
{
    public partial class Default : System.Web.UI.Page
    {
        public static GameOfWar game = new GameOfWar();
        List<Card> deck = game.Deck;
        List<Card> warPile = game.WarPile;
        Player Player1 = game.Player1;
        Player Player2 = game.Player2;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { PlayWar(); }
            
        }

        public void PlayWar()
        {
            
            Random random = new Random();
            
            deck.Shuffle(random);
            
            deck.Deal(Player1, Player2);
            Session.Add("Player1", Player1);
            Session.Add("Player2", Player2);
            DrawBoard(game);

            PrintPlayerCards(Player1, Player2);

        }

        private void DrawBoard(GameOfWar game)
        {
            player2deckImage.ImageUrl = "Cards//playingCardBack.jpg";
            player1deckImage.ImageUrl = "Cards//playingCardBack.jpg";

           
        }

        private void PrintPlayerCards(Player Player1, Player Player2)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Player 1 Cards: <br />");
            foreach (var card in Player1.hand)
            {
                sb.Append(card.name + "<br />");
            }
            sb.Append("<br /> <br />");
            sb.Append("Player 2 Cards: <br />");
            foreach (var card in Player2.hand)
            {
                sb.Append(card.name + "<br />");
            }

            unshuffledLabel.Text = sb.ToString();
        }

        protected List<Card> GetCards()
        {
            List<Card> tmpCards = new List<Card>();
            string[] filePath = Directory.GetFiles(HttpContext.Current.Server.MapPath("/Cards"), "*.png");
            int i = 0;
            while (i < filePath.Count())
            {
                foreach (var path in filePath)
                {
                    Card card = new Card();
                    var shortPath = path.Substring(path.LastIndexOf("\\") + 1);
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    tmpCards.Add(card);
                    tmpCards[i].file = path;

                    string[] delimiters = { "_", ".png" };
                    string[] cardIdArray = shortPath.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                    tmpCards[i].suit = textInfo.ToTitleCase(cardIdArray[2]);
                    tmpCards[i].name = textInfo.ToTitleCase(cardIdArray[0]) + " " + cardIdArray[1] + " " + tmpCards[i].suit;

                    try
                    {
                        tmpCards[i].value = Int32.Parse(cardIdArray[0]);
                    }
                    catch
                    {
                        switch (cardIdArray[0].ToLower())
                        {
                            case "ace":
                                tmpCards[i].value = 14;
                                break;
                            case "king":
                                tmpCards[i].value = 13;
                                break;
                            case "queen":
                                tmpCards[i].value = 12;
                                break;
                            case "jack":
                                tmpCards[i].value = 11;
                                break;

                        }
                    }
                    i++;
                }
            }
            return tmpCards;
        }

        protected void throwCardButton_Click(object sender, EventArgs e)
        {
            PlayRound();

        }

        private void PlayRound()
        {
            var Player1 = (Player)Session["Player1"];
            var Player2 = (Player)Session["Player2"];
            try {
                Player1.ThrowCard(warPile);
                Player2.ThrowCard(warPile);
                Compare(warPile, Player1, Player2);
            }
            catch
            {
                GameOver();
            }
        }

        private void Compare(List<Card> warPile, Player player1, Player player2)
        {
            var p1Compare = warPile[warPile.Count - 2].value;
            var p2Compare = warPile[warPile.Count - 1].value;
            if (p1Compare > p2Compare)
            {
                PrintRoundResults(player1, warPile);
                player1.Wins(warPile);
                
            }
            else if (p1Compare < p2Compare)
            {
                PrintRoundResults(player2, warPile);
                player2.Wins(warPile);
                
            }
            else
            {
                War(player1, player2);  
            }
        }

        private void PrintRoundResults(Player player, List<Card> warPile)
        {
            resultsLabel.Text += player.name + "wins <br />";
            foreach (var card in warPile)
            {
                resultsLabel.Text += "   " + card.name + "<br />";
            }
            var winnerCardCount = player.hand.Count + warPile.Count;
            resultsLabel.Text += "Card count (" + player.name + "): " + winnerCardCount + "<br />";
        }

        public void War(Player player1, Player player2)
        { //test war code. Show cards. Create Auto Play loop
            int counter = 2;
            bool facedown = true;
            while (counter > 0)
            {
                try {
                    player1.ThrowCard(warPile, facedown);
                    player2.ThrowCard(warPile, facedown);
                    facedown = !facedown;
                    counter--;
                }
                catch
                {
                    //investigate loop behavior
                    GameOver();
                }

            }
            Compare(warPile, player1, player2);
        }

        protected void warButton_Click(object sender, EventArgs e)
        {
            Player1 = (Player)Session["Player1"];
            Player2 = (Player)Session["Player2"];
            while (Player1.hand.Count > 0 && Player2.hand.Count > 0)
            {
                PlayRound();
                //Response.Redirect("Default.aspx");
            }

        }
        public void GameOver()
        {
            resultsLabel.Text = "Testing Game Over";
        }


    }

    public static class ExtensionMethods
    {
        public static List<Card> Shuffle(this List<Card> deck, Random random)

        {
            int i = deck.Count;
            while (i > 1)
            {
                i--;
                int k = random.Next(i + 1);
                Card tmpcard = deck[k];
                deck[k] = deck[i];
                deck[i] = tmpcard;
            }
            return deck;
        }

        public static void Deal(this List<Card> deck, Player player1, Player player2)
        {
            int i = 0;
            int counter = 1;
            while (i < deck.Count)
            {
                switch (counter%2)
                {
                    case (1):
                        player1.hand.Add(deck[i]);
                        deck.RemoveAt(i);
                        break;
                    case (0):
                        player2.hand.Add(deck[i]);
                        deck.RemoveAt(i);
                        break;
                }
                counter++;
            }

        }
        public static void Wins(this Player player, List<Card> winPile)
        {
            foreach (var card in winPile)
            {
                player.hand.Add(card);
            }
            winPile.Clear();
        }

        public static void ThrowCard(this Player player, List<Card> warPile)
        {
                warPile.Add(player.hand[0]);
                player.hand.RemoveAt(0);
                //show card faceup on table?
          
        }

        public static void ThrowCard(this Player player, List<Card> warPile, bool facedown)
        {
            warPile.Add(player.hand[0]);
            player.hand.RemoveAt(0);

            //show card facedown on table.
        }
        
    }
}