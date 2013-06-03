﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage.TableData;
using DecisionTree.Storage;
using DecisionTree.Storage.TreeData;

namespace DecisionTree.Logic
{
    /// <summary>
    /// Enthält die gesamte Logik um die Daten der Tabelle zu steuern
    /// </summary>
    public class CTableLogic
    {
        protected IDBDataReader mDBAccess = new CDBDataReader();

        // Testweise .. Der name der Spalte muss später von der GUI kommen
        int NUM_COLUMNS = 0;

        /*********************************************************************/
        /// <summary>
        /// Gibt alle Tabellendaten aus der Datenbank zurück.
        /// </summary>
        /// <returns>CTableEntryList</returns>
        public CTableEntryList getAllTableData()
        {
            return mDBAccess.getAllEntries();
        }

        /*********************************************************************/
        /// <summary>
        /// gibt ein Liste mit Datensätzen zurück die von dem übergebenen 
        /// Knoten repräsentiert werden.
        /// </summary>
        /// <param name="vertexToIdentify">Knoten der Identifiziert werden 
        /// soll</param>
        /// <returns>Liste mit Datensätzen des Knotens</returns>
        internal CTableEntryList getFilteredTableData(CTreeVertex vertexToIdentify)
        {
            return mDBAccess.getFilteredTableData(vertexToIdentify);
        }

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum hinzufügen eines weiteren Attributes zur Tabelle
        /// </summary>
        public CAttributeType addAttribute(string attributeName)
        {
            NUM_COLUMNS++;
            return mDBAccess.addColumn(attributeName);
        }

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum löschen des letzten Attributes zur Tabelle
        /// </summary>
        public bool removeAttribute(string attributeName)
        {
            return mDBAccess.removeColumn(attributeName);

            // Testweise .. Der name der Spalte muss später von der GUI kommen
            //mTableReader.addColumn("Column" + NUM_COLUMNS.ToString());
            //NUM_COLUMNS--;
        }

        /*********************************************************************/
        /// <summary>
        /// Öffnet eine CSV-Datei und fügt den Inhalt in die Datenbank ein
        /// </summary>
        /// <param name="filePath">Pfad der CSV-Datei</param>
        public List<CAttributeType> openCSVFile(string filePath)
        {
            CCSVReader csvReader = new CCSVReader(mDBAccess);
            return csvReader.insertFileDataToDatabase(filePath);
        }

        /*********************************************************************/
        /// <summary>
        /// Speichert eine CSV-Datei und fügt den Inhalt der Datenbank ein
        /// </summary>
        /// <param name="filePath">Pfad zum Speicherort</param>
        public void saveCSVFile(string filePath)
        {
            CCSVWriter csvWriter = new CCSVWriter(mDBAccess);
            csvWriter.saveDatabaseToCSV(filePath);
        }

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum hinzufügen einer neuen Zeile/Row zur Tabelle
        /// </summary>
        /// <returns>neu Eingefügter Eintrag</returns>
        public CTableEntry addRow()
        {
            return mDBAccess.insertEntry();
        }

        /*********************************************************************/
        /// <summary>
        /// Löscht einen Datensatz aus der Datenbank
        /// </summary>
        /// <param name="entry">Zu Löschender Datensatz</param>
        /// <returns>Erfolg des Löschens</returns>
        public bool removeDataset(CTableEntry entry)
        {
            return mDBAccess.removeEntry(entry);
        }

        /*********************************************************************/
        /// <summary>
        /// Setzt das Zielattribut auf den übergebenen Typen
        /// </summary>
        /// <param name="targetAttributeType">Attributtyp der zum Zielattribut 
        /// werden soll</param>
        /// <returns>Erfolg des Setztens</returns>
        public bool setTargetAttribute(CAttributeType targetAttributeType)
        {
            return mDBAccess.setTargetAttribute(targetAttributeType);
        }




        internal List<CAttributeType> getAttributeTypes()
        {
            return mDBAccess.getAttributeTypes();
        }
    }
}