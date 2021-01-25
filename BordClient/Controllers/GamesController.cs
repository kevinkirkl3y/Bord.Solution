using BordClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BordClient.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult Index()
        {
            var allGames = Game.GetGames();
            return View(allGames);
        }
        [HttpPost]
        public IActionResult Index(Game game)
        {
            Game.Post(game);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var game = Game.GetDetails(id);
            return View(game);
        }

        public IActionResult Edit(int id)
        {
            var game = Game.GetDetails(id);
            return View(game);
        }

        [HttpPost]
        public IActionResult Details(int id, Game game)
        {
            game.GameId = id;
            Game.Put(game);
            return RedirectToAction("Details", id);
        }

        public IActionResult Delete(int id)
        {
            Game.Delete(id);
            return RedirectToAction("Index");
        }
    }
}