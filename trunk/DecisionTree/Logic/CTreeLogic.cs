using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Storage.TreeData;
using DecisionTree.Storage;

namespace DecisionTree.Logic
{
    /*************************************************************************/
    /// <summary>
    /// Enthält die gesamte Logik um die Daten der Bäume zu steuern
    /// </summary>
    public class CTreeLogic
    {
        ITreeHandler mTreeHandler;

        bool mbInteractiveTreeNeedReset = false;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CTreeLogic()
        {
            mTreeHandler = new CTreeHandler();

            setupTestData();
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

        /*********************************************************************/
        /// <summary>
        /// Wird aufgerufen wenn sich am Aufbau der Tabelle etwas geändert hat,
        /// damit z.B. die Interaktive Ansicht neu zurückgesetzt werden kann.
        /// </summary>
        public void setTableSetupChanged()
        {
            mbInteractiveTreeNeedReset = true;
        }

        /*********************************************************************/
        /// <summary>
        /// Der Nutzer hat auf die angegebene Ansicht gewechselt
        /// </summary>
        /// <param name="selectedView">View auf die gewechselt wurde</param>
        internal void setTreeForView(E_VIEW selectedView)
        {
            // Je nach View den Entsprechenden Baum aktive setzen
            if (selectedView == E_VIEW.E_TREE_INTERACTIVE_VIEW)
            {
                mTreeHandler.setActiveTree(E_TREE_TYPE.E_TREE_INTERACTIVE);

                // wenn in der Tabelle Attribute hinzugefügt oder gelöscht 
                // wurden, muss der Baum interaktiven Ansicht reseted werden
                if (mbInteractiveTreeNeedReset == true)
                {
                    mTreeHandler.resetActiveTree();
                    mbInteractiveTreeNeedReset = false;
                }
            } else
            {
                mTreeHandler.setActiveTree(E_TREE_TYPE.E_TREE_AUTOMATIC);
            }
        }

        protected CTreeVertex addVertex(CTreeVertex parent, CAttributeType type = null)
        {
            return mTreeHandler.addVertex(parent, type);
        }

        protected bool removeVertex(CTreeVertex vertex)
        {
            return mTreeHandler.removeVertex(vertex);
        }

        /*********************************************************************/
        /// <summary>
        /// Testdaten für Funktionstest der Anzeige der Baumansicht
        /// TODO Nach erfolgreicher Implementierung auskommentieren .. 
        /// damit im Fehlerfall wieder genutzt werden kann.
        /// </summary>
        private void setupTestData()
        {
            CTreeVertex root = addVertex(null);
            root.setDemoData("Geschlecht", 11, 5, 6, 0.2134);

            CTreeVertex v1_1 = addVertex(root);
            v1_1.setDemoData("Sendung Enthält", 6, 4, 2, 0.3234);
            CTreeVertex v1_2 = addVertex(root);
            v1_2.setDemoData("", 6, 4, 2, 0.3234);

            CAttributeType testType = new CAttributeType(0);
            CTreeEdge edgeR_1_1 = new CTreeEdge(root, v1_1, new CAttributeValue(testType, "0", "f", null));
            CTreeEdge edgeR_1_2 = new CTreeEdge(root, v1_2, new CAttributeValue(testType, "0", "m", null));

            CTreeVertex v2_1 = addVertex(v1_1);
            v2_1.setDemoData("", 3, 2, 1, 0.3234);
            CTreeVertex v2_2 = addVertex(v1_1);
            v2_2.setDemoData("", 2, 2, 0, 0.3234);
            CTreeVertex v2_3 = addVertex(v1_1);
            v2_3.setDemoData("", 1, 0, 1, 0.3234);
            /*
            CTreeEdge edge1_1_2_1 = new CTreeEdge(v1_1, v2_1, new CAttributeValue(testType, "0", "Filme", null));
            CTreeEdge edge1_1_2_2 = new CTreeEdge(v1_1, v2_2, new CAttributeValue(testType, "0", "Bücher", null));
            CTreeEdge edge1_1_2_3 = new CTreeEdge(v1_1, v2_3, new CAttributeValue(testType, "0", "Musik", null));
            
            mGraph.AddVertex(root);
            mGraph.AddVertex(v1_1);
            mGraph.AddVertex(v1_2);
            mGraph.AddVertex(v2_1);
            mGraph.AddVertex(v2_2);
            mGraph.AddVertex(v2_3);

            mGraph.AddEdge(edgeR_1_1);
            mGraph.AddEdge(edgeR_1_2);
            mGraph.AddEdge(edge1_1_2_1);
            mGraph.AddEdge(edge1_1_2_2);
            mGraph.AddEdge(edge1_1_2_3);
            */
        }
    }
}
