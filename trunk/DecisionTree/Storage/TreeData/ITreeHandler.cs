using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage.TreeData;

namespace DecisionTree.Storage
{
    public enum E_TREE_TYPE
    { 
        E_TREE_INTERACTIVE = 0,
        E_TREE_AUTOMATIC = 1
    }

    /// <summary>
    /// Schnittstelle für den Datenzugriff auf die Baumdaten.
    /// Alle Methoden die benötigt werden um auf die Daten zuzugreifen werden zuerst hier 
    /// eingefügt und dann durch die implementierende Klasse umgesetzt.
    /// </summary>
    public interface ITreeHandler
    {
        /*********************************************************************/
        /// <summary>
        /// Holt den aktuell aktiven Graphen aus der Datenschicht
        /// </summary>
        /// <returns></returns>
        CTreeGraph getGraph();

        /*********************************************************************/
        /// <summary>
        /// Setzt den Baum der verwendet werden soll für die Zugriffe
        /// </summary>
        /// <param name="tree">Baum der verwendet werden soll</param>
        void setActiveTree(E_TREE_TYPE tree);
    }
}
