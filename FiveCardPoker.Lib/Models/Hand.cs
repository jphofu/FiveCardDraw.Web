using System.Collections.Generic;

namespace FiveCardDraw.Lib.Models
{
    public class Hand
    {
        public Hand()
        {
            Cards = new List<Card>();
        }

        public List<Card> Cards { get; set; }

        public override string ToString()
        { // list cards for printing 
            string returnString = string.Empty;
            foreach (Card card in Cards)
                returnString += string.Format("{0}, ", card.ToString());

            returnString = returnString.Trim();
            returnString = returnString.Remove(returnString.Length - 1, 1);

            return returnString;
        }
    }
}