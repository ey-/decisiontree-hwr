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
        protected CTreeLogic mTreeLogic = null;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CBusinessLogic()
        {
            mTreeLogic = new CTreeLogic(mTableLogic);
        }
        
        /*********************************************************************/
        /// <summary>
        /// holt die globale Instanz der BusinessLogic
        /// </summary>
        /// <returns>Interface zur BusinessLogic</returns>
        public static IBusinessLogic getInstance()
        {
            return mInstance ;
        }

        public void init()
        {
            mTreeLogic.setTableSetupChanged();
            mTreeLogic.setTreeForView(E_VIEW.E_TREE_INTERACTIVE_VIEW);
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

        /*********************************************************************/
        /// <summary>
        /// gibt ein Liste mit Datensätzen zurück die von dem übergebenen 
        /// Knoten repräsentiert werden.
        /// </summary>
        /// <param name="vertexToIdentify">Knoten der Identifiziert werden 
        /// soll</param>
        /// <returns>Liste mit Datensätzen des Knotens</returns>
        public CTableEntryList getFilterdTableData(CTreeVertex vertexToIdentify)
        {
            return mTableLogic.getFilteredTableData(vertexToIdentify);
        }

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum hinzufügen eines weiteren Attributes zur Tabelle
        /// </summary>
        public CAttributeType addAttribute(string attributeName)
        {
            mTreeLogic.setTableSetupChanged();
            return mTableLogic.addAttribute(attributeName);
        }

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum löschen des letzten Attributes zur Tabelle
        /// </summary>
        public bool removeAttribute(string attributeName)
        {
            mTreeLogic.setTableSetupChanged();
            return mTableLogic.removeAttribute(attributeName);
        }

        /*********************************************************************/
        /// <summary>
        /// Öffnet eine CSV-Datei und fügt den Inhalt in die Datenbank ein
        /// </summary>
        /// <param name="filePath">Pfad der CSV-Datei</param>
        public List<CAttributeType> openCSVFile(string filePath)
        {
            mTreeLogic.setTableSetupChanged();
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
            mTreeLogic.setTableSetupChanged();
            return mTableLogic.setTargetAttribute(targetAttributeType);
        }


        #endregion

        /*********************************************************************/
        #region Funktionen Identifikationsfenster
        /*********************************************************************/

        /*********************************************************************/
        /// <summary>
        /// Holt die Liste aller AttributTypen und gibt diese zurück. 
        /// Auch die nicht verwendeten!
        /// </summary>
        /// <returns>Liste mit allen Attributtypen</returns>
        public List<CAttributeType> getAttributeTypes()
        {
            return mTableLogic.getAttributeTypes();
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

        /*********************************************************************/
        /// <summary>
        /// Setzt für einen Vertex das Attribut welches dieser Repräsentiert
        /// </summary>
        /// <param name="vertex">Vertex dessen Attribut geändert werden soll</param>
        /// <param name="attributeType">neues Attribut des Vertex</param>
        public bool setVertexAttribute(CTreeVertex vertex, CAttributeType attributeType)
        {
            if (mTreeLogic.setVertexAttribute(vertex, attributeType) == true)
            {
                mTreeLogic.updateVertexValues();
                return true;
            }
            return false;
        }

        #endregion

        /*********************************************************************/
        /// <summary>
        /// Der Nutzer hat auf die angegebene Ansicht gewechselt
        /// </summary>
        /// <param name="selectedView">View auf die gewechselt wurde</param>
        public void changeView(E_VIEW selectedView)
        {
            switch (selectedView)
            { 
                case E_VIEW.E_TABLE_VIEW:
                    // wir machen nichts
                    break;
                case E_VIEW.E_TREE_INTERACTIVE_VIEW:
                case E_VIEW.E_TREE_AUTOMATIC_VIEW:
                    mTreeLogic.setTreeForView(selectedView);
                    mTreeLogic.updateVertexValues();
                    break;
            }
        }

        public void setMinObjectCountAutoTree(int number)
        {
            mTreeLogic.setMinObjectCountAutoTree(number);
        }

        public void test()
        { 
        }
    }// class
} // namespace
