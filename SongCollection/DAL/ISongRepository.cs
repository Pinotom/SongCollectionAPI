using SongCollection.Models;
using System.Collections.Generic;

namespace SongCollection.DAL
{
    internal interface ISongRepository
    {
        List<Song> GetSongs();
        Song GetSong(int songId);
        Song CreateSong(Song song);
        bool DeleteSong(int songID);
        bool UpdateSong(Song song);

    }
}
