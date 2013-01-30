﻿using System;
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

    }
}
