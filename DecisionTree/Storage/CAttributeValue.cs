using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Storage
{
    /// <summary>
    /// 
    /// </summary>
    public class CAttributeValue
    {
        CAttributeType mAttributeType;
        string msEntryIndex;

        string mStringValue;
        int mIntegerValue;
        float mFloatValue;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor um einen Stringwert zu speichern
        /// </summary>
        /// <param name="type">Beschreibung von welchem Typ die Varialbe ist</param>
        /// <param name="sEntryIndex">ID des Eintrags aus der DB um den Eintrag eindeutig identifizieren zu können</param>
        /// <param name="sValue">Wert im Stringformat</param>
        public CAttributeValue(CAttributeType type, string sEntryIndex, string sValue)
        {
            mAttributeType = type;
            msEntryIndex = sEntryIndex;

            mStringValue = "";
            mIntegerValue = 0;
            mFloatValue = 0f;

            switch (mAttributeType.DataType)
            { 
                case E_DATATYPE.E_STRING:   mStringValue = sValue;                      break;
                case E_DATATYPE.E_INT:      mIntegerValue = Convert.ToInt32(sValue);    break;
                case E_DATATYPE.E_FLOAT:    mFloatValue = Convert.ToSingle(sValue);     break;
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
            // ToDo : Änderung auch in die Datenbank schreiben 
            set
            {
                switch (mAttributeType.DataType)
                {
                    case E_DATATYPE.E_STRING: mStringValue = value; break;
                    case E_DATATYPE.E_INT: mIntegerValue = Convert.ToInt32(value); break;
                    case E_DATATYPE.E_FLOAT: mFloatValue = Convert.ToSingle(value); break;
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
