using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace AirportLibrary
{
    public class Database
    {
        private readonly string _connString;

        public Database(string dbPath)
        {
            _connString = $"Data Source={dbPath}";
        }

        public SqliteConnection CreateConnection()
        {
            var conn = new SqliteConnection(_connString);
            conn.Open();
            return conn;
        }
    }
}
