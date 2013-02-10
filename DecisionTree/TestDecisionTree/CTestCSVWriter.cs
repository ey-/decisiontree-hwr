using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DecisionTree.Storage.TableData;
using DecisionTree.Logic;
using DecisionTree.Storage;
using System.IO;

namespace TestDecisionTree
{
    [TestClass]
    public class CTestCSVWriter
    {
        protected const string TEST_CSV_FILE_PATH = "\\..\\..\\WriterTest.csv";

        [TestMethod]
        public void testWriteFile()
        {
            IDBDataReader dbAccess = new CDBDataReader();

            dbAccess.clearDatabase();
            dbAccess.addColumn("Col1");
            dbAccess.addColumn("Col2");

            CTableEntry entry = dbAccess.insertEntry();
            entry[0].TableValue = "entry1";
            entry[1].TableValue = "0.01";

            entry = dbAccess.insertEntry();
            entry[0].TableValue = "entry2";
            entry[1].TableValue = "0.02";

            entry = dbAccess.insertEntry();
            entry[0].TableValue = "entry3";
            entry[1].TableValue = "0.03";

            CCSVWriter csvWriter = new CCSVWriter(dbAccess);

            string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string csvFile = exePath + TEST_CSV_FILE_PATH;
            File.Delete(csvFile);
            csvWriter.saveDatabaseToCSV(csvFile);

            // zum prüfen braucen wir den Reader
            CCSVReader csvReader = new CCSVReader(dbAccess);
            csvReader.insertFileDataToDatabase(csvFile);

            CTableEntryList entrys = dbAccess.getAllEntries();

            Assert.IsTrue(entrys.Count == 3);

            Assert.IsTrue(entrys[0][0].TableValue == "entry1");
            Assert.IsTrue(entrys[0][1].TableValue == "0.01");
            
            Assert.IsTrue(entrys[1][0].TableValue == "entry2");
            Assert.IsTrue(entrys[1][1].TableValue == "0.02");

            Assert.IsTrue(entrys[2][0].TableValue == "entry3");
            Assert.IsTrue(entrys[2][1].TableValue == "0.03");
        }
    }
}
