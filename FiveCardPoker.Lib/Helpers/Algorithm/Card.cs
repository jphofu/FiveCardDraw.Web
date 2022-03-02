using FiveCardDraw.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveCardDraw.Lib.Helpers.Algorithm
{
    public class Card : IComparable<Card>
    {
        internal int SortScore => (int)(Score * 100 + Suit);

        public SuitEnum Suit
        {
            get;
        }

        public Value Value
        {
            get;
        }

        public bool InFormation
        {
            get;
            internal set;
        }

        public int Score => (int)Value;

        public Color Color
        {
            get
            {
                switch (Suit)
                {
                    case SuitEnum.Hearts:
                    case SuitEnum.Diamonds:
                        return Color.Red;
                    case SuitEnum.Clubs:
                    case SuitEnum.Spades:
                        return Color.Black;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Card(SuitEnum suit, Value value)
        {
            Suit = suit;
            Value = value;
        }

        protected bool Equals(Card other)
        {
            if (Suit == other.Suit && Value == other.Value)
            {
                return InFormation == other.InFormation;
            }

            return false;
        }

        public static Card Create(SuitEnum suit, Value value)
        {
            return new Card(suit, value);
        }

        public static Card Create(SuitEnum suit, int value)
        {
            value = ((value == 1) ? 14 : value);
            if (value < 2 || value > 14)
            {
                throw new Exception("Valid range: 2-14 or 1.");
            }

            return new Card(suit, (Value)value);
        }

        public static Card Create(int oneBasedSuit, int value)
        {
            value = ((value == 1) ? 14 : value);
            if (value < 2 || value > 14)
            {
                throw new Exception("Valid value range: 2-14 or 1.");
            }

            if (oneBasedSuit < 1 || oneBasedSuit > 4)
            {
                throw new Exception("Valid suit range: 1-4.");
            }

            return new Card((SuitEnum)(oneBasedSuit - 1), (Value)value);
        }

        public static Card Create(string card)
        {
            return CardParser.Parse(card);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }

            if (obj.GetType() == GetType())
            {
                return Equals((Card)obj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ((((int)Suit * 397) ^ (int)Value) * 397) ^ InFormation.GetHashCode();
        }

        public static Card Parse(string card)
        {
            return CardParser.Parse(card);
        }

        public int CompareTo(Card other)
        {
            if (SortScore != other.SortScore)
            {
                if (SortScore <= other.SortScore)
                {
                    return -1;
                }

                return 1;
            }

            return 0;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            switch (Suit)
            {
              
                case SuitEnum.Hearts:
                    stringBuilder.Append("\u2665");
                    break;
                case SuitEnum.Diamonds:
                    stringBuilder.Append("\u2666");
                    break;
                case SuitEnum.Clubs:
                    stringBuilder.Append("\u2663");
                    break;
                case SuitEnum.Spades:
                    stringBuilder.Append("\u2660");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Score <= 10)
            {
                stringBuilder.Append(Score.ToString("00"));
            }
            else
            {
                switch (Score)
                {
                    case 11:
                        stringBuilder.Append('J');
                        break;
                    case 12:
                        stringBuilder.Append('Q');
                        break;
                    case 13:
                        stringBuilder.Append('K');
                        break;
                    case 14:
                        stringBuilder.Append('A');
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return stringBuilder.ToString();
        }

        public static bool operator ==(Card a, Card b)
        {
            if (a?.Suit == b?.Suit)
            {
                return a?.Value == b?.Value;
            }

            return false;
        }

        public static bool operator !=(Card a, Card b)
        {
            return !(a == b);
        }
    }
}
