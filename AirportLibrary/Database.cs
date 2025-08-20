using Microsoft.Data.Sqlite;
using System.IO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AirportLibrary
{
    public class Database
    {
        private readonly string _connString;
        private readonly string _dbPath;

        public Database(string dbPath)
        {
            _dbPath = dbPath;
            _connString = $"Data Source={dbPath}";
            EnsureDatabaseCreated();
        }

        private void EnsureDatabaseCreated()
        {
            // Хэрэв файл байхгүй бол .db файл үүснэ
            if (!File.Exists(_dbPath))
            {
                Console.WriteLine("Database file not found. Creating new one...");
                using var conn = new SqliteConnection(_connString);
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Flights (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FlightCode TEXT NOT NULL,
                    Status TEXT NOT NULL
                );";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"
                    INSERT INTO Flights(FlightCode, Status) VALUES 
                    ('MN121', 'Онгоцонд сууж байна'),
                    ('MN162', 'Бүртгэж байна'),
                    ('MN382', 'Цуцалсан');
                ";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Passenger (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        PassportNo TEXT NOT NULL,
                        FlightId INTEGER NOT NULL,
                        SeatNo TEXT,
                        FOREIGN KEY (FlightId) REFERENCES Flights(Id)
                    );
                ";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"
                    INSERT INTO Passenger (Name, PassportNo, FlightId, SeatNo) VALUES
                    ('Aydana Bolat', 'MN1234567', 1, '1A'),
                    ('John Doe', 'US9876543', 1, '1B'),
                    ('Jane Smith', 'UK2468101', 2, '2A');
                ";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Seat (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        SeatNo TEXT NOT NULL,
                        FlightId INTEGER NOT NULL,
                        isTaken INTEGER NOT NULL,
                        FOREIGN KEY (FlightId) REFERENCES Flights(Id)
                    );
                ";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"
                    INSERT INTO Seat (SeatNo, FlightId, isTaken) VALUES
                    ('1A', 1, 1),
                    ('1B', 1, 1),
                    ('1C', 1, 0),
                    ('2A', 2, 1),
                    ('2B', 2, 0),
                    ('3B', 3, 0);
                ";
                cmd.ExecuteNonQuery();

                Console.WriteLine("✅ New database and Flights table created.");
            }
        }

        public SqliteConnection CreateConnection()
        {
            var conn = new SqliteConnection(_connString);
            conn.Open();
            return conn;
        }
    }
}
