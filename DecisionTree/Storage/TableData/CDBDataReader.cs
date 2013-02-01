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
           if (reader != null)
           {
               reader.Close();
               reader.Dispose();
           }
        }

        /*********************************************************************/
        /// <summary>
        /// Ließt alle Einträge der Tabelle aus und gibt diese zurück
        /// !!Zurzeit TestDATA!!!
        /// </summary>
        /// <returns>Liste mit allen Tabelleneinträgen</returns>
        public CTableEntryList getAllEntries()
        {
            

            CTableEntryList entryList = new CTableEntryList();

            /** Fake Daten um die GUI provisorisch zu testen**/
            //**BEGIN**//
            //Attributetypen erstellen
            CAttributeType attribute1 = new CAttributeType("erstesAttribute", E_DATATYPE.E_STRING, false);
            CAttributeType attribute2 = new CAttributeType("2tes Attribute", E_DATATYPE.E_FLOAT, true);

            //AttributeValues erstellen

            CAttributeValue value1 = new CAttributeValue(attribute1, "index", "value");
            CAttributeValue value2 = new CAttributeValue(attribute2, "index2", "1.2");

            //Entry erstellen

            CTableEntry entry1 = new CTableEntry();
            entry1.addValue(value1);
            entry1.addValue(value2);

            CTableEntry entry2 = new CTableEntry();
            entry2.addValue(value1);
            entry2.addValue(value2);


            //EntryList erstellen

            entryList.Add(entry1);
            entryList.Add(entry2);

            return entryList;
            //**ENDE**//


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
                closeReader(reader);
            }

            return entryList;
        }

        /*********************************************************************/
        /// <summary>
        /// fügt neuen leeren Datensatz in die Datenbak ein und gibt diesen zurück
        /// </summary>
        /// <returns>leerer Datenbankeintrag</returns>
        public CTableEntry insertEntry()
        {
            string sSQLCommand = "INSERT INTO "+ CTableConstants.TABLE_ATTRIBUTES +" (id) VALUES(NULL)";
            mConnection.sqlExecuteStatement(sSQLCommand);
            //nimmt die letzte Zeile der DB 
            sSQLCommand = "SELECT *  FROM DataTable ORDER BY id DESC LIMIT 1";   
            SQLiteDataReader reader;
            //sendet den Request ab und  packt die Zeile in den Reader
            mConnection.sqlRequestStatement(sSQLCommand, out reader);
            {
                while (reader.Read()) //für jede Zeile
                {
                    //für jede Spalte arbeite er jedes element einzeln ab 
                    for (int field = 0; field < reader.FieldCount; field++) 
                    {
                        Console.Write(reader[field] + " ");
                    }
                    Console.Write("\n");
                }
            }
            return null;

        }
    }
}
