using SongCollection.DAL;
using SongCollection.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SongCollection.Controllers
{
    public class SongsController : ApiController
    {
        private ISongRepository _songRepository;

        public SongsController()
        {
            _songRepository = new SongRepository();
        }

        // GET: api/Songs
        public IHttpActionResult Get()
        {
            IEnumerable<Song> songs;
            try
            {
                songs = _songRepository.GetSongs();
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }

            return Ok(songs);
        }

        // GET: api/Songs/5
        public IHttpActionResult Get(int id)
        {
            Song song;
            try
            {
                song = _songRepository.GetSong(id);
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }

            if (song == null)
            {
                return NotFound();
            }
            return Ok(song);
        }

        // POST: api/Songs
        [HttpPost]
        public IHttpActionResult CreateSong([FromBody]Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Song newSong;
            try
            {
                newSong = _songRepository.CreateSong(song);
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }

            return Created(new Uri(Request.RequestUri + "/" + song.SongID), newSong);
        }

        // PUT: api/Songs/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            song.SongID = id;
            bool updateSuccess;
            try
            {
                updateSuccess = _songRepository.UpdateSong(song);
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }


            if (!updateSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Songs/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            bool deleteSuccess;
            try
            {
                deleteSuccess = _songRepository.DeleteSong(id);
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }

            if (!deleteSuccess)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
