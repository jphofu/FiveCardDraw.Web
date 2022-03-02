using FiveCardDraw.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace FiveCardDraw.Lib
{
    public class Game
    {
        private static string spades = "\u2660";
        private static string clubs = "\u2663";
        private static string diamonds = "\u2666";
        private static string hearts = "\u2665";
        public static string[] Suits = { spades, clubs, diamonds, hearts };
        public static string[] values = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        public List<Card> Cards;

        public Game()
        {
            Cards = new List<Card>();
        }

        public void Start()
        {
            try
            {
                Cards = new List<Card>();

                for (int k = 0; k < Suits.Length; k++)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        Card card = new Card(values[i], Suits[k]);
                        Cards.Add(card);
                    }
                }

            }
            catch (IndexOutOfRangeException e)
            {

                throw new IndexOutOfRangeException("Game Unable to start: " + e.Message);
            }

        }

        public void Exit()
        {
            this.Exit();
        }

        public void ShuffleCards()
        {
            Random randomizer = new Random();
            for (int i = Cards.Count - 1; i > 0; i--)
            {
                int index = randomizer.Next(0, i);
                (Cards[index], Cards[i]) = (Cards[i], Cards[index]);
            }

        }

        public void UpdateAfterDeal(int numberOfCards)
        {
            if (Cards.Count - numberOfCards < 0)
                return;

            for (int i = 0; i < numberOfCards; i++)
            {
                Cards.RemoveAt(0);
            }

        }

    }
}
