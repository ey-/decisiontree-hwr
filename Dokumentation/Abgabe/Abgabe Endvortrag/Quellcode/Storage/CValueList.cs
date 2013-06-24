using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DecisionTree.Storage
{
    /// <summary>
    /// Liste mit Werten aus der Datenbank
    /// </summary>
    public class CValueList : IEnumerable
    {
        protected List<CAttributeValue> mAttributeList = new List<CAttributeValue>();

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CValueList()
        { }

        /*********************************************************************/
        /// <summary>
        /// fügt einen Attributwert in die Liste hinzu
        /// </summary>
        /// <param name="value">Wert der hinzugefügt werden soll</param>
        public void addValue(CAttributeValue value)
        {
            mAttributeList.Add(value);
        }

        /*********************************************************************/
        /// <summary>
        /// Größe der Liste (Anzahl der Elemente in der Liste)
        /// </summary>
        public int Size
        {
            get { return mAttributeList.Count; }
        }

        /*********************************************************************/
        /// <summary>
        /// Zugriff auf eine Element in der Liste per Index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Attributwert</returns>
        public CAttributeValue this[int index]
        {
            get 
            { 
                if (index < mAttributeList.Count)
                {
                    return mAttributeList[index]; 
                }
                return null;
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Methode zum Iterieren per foreach
        /// </summary>
        /// <returns>Attributwert</returns>
        public IEnumerator GetEnumerator()
        {
 	        foreach (CAttributeValue value in mAttributeList)
            {
                yield return value;
            }
        }

    } // class
}// namespace
