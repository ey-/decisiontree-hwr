using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.GUI;
using DecisionTree.Storage.TableData;
using DecisionTree.Storage;

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
        
        //TableLogic
        protected CTableLogic mTableLogic = new CTableLogic();

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
        public void openCSVFile(string filePath)
        {
            mTableLogic.openCSVFile(filePath);
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
    }// class
} // namespace
