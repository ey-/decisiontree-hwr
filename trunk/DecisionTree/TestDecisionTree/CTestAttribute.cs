using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DecisionTree.Storage;

namespace TestDecisionTree
{
    [TestClass]
    public class CTestAttribute
    {
        [TestMethod]
        public void testTypes()
        {
            CAttributeType typeNumber = new CAttributeType("Number", E_DATATYPE.E_INT, true );

            Assert.IsTrue(typeNumber.DataType == E_DATATYPE.E_INT);
            Assert.IsTrue(typeNumber.Name == "Number");
            Assert.IsTrue(typeNumber.TargetAttribute == true);
        }

        [TestMethod]
        public void testAttributeGeneration()
        {
            CAttributeType typeNumber = new CAttributeType("Number", E_DATATYPE.E_INT, false);
            CAttributeType typeString = new CAttributeType("String", E_DATATYPE.E_STRING, false);
            CAttributeType typeFloat = new CAttributeType("Float", E_DATATYPE.E_FLOAT, false);
            
            // Attributwerte mit unterschiedlichen Typen erstellen
            CAttributeValue[] values = new CAttributeValue[10];
            for (int i = 0; i < 10; i++)
            {
                switch (i%2)
                {
                    case 0:
                        values[i] = new CAttributeValue(typeString, i.ToString(), i.ToString());
                        break;
                    case 1:
                        values[i] = new CAttributeValue(typeNumber, i.ToString(), i.ToString());
                        break;
                    case 2:
                        values[i] = new CAttributeValue(typeFloat, i.ToString(), i.ToString());
                        break;
                }
            }

            // testen ob die Werte korrekt eingetragen wurden
            for (int i = 0; i < 10; i++)
            {
                switch (values[i].AttributeType.DataType)
                { 
                    case E_DATATYPE.E_STRING:
                        Assert.IsTrue(values[i].StringValue == i.ToString());
                        break;
                    case E_DATATYPE.E_INT:
                        Assert.IsTrue(values[i].IntegerValue == i);
                        break;
                    case E_DATATYPE.E_FLOAT:
                        Assert.IsTrue(values[i].FloatValue == i);
                        break;
                }
            }
        }

        [TestMethod]
        public void testValuesList()
        {
            CValueList valueList = new CValueList();

            CAttributeType typeNumber = new CAttributeType("Number", E_DATATYPE.E_INT, false);
            CAttributeType typeString = new CAttributeType("String", E_DATATYPE.E_STRING, false);
            CAttributeType typeFloat = new CAttributeType("Float", E_DATATYPE.E_FLOAT, false);

            // Attributwerte mit unterschiedlichen Typen erstellen und in die Liste einfügen
            for (int i = 0; i < 10; i++)
            {
                switch (i % 2)
                {
                    case 0:
                        valueList.addValue(new CAttributeValue(typeString, i.ToString(), i.ToString()));
                        break;
                    case 1:
                        valueList.addValue(new CAttributeValue(typeNumber, i.ToString(), i.ToString()));
                        break;
                    case 2:
                        valueList.addValue(new CAttributeValue(typeFloat, i.ToString(), i.ToString()));
                        break;
                }
            }

            Assert.IsTrue(valueList.Size == 10);

            // Zugriff per foreach auf Elemente der Liste
            foreach (CAttributeValue value in valueList)
            {
                value.ToString();
            }

            // Zugriff per Index auf ein Element
            valueList[0].ToString();
        }

    } // class
} // namespace
