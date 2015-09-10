using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace MegaWarChallenge
{
    public class GameOfWar
    {
        public List<Card> Deck {
            get
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

            set { }
        }
        public List<Card> WarPile { get; set; }
        public Player Player1 { get { return new Player(); } set { } }
        public Player Player2 { get { return new Player(); } set { } }
        public string cardBackURI = Directory.GetFiles(HttpContext.Current.Server.MapPath("/Cards"), "*.jpg")[0].ToString();
    }
}