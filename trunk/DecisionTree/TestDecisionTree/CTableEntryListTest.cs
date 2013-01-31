using DecisionTree.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestDecisionTree
{
    
    
    /// <summary>
    ///Dies ist eine Testklasse für "CTableEntryListTest" und soll
    ///alle CTableEntryListTest Komponententests enthalten.
    ///</summary>
    [TestClass()]
    public class CTableEntryListTest
    {
        /// <summary>
        ///Ein Test für "CTableEntryList-Konstruktor"
        ///</summary>
        [TestMethod()]
        public void CTableEntryListConstructorTest()
        {
            CTableEntryList target = new CTableEntryList();
            Assert.IsTrue(target.Size == 0);
        }

        /// <summary>
        ///Ein Test für "addEntry"
        ///</summary>
        [TestMethod()]
        public void addEntryTest()
        {
            CTableEntryList list = new CTableEntryList();

            for (int i = 0; i < 10; i++)
            {
                CTableEntry entry = new CTableEntry();
                list.addEntry(entry);
            }

            Assert.IsTrue(list.Size == 10);
        }

        /// <summary>
        ///Ein Test für "Item"
        ///</summary>
        [TestMethod()]
        public void ItemTestString()
        {
            CTableEntryList list = new CTableEntryList();
            CAttributeType attrType = new CAttributeType("asd", E_DATATYPE.E_STRING, false);
            
            for (int i = 0; i < 10; i++)
            {
                CTableEntry entry = new CTableEntry();
                entry.addValue(new CAttributeValue(attrType, i.ToString(), i.ToString()));
                list.addEntry(entry);
            }

            CTableEntry singleEntry;
            singleEntry = list["5"];

            Assert.IsTrue(singleEntry.Size == 1);
            Assert.IsTrue(singleEntry[0].EntryIndex == "5");
            Assert.IsTrue(singleEntry[0].StringValue == "5");
        }

        /// <summary>
        ///Ein Test für "Item"
        ///</summary>
        [TestMethod()]
        public void ItemTestIndex()
        {
            CTableEntryList list = new CTableEntryList();
            CAttributeType attrType = new CAttributeType("asd", E_DATATYPE.E_STRING, false);
            
            for (int i = 0; i < 10; i++)
            {
                CTableEntry entry = new CTableEntry();
                entry.addValue(new CAttributeValue(attrType, i.ToString(), i.ToString()));
                list.addEntry(entry);
            }

            CTableEntry singleEntry;
            singleEntry = list[5];

            Assert.IsTrue(singleEntry.Size == 1);
            Assert.IsTrue(singleEntry[0].EntryIndex == "5");
            Assert.IsTrue(singleEntry[0].StringValue == "5");
        }

        /// <summary>
        ///Ein Test für "Size"
        ///</summary>
        [TestMethod()]
        public void SizeTest()
        {
            CTableEntryList list = new CTableEntryList();

            for (int i = 1; i <= 10; i++)
            {
                list.addEntry(new CTableEntry());
                Assert.IsTrue(list.Size == i);
            }
        }
    }
}
