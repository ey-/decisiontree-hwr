using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DecisionTree.Storage
{
    /// <summary>
    /// Liste Datenbankeinträgen.
    /// Es werden nur Lesezugeriffe zugelassen. Schreiben in die Datenbank
    /// </summary>
    public class CTableEntryList : IEnumerable
    {
        List<CTableEntry> mEntryList = new List<CTableEntry>();

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
            mEntryList.Add(entry);
        }

        /*********************************************************************/
        /// <summary>
        /// Größe der Liste (Anzahl der Elemente in der Liste)
        /// </summary>
        public int Size
        {
            get { return mEntryList.Count; }
        }

        /*********************************************************************/
        /// <summary>
        /// Zugriff auf einen Tabelleneintrag in der Liste per Index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Tabelleneintrag</returns>
        public CTableEntry this[int index]
        {
            get
            {
                if (index < mEntryList.Count)
                {
                    return mEntryList[index];
                }
                return null;
            }
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
                foreach (CTableEntry entry in mEntryList)
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

        /*********************************************************************/
        /// <summary>
        /// Methode zum Iterieren per foreach
        /// </summary>
        /// <returns>Tabelleneintrag</returns>
        public IEnumerator GetEnumerator()
        {
            foreach (CTableEntry value in mEntryList)
            {
                yield return value;
            }
        }
    }
}
