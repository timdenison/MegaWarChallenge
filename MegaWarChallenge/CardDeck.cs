using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace MegaWarChallenge
{
    public class CardDeck
    {
        public List<Card> Cards {

            get {
                List<Card> tmpCards = new List<Card>();
                string[] filePath = Directory.GetFiles(HttpContext.Current.Server.MapPath("/Cards"), "*.png");
                int i = 0;
                while (i < filePath.Count())
                {
                    foreach (var path in filePath)
                    {
                        Card card = new Card();
                        var shortPath = path.Substring(path.LastIndexOf("\\")+1);
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

                        int nameIndexFrom = path.LastIndexOf("\\") + 1;
                        //tmpCards[i].name = (path.Substring(nameIndexFrom, ) + " of " + tmpCards[i].suit).ToUpper();

                        //create new substring beginning after full path. Then substring that into name.
                        //OR do that in the first place and split it into a name?

                        i++;
                    }
                }
                return tmpCards;
            }

        }

    


        public void GetCards()
        {
            List<Card> Cards = new List<Card>();
            string[] filePath = Directory.GetFiles(HttpContext.Current.Server.MapPath("/Cards"), "*.png");
            int i = 0;
            while (i < filePath.Count())
            {
                foreach (var path in filePath)
                {
                    Card card = new Card();
                    Cards.Add(card);
                    Cards[i].file = path;
                    //Here indexFRom is returning -1
                    int indexFrom = path.LastIndexOf("_");
                    int indexTo = path.LastIndexOf(".png");
                    Cards[i].suit = path.Substring(indexFrom+1, indexTo- indexFrom -1);
                    i++;
                }
            }
        }

        
        

        public void Shuffle()
        {


            //Random random = new Random();

            //for (var i = 0; i < Cards.Count(); i++)
            //{
            //    Cards[i] = Cards[random.Next(Cards.Length + 1)];
            //}
        }

    }
}