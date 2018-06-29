using Dapper;
using SongCollection.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SongCollection.DAL
{
    public class SongRepository : ISongRepository
    {
        private IDbConnection _db;

        public SongRepository()
        {
            _db = DBConnection.GetConnection();
        }
        public Song CreateSong(Song song)
        {
            var sqlString = @"
                INSERT Songs(ArtistId, Title, Price) VALUES (@ArtistID, @Title, @Price);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            var newId = _db.QuerySingle<int>(sqlString, new { ArtistId = song.ArtistId, Title = song.Title, Price = song.Price });

            Song insertedSong = GetSong(newId);

            return insertedSong;
        }

        public bool DeleteSong(int songId)
        {
            int rowsAffected = _db.Execute(@"DELETE FROM Songs WHERE SongID = @SongID", new { SongID = songId });

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public Song GetSong(int songId)
        {
            var sqlString = "SELECT * FROM Songs AS S JOIN Artists AS A ON S.ArtistID = A.ArtistID WHERE S.SongID = @SongID";

            var song = _db.Query<Song, Artist, Song>(
                sqlString,
                (s, a) =>
                {
                    s.Artist = a;
                    return s;
                },
                new { SongID = songId },
                splitOn: "ArtistId")
                .SingleOrDefault();

            return song;
        }

        public List<Song> GetSongs()
        {
            var sqlString = "SELECT * FROM Songs AS S JOIN Artists AS A ON S.ArtistID = A.ArtistID";

            var songs = _db.Query<Song, Artist, Song>(
                sqlString,
                (s, a) =>
                {
                    s.Artist = a;
                    return s;
                },
                splitOn: "ArtistId")
                .ToList();

            return songs;
        }

        public bool UpdateSong(Song song)
        {
            int rowsAffected = _db.Execute($"UPDATE Songs SET Title = @Title, ArtistID = @ArtistId, Price = @Price WHERE SongID = {song.SongID}", song);

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}