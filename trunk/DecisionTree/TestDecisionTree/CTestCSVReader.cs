using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using DecisionTree.Logic;
using DecisionTree.Storage.TableData;
using DecisionTree.Storage;

namespace TestDecisionTree
{
    [TestClass]
    public class CTestCSVReader
    {
        protected const string TEST_CSV_FILE_PATH = "\\..\\..\\ReaderTest.csv";

        [TestMethod]
        public void testReadFile()
        {

            string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string csvFile = exePath + TEST_CSV_FILE_PATH;

            IDBDataReader mDBAccess = new CDBDataReader();
            CCSVReader csvReader = new CCSVReader(mDBAccess);

            csvReader.insertFileDataToDatabase(csvFile);

            CTableEntryList entrys = mDBAccess.getAllEntries();

            Assert.IsTrue(entrys.Count == 8);

            Assert.IsTrue(entrys[0][0].TableValue == "1");
            Assert.IsTrue(entrys[0][1].TableValue == "2");
            Assert.IsTrue(entrys[0][3].TableValue == "4");

            Assert.IsTrue(entrys[4][0].TableValue == "5");

            Assert.IsTrue(entrys[7][0].TableValue == "8");
            Assert.IsTrue(entrys[7][3].TableValue == "11");
        }
    }
}
