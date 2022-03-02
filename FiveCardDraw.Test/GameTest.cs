using FiveCardDraw.Lib;
using FiveCardDraw.Lib.Helpers.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FiveCardDraw.Test
{

    public class GameTest
    {
        private Game CreateGame()
        {
            return new Game();
        }

        private Random CreateRandom()
        {
            return new Random();
        }

        private FormationChecker CreateThreeOfAKindFormationChecker()
        {
            return new FormationChecker("3\u2663, 7\u2666, 7\u2665, 7\u2660, K\u2665");
        }

        [Fact]
        public void Start()
        {
            Game game = CreateGame();
            game.Start();

            int expectedResult = 52; // expect 52 cards to be generated
            int actualResult = game.Cards.Count;
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Shuffle()
        {
            Game game = CreateGame();
            game.Start();


            string expectedResult = string.Empty;
            foreach (var card in game.Cards)
                expectedResult += card.ToString();

            Random randomizer = CreateRandom();
            for (int i = game.Cards.Count - 1; i > 0; i--)
            {
                int index = randomizer.Next(0, i);
                (game.Cards[index], game.Cards[i]) = (game.Cards[i], game.Cards[index]);
            }

            string actualResult = string.Empty;
            foreach (var card in game.Cards)
                actualResult += card.ToString();

            Assert.NotEqual(expectedResult, actualResult);

        }

        [Fact]
        public void CheckHandRank()
        {
            var formatChecker = CreateThreeOfAKindFormationChecker();
            var formation = formatChecker.CheckFormation();

            string expectedResult = "3-OF-A-KIND";
            string actualResult = formatChecker.GetFormationDescription().ToString();

            Assert.Equal(expectedResult, actualResult);

        }

    }

}
