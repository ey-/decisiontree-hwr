using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Storage.TableData
{
    /// <summary>
    /// Schnittstelle für den Datenzugriff auf die Tabellendaten.
    /// Alle Methoden die benötigt werden um auf die Daten zuzugreifen werden zuerst hier 
    /// eingefügt und dann durch die implementierende Klasse umgesetzt.
    /// </summary>
    public interface IDBDataReader
    {
        /*********************************************************************/
        /// <summary>
        /// Ließt alle Einträge der Tabelle aus und gibt diese zurück
        /// </summary>
        /// <returns>Liste mit allen Tabelleneinträgen</returns>
        CTableEntryList getAllEntries();

        /*********************************************************************/
        /// <summary>
        /// gibt ein Liste mit Datensätzen zurück die von dem übergebenen 
        /// Knoten repräsentiert werden.
        /// </summary>
        /// <param name="vertexToIdentify">Knoten der Identifiziert werden 
        /// soll</param>
        ///<param name="distinct">sollen doppelte Werte angezeigt werden</param>
        /// <returns>Liste mit Datensätzen des Knotens</returns>
        CTableEntryList getFilteredTableData(TreeData.CTreeVertex vertexToIdentify);

        /*********************************************************************/
        /// <summary>
        /// fügt neuen leeren Datensatz in die Datenbak ein und gibt diesen zurück
        /// </summary>
        /// <returns>leerer Datenbankeintrag</returns>
        CTableEntry insertEntry();

        /*********************************************************************/
        /// <summary>
        /// Löscht einen Eintrag aus der Datenbank
        /// </summary>
        /// <param name="entry">Zu Löschender Eintrag</param>
        /// <returns>Erfolg des Löschens</returns>
        bool removeEntry(CTableEntry entry);

        /*********************************************************************/
        /// <summary>
        /// fügt eine Spalte zur Tabelle hinzu.
        /// </summary>
        /// <param name="name">Name/Überschrift der Spalte</param>
        /// <returns>Erfolg beim anlegen</returns>
        CAttributeType addColumn(string name);

        /*********************************************************************/
        /// <summary>
        /// löscht eine Spalte der Tabelle 
        /// </summary>
        /// <param name="name">Name/Überschrift der Spalte</param>
        /// <returns>Erfolg beim anlegen</returns>
        bool removeColumn(string name);

        /*********************************************************************/
        /// <summary>
        /// Aktualisert den Wert eines Attributes in der Datenbank auf den übergebenen Wert
        /// </summary>
        /// <param name="attribute">Attribute mit aktualisiertem Wert</param>
        /// <param name="newValue">Wert auf den das Attribut gesetzt werden soll</param>
        /// <returns>Erfolg der Aktualisierung</returns>
        bool updateAttributeValue(CAttributeValue attribute, string newValue);

        /*********************************************************************/
        /// <summary>
        /// Leert komplett alle Einträge der Datenbank und löscht die Spalten.
        /// </summary>
        void clearDatabase();

        /*********************************************************************/
        /// <summary>
        /// Setzt das Zielattribut auf den übergebenen Typen
        /// </summary>
        /// <param name="targetAttributeType">Attributtyp der zum Zielattribut 
        /// werden soll</param>
        /// <returns>Erfolg des Setztens</returns>
        bool setTargetAttribute(CAttributeType targetAttributeType);
        List<CAttributeType> getAttributeTypes();

        CValueList getDataforChildVertices(TreeData.CTreeVertex vertexToIdentify);
        
    } // class    
} // namespace
