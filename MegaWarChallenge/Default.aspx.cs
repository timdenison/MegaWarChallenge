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
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Card> deck = GetCards();
            StringBuilder sb = new StringBuilder();
            foreach (var card in deck)
            {
                sb.Append(card.name);
                sb.Append(" " + card.value.ToString());
                sb.Append("<br />");
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

                    int nameIndexFrom = path.LastIndexOf("\\") + 1;
                    //tmpCards[i].name = (path.Substring(nameIndexFrom, ) + " of " + tmpCards[i].suit).ToUpper();

                    //create new substring beginning after full path. Then substring that into name.
                    //OR do that in the first place and split it into a name?

                    i++;
                }
            }
            return tmpCards;
        }

        protected static void Shuffle(this List<Card> deck)

        {
            int i = deck.Count;
            while (i > 1)
            {
                Card tmpCard = deck[i];

            }
        }
    }
}