// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

using Lyudmila.API.Dto;
using Lyudmila.API.Models;

namespace Lyudmila.API.Controllers
{
    public class PlayersController : ApiController
    {
        private readonly LyudmilaAPIContext db = LyudmilaAPIContext.GetInstance();

        // GET: api/Players
        public List<Player> GetPlayers()
        {
            return db.Players;
        }

        // GET: api/Players/5
        [ResponseType(typeof(Player))]
        public IHttpActionResult GetPlayer(int id)
        {
            var player = db.Players.Find(p => p.Id == id);
            if(player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // PUT: api/Players/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlayer(int id, Player player)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != player.Id)
            {
                return BadRequest();
            }

            if(!PlayerExists(id))
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Players
        [ResponseType(typeof(Player))]
        public IHttpActionResult PostPlayer(PlayerDto player)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nPlayer = new Player {Nickname = player.Nickname, Status = player.Status, IpAddress = player.IpAddress};

            db.Players.Add(nPlayer);

            return CreatedAtRoute("DefaultApi", new {id = nPlayer.Id}, player);
        }

        // DELETE: api/Players/5
        [ResponseType(typeof(Player))]
        public IHttpActionResult DeletePlayer(int id)
        {
            var player = db.Players.Find(p => p.Id == id);
            if(player == null)
            {
                return NotFound();
            }

            db.Players.Remove(player);

            return Ok(player);
        }

        private bool PlayerExists(int id)
        {
            return db.Players.Count(e => e.Id == id) > 0;
        }
    }
}