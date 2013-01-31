using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class CAttributeType
    {
        protected string mName;
        protected E_DATATYPE mDataType;
        protected bool mbTargetAttribute;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Name des Attributes (In der Tabelle Spaltenüberschrift)</param>
        /// <param name="dataType">Datentyp des Attributes</param>
        /// <param name="bTargetAttribute">Zielattribut (ja, nein)</param>
        public CAttributeType(string name, E_DATATYPE dataType, bool bTargetAttribute)
        {
            mName = name;
            mbTargetAttribute = bTargetAttribute;
            mDataType = dataType;
        }

        /*********************************************************************/
        /// <summary>
        /// Name des Attributes
        /// </summary>
        public string Name 
        {
            get { return mName; }
        }

        /*********************************************************************/
        /// <summary>
        /// Datentyp des Attributes
        /// </summary>
        public E_DATATYPE DataType
        {
            get { return mDataType; }
        }

        /*********************************************************************/
        /// <summary>
        /// Name des Attributes
        /// </summary>
        public bool TargetAttribute
        {
            get { return mbTargetAttribute; }
        }

    }// class
}//namespace
