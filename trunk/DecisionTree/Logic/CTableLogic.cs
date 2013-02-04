using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage.TableData;
using DecisionTree.Storage;

namespace DecisionTree.Logic
{
    /// <summary>
    /// Implementierung der TableLogic
    /// </summary>
    public class CTableLogic
    {
        protected IDBDataReader mTableReader = new CDBDataReader();

        // Testweise .. Der name der Spalte muss später von der GUI kommen
        int NUM_COLUMNS = 0;

        /*********************************************************************/
        /// <summary>
        /// Gibt alle Tabellendaten aus der Datenbank zurück.
        /// </summary>
        /// <returns>CTableEntryList</returns>
        public CTableEntryList getAllTableData()
        {
            return mTableReader.getAllEntries();
        }

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum hinzufügen eines weiteren Attributes zur Tabelle
        /// </summary>
        public CAttributeType addAttribute()
        {
            // Testweise .. Der name der Spalte muss später von der GUI kommen
            NUM_COLUMNS++;
            string name = "Column" + NUM_COLUMNS.ToString();
            return mTableReader.addColumn(name);
            //return name;
        }

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum löschen des letzten Attributes zur Tabelle
        /// </summary>
        public bool removeAttribute(string attributeName)
        {
            return mTableReader.removeColumn(attributeName);

            // Testweise .. Der name der Spalte muss später von der GUI kommen
            //mTableReader.addColumn("Column" + NUM_COLUMNS.ToString());
            //NUM_COLUMNS--;
        }
    }
}
