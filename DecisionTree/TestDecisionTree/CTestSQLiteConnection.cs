using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DecisionTree;
using DecisionTree.Storage.TableData;
using System.Data.SQLite;


namespace TestDecisionTree
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für UnitTest1
    /// </summary>
    [TestClass]
    public class CTestSQLiteConnection
    {
        private const string TABLE_TEST = "test";

        public CTestSQLiteConnection()
        {
        }


        [TestMethod]
        public void construktorTest()
        {
            CSQLiteConnection connection = new CSQLiteConnection();
        }


        [TestMethod]
        public void executionText()
        {
            CSQLiteConnection connection = new CSQLiteConnection();

            string sqlCommand = "DROP TABLE IF EXISTS " + TABLE_TEST;
            Assert.IsTrue(connection.sqlExecuteStatement(sqlCommand));

            sqlCommand = "CREATE TABLE " + TABLE_TEST + " (id INTEGER PRIMARY KEY, testAttr TEXT)";
            Assert.IsTrue(connection.sqlExecuteStatement(sqlCommand));

            sqlCommand = "INSERT INTO " + TABLE_TEST + " (id, testAttr) VALUES (1, 'asd'), (2, 'qwe'), (3, 'yxc')";
            Assert.IsTrue(connection.sqlExecuteStatement(sqlCommand));

            SQLiteDataReader reader;
            sqlCommand = "SELECT * FROM " + TABLE_TEST;
            Assert.IsTrue(connection.sqlRequestStatement(sqlCommand, out reader));

            int count = 0;
            while (reader.Read() == true)
            {
                count++;
            }
        }
    }
}
