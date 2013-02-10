using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage.TableData;
using System.IO;
using System.Windows;
using DecisionTree.Storage;

namespace DecisionTree.Logic
{
    /// <summary>
    /// Schreibt die Daten der Datenbank in eine CSV-Datei
    /// </summary>
    public class CCSVWriter
    {
        IDBDataReader mDBAccess;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="dbAccess">Interface für den Zugriff auf die Datenbank</param>
        public CCSVWriter(IDBDataReader dbAccess)
        {
            mDBAccess = dbAccess;
        }

        /*********************************************************************/
        /// <summary>
        /// Speichert den Inhalt der Datenbank 
        /// </summary>
        /// <param name="csvFilePath">Pfad unter dem die Datei gespeichert werden soll</param>
        public void saveDatabaseToCSV(string csvFilePath)
        {
            // Testen ob Datei bereits vorhanden ist und ob wir darauf schreiben können
            if (isFileAvailable(csvFilePath) == true)
            {
                // Datei anlegen
                using (StreamWriter csvFile = File.CreateText(csvFilePath))
                {
                    // Datenbankdaten in Datei schreiben
                    writeDatabaseData(csvFile);
                }
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Prüft ob die Datei vorhanden ist und wenn ja ob diese überschrieben
        /// werden soll
        /// </summary>
        /// <param name="csvFilePath">Pfad zur CSV-Datei</param>
        /// <returns>Verfügbarkeit der angegbenen Datei</returns>
        private bool isFileAvailable(string csvFilePath)
        {
            if (csvFilePath.EndsWith(".csv") == true)
            {
                // sollte die Datei bereits existieren, fragen wir lieber
                // den User ob diese überschrieben werden soll
                if (File.Exists(csvFilePath) == true)
                {
                    MessageBoxResult result = MessageBox.Show("Die Datei existiert bereits. Soll existierende Datei überschrieben werden?", 
                        "Datei existiert bereits", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (result == MessageBoxResult.No)
                    {
                        // Nutzer will die Datei nicht überschreiben .. also hier abbrechen
                        return false;
                    }
                }

                // alles ok
                return true;
            }

            return false;
        }

        /*********************************************************************/
        /// <summary>
        /// Schreibt den Inhalt der Datenbank in die übergebene CSV-Datei
        /// </summary>
        /// <param name="csvFile">Stream zum Schreiben in die CSV-Datei</param>
        private void writeDatabaseData(StreamWriter csvFile)
        {
            CTableEntryList entryList = mDBAccess.getAllEntries();

            if (entryList.Count > 0)
            {
                // zuerst die Namen der Spalten einfügen. Dazu vom einem Element
                string sLine = getAttributeNameLine(entryList);
                csvFile.WriteLine(sLine);

                foreach (CTableEntry entry in entryList)
                {
                    csvFile.WriteLine(getCSVLineFromEntry(entry));
                }
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Baut die Zeile mit den Attributnamen zusammen, damit diese in die 
        /// CSV-Datei eingefügt werden kann.
        /// </summary>
        /// <param name="entryList">Liste mit allen Einträgen</param>
        /// <returns>fertige Zeile zum einfügen in die CSV-Datei</returns>
        private string getAttributeNameLine(CTableEntryList entryList)
        {
            string sLine = "";
            CTableEntry firstEntry = entryList[0];            

            bool bSkipSeperator = true;
            foreach (CAttributeValue value in firstEntry)
            {
                // wir tragen nur Attribute ein die benutzt werden
                if (value.AttributeType.Used == true)
                {
                    if (bSkipSeperator == false)
                    {
                        sLine += CCSVReader.CSV_SEPERATOR;
                    }
                    bSkipSeperator = false;

                    // den Namen eintragen
                    sLine += value.AttributeType.Name;
                }
            }
            return sLine;
        }

        /*********************************************************************/
        /// <summary>
        /// Baut den CSV-String für einen Tabellen-Eintrag zusammen
        /// </summary>
        /// <param name="entry">Entry für den die Zeile zusammengestellt werden soll</param>
        /// <returns>fertige Zeile</returns>
        protected string getCSVLineFromEntry(CTableEntry entry)
        {
            string sLine = "";
            bool bSkipSeperator = true;

            foreach (CAttributeValue value in entry)
            {
                // wir tragen nur Attribute ein die benutzt werden
                if (value.AttributeType.Used == true)
                {
                    // beim ersten mal kein ';' vor den Eintrag schreiben
                    if (bSkipSeperator == false)
                    {
                        sLine += CCSVReader.CSV_SEPERATOR;
                    }
                    bSkipSeperator = false;

                    sLine += value.TableValue;
                }
            }

            return sLine;
        }

    } // class
} // Namespace
