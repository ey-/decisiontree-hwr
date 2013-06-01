using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage.TableData;
using System.Diagnostics;

namespace DecisionTree.Storage
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("Index: {EntryIndex} Attr: {AttributeType.msName} Value: {TableValue}")]
    public class CAttributeValue
    {
        CAttributeType mAttributeType;
        string msEntryIndex;

        string mStringValue;
        int mIntegerValue;
        float mFloatValue;

        IDBDataReader mDBAccess;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor um einen Attributwert zu speichern
        /// </summary>
        /// <param name="type">Beschreibung von welchem Typ die Varialbe ist</param>
        /// <param name="sEntryIndex">ID des Eintrags aus der DB um den Eintrag eindeutig identifizieren zu können</param>
        /// <param name="sValue">Wert im Stringformat</param>
        /// <param name="dbAccess">Interface für den Zugriff auf die Datenbank, damit Werte geändert werden können</param>
        public CAttributeValue(CAttributeType type, string sEntryIndex, string sValue, IDBDataReader dbAccess)
        {
            mDBAccess = dbAccess;

            mAttributeType = type;
            msEntryIndex = sEntryIndex;

            mStringValue = "";
            mIntegerValue = 0;
            mFloatValue = 0f;

            switch (mAttributeType.DataType)
            { 
                case E_DATATYPE.E_STRING:   mStringValue = sValue;                      break;
                case E_DATATYPE.E_INT:      mIntegerValue = (sValue != "") ? Convert.ToInt32(sValue) : 0; break;
                case E_DATATYPE.E_FLOAT:    mFloatValue = (sValue != "") ? Convert.ToSingle(sValue) : 0.0f; break;
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Informationen über den Typen des Attributwertes
        /// </summary>
        public CAttributeType AttributeType
        {
            get { return mAttributeType; }
        }

        /// <summary>
        /// Zugriff auf Wert der in der Tabelle dargestellt wird
        /// </summary>
        public string TableValue
        {
            get
            {
                switch (mAttributeType.DataType)
                {
                    case E_DATATYPE.E_STRING: return mStringValue;
                    case E_DATATYPE.E_INT: return mIntegerValue.ToString();
                    case E_DATATYPE.E_FLOAT: return mFloatValue.ToString();
                }
                return "";
            }
            set
            {
                // zuerst unten in der Datenbank ändern
                if (mDBAccess.updateAttributeValue(this, value) == true)
                {
                    // jetzt auch wirklich in den Daten ändern
                    switch (mAttributeType.DataType)
                    {
                        case E_DATATYPE.E_STRING: mStringValue = value; break;
                        case E_DATATYPE.E_INT: mIntegerValue = Convert.ToInt32(value); break;
                        case E_DATATYPE.E_FLOAT: mFloatValue = Convert.ToSingle(value); break;
                    }
                }
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Index des Eintrags zu dem der Wert gehört
        /// </summary>
        public string EntryIndex
        {
            get { return msEntryIndex; }
        }

        /*********************************************************************/
        /// <summary>
        /// Eingetragener Stringwert
        /// </summary>
        public string StringValue
        {
            get { return mStringValue; }
        }

        /*********************************************************************/
        /// <summary>
        /// Eingetragener Integerwert
        /// </summary>
        public int IntegerValue
        {
            get { return mIntegerValue; }
        }

        /*********************************************************************/
        /// <summary>
        /// Eingetragener Floatwert
        /// </summary>
        public float FloatValue
        {
            get { return mFloatValue; }
        }
        /*********************************************************************/
        /// <summary>
        /// Overrides ToString-Function
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            switch (mAttributeType.DataType)
            {
                case E_DATATYPE.E_STRING:
                    return mStringValue;
                case E_DATATYPE.E_FLOAT:
                    return mFloatValue.ToString();
                case E_DATATYPE.E_INT: 
                    return mIntegerValue.ToString();
            }
            return msEntryIndex;
        }
   
    }// class
} // namespace
