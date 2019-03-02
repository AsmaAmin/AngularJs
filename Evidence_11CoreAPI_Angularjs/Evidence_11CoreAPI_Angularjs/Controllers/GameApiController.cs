using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evidence_11CoreAPI_Angularjs.Models;
using Evidence_11CoreAPI_Angularjs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Evidence_11CoreAPI_Angularjs.Controllers
{
    [Produces("application/json")]
    [Route("api/GameApi")]
    public class GameApiController : Controller
    {
        private readonly DbGames db;

        public GameApiController(DbGames _db)
        {
            db = _db;
        }
        #region Crud
        [Authorize(Roles="Member")]
        [HttpGet]
        public IEnumerable<Game> Get()
        {
            return db.Games;
        }
        [HttpGet("{id}")]

        public Game Get(int id)
        {
            return db.Games.Find(id);
        }
        [HttpPost]
        public IActionResult Post([FromBody] VmGames g)
        {

            if (g != null)
            {
               // if (ModelState.IsValid)
               // {
                    var ga = new Game
                    {
                        Name = g.Name,
                    };

                    db.Games.Add(ga);

                    if (db.SaveChanges() > 0)
                    {
                        return CreatedAtAction("Get", ga);
                    }
                //}
            }


            return BadRequest(/*ModelState*/);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Game g)
        {
            if (g != null)
            {
                db.Entry(g).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    return CreatedAtAction("Get", db.Games);
                }
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (id > 0)
            {
                db.Games.Remove(db.Games.Find(id));
                if (db.SaveChanges() > 0)
                {
                    return CreatedAtAction("Get", db.Games);

                }
            }
            return BadRequest();
        }
        #endregion
    }
}