using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Storage.TableData
{
    /// <summary>
    /// Bestimmt AttributeDataType vom gegebenen Wert.
    /// </summary>
    public class CDataTypeChecker
    {
        
        /*********************************************************************/
        /// <summary>
        /// setzt DataType
        /// </summary>
        /// <param name="value">CAttributeValue dessen Typ überprüft werden soll</param>
        /// <param name="newValue">string der neue Wert</param>
        public void handleDataTypeofValue(CAttributeValue value, string newValue)
        {
            value.AttributeType.DataType = checkDataType(newValue);
        }

        /*********************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">String dessen Datentyp bestimmt werden soll.</param>
        /// <returns>E_DATATYPE der Datentyp des gegebenen Strings</returns>
        protected E_DATATYPE checkDataType(string value)
        {
            int intvalue;
            double doubleValue;


            if (int.TryParse(value, out intvalue))
            {
                return E_DATATYPE.E_INT;
            }
            if (double.TryParse(value, out doubleValue))
            {
                return E_DATATYPE.E_FLOAT;
            }
            return E_DATATYPE.E_STRING;
        }
    }
}
