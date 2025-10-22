using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace Flashcards.Data
{
    internal class Database
    {
        private readonly IConfiguration _configuration;

        public Database(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection GetConnection()
        {
            var connectionString = _configuration.GetConnectionString("FlashcardsDatabase");
            return new SqlConnection(connectionString);
        }
    }
}
