using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Reflection;


namespace SQLite
{
    class Program
    {
        const string SQL_DATA_SOURCE = "Data Source=";
        const string DB_PATH = "..\\..\\..\\DB\\test.sl3";
        const string DB_INMEMORY = "Data Source=:memory:";

        static void Main(string[] args)
        {
            string exePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            // SQLiteConnection.CreateFile(exePath + DB_PATH);

            // SQLite Verbinung zu Datenbank öffnen
            SQLiteConnection connection = new SQLiteConnection(SQL_DATA_SOURCE + exePath + DB_PATH);
            connection.Open();

            
            SQLiteCommand command = new SQLiteCommand(connection);

            command.CommandText = "DROP TABLE IF EXISTS Attribut";
            command.ExecuteNonQuery();

            // Tabelle erstellen
            command.CommandText = "CREATE TABLE IF NOT EXISTS Attribut (id INTEGER PRIMARY KEY AUTOINCREMENT)";
            command.ExecuteNonQuery();

            // Der Tabelle ein Paar SPalten hinzufügen
            command.CommandText = "ALTER TABLE Attribut ADD COLUMN Attribut1 TEXT";
            command.ExecuteNonQuery();
            command.CommandText = "ALTER TABLE Attribut ADD COLUMN Attribut2 TEXT";
            command.ExecuteNonQuery();

            // Eintrag erzeugen
            for (int i = 1; i <= 15; i++)
            {
                command.CommandText = "INSERT INTO Attribut VALUES(NULL, 'eintrag" + i + "1', 'Eintrag" + i + "2')";
                command.ExecuteNonQuery();
            }

            command.CommandText = "SELECT Attribut1, Attribut2 FROM Attribut";
            using (SQLiteDataReader reader = command.ExecuteReader())
            {

                Console.WriteLine(command.CommandText);
                while (reader.Read())
                {
                    for (int field = 0; field < reader.FieldCount; field++)
                    {
                        Console.Write(reader[field] + " ");
                    }
                    Console.Write("\n");
                }
            }

            command.CommandText = "SELECT * FROM Attribut WHERE id = 1 or id = 3";
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine(command.CommandText);
                while (reader.Read())
                {
                    for (int field = 0; field < reader.FieldCount; field++)
                    {
                        Console.Write(reader[field] + " ");
                    }
                    Console.Write("\n");
                }
            }

            command.Dispose();

            // zum Schluss wieder schließen
            connection.Close();
            connection.Dispose();
        }
    }
}
