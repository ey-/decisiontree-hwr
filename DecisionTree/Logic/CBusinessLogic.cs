using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.GUI;
using DecisionTree.Storage.TableData;
using DecisionTree.Storage;
using DecisionTree.Storage.TreeData;

namespace DecisionTree.Logic
{
    /// <summary>
    /// Implementierung der BusinessLogic.
    /// Ist als Singletonklasse designed.
    /// </summary>
    public class CBusinessLogic : IBusinessLogic
    {
        protected static IBusinessLogic mInstance = new CBusinessLogic();
        
        // Interfaces zur GUI
        protected IMainWindow mMainWindow = null;
        
        // Logiken für die verschiedenen Ansichten
        protected CTableLogic mTableLogic = new CTableLogic();
        protected CTreeLogic mTreeLogic = new CTreeLogic();

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CBusinessLogic()
        { }
        
        /*********************************************************************/
        /// <summary>
        /// holt die globale Instanz der BusinessLogic
        /// </summary>
        /// <returns>Interface zur BusinessLogic</returns>
        public static IBusinessLogic getInstance()
        {
            return mInstance ;
        }

        /*********************************************************************/
        /// <summary>
        /// Registiert das MainWindow Interface damit der Zugriff auf die Fenster erfolgen kann.
        /// </summary>
        /// <param name="mainWindow">Interface zum MainWindow</param>
        public void registerWindow(GUI.IMainWindow mainWindow)
        {
            mMainWindow = mainWindow;
        }

        /*********************************************************************/
        #region Tabellenfunktionen
        /*********************************************************************/

        /*********************************************************************/
        /// <summary>
        /// Gibt alle Tabellendaten zurück
        /// </summary>
        /// <returns>CTableEntryList</returns>
        public CTableEntryList getAllTableData()
        {
            return mTableLogic.getAllTableData();
        }

        public void loadCSV()
        {

        }

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum hinzufügen eines weiteren Attributes zur Tabelle
        /// </summary>
        public CAttributeType addAttribute()
        {
            return mTableLogic.addAttribute();
        }

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum löschen des letzten Attributes zur Tabelle
        /// </summary>
        public bool removeAttribute(string attributeName)
        {
            return mTableLogic.removeAttribute(attributeName);
        }

        /*********************************************************************/
        /// <summary>
        /// Öffnet eine CSV-Datei und fügt den Inhalt in die Datenbank ein
        /// </summary>
        /// <param name="filePath">Pfad der CSV-Datei</param>
        public List<CAttributeType> openCSVFile(string filePath)
        {
            return mTableLogic.openCSVFile(filePath);
        }

        /*********************************************************************/
        /// <summary>
        /// Speichert eine CSV-Datei und fügt den Inhalt der Datenbank ein
        /// </summary>
        /// <param name="filePath">Pfad zum Speicherort</param>
        public void saveCSVFile(string filePath)
        {
            mTableLogic.saveCSVFile(filePath);
        }

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum hinzufügen einer neuen Zeile/Row zur Tabelle
        /// </summary>
        /// <returns>neu Eingefügter Eintrag</returns>
        public CTableEntry addDataset()
        {
            return mTableLogic.addRow();
        }

        /*********************************************************************/
        /// <summary>
        /// Löscht einen Datensatz aus der Datenbank
        /// </summary>
        /// <param name="entry">Zu Löschender Datensatz</param>
        /// <returns>Erfolg des Löschens</returns>
        public bool removeDataset(CTableEntry entry)
        {
            return mTableLogic.removeDataset(entry);
        }

        /*********************************************************************/
        /// <summary>
        /// Setzt das Zielattribut auf den übergebenen Typen
        /// </summary>
        /// <param name="cAttributeType">Attributtyp der zum Zielattribut werden soll</param>
        /// <returns>Erfolg des Setztens</returns>
        public bool setTargetAttribute(CAttributeType targetAttributeType)
        {
            return mTableLogic.setTargetAttribute(targetAttributeType);
        }


        #endregion

        /*********************************************************************/
        #region Graphenfunktionen
        /*********************************************************************/

        /*********************************************************************/
        /// <summary>
        /// Holt den aktuell aktiven Graphen aus der Datenschicht
        /// </summary>
        /// <returns></returns>
        public CTreeGraph getGraph()
        {
            return mTreeLogic.getGraph();
        }

        #endregion
    }// class
} // namespace
