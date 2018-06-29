namespace SongCollection.Models
{
    public class Song
    {
        public int SongID { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public virtual Artist Artist { get; set; }
    }
}