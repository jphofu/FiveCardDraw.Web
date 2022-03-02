using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveCardDraw.Lib.Models
{
    public class Player
    {
        public Player()
        {
            Hand = new Hand();
        }

        public Hand Hand { get; set; }

        public void Deal(int numberOfCards, List<Card> cards)
        {
            if (cards.Count - numberOfCards < 0)
                return;

                for (int i = 0; i < numberOfCards; i++)
                {
                    Hand.Cards.Add(cards[i]);
                }
        }
    }
}
