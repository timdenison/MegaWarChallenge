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
            PlayWar();
        }

        public void PlayWar()
        {
            
            Random random = new Random();
            
            deck.Shuffle(random);
            
            deck.Deal(Player1, Player2);
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
            //warPile.Add(Player1.hand[0]);
            //deck.RemoveAt(0);
            //warPile.Add(Player2.hand[0]);
            //deck.RemoveAt(0);
            Player1.ThrowCard(warPile);
            Player2.ThrowCard(warPile);

            if (warPile[0].value > warPile[1].value)
            {
                Player1.Wins(warPile);
            }
            else if (warPile[0].value < warPile[1].value)
            {
                Player2.Wins(warPile);
            }
            else
            {
                War();
            }
        }

        public void War()
        {
            int counter = 3;
            while (counter > 0)
            {
                warPile.Add(Player1.hand[0]);
                Player1.hand.RemoveAt(0);
            }
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
        }

        public static void ThrowCard(this Player player, List<Card> warPile)
        {
            warPile.Add(player.hand[0]);
            player.hand.RemoveAt(0);
        }
    }
}