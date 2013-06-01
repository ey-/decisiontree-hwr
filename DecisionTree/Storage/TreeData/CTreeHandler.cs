using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Storage.TreeData
{
    /// <summary>
    /// Klasse für den Zugriff auf die Bäume. Verwaltet den aktuell aktiven 
    /// Baum und erlaubt Zugriff auf diesen.
    /// </summary>
    public class CTreeHandler : ITreeHandler
    {
        CTree[] mTrees = new CTree[(int)E_TREE_TYPE.E_TREE_COUNT];
        CTree mActiveTree;

        /*********************************************************************/
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CTreeHandler()
        {
            mTrees[(int)E_TREE_TYPE.E_TREE_INTERACTIVE] = new CTree();
            mTrees[(int)E_TREE_TYPE.E_TREE_AUTOMATIC] = new CTree();

            setActiveTree(E_TREE_TYPE.E_TREE_INTERACTIVE);
        }

        /*********************************************************************/
        /// <summary>
        /// Holt den aktuell aktiven Graphen aus der Datenschicht
        /// </summary>
        /// <returns></returns>
        public CTreeGraph getGraph()
        {
            return mActiveTree.Graph;
        }

        /*********************************************************************/
        /// <summary>
        /// Setzt den Baum der verwendet werden soll für die Zugriffe
        /// </summary>
        /// <param name="tree">Baum der verwendet werden soll</param>
        public void setActiveTree(E_TREE_TYPE tree)
        {
            switch (tree)
            { 
                case E_TREE_TYPE.E_TREE_AUTOMATIC:
                    mActiveTree = mTrees[(int)E_TREE_TYPE.E_TREE_AUTOMATIC];
                    break;
                case E_TREE_TYPE.E_TREE_INTERACTIVE:
                    mActiveTree = mTrees[(int)E_TREE_TYPE.E_TREE_INTERACTIVE];
                    break;
            }
        }

        /*********************************************************************/
        /// <summary>
        /// Setzt den Baum auf den Initalzustand mit einem leeren Root-Knoten 
        /// zurück
        /// </summary>
        public void resetActiveTree()
        {
            mActiveTree.resetTree();
        }


        public CTreeVertex addVertex(CTreeVertex parent, CAttributeType type)
        {
            return mActiveTree.addVertex(parent, type);
        }

        public bool removeVertex(CTreeVertex vertex)
        {
            return mActiveTree.removeVertex(vertex);
        }

        public CTreeEdge addEdge(CTreeVertex parent, CTreeVertex child, CAttributeValue attributeValue)
        {
            return mActiveTree.addEdge(parent, child, attributeValue);
        }

    }
}
