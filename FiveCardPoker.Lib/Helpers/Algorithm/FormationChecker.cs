using FiveCardDraw.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveCardDraw.Lib.Helpers.Algorithm
{
    public class FormationChecker
    {
        private readonly Card[] _cards;

        internal Formation Formation
        {
            get;
            set;
        }

        internal int Count => _cards.Count((Card t) => !(t == null));

        internal int Score
        {
            get
            {
                int num = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (!(_cards[i] == null) && _cards[i].InFormation)
                    {
                        num += _cards[i].Score;
                    }
                }

                return num + (int)Formation * 100;
            }
        }

        public FormationChecker(string hand)
        {
            _cards = new Card[5];
            Formation = Formation.Nothing;
            if (hand.IndexOf(',') < 0)
            {
                throw new Exception("A hand is a comma separated string with five cards. Example: SPDAC, SPD02, SPD03, SPD04, SPD05");
            }

            string[] array = hand.Split(',');
            if (array.Length != 5)
            {
                throw new Exception("A hand is a comma separated string with five cards. Example: HRT10, HRTKN, HRTQU, HRTKI, HRTAC");
            }

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    Card card = Card.Parse(array[i].Trim().ToUpper());
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (_cards[j] == card)
                            {
                                throw new Exception($"Card {i + 1} ({card}) is a duplicate.");
                            }
                        }
                    }

                    _cards[i] = card;
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
        }

        internal void Clear()
        {
            for (int i = 0; i < _cards.Length; i++)
            {
                _cards[i] = null;
            }
        }

        internal int PutCard(Card card)
        {
            int firstFreeIndex = GetFirstFreeIndex();
            if (firstFreeIndex >= 0 && firstFreeIndex < _cards.Length)
            {
                _cards[firstFreeIndex] = card;
            }

            return firstFreeIndex;
        }

        internal void PutCard(Card card, int index)
        {
            _cards[index] = card;
        }

        internal Card PeekCard(int index)
        {
            return _cards[index];
        }

        internal Card PopCard(int index)
        {
            Card result = _cards[index];
            _cards[index] = null;
            return result;
        }

        internal List<Card> PeekCards()
        {
            return _cards.Where((Card t) => !(t == null)).ToList();
        }

        internal List<Card> PopCards()
        {
            List<Card> list = new List<Card>();
            for (int i = 0; i < _cards.Length; i++)
            {
                if (!(_cards[i] == null))
                {
                    list.Add(_cards[i]);
                }

                _cards[i] = null;
            }

            return list;
        }

        internal int GetFirstFreeIndex()
        {
            for (int i = 0; i < _cards.Length; i++)
            {
                if (_cards[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }

        internal int CountSuit(SuitEnum suit)
        {
            return _cards.Count((Card x) => x.Suit == suit);
        }

        internal int CountValue(Value value)
        {
            return _cards.Count((Card x) => x.Value == value);
        }

        internal bool Sort()
        {
            if (Count < 5)
            {
                return false;
            }

            Array.Sort(_cards);
            return true;
        }

        internal void Swap(int index1, int index2)
        {
            Card card = _cards[index1];
            _cards[index1] = _cards[index2];
            _cards[index2] = card;
        }

        internal void ShiftRight()
        {
            Card card = _cards[4];
            for (int num = 3; num >= 0; num--)
            {
                _cards[num + 1] = _cards[num];
            }

            _cards[0] = card;
        }

        public FormationDescription GetFormationDescription()
        {
            return new FormationDescription(Formation, _cards, Score);
        }

        public override string ToString()
        {
            return GetFormationDescription().ToString();
        }

        public bool CheckFormation()
        {
            if (Count < 5)
            {
                return false;
            }

            Sort();
            Formation = Formation.Nothing;
            Card[] cards = _cards;
            foreach (Card card in cards)
            {
                if (!(card == null))
                {
                    card.InFormation = false;
                }
            }

            bool flag = (_cards[1].Score == _cards[0].Score + 1 && _cards[2].Score == _cards[1].Score + 1 && _cards[3].Score == _cards[2].Score + 1 && _cards[4].Score == _cards[3].Score + 1) || (_cards[4].Value == Value.Ace && _cards[0].Value == Value.Value02 && _cards[1].Value == Value.Value03 && _cards[2].Value == Value.Value04 && _cards[3].Value == Value.Value05);
            SuitEnum suit = _cards[0].Suit;
            bool flag2 = CountSuit(suit) == 5;
            if (flag || flag2)
            {
                cards = _cards;
                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i].InFormation = true;
                }
            }

            int[] array = new int[5];
            for (int j = 0; j < 5; j++)
            {
                array[j] = CountValue(_cards[j].Value);
            }

            if (flag && _cards[0].Value == Value.Value02 && _cards[4].Value == Value.Ace)
            {
                ShiftRight();
            }

            int num = 0;
            for (int k = 0; k < 5; k++)
            {
                if (array[k] == 2)
                {
                    num++;
                }
            }

            bool flag3 = num == 4;
            bool flag4 = num == 2;
            if (flag && _cards[0].Value == Value.Value10 && flag2)
            {
                Formation = Formation.RoyalFlush;
                return true;
            }

            if (flag && flag2)
            {
                Formation = Formation.StraightFlush;
                return true;
            }

            if (array[0] == 4 || array[1] == 4)
            {
                Formation = Formation.FourOfAKind;
                Value value = (array[0] == 4) ? _cards[0].Value : _cards[1].Value;
                for (int l = 0; l < 5; l++)
                {
                    _cards[l].InFormation = (_cards[l].Value == value);
                }

                return true;
            }

            if ((array[0] == 2 && array[4] == 3) || (array[0] == 3 && array[4] == 2))
            {
                Formation = Formation.FullHouse;
                cards = _cards;
                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i].InFormation = true;
                }

                return true;
            }

            if (flag2)
            {
                Formation = Formation.Flush;
                return true;
            }

            if (flag)
            {
                Formation = Formation.Straight;
                return true;
            }

            if (array[0] == 3 || array[2] == 3 || array[4] == 3)
            {
                Formation = Formation.ThreeOfAKind;
                if (array[0] == 3)
                {
                    _cards[0].InFormation = true;
                    _cards[1].InFormation = true;
                    _cards[2].InFormation = true;
                }
                else if (array[4] == 3)
                {
                    _cards[2].InFormation = true;
                    _cards[3].InFormation = true;
                    _cards[4].InFormation = true;
                }
                else
                {
                    _cards[1].InFormation = true;
                    _cards[2].InFormation = true;
                    _cards[3].InFormation = true;
                }

                return true;
            }

            if (flag3)
            {
                Formation = Formation.TwoPairs;
                for (int m = 0; m < 5; m++)
                {
                    _cards[m].InFormation = (array[m] == 2);
                }

                return true;
            }

            if (flag4)
            {
                Formation = Formation.OnePair;
                for (int n = 0; n < 5; n++)
                {
                    _cards[n].InFormation = (array[n] == 2);
                }

                return true;
            }

            Formation = Formation.Nothing;
            _cards[4].InFormation = true;
            return true;
        }
    }

#if false // Decompilation log
'154' items in cache
------------------
Resolve: 'System.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.dll'
------------------
Resolve: 'System.Collections, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Collections.dll'
------------------
Resolve: 'System.Linq, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Linq, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Linq.dll'
#endif
}
