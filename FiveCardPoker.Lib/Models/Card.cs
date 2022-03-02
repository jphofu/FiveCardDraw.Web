using System;

namespace FiveCardDraw.Lib.Models
{
    public class Card
    {
        private  string suit;
        private  string value;

        public Card(string ValueType, string suitType )
        {
            this.suit = suitType;
            this.value = ValueType;
        }

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public string Suit
        {
            get { return this.suit; }
            set { this.suit = value; }
        }

        public override string ToString()
        {
            return String.Format("{0}{1} ", value, suit);
        }

    }
}