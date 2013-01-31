using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;

namespace DecisionTree.Storage.TableData
{
    /// <summary>
    /// Stellt die Verbindung zur Datenbank her und holt Daten aus dieser.
    /// </summary>
    public class CSQLiteConnection
    {
        protected SQLiteConnection mConnection;
        protected SQLiteCommand mCommand;

        const string DATABASE_SOURCE = "Data Source=";
#if DEBUG
        const string DATABASE_PATH = "\\..\\..\\Database\\test.sl3";
#else
        const string DATABASE_PATH = ":memory:";
#endif

        /*********************************************************************/
        /// <summary>
        /// Basiskonstruktor
        /// </summary>
        public CSQLiteConnection()
        {
#if TEST || RELEASE
            // Im Releasebetrieb verwenden wir eine Inmemory Datenbank.
            // Daher muss der Pfad zur exe nicht angegeben werden.
            string exePath = "";
#else
            // Im Debugbetrieb verwenden wir eine Datenbank auf der Festplatte.
            // Dazu müssen wir relativ zur exe den Datenbankpfad ermitteln
            string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            
            // wenn die Datenbankdatei noch nicht existiert, legen wir die jetzt an
            if (File.Exists(exePath + DATABASE_PATH) == false)
            {
                //string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                // wenn die Datenbankdatei noch nicht existiert, legen wir die jetzt an
                if (File.Exists(exePath + DATABASE_PATH) == false)
                {
                    SQLiteConnection.CreateFile(exePath + DATABASE_PATH);
                }
            }
            catch
            { 
               exePath = ""; 
            }            
#endif

            string databaseSource = DATABASE_SOURCE;
            databaseSource += exePath;
            databaseSource += DATABASE_PATH;

            mConnection = new SQLiteConnection(databaseSource);
            mConnection.Open();

            mCommand = new SQLiteCommand(mConnection);
        }

        /*********************************************************************/
        /// <summary>
        /// Destruktor
        /// </summary>
        ~CSQLiteConnection()
        {
            mCommand.Dispose();
        }

        /*********************************************************************/
        /// <summary>
        /// Führt einen SQL-Befehl aus. 
        /// Verwenden um z.B. Daten zu ändern oder eine Tabelle anzupassen.
        /// </summary>
        /// <param name="sqlString">SQL Befehl der ausgeführt werden soll</param>
        /// <returns>Erfolg oder Fehler bei der Ausführung</returns>
        public bool sqlExecuteStatement(string sqlString)
        {
            try
            {
                // Zur Sicherheit Threadsicher machen
                lock (mCommand)
                {
                    mCommand.CommandText = sqlString;
                    mCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Fehler beim Ausführen des SQL-Befehls: " + sqlString);
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Führt einen SQL-Befehl aus um Daten aus der Datenbank abzufragen.
        /// Verwenden um z.B. Daten aus der Datenbank zu holen
        /// </summary>
        /// <param name="sqlString">SQL-Befehl der aisgeführt werden soll</param>
        /// <param name="dataReader">Reader um auf die Daten der Datenbank zugreifen zu können</param>
        /// <returns>Erfolg oder Fehler bei der Ausführung</returns>
        public bool sqlRequestStatement(string sqlString, out SQLiteDataReader dataReader)
        {
            try
            {
                // Zur Sicherheit Threadsicher machen
                lock (mCommand)
                {
                    mCommand.CommandText = sqlString;
                    dataReader = mCommand.ExecuteReader();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Fehler beim Ausführen des SQL-Befehls: " + sqlString);
                Debug.WriteLine(e.Message);
                dataReader = null;
                return false;
            }
        }
    } // class
} // namespace
