using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using BordAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace BordAPI.Controllers
{
  [Route("api/games")]
  [ApiController]
  public class GamesController : ControllerBase
  {
    private BordAPIContext _db;
    public GamesController(BordAPIContext db)
    {
      _db = db;
    }

    //GET api/games
    [HttpGet]
    public ActionResult<IEnumerable<Game>> Get(string gameName, int? minPlayers, int? maxPlayers, int? minAge, int? playTimeMin, string search, bool random)
    {
      var query = _db.Games.AsQueryable();
      if (gameName != null)
      {
        query = query.Where(entry => entry.GameName == gameName);
      }
      if (minPlayers != null)
      {
        query = query.Where(entry => entry.MinPlayers >= minPlayers);
      }
      if (maxPlayers != null)
      {
        query = query.Where(entry => entry.MaxPlayers <= maxPlayers);
      }
      if (search != null)
      {
        query = query.Where(entry => entry.GameName == search);
      }
      if (random != false)
      {
        Random rnd = new Random();
        int toSkip = rnd.Next(0, _db.Games.Count());
        Game randomGame = _db.Games.OrderBy(r => Guid.NewGuid()).Skip(toSkip).Take(1).First();

        query = query.Where(entry => entry.GameId == randomGame.GameId);
      }
      return query.ToList();
    }
    //POST api/games
    [HttpPost]
    public void Post([FromBody] Game game)
    {
      _db.Games.Add(game);
      _db.SaveChanges();
    }
    //GET api/games/{id}
    [HttpGet("{id}")]
    public ActionResult<Game> Get(int id)
    {
      return _db.Games.FirstOrDefault(entry => entry.GameId == id);
    }
    //PUT api/games/{id}
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Game game)
    {
      game.GameId = id;
      _db.Entry(game).State = EntityState.Modified;
      _db.SaveChanges();
    }
    //DELETE api/games/{id}
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      Game gameToDelete = _db.Games.FirstOrDefault(entry => entry.GameId == id);
      _db.Games.Remove(gameToDelete);
      _db.SaveChanges();
    }
  }
}