using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;

namespace DecisionTree.Storage.TableData
{
    /// <summary>
    /// Implementierung des Datenzugriffs auf die Tabellendaten.
    /// </summary>
    public class CDBDataReader : IDBDataReader
    {
        CSQLiteConnection mConnection;
        CTableDataManager mTableManager;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CDBDataReader()
        {
            mConnection = new CSQLiteConnection();
            mTableManager = new CTableDataManager(mConnection);

            mTableManager.setUpDatabase();

            mTableManager.addTableAttribute("asd");

            mTableManager.removeTableAttribute("asd");
        }

        /*********************************************************************/
        /// <summary>
        /// Schließt den Reader und gibt das Objekt frei.
        /// </summary>
        /// <param name="reader">Reader der geschlossen werden soll</param>
        protected void closeReader(SQLiteDataReader reader)
       {
            reader.Close();
            reader.Dispose();
        }

        /*********************************************************************/
        /// <summary>
        /// Ließt alle Einträge der Tabelle aus und gibt diese zurück
        /// </summary>
        /// <returns>Liste mit allen Tabelleneinträgen</returns>
        public CTableEntryList getAllEntries()
        {
            // 

            CTableEntryList entryList = new CTableEntryList();
            string sSQLCommand = "SELECT * FROM ATTRIBUTES";

            SQLiteDataReader reader;
            if (mConnection.sqlRequestStatement(sSQLCommand, out reader) == true)
            {
                // So lange die Datensätze lesen bis keine mehr vorhanden sind
                while (reader.Read() == true)
                {
                    CTableEntry tableEntry = new CTableEntry();

                    // die Felder des Datensatzes übernehmen
                    for (int field = 0; field < reader.FieldCount; field++)
                    { 
                        //tableEntry.addValue(CAtt dataReader[field]
                    }
                }
            }
            closeReader(reader);

            return entryList;
        }

    }
}
