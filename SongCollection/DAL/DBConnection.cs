using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SongCollection.DAL
{
    public class DBConnection
    {
        public static IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["SongCollectionDB"].ConnectionString);
        }
    }
}