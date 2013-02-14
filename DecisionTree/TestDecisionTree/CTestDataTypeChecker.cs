using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DecisionTree.Storage;
using DecisionTree.Storage.TableData;

namespace TestDecisionTree
{
    [TestClass]
    public class CTestDataTypeChecker
    {
        [TestMethod]
        public void TesthandleDataTypeofValue()
        {
            CAttributeType typeNumber = new CAttributeType(0);
            typeNumber.setUsed("Number", E_DATATYPE.E_INT, true);

            CAttributeValue myValue = new CAttributeValue(typeNumber, "1", "1", null);
            CDataTypeChecker checker = new CDataTypeChecker();

            checker.handleDataTypeofValue(myValue, "1.3");
            Assert.IsTrue(myValue.AttributeType.DataType == E_DATATYPE.E_FLOAT, "float hinzufügen");
            checker.handleDataTypeofValue(myValue, "2");
            Assert.IsTrue(myValue.AttributeType.DataType == E_DATATYPE.E_INT, "int hinzufügen");
            checker.handleDataTypeofValue(myValue, "wer");
            Assert.IsTrue(myValue.AttributeType.DataType == E_DATATYPE.E_STRING, "string hinzufügen");
        }
    }
}
