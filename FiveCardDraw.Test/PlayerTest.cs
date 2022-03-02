using FiveCardDraw.Lib;
using FiveCardDraw.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FiveCardDraw.Test
{
    public class PlayerTest
    {
        private Game CreateGame()
        {
            return new Game();
        }

        private Player CreatePlayer()
        {
            return new Player();
        }

        [Fact]
        public void Deal()
        {
            Game game = CreateGame();
            game.Start();
            game.ShuffleCards();

            Player player = CreatePlayer();
            player.Deal(5, game.Cards);

            int expectedResult = 5;
            int actualResult = player.Hand.Cards.Count;

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
