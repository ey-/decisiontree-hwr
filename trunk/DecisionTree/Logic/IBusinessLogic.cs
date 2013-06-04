using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.GUI;
using System.Collections.ObjectModel;
using DecisionTree.Storage;
using DecisionTree.Storage.TableData;
using DecisionTree.Storage.TreeData;

namespace DecisionTree.Logic
{
    /*************************************************************************/
    /// <summary>
    /// Schnittstelle für Zugriff auf die Logikebene. Alle Methoden die 
    /// benötigt werden, werden zuerst hier eingefügt und dann in der 
    /// implementierenden Klasse umgesetzt.
    /// </summary>
    public interface IBusinessLogic
    {
        
        /*********************************************************************/
        /// <summary>
        /// Registiert das MainWindow Interface damit der Zugriff auf die Fenster erfolgen kann.
        /// </summary>
        /// <param name="mainWindow">Interface zum MainWindow</param>
        void registerWindow(IMainWindow mainWindow);

        void init();

        #region Tabellenfunktionen
        /*********************************************************************/
        /// <summary>
        /// gibt die Liste mit allen Datensätzen zurück
        /// </summary>
        /// <returns>Liste mit allen Datensätzen</returns>
        CTableEntryList getAllTableData();

        /*********************************************************************/
        /// <summary>
        /// gibt ein Liste mit Datensätzen zurück die von dem übergebenen 
        /// Knoten repräsentiert werden.
        /// </summary>
        /// <param name="vertexToIdentify">Knoten der Identifiziert werden 
        /// soll</param>
        /// <returns>Liste mit Datensätzen des Knotens</returns>
        CTableEntryList getFilterdTableData(CTreeVertex vertexToIdentify);

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum hinzufügen eines weiteren Attributes zur Tabelle
        /// </summary>
        CAttributeType addAttribute(string attributeName);

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum löschen des letzten Attributes zur Tabelle
        /// </summary>
        /// <param name="attributeName">Name des Attributes welches gelöscht  
        /// werden soll</param>
        /// <returns>Erfolg des Löschens</returns>
        bool removeAttribute(string attributeName);

        /*********************************************************************/
        /// <summary>
        /// Öffnet eine CSV-Datei und fügt den Inhalt in die Datenbank ein
        /// </summary>
        /// <param name="filePath">Pfad der CSV-Datei</param>
        List<CAttributeType> openCSVFile(string filePath);

        /*********************************************************************/
        /// <summary>
        /// Speichert eine CSV-Datei und fügt den Inhalt der Datenbank ein
        /// </summary>
        /// <param name="filePath">Pfad zum Speicherort</param>
        void saveCSVFile(string filePath);

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum hinzufügen einer neuen Zeile/Row zur Tabelle
        /// </summary>
        /// <returns>neu Eingefügter Eintrag</returns>
        CTableEntry addDataset();

        /*********************************************************************/
        /// <summary>
        /// Löscht einen Datensatz aus der Datenbank
        /// </summary>
        /// <param name="entry">Zu Löschender Datensatz</param>
        /// <returns>Erfolg des Löschens</returns>
        bool removeDataset(CTableEntry entry);

        /*********************************************************************/
        /// <summary>
        /// Setzt das Zielattribut auf den übergebenen Typen
        /// </summary>
        /// <param name="targetAttributeType">Attributtyp der zum Zielattribut 
        /// werden soll</param>
        /// <returns>Erfolg des Setztens</returns>
        bool setTargetAttribute(CAttributeType targetAttributeType);

        #endregion

        #region Funktionen Identifikationsfenster

        /*********************************************************************/
        /// <summary>
        /// Holt die Liste aller AttributTypen und gibt diese zurück. 
        /// Auch die nicht verwendeten!
        /// </summary>
        /// <returns>Liste mit allen Attributtypen</returns>
        List<CAttributeType> getAttributeTypes();

        #endregion

        #region Graphenfunktionen

        /*********************************************************************/
        /// <summary>
        /// Holt den aktuell aktiven Graphen aus der Datenschicht
        /// </summary>
        /// <returns></returns>
        CTreeGraph getGraph();

        /*********************************************************************/
        /// <summary>
        /// Setzt für einen Vertex das Attribut welches dieser Repräsentiert
        /// </summary>
        /// <param name="vertex">Vertex dessen Attribut geändert werden soll</param>
        /// <param name="attributeType">neues Attribut des Vertex</param>
        bool setVertexAttribute(CTreeVertex vertex, CAttributeType attributeType);

        #endregion
        
        /*********************************************************************/
        /// <summary>
        /// Der Nutzer hat auf die angegebene Ansicht gewechselt
        /// </summary>
        /// <param name="selectedView">View auf die gewechselt wurde</param>
        void changeView(E_VIEW selectedView);

        void test();

    }// class
} // namespace
