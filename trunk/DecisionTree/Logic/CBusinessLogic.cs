using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.GUI;
using DecisionTree.Storage.TableData;

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

        // Schnittstellen zur Datenhaltung
        protected IDBDataReader mTableReader = new CDBDataReader();

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

        
    }// class
} // namespace
