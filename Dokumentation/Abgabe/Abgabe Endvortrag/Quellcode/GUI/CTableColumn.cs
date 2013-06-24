using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage;
using System.Windows.Controls;
using System.Windows.Data;

namespace DecisionTree.GUI
{
    /// <summary>
    /// Erweitert die DatagridColumn und speichert den Attributtypen
    /// den eine Spalte repräsentiert
    /// </summary>
    public class CTableColumn : DataGridTextColumn
    {
        protected CAttributeType mAttributeType;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="attributyType">Typ der Spalte</param>
        public CTableColumn(CAttributeType attributyType)
        {
            mAttributeType = attributyType;

            Header = attributyType.Name;
            Binding = new Binding("[" + attributyType.Index + "].TableValue");
        }

        /// <summary>
        /// Datentyp den die Spalte repräsentiert
        /// </summary>
        public CAttributeType ColumnDataType
        {
            get { return mAttributeType; }
        }
    }
}
