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
        List<CAttributeType> mAttributeTypeList;
        CSQLiteConnection mConnection;
        CTableDataManager mTableManager;
        CDataTypeChecker mDataTypeChecker;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CDBDataReader()
        {
            mDataTypeChecker = new CDataTypeChecker();
            mConnection = new CSQLiteConnection();
            mAttributeTypeList = new List<CAttributeType>();
            mTableManager = new CTableDataManager(mConnection, mAttributeTypeList);
            
            mTableManager.setUpDatabase();

            // Einkommentieren um Testdaten neu einzufügen
            // ! Überschreibt alle bisherigen Daten !
            //createTestData();
        }

        /*********************************************************************/
        /// <summary>
        /// Fügt ein paar Testdaten in die Tabelle ein
        /// </summary>
        private void createTestData()
        {
            clearDatabase();

            string sSQLCommand = "INSERT INTO " + CTableConstants.TABLE_ATTRIBUTES + " (id";

            for (int attribute = 0; attribute < CTableConstants.MAX_ATTRIBUTE_COUNT; attribute++)
            {
                sSQLCommand += ", ";
                sSQLCommand += CTableConstants.ATTR_X + attribute.ToString();
            }

            sSQLCommand += ") VALUES ";

            for (int entry = 0; entry < 50; entry++)
            {
                if (entry != 0) sSQLCommand += ", ";

                sSQLCommand += "(NULL"; 
                for (int attribute = 0; attribute < CTableConstants.MAX_ATTRIBUTE_COUNT; attribute++)
                {
                    sSQLCommand += ", '" + entry.ToString() + " " + attribute.ToString() + "'";
                }
                sSQLCommand += ") ";
            }

            mConnection.sqlExecuteStatement(sSQLCommand);
            
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

            #region Engin Test
            /*// Fake Daten um die GUI provisorisch zu testen //
            // BEGIN //
            //Attributetypen erstellen
            //CAttributeType attribute1 = new CAttributeType("erstesAttribute", E_DATATYPE.E_STRING, false);
            //CAttributeType attribute2 = new CAttributeType("2tes Attribute", E_DATATYPE.E_FLOAT, true);

            CAttributeType attribute1 = new CAttributeType(CTableConstants.ATTR_X + "1");
            CAttributeType attribute2 = new CAttributeType(CTableConstants.ATTR_X + "2");
            attribute1.setUsed("erstesAttribute", E_DATATYPE.E_STRING, false);
            attribute2.setUsed("2tes Attribute", E_DATATYPE.E_FLOAT, true);

            //AttributeValues erstellen

            CAttributeValue value1 = new CAttributeValue(attribute1, "index", "value");
            CAttributeValue value2 = new CAttributeValue(attribute2, "index2", "1.2");

            //Entry erstellen

            CTableEntry entry1 = new CTableEntry("1");
            entry1.addValue(value1);
            entry1.addValue(value2);

            CTableEntry entry2 = new CTableEntry("2");
            entry2.addValue(value1);
            entry2.addValue(value2);


            //EntryList erstellen

            entryList.Add(entry1);
            entryList.Add(entry2);

            return entryList;
            //**ENDE**/
            #endregion

            string sSQLCommand = "SELECT * FROM " + CTableConstants.TABLE_ATTRIBUTES;

            SQLiteDataReader reader;
            if (mConnection.sqlRequestStatement(sSQLCommand, out reader) == true)
            {
                CTableEntry tableEntry;
                while (getNextTableEntry(reader, out tableEntry) == true)
                {
                    entryList.Add(tableEntry);
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
            // Return wert
            CTableEntry tableEntry = null;

            // Neuen Eintrag in die Datenbank eintragen
            string sSQLCommand = "INSERT INTO "+ CTableConstants.TABLE_ATTRIBUTES +" (id) VALUES(NULL)";
            mConnection.sqlExecuteStatement(sSQLCommand);

            // wie holen die zuletzt in die Tabelle eingefügten Eintrag
            sSQLCommand = "SELECT *  FROM DataTable ORDER BY id DESC LIMIT 1";   
            SQLiteDataReader reader;
            //sendet den Request ab und  packt die Zeile in den Reader
            mConnection.sqlRequestStatement(sSQLCommand, out reader);
            {
                getNextTableEntry(reader, out tableEntry);
            }

            // den Reader schließen 
            closeReader(reader);

            // und den Eintrag zurückgeben
            return tableEntry;
        }

        /*********************************************************************/
        /// <summary>
        /// Löscht einen Eintrag aus der Datenbank
        /// </summary>
        /// <param name="entry">Zu Löschender Eintrag</param>
        /// <returns>Erfolg des Löschens</returns>
        public bool removeEntry(CTableEntry entry)
        {
            string sSQLCommand = "DELETE FROM " + CTableConstants.TABLE_ATTRIBUTES + " WHERE id='" + entry.Index + "'";
            return mConnection.sqlExecuteStatement(sSQLCommand);
        }

        /*********************************************************************/
        /// <summary>
        /// Ließt aus dem Reader den nächsten TableEntry aus. 
        /// </summary>
        /// <param name="reader">Reader der den Zugriff auf den Datensatz bietet</param>
        /// <param name="tableEntry">ausgelesener Datensatz</param>
        /// <returns>Erfolg des Auslesens</returns>
        /// <remarks>Am besten mit einer Schleife aufrufen, bis die Methode false zurückliefert. 
        /// Außerdem muss sichergestellt sein dass das ID Attribut mit ausgelesen wurde</remarks>
        protected bool getNextTableEntry(SQLiteDataReader reader, out CTableEntry tableEntry)
        {
            tableEntry = null;
            if (reader.Read() == true)
            {
                //für jede Spalte arbeite er jedes element einzeln ab 
                for (int field = 0; field < reader.FieldCount; field++) 
                {
                    switch (field)
                    {
                        case 0:
                            tableEntry = new CTableEntry(reader[field].ToString());
                                break;
                        default:
                            //if (mAttributeTypeList[field - 1].Used == true)
                            {
                                tableEntry.addValue(new CAttributeValue(mAttributeTypeList[field - 1], tableEntry.Index, reader[field].ToString(), this));
                            }
                            // die Normalen Attribute
                            break;
                    }
                }
                return true;
            }
            return false;
        }

        /*********************************************************************/
        /// <summary>
        /// fügt eine Spalte zur Tabelle hinzu.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CAttributeType addColumn(string name)
        {
            return mTableManager.addTableAttribute(name, false);
        }

        /*********************************************************************/
        /// <summary>
        /// löscht eine Spalte der Tabelle 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool removeColumn(string name)
        {
            return mTableManager.removeTableAttribute(name);
        }

        /*********************************************************************/
        /// <summary>
        /// Aktualisert den Wert eines Attributes in der Datenbank auf den übergebenen Wert
        /// </summary>
        /// <param name="attribute">Attribute mit aktualisiertem Wert</param>
        /// <param name="newValue">Wert auf den das Attribut gesetzt werden soll</param>
        /// <returns>Erfolg der Aktualisierung</returns>
        public bool updateAttributeValue(CAttributeValue attribute, string newValue)
        {
            mDataTypeChecker.handleDataTypeofValue(attribute, newValue);

            string sSQLCommand = "UPDATE " + CTableConstants.TABLE_ATTRIBUTES + 
                " SET " + attribute.AttributeType.InternalName + "='" + newValue + "'" +
                " WHERE id='" + attribute.EntryIndex + "'";

            return mConnection.sqlExecuteStatement(sSQLCommand);
        }

        /*********************************************************************/
        /// <summary>
        /// Leert komplett alle Einträge der Datenbank und löscht die Spalten.
        /// </summary>
        public void clearDatabase()
        {
            // Alle Datensätze aus der Datenbank löschen
            string sSQLCommand = "DELETE FROM " + CTableConstants.TABLE_ATTRIBUTES;
            mConnection.sqlExecuteStatement(sSQLCommand);

            // Datenbank neu aufsetzen
            mTableManager.setUpDatabase();
        }

        /*********************************************************************/
        /// <summary>
        /// Setzt das Zielattribut auf den übergebenen Typen
        /// </summary>
        /// <param name="targetAttributeType">Attributtyp der zum Zielattribut 
        /// werden soll</param>
        /// <returns>Erfolg des Setztens</returns>
        public bool setTargetAttribute(CAttributeType targetAttributeType)
        {
            foreach (CAttributeType attributeType in mAttributeTypeList)
            {
                attributeType.TargetAttribute = false;
            }

            targetAttributeType.TargetAttribute = true;
            return true;
        }

    }
}
