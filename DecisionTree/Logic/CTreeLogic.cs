using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage.TreeData;
using DecisionTree.Storage;

namespace DecisionTree.Logic
{
    /// <summary>
    /// Enthält die gesamte Logik um die Daten der Bäume zu steuern
    /// </summary>
    public class CTreeLogic
    {
        ITreeHandler mTreeHandler;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CTreeLogic()
        {
            mTreeHandler = new CTreeHandler();
        }

        /*********************************************************************/
        /// <summary>
        /// Holt den aktuell aktiven Graphen aus der Datenschicht
        /// </summary>
        /// <returns></returns>
        public CTreeGraph getGraph()
        {
            return mTreeHandler.getGraph();
        }

    }
}
