using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.GUI;
using System.Collections.ObjectModel;
using DecisionTree.Storage;
using DecisionTree.Storage.TableData;

namespace DecisionTree.Logic
{
    /// <summary>
    /// Schnittstelle für Zugriff auf die Logikebene. Alle Methoden die benötigt werden,
    /// werden zuerst hier eingefügt und dann in der implementierenden Klasse umgesetzt.
    /// </summary>
    public interface IBusinessLogic
    {
        /*********************************************************************/
        /// <summary>
        /// Registiert das MainWindow Interface damit der Zugriff auf die Fenster erfolgen kann.
        /// </summary>
        /// <param name="mainWindow">Interface zum MainWindow</param>
        void registerWindow(IMainWindow mainWindow);

        /*********************************************************************/
        /// <summary>
        /// gibt die Liste mit allen Datensätzen zurück
        /// </summary>
        /// <returns>Liste mit allen Datensätzen</returns>
        CTableEntryList getAllTableData();

        /*********************************************************************/
        /// <summary>
        /// Testweise Methode zum hinzufügen eines weiteren Attributes zur Tabelle
        /// </summary>
        CAttributeType addAttribute();

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
        void openCSVFile(string filePath);

        /*********************************************************************/
        /// <summary>
        /// Speichert eine CSV-Datei und fügt den Inhalt der Datenbank ein
        /// </summary>
        /// <param name="filePath">Pfad zum Speicherort</param>
        void saveCSVFile(string filePath);
    }
}
