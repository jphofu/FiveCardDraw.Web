using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveCardDraw.Lib.Helpers.Algorithm
{
    public class FormationDescription
    {
        public Formation Formation
        {
            get;
        }

        public List<Card> Cards
        {
            get;
        }

        public int Score
        {
            get;
        }

        internal FormationDescription(Formation formation, IEnumerable<Card> cards, int score)
        {
            Formation = formation;
            Cards = new List<Card>();
            Cards.AddRange(cards);
            Score = score;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            switch (Formation)
            {
                case Formation.OnePair:
                    stringBuilder.Append("ONE PAIR");
                    break;
                case Formation.TwoPairs:
                    stringBuilder.Append("TWO PAIRS");
                    break;
                case Formation.ThreeOfAKind:
                    stringBuilder.Append("THREE OF A KIND");
                    break;
                case Formation.Straight:
                    stringBuilder.Append("STRAIGHT");
                    break;
                case Formation.Flush:
                    stringBuilder.Append("FLUSH");
                    break;
                case Formation.FullHouse:
                    stringBuilder.Append("FULL HOUSE");
                    break;
                case Formation.FourOfAKind:
                    stringBuilder.Append("FOUR OF A KIND");
                    break;
                case Formation.StraightFlush:
                    stringBuilder.Append("STRAIGHT FLUSH");
                    break;
                case Formation.RoyalFlush:
                    stringBuilder.Append("ROYAL FLUSH");
                    break;
                case Formation.Nothing:
                    stringBuilder.Append("NOTHING");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            return stringBuilder.ToString();
        }
    }
}
