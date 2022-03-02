using FiveCardDraw.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveCardDraw.Lib.Helpers.Algorithm
{
    internal class CardParser
    {
        public static Card Parse(string card)
        {
            //if (card == null)
            //{
            //    throw new ParseCardFailedException(string.Format("{0} cannot be parsed as a playing card. A card is described as CCCVV where CCC is HRT, DMN, CLB or SPD and VV is 02-10, KN, QU, KI or AC.", "Null"));
            //}

            //card = card.Trim().ToUpper();
            //if (card == "")
            //{
            //    throw new ParseCardFailedException(string.Format("{0} cannot be parsed as a playing card. A card is described as CCCVV where CCC is HRT, DMN, CLB or SPD and VV is 02-10, KN, QU, KI or AC.", "An empty string"));
            //}

            if (card.Length != 2 && card.Length != 3)
            {
                throw new Exception($"{card} cannot be parsed as a playing card. A card is described as CCCVV where CCC is HRT, DMN, CLB or SPD and VV is 02-10, KN, QU, KI or AC.");
            }


            string text = card.Length ==2 ? card.Substring(1,1) : card.Substring(2, 1);
            SuitEnum suit;
            switch (text)
            {
                case "\u2665":
                    suit = SuitEnum.Hearts;
                    break;
                case "\u2666":
                    suit = SuitEnum.Diamonds;
                    break;
                case "\u2663":
                    suit = SuitEnum.Clubs;
                    break;
                case "\u2660":
                    suit = SuitEnum.Spades;
                    break;
                default:
                    throw new Exception(text + " cannot be parsed as a suit. A suit is described as HRT, DMN, CLB or SPD.");
            }

            string text2 = card.Length == 2 ? card.Substring(0, 1) : card.Substring(0, 2);
            Value value;
            switch (text2)
            {
                case "02":
                case "2":
                    value = Value.Value02;
                    break;
                case "03":
                case "3":
                    value = Value.Value03;
                    break;
                case "04":
                case "4":
                    value = Value.Value04;
                    break;
                case "05":
                case "5":
                    value = Value.Value05;
                    break;
                case "06":
                case "6":
                    value = Value.Value06;
                    break;
                case "07":
                case "7":
                    value = Value.Value07;
                    break;
                case "08":
                case "8":
                    value = Value.Value08;
                    break;
                case "09":
                case "9":
                    value = Value.Value09;
                    break;
                case "10":
                    value = Value.Value10;
                    break;
                case "J":
                case "11":
                    value = Value.Knight;
                    break;
                case "Q":
                case "12":
                    value = Value.Queen;
                    break;
                case "K":
                case "13":
                    value = Value.King;
                    break;
                case "A":
                case "1":
                case "01":
                case "14":
                    value = Value.Ace;
                    break;
                default:
                    throw new Exception(text2 + " cannot be parsed as a value.");
            }

            return new Card(suit, value);
        }
    }
}
