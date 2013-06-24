using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace DecisionTree.Storage.TableData
{
    /// <summary>
    /// Verwaltet das grobe aussehen der Tabelle. 
    /// Kann Attribute
    /// </summary>
    public class CTableDataManager
    {
        CSQLiteConnection mConnection;
        List<CAttributeType> mAttributeTypeList;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CTableDataManager(CSQLiteConnection connection, List<CAttributeType> attributeTypeList)
        {
            mConnection = connection;
            mAttributeTypeList = attributeTypeList;
        }

        /*********************************************************************/
        /// <summary>
        /// Bereitet die Datenbank zur Nutzung vor.
        /// </summary>
        public void setUpDatabase()
        {
            // zuerst die Datentabelle
            /////////////////////////////
            string sSQLCommand = "CREATE TABLE IF NOT EXISTS " + CTableConstants.TABLE_ATTRIBUTES + " (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT, ";
            for (int attribute = 0; attribute < CTableConstants.MAX_ATTRIBUTE_COUNT; attribute++)
            {
                sSQLCommand += CTableConstants.ATTR_X + attribute + " TEXT";
                if (attribute < CTableConstants.MAX_ATTRIBUTE_COUNT - 1)
                {
                    sSQLCommand += ", ";
                }
            }
            sSQLCommand += ")";
            mConnection.sqlExecuteStatement(sSQLCommand);

            
            // jetzt die Attributtypen vorbereiten
            mAttributeTypeList.Clear();
            for (int attributeIndex = 0; attributeIndex < CTableConstants.MAX_ATTRIBUTE_COUNT; attributeIndex++)
            {
                mAttributeTypeList.Add(new CAttributeType(attributeIndex));
            }

            #region AttributeTypeTabelle (Alt, aber vllt brauchen wir es doch nochmal)
            /*
            // feststellen ob die Tabelle an und für sich existiert
            sSQLCommand = "SELECT * FROM " + CTableConstants.TABLE_ATTRIBUTE_TYPES;
            bool bAttrTypeTableAvailabe = (mConnection.sqlExecuteStatement(sSQLCommand) == true);

            // dann die Attributtypen Tabelle
            ////////////////////////////////////
            sSQLCommand = "CREATE TABLE IF NOT EXISTS " + CTableConstants.TABLE_ATTRIBUTE_TYPES + " (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                CTableConstants.ATTR_TYPES_NAME + " TEXT, " +
                CTableConstants.ATTR_TYPES_TYPE + " INTEGER, " +
                CTableConstants.ATTR_TYPES_INTERNAL_NAME + " TEXT, " +
                CTableConstants.ATTR_TYPES_USED + " INTEGER, " +
                CTableConstants.ATTR_TYPES_TARGET_ATTR + " INTEGER )";
            mConnection.sqlExecuteStatement(sSQLCommand);

            // wenn die Tabelle noch nicht vorhanden war, dann müssen wir da die Attribute einfügen
            if (bAttrTypeTableAvailabe == false)
            {
                sSQLCommand = "INSERT INTO " + CTableConstants.TABLE_ATTRIBUTE_TYPES +
                    "(" + CTableConstants.ATTR_TYPES_NAME + ", " + CTableConstants.ATTR_TYPES_TYPE + ", " + CTableConstants.ATTR_TYPES_INTERNAL_NAME + ", " + CTableConstants.ATTR_TYPES_USED + ", " + CTableConstants.ATTR_TYPES_TARGET_ATTR + ") " +
                    "VALUES " +
                    "('', 0, '" + CTableConstants.ATTR_X + "1', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "2', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "3', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "4', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "5', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "6', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "7', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "8', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "9', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "10', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "11', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "12', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "13', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "14', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "15', 0, 0), " +
                    "('', 0, '" + CTableConstants.ATTR_X + "16', 0, 0) ";
                mConnection.sqlExecuteStatement(sSQLCommand);
            }
            */
            #endregion
        }

        /*********************************************************************/
        /// <summary>
        /// fügt eine zusätzliche Spalte in die Tabelle ein
        /// </summary>
        /// <param name="name"></param>
        /// <param name="targetAttribute"></param>
        /// <returns></returns>
        public CAttributeType addTableAttribute(string sName, bool bTargetAttribute = false)
        {
            if (isAttributExistent(sName) == false)
            {
                CAttributeType attrType = getNextAvailableAttibuteType();

                if (attrType != null)
                {
                    attrType.setUsed(sName, E_DATATYPE.E_STRING, bTargetAttribute);
                    return attrType;
                }
            }

            return null;
            #region altcode .. kann eventuell noch gebraucht werden
            /*
            string sSQLCommand = "UPDATE " + CTableConstants.TABLE_ATTRIBUTE_TYPES + " SET " +
                CTableConstants.ATTR_TYPES_NAME + "='" + sName + "', " +
                CTableConstants.ATTR_TYPES_USED + "=1, " +
                CTableConstants.ATTR_TYPES_TARGET_ATTR + "=" + Convert.ToInt16(bTargetAttribute) + 
                " WHERE " + CTableConstants.ATTR_TYPES_INTERNAL_NAME + "='" + sInternalName + "'";
            mConnection.sqlExecuteStatement(sSQLCommand);
            
            return true;
             */
            #endregion
        }

        /*********************************************************************/
        /// <summary>
        /// Entfernt ein Attribut aus der Liste
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        public bool removeTableAttribute(string sName)
        {
            if (isAttributExistent(sName) == false)
            {
                return false;
            }

            foreach (CAttributeType type in mAttributeTypeList)
            {
                if ((type.Name == sName) && (type.Used == true))
                {
                    // der Typ soll nicht mehr verwendet werden
                    type.setUnused();

                    // jetzt noch die Daten der Spalte in der Datenbank aufräumen
                    string sSQLCommand = "UPDATE " + CTableConstants.TABLE_ATTRIBUTES + " SET " + type.InternalName + "=''";
                    mConnection.sqlExecuteStatement(sSQLCommand);

                    return true;
                }
            }
            return false;

            #region altcode .. kann eventuell noch gebraucht werden
            /*
            string sSQLCommand = "SELECT " + CTableConstants.ATTR_TYPES_INTERNAL_NAME + " FROM " + CTableConstants.TABLE_ATTRIBUTE_TYPES;
            SQLiteDataReader reader;
            if (mConnection.sqlRequestStatement(sSQLCommand, out reader) == false)
            {
                return false;
            }

            if (reader.Read() == false)
            {
                return false;
            }

            string sInternalName = (string)reader[0];
            closeReader(reader);

            // Das Attribut austragen
            sSQLCommand = "UPDATE " + CTableConstants.TABLE_ATTRIBUTE_TYPES + " SET " +
                CTableConstants.ATTR_TYPES_NAME + "='', " +
                CTableConstants.ATTR_TYPES_USED + "=0, " +
                CTableConstants.ATTR_TYPES_TARGET_ATTR + "=0, " +
                CTableConstants.ATTR_TYPES_TYPE + "=0 " +
                "WHERE " + CTableConstants.ATTR_TYPES_INTERNAL_NAME + "='" + sInternalName + "'";
            if (mConnection.sqlExecuteStatement(sSQLCommand) == false)
            {
                return false;
            }

            // Jetzt noch die Datentabelle aufräumen und die bisher 
            // eingetragenen Daten für dieses Attribut rausschmeißen
            sSQLCommand = "UPDATE " + CTableConstants.TABLE_ATTRIBUTES + " SET " + sInternalName + "=''";
            mConnection.sqlExecuteStatement(sSQLCommand);

            return true;
             */
            #endregion
        }

        /*********************************************************************/
        /// <summary>
        /// fügt eine neue Zeile in die Tabelle ein 
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
       

        /*********************************************************************/
        /// <summary>
        /// Prüft ob eine Attribut mit den übergebenen Namen bereits existert
        /// </summary>
        /// <param name="sName">Name des Attributes</param>
        /// <returns>Attribut ist bereits vorhanden oder nicht</returns>
        protected bool isAttributExistent(string sName)
        {
            foreach (CAttributeType type in mAttributeTypeList)
            {
                if ((type.Used == true) && (type.Name == sName))
                {
                    return true;
                }
            }
            return false;

            #region altcode .. kann eventuell noch gebraucht werden
            /*
            SQLiteDataReader reader;

            // zuerst prüfen ob bereits ein Attribut mit diesem Namen existiert
            string sSQLCommand = "SELECT * FROM " + CTableConstants.TABLE_ATTRIBUTE_TYPES + " WHERE(" + CTableConstants.ATTR_TYPES_NAME + "='" + sName + "')";
            if (mConnection.sqlRequestStatement(sSQLCommand, out reader) == false)
            {
                closeReader(reader);
                // Fehler beim lesen
                return true;
            }

            if (reader.HasRows == true)
            {
                closeReader(reader);
                // es ist bereits ein Attribut mit diesem Namen vorhanden
                return true;
            }

            // wir brauchen den Reader nicht mehr
            closeReader(reader);
            return false;*/
            #endregion
        }

        /*********************************************************************/
        /// <summary>
        /// holt den nächsten verfügbaren Attributnamen
        /// </summary>
        /// <returns>Nächster Verfügbarer Attributname</returns>
        protected CAttributeType getNextAvailableAttibuteType()
        {
            foreach (CAttributeType type in mAttributeTypeList)
            {
                if (type.Used == false)
                {
                    return type;
                }
            }
            return null;
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

    } // class
} // namespace
