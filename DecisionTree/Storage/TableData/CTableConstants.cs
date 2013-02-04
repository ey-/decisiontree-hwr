using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Storage.TableData
{
    public class CTableConstants
    {
        public const int MAX_ATTRIBUTE_COUNT = 16;
        public const int MAX_ENTRY_COUNT = 1000;

        public const string TABLE_ATTRIBUTES = "DataTable";
        //public const string TABLE_ATTRIBUTE_TYPES = "AttributeTypesTable";

        // Spaltennamen für die AttributTypesTabelle
        public const string ATTR_TYPES_NAME = "name";
        public const string ATTR_TYPES_TYPE = "type";
        public const string ATTR_TYPES_INTERNAL_NAME = "interalName";
        public const string ATTR_TYPES_USED = "used";
        public const string ATTR_TYPES_TARGET_ATTR = "targetAttribute";

        // Spaltennamen für die Datentabelle
        /// <summary>
        /// hinter den String die Zahl des Attributes schreiben mit 1 - 16
        /// </summary>
        public const string ATTR_X = "attribute";
    }
}
