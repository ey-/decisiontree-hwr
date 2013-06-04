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

        /*********************************************************************/
        /// <summary>
        /// Fügt einen Knoten zum Graphen hinzu
        /// </summary>
        /// <param name="parent">Parent-Knoten des Knotens</param>
        /// <param name="type">Typ des Attributes welches der Knoten 
        /// repräsentiert</param>
        /// <returns>erstellter Vertex</returns>
        public CTreeVertex addVertex(CTreeVertex parent, CAttributeType type)
        {
            return mActiveTree.addVertex(parent, type);
        }

        /*********************************************************************/
        /// <summary>
        /// Löscht einen Vertex aus dem Graphen. 
        /// !!Dabei werden KEINE Edges gelöscht!!
        /// </summary>
        /// <param name="vertex">zu Entfernender Vertex</param>
        /// <returns>Erfolg der Operation</returns>
        public bool removeVertex(CTreeVertex vertex)
        {
            return mActiveTree.removeVertex(vertex);
        }

        /*********************************************************************/
        /// <summary>
        /// Löscht die Kindelement des übergebenen Vertex und die dazugehörigen 
        /// Verbindungen.
        /// </summary>
        /// <param name="vertex">Vertex dessen Kindknoten entfernt werden sollen</param>
        /// <returns>Erfolg der Operation</returns>
        public bool removeChildVertices(CTreeVertex vertex)
        {
            if (vertex.ChildList.Count > 0)
            {
                foreach (CTreeVertex child in vertex.ChildList)
                {
                    // von dem Kind die Kindelmente löschen
                    removeChildVertices(child);

                    // Die Verbindungzum Parent löschen
                    removeEdge(child.ParentEdge);
                    // und den Kindknoten selbst
                    removeVertex(child);
                }
                vertex.ChildList.Clear();
                return true;
            }
            // es wurden kein Elemente gelöscht
            return false;
        }

        public CTreeEdge addEdge(CTreeVertex parent, CTreeVertex child, CAttributeValue attributeValue)
        {
            return mActiveTree.addEdge(parent, child, attributeValue);
        }

        public bool removeEdge(CTreeEdge edge)
        {
            return mActiveTree.removeEdge(edge);
        }

        public void updateVertexValues(Logic.CTableLogic tableLogic)
        {
            mActiveTree.updateVertexValues(tableLogic);
        }

    }
}
