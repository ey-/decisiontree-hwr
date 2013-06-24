using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DecisionTree.Storage
{
    /// <summary>
    /// Tabelleneintrag mit allen seinen Attributen.
    /// </summary>
    public class CTableEntry : CValueList
    {
        string msEntryIndex;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="entryIndex">Tabellen Index des Tabelleneintrags</param>
        public CTableEntry(string entryIndex)
        {
            msEntryIndex = entryIndex;
        }

        /*********************************************************************/
        /// <summary>
        /// Tabellen Index des Tabelleneintrags
        /// </summary>
        public string Index
        {
            get { return msEntryIndex; }
        }

        /*
        public CAttributeValue Attribute0
        {
            get { return mAttributeList[0]; }
        }

        public CAttributeValue Attribute1
        {
            get { return mAttributeList[1]; }
        }

        public CAttributeValue Attribute2
        {
            get { return mAttributeList[2]; }
        }

        public CAttributeValue Attribute3
        {
            get { return mAttributeList[3]; }
        }

        public CAttributeValue Attribute4
        {
            get { return mAttributeList[4]; }
        }

        public CAttributeValue Attribute5
        {
            get { return mAttributeList[5]; }
        }

        public CAttributeValue Attribute6
        {
            get { return mAttributeList[6]; }
        }

        public CAttributeValue Attribute7
        {
            get { return mAttributeList[7]; }
        }

        public CAttributeValue Attribute8
        {
            get { return mAttributeList[8]; }
        }

        public CAttributeValue Attribute9
        {
            get { return mAttributeList[9]; }
        }

        public CAttributeValue Attribute10
        {
            get { return mAttributeList[10]; }
        }*/
    }
}
