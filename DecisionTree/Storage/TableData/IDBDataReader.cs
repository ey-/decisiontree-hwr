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
        /// fügt neuen leeren Datensatz in die Datenbak ein und gibt diesen zurück
        /// </summary>
        /// <returns>leerer Datenbankeintrag</returns>
        CTableEntry insertEntry();

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
    }
    
}
