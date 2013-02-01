using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage.TableData;

namespace DecisionTree.Logic
{
    /// <summary>
    /// Implementierung der TableLogic
    /// </summary>
    public class CTableLogic
    {
        protected IDBDataReader mTableReader = new CDBDataReader();

        /*********************************************************************/
        /// <summary>
        /// Gibt alle Tabellendaten aus der Datenbank zurück.
        /// </summary>
        /// <returns>CTableEntryList</returns>
        public CTableEntryList getAllTableData()
        {
            return mTableReader.getAllEntries();
        }

    }
}
