using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DecisionTree.Storage.TableData;
using System.Diagnostics;

namespace DecisionTree.Storage
{
    /// <summary>
    /// Datentyp den ein Attribut annehmen kann
    /// </summary>
    public enum E_DATATYPE
    { 
        E_STRING,
        E_INT,
        E_FLOAT
    }

    /// <summary>
    /// Diese Klasse beschreibt einen Attributtypen.
    /// </summary>
    [DebuggerDisplay("{Name} Used: {Used} TargetAttribut: {TargetAttribute}")]
    public class CAttributeType
    {
        protected string msName;

        protected int mInternalIndex;

        protected E_DATATYPE mDataType;

        protected bool mbTargetAttribute;
        protected bool mbUsed;
        

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Name des Attributes (In der Tabelle Spaltenüberschrift)</param>
        /// <param name="dataType">Datentyp des Attributes</param>
        /// <param name="bTargetAttribute">Zielattribut (ja, nein)</param>
        public CAttributeType(int internalIndex)
        {
            mInternalIndex = internalIndex;

            setUnused();
        }

        /*********************************************************************/
        /// <summary>
        /// Setzt den Typen als unbenutzt und setzt alle Daten zurück
        /// </summary>
        public void setUnused()
        {
            msName = "";
            mDataType = E_DATATYPE.E_STRING;
            mbTargetAttribute = false;
            mbUsed = false;
        }

        /*********************************************************************/
        /// <summary>
        /// Setzt den Typen als benutzt und leg die 
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="dataType"></param>
        /// <param name="bTargetAttribute"></param>
        public void setUsed(string sName, E_DATATYPE dataType, bool bTargetAttribute)
        {
            msName = sName;
            mDataType = dataType;
            mbTargetAttribute = bTargetAttribute;

            mbUsed = true;
        }

        /*********************************************************************/
        /// <summary>
        /// Name des Attributes
        /// </summary>
        public string Name 
        {
            get { return msName; }
            set { msName = value; }
        }

        /*********************************************************************/
        /// <summary>
        /// Datentyp des Attributes
        /// </summary>
        public E_DATATYPE DataType
        {
            get { return mDataType; }
            set { mDataType = value; }
        }

        /*********************************************************************/
        /// <summary>
        /// Name des Attributes
        /// </summary>
        public bool TargetAttribute
        {
            get { return mbTargetAttribute; }
            set { mbTargetAttribute = value; }
        }

        /*********************************************************************/
        /// <summary>
        /// gibt an ob dieses Attribut benutzt wird oder nicht
        /// </summary>
        public bool Used
        {
            get { return mbUsed; }
        }

        public int Index
        {
            get { return mInternalIndex; }
        }

        /*********************************************************************/
        /// <summary>
        /// Spaltenname in der Datenbank
        /// </summary>
        public string InternalName
        {
            get { return CTableConstants.ATTR_X + mInternalIndex.ToString(); }
        }

        /*********************************************************************
        /// <summary>
        /// Vergleicht ob die beiden Datentypen gleich sind.
        /// </summary>
        /// <param name="type1">erster Typ der verglichen wird</param>
        /// <param name="type2">zweiter Typ der verglichen wird</param>
        /// <returns>Gleichheit der AttributTypen</returns>
        public static bool operator ==(CAttributeType type1, CAttributeType type2)
        {
            if (type1.DataType != type2.DataType) return false;
            if (type1.Name != type2.Name) return false;
            if (type1.TargetAttribute != type2.TargetAttribute) return false;

            return true;
        }

        /*********************************************************************
        /// <summary>
        /// Vergleicht ob die beiden Datentypen ungleich sind
        /// </summary>
        /// <param name="type1">erster Typ der verglichen wird</param>
        /// <param name="type2">zweiter Typ der verglichen wird</param>
        /// <returns>Gleichheit der AttributTypen</returns>
        public static bool operator !=(CAttributeType type1, CAttributeType type2)
        {
            if (type1.DataType == type2.DataType) return false;
            if (type1.Name == type2.Name) return false;
            if (type1.TargetAttribute == type2.TargetAttribute) return false;

            return true;
        }
        */

    }// class
}//namespace
