using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;

namespace DecisionTree.Storage
{
    /// <summary>
    /// Liste Datenbankeinträgen.
    /// Es werden nur Lesezugeriffe zugelassen. Schreiben in die Datenbank
    /// </summary>
    public class CTableEntryList : ObservableCollection<CTableEntry>
    {
        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CTableEntryList()
        { }
        
        /*********************************************************************/
        /// <summary>
        /// Fügt einen Eintrag in die Liste ein. (Ein Eintrag entspricht in der Tabelle einer Zeile
        /// </summary>
        /// <param name="entry"></param>
        public void addEntry(CTableEntry entry)
        {
            this.Add(entry);
        }

        /*********************************************************************/
        /// <summary>
        /// Größe der Liste (Anzahl der Elemente in der Liste)
        /// </summary>
        public int Size
        {

            get { return this.Count; }
        }

        /*********************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public CTableEntry this[string entryIndex]
        {
            get
            {
                foreach (CTableEntry entry in this)
                {
                    if (entry.Size > 0)
                    {
                        if (entry[0].EntryIndex == entryIndex)
                        {
                            return entry;
                        }
                    }
                }
                return null;
            }
        }

    } // class
} // namespace
