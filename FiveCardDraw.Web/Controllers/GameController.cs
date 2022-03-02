using FiveCardDraw.Lib;
using FiveCardDraw.Lib.Helpers.Algorithm;
using FiveCardDraw.Lib.Models;
using FiveCardDraw.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FiveCardDraw.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;

        private Game game;
        private Player player;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
            game = GameStart();
            //game.Start();
            player = new Player();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shuffle()
        {
            game = GameStart();
            game.Start();
            game.ShuffleCards();

            return View(game);
        }

        public ActionResult Deal()
        {
            if (game.Cards.Count != 52)
                return View();

            game.Start();
            game.ShuffleCards();

            player.Deal(5, game.Cards);
            ViewBag.Message = player.Hand.ToString();

            return View(game);
        }

        public ActionResult DealAndEvaluateHand()
        {
            try
            {
                if (!player.Hand.Cards.Any())
                {
                    game.Start();
                    game.ShuffleCards();
                    player.Deal(5, game.Cards);
                    game.UpdateAfterDeal(5);
                }

                FormationChecker formationChecker = new FormationChecker(player.Hand.ToString());
                var cf = formationChecker.CheckFormation();
                ViewBag.PlayerCards = player.Hand.ToString();
                ViewBag.Formation = formationChecker.GetFormationDescription().ToString();

            }
            catch (Exception e)
            {

                throw;
            }

            return View(game);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Game GameStart()
        {
            return new Game();
        }
    }
}
