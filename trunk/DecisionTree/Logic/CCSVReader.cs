using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage.TableData;
using System.IO;
using DecisionTree.Storage;

namespace DecisionTree.Logic
{
    /// <summary>
    /// Ließt die Daten einer CSV-Datei aus und fügt die Daten in die Tabelle ein
    /// </summary>
    public class CCSVReader
    {
        protected IDBDataReader mDBAccess;

        public const char CSV_SEPERATOR = ';';

        /*********************************************************************/
        /// <summary>
        /// Konstruktor 
        /// </summary>
        /// <param name="dbAccess">Interface für den Zugriff auf die Datenbank</param>
        public CCSVReader(IDBDataReader dbAccess)
        {
            mDBAccess = dbAccess;
        }

        /*********************************************************************/
        /// <summary>
        /// Ließt die CSV-Datei aus und füllt die Datenbank mit den Daten
        /// </summary>
        /// <param name="csvFilePath">Pfad zur auszulesenden CSV-Datei</param>
        public void insertFileDataToDatabase(string csvFilePath)
        {
            if (isCSVFileExistent(csvFilePath) == true)
            {
                FileStream csvFile = File.Open(csvFilePath, FileMode.Open);

                if (isValidCSVFile(csvFile) == true)
                {   
                    // Alte Daten aus der Datenbank entfernen
                    mDBAccess.clearDatabase();

                    // Daten aus der Datei lesen und in die Datenbank einfügen
                    readFile(csvFile);
                }
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Prüft ob die CSV-Datei vorhanden ist.
        /// </summary>
        /// <returns>Vorhandensein der Datei</returns>
        protected bool isCSVFileExistent(string csvFilePath)
        {
            if (csvFilePath.EndsWith(".csv") == true)
            {
                return File.Exists(csvFilePath);
            }
            return false;
        }

        /*********************************************************************/
        /// <summary>
        /// Prüft ob die CSV-Datei gültige Daten enthält
        /// </summary>
        /// <param name="csvFile">CSV-Datei die geprüft werden soll</param>
        /// <returns>Gültigkeit des Inhalts der CSV-Datei</returns>
        protected bool isValidCSVFile(FileStream csvFile)
        {
            StreamReader reader = new StreamReader(csvFile);
            int lineIndex = 0;
            string sLine = "";
            
            // bis zum Ende der Datei die Zeilen auslesen
            while (readNextLine(reader, ref sLine) == true)
            {
                // wir können nicht viel überprüfen.. aber die Folgenden Punkte sind sinnvoll
                // - Anzahl der Einträge in der ersten Spalte prüfen (max 16)
                // - Die Attributnamen dürfen nicht leer sein
                if (lineIndex == 0)
                {
                    string[] parts = sLine.Split(CSV_SEPERATOR);
                    if (parts.Length > CTableConstants.MAX_ATTRIBUTE_COUNT)
                    {
                        return false;
                    }

                    foreach (string attributeName in parts)
                    {
                        if (attributeName == "")
                        {
                            return false;
                        }
                    }
                }

                // es wurde eine Zeile auslesen, also den Zähler erhöhen
                lineIndex++;
            }

            
            return true;
        }

        /*********************************************************************/
        /// <summary>
        /// Ließt die Daten aus der Datei und schreibt sie in die Datenbank
        /// </summary>
        /// <param name="csvFile">CSV-Datei mit den Daten</param>
        protected void readFile(FileStream csvFile)
        {
            // damit die Datei von Vorne gelesenwerden kann, 
            // den Datenpointer auf den Anfang setzen
            csvFile.Position = 0;
            using (StreamReader reader = new StreamReader(csvFile))
            {
                // Spalten auslesen und einfügen
                prepareColumns(reader);

                // Datensätze auslesen und einfügen
                fillDatabase(reader);
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Bereitet die Spalten der Datenbank vor damit danach die Daten
        /// eingefügt werden können
        /// </summary>
        /// <param name="reader">Reader der zum lesen verwendet werden soll</param>
        protected void prepareColumns(StreamReader reader)
        {
            string sLine = "";
            if (readNextLine(reader, ref sLine) == true)
            {
                string[] attrbiuteNames = sLine.Split(CSV_SEPERATOR);

                foreach (string sAttribute in attrbiuteNames)
                {
                    mDBAccess.addColumn(sAttribute);
                }
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Füllt die Datenbank mit Werten aus der CSV-Datei
        /// </summary>
        /// <param name="reader">Reader zum Auslesen der Datensätze</param>
        protected void fillDatabase(StreamReader reader)
        {
            string sLine = "";
            while (readNextLine(reader, ref sLine) == true)
            {
                string[] attrbiuteNames = sLine.Split(CSV_SEPERATOR);
                CTableEntry entry = mDBAccess.insertEntry();

                // da wir gerade einen Leeren Eintrag erstellt haben und von einer leeren Datenbank
                // ausgegangen sind. Können wir die Werte genau den Attributen zuordnen.
                // Das ist schon ziemlich gehackt, aber es funktioniert. 
                for (int attributeIndex = 0; attributeIndex < attrbiuteNames.Length; attributeIndex++)
                {
                    // zur Sicherheit lesen wir nur die Maximalanzahl an Attributen aus
                    if (attributeIndex <= CTableConstants.MAX_ATTRIBUTE_COUNT)
                    {
                        entry[attributeIndex].TableValue = attrbiuteNames[attributeIndex];
                    }
                }
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Ließt die nächste Zeile vom Reader aus und gibt diese zurück
        /// </summary>
        /// <param name="reader">Reader zum Auslesen eines Datensätzes</param>
        /// <param name="sLine">Ausgelesene Zeile</param>
        /// <returns>Erfolg beim holen des nächsten Zeile</returns>
        protected bool readNextLine(StreamReader reader, ref string sLine)
        {
            // So lange auslesen bis wir das Ende erreicht 
            // oder eine Zeile mit Inhalt gefunden haben
            while (reader.EndOfStream == false)
            {
                sLine = reader.ReadLine();
                // die Zeile muss auch inhalt haben
                if (sLine.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
